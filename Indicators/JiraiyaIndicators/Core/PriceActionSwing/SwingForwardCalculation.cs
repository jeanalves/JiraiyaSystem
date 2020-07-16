using NinjaTrader.NinjaScript;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators.PriceActionSwing
{
    public class SwingForwardCalculation : Calculation
    {
        public SwingForwardCalculation(NinjaScriptBase owner, PriceActionSwingClass priceActionSwingClass) : base(owner, priceActionSwingClass) { }

        protected override CalculationData OnCalculationOfEachBarSwingPointRequest()
        {
            //logPrinter.Print(owner, "SwingForwardCalculation.OnCalculationOfEachBarSwingPointRequest()");

            return DefaultLogicCalculation();
        }

        protected override CalculationData OnCalculationOFEachTickSwingPointRequest()
        {
            //logPrinter.Print(owner, "SwingForwardCalculation.CalculateEachTickSwing()");

            return DefaultLogicCalculation();
        }

        private CalculationData DefaultLogicCalculation()
        {
            bool newHigh = true;
            bool newLow = true;

            // For a new swing high in an uptrend, Highs[BarsInProgress][0] must be 
            // greater than the current swing high
            if (LastSideTrend() == Point.SidePoint.High)
            {
                if (LastHigh().Price > highs[0])
                    newHigh = false;
            }

            // For a new swing low in a downtrend, Lows[BarsInProgress][0] must be 
            // smaller than the current swing low
            if (LastSideTrend() == Point.SidePoint.Low)
            {
                if (LastLow().Price < lows[0])
                    newLow = false;
            }

            // Calculates if the current high value is a new swing
            if (newHigh)
            {
                for (int i = 1; i <= priceActionSwingClass.Strength; i++)
                {
                    if (highs[0] <= highs[i])
                    {
                        newHigh = false;
                        break;
                    }
                }
            }

            // Calculates if the current low value is a new swing
            if (newLow)
            {
                for (int i = 1; i <= priceActionSwingClass.Strength; i++)
                {
                    if (lows[0] >= lows[i])
                    {
                        newLow = false;
                        break;
                    }
                }
            }

            if (newHigh && newLow)
            {
                if (LastSideTrend() == Point.SidePoint.High)
                {
                    return new CalculationData(highs[0], owner.CurrentBar, Point.SidePoint.High);
                }
                else
                {
                    return new CalculationData(lows[0], owner.CurrentBar, Point.SidePoint.Low);
                }
            }
            else if (newHigh)
            {
                return new CalculationData(highs[0], owner.CurrentBar, Point.SidePoint.High);
            }
            else if (newLow)
            {
                return new CalculationData(lows[0], owner.CurrentBar, Point.SidePoint.Low);
            }
            return new CalculationData();
        }
    }
}
