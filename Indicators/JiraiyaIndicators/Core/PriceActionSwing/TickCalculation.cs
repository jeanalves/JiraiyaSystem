using NinjaTrader.NinjaScript;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators.PriceActionSwing
{
    public class TickCalculation : Calculation
    {
        public TickCalculation(NinjaScriptBase owner, PriceActionSwingClass priceActionSwingClass) : base(owner, priceActionSwingClass) { }

        protected override CalculationData OnCalculationOfEachBarSwingPointRequest()
        {
            bool isRising = highs[0] > highs[1];
            bool isFalling = lows[0] < lows[1];

            bool isOverHighStrength = false;
            bool isOverLowStrength = false;

            if (LastLow() != null)
            {
                isOverHighStrength = highs[0] > (LastLow().Price + (priceActionSwingClass.Strength * owner.TickSize));
            }

            if (LastHigh() != null)
            {
                isOverLowStrength = lows[0] < (LastHigh().Price - (priceActionSwingClass.Strength * owner.TickSize));
            }

            if (isRising && isOverHighStrength)
                return new CalculationData(highs[0], owner.CurrentBar, Point.SidePoint.High);
            if (isFalling && isOverLowStrength)
                return new CalculationData(lows[0], owner.CurrentBar, Point.SidePoint.Low);
            
            return new CalculationData();
        }
    }
}
