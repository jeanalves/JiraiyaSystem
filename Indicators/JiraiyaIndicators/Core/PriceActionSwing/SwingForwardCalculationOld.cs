using NinjaTrader.NinjaScript;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators.PriceActionSwing
{
    public class SwingForwardCalculationOld : Calculation
    {
        public SwingForwardCalculationOld(NinjaScriptBase owner, PriceActionSwingClass priceActionSwingClass) : base(owner, priceActionSwingClass) { }

        protected override CalculationData OnCalculationOfFirstSwingPointRequest()
        {
            //logPrinter.Print(owner, "SwingForwardCalculationOld.OnCalculationOfFirstSwingPointRequest()");

            double highCandidateValue = highs.GetValueAt(0);
            double lowCandidateValue = lows.GetValueAt(0);
            int highCandidateIndex = 0;
            int lowCandidateIndex = 0;

            if (owner.CurrentBar == priceActionSwingClass.Strength)
            {
                //logPrinter.Print(owner, "Testing the high values to find the highest one");
                // Test the high values to find the highest one
                for (int i = 0; i < priceActionSwingClass.Strength; i++)
                {
                    if (highs.GetValueAt(i) > highCandidateValue)
                    {
                        highCandidateValue = highs.GetValueAt(i);
                        highCandidateIndex = i;
                        //logPrinter.Print(owner, "High index : " + i);
                    }
                }

                //logPrinter.Print(owner, "Testing the low values to find the lowest one");
                // Test the low values to find the lowest one
                for (int i = 0; i < priceActionSwingClass.Strength; i++)
                {
                    if (lows.GetValueAt(i) < lowCandidateValue)
                    {
                        lowCandidateValue = lows.GetValueAt(i);
                        lowCandidateIndex = i;
                        //logPrinter.Print(owner, "Low index : " + i);
                    }
                }

                if (highCandidateIndex < lowCandidateIndex)
                {
                    /*
                    logPrinter.Print(owner, "Add high," +
                        " highCandidateValue: " + highCandidateValue +
                        ", highCandidateIndex: " + highCandidateIndex);
                    */
                    return new CalculationData(highCandidateValue, highCandidateIndex, Point.SidePoint.High);
                }
                else if (highCandidateIndex > lowCandidateIndex)
                {
                    /*
                    logPrinter.Print(owner, "Add low," +
                        " lowCandidateValue: " + lowCandidateValue +
                        ", lowCandidateIndex: " + lowCandidateIndex);
                    */
                    return new CalculationData(lowCandidateValue, lowCandidateIndex, Point.SidePoint.Low);
                }
                else if(highCandidateIndex == lowCandidateIndex)
                {
                    //logPrinter.Print(owner, "Error: The two indexes are equal.");
                    /*
                    logPrinter.PrintError(owner, "Error: The two indexes are equal. " +
                        "High bar index: " + highCandidateIndex + " Low bar index: " + lowCandidateIndex);
                    */
                    return new CalculationData(0, 0, Point.SidePoint.Unknow);
                }
                else
                {
                    //logPrinter.Print(owner, "Error: No point was found");
                    //logPrinter.PrintError(owner, "Error: No point was found");
                }
            }

            return new CalculationData();
        }

        protected override CalculationData OnCalculationOfEachBarSwingPointRequest()
        {
            //logPrinter.Print(owner, "SwingForwardCalculationOld.OnCalculationOfEachBarSwingPointRequest()");

            bool isRising= true;
            bool isFalling = true;

            bool isOverHighStrength = false;
            bool isOverLowStrength = false;
            
            if (LastLow() != null)
            {
                isOverHighStrength = (owner.CurrentBar - LastLow().BarIndex) >= priceActionSwingClass.Strength;
            }

            if (LastHigh() != null)
            {
                isOverLowStrength = (owner.CurrentBar - LastHigh().BarIndex) >= priceActionSwingClass.Strength;
            }

            double swingHighCandidateValue = highs[0];
            double swingLowCandidateValue = lows[0];

            int initForIndex = owner.CurrentBar - (int)priceActionSwingClass.Strength;

            //logPrinter.Print(owner, "isOverHighStrength : " + isOverHighStrength + ", isOverLowStrength : " + isOverLowStrength);

            // High calculation
            for (int i = initForIndex; i < owner.CurrentBar; i++)
                if (swingHighCandidateValue < highs.GetValueAt(i))
                    isRising = false;

            // Low calculation
            for (int i = initForIndex; i < owner.CurrentBar; i++)
                if (swingLowCandidateValue > lows.GetValueAt(i))
                    isFalling = false;

            //logPrinter.Print(owner, "isRising : " + isRising + ", isFalling : " + isFalling);

            if (isRising && isOverHighStrength)
                return new CalculationData(swingHighCandidateValue, owner.CurrentBar, Point.SidePoint.High);
            if (isFalling && isOverLowStrength)
                return new CalculationData(swingLowCandidateValue, owner.CurrentBar, Point.SidePoint.Low);

            return new CalculationData();
        }

        protected override CalculationData OnCalculationOFEachTickSwingPointRequest()
        {
            //logPrinter.Print(owner, "SwingForwardCalculationOld.CalculateEachTickSwing()");
            return base.OnCalculationOFEachTickSwingPointRequest();
        }
    }
}
