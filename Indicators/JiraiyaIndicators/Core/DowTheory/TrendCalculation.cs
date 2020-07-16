using NinjaTrader.Custom.Indicators.JiraiyaIndicators.PriceActionSwing;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators.DowPivot
{
    public class TrendCalculation : Calculation
    {
        public TrendCalculation(NinjaScriptBase owner) : base(owner) { }

        protected override CalculationData OnCalculationRequest(PriceActionSwingClass priceActionSwingClass)
        {
            /*
            MatrixPoints matrixPoints = new MatrixPoints(MatrixPoints.WhichGraphicPatternType.Trend);

            bool isNewMatrixPoints = true;

            if(priceActionSwingClass.GetPoint(3) == null)
            {
                return new CalculationData(false);
            }

            matrixPoints.AddPoint(priceActionSwingClass.GetPoint(0));
            matrixPoints.AddPoint(priceActionSwingClass.GetPoint(1));
            matrixPoints.AddPoint(priceActionSwingClass.GetPoint(2));
            matrixPoints.AddPoint(priceActionSwingClass.GetPoint(3));

            if (matrixPoints.PointsList[3].CurrentSideSwing == Point.SidePoint.Low)
            {
                isNewMatrixPoints = matrixPoints.PointsList[3].Price < matrixPoints.PointsList[1].Price &&
                                    matrixPoints.PointsList[2].Price < matrixPoints.PointsList[0].Price;
            }
            else if(matrixPoints.PointsList[3].CurrentSideSwing == Point.SidePoint.High)
            {
                isNewMatrixPoints = matrixPoints.PointsList[3].Price > matrixPoints.PointsList[1].Price &&
                                    matrixPoints.PointsList[2].Price > matrixPoints.PointsList[0].Price;
            }

            if (isNewMatrixPoints)
            {
                if (matrixPoints.PointsList[3].CurrentSideSwing == Point.SidePoint.Low)
                {
                    matrixPoints.TrendSideSignal = MatrixPoints.WhichTrendSideSignal.Bullish;
                }
                else if (matrixPoints.PointsList[3].CurrentSideSwing == Point.SidePoint.High)
                {
                    matrixPoints.TrendSideSignal = MatrixPoints.WhichTrendSideSignal.Bearish;
                }
            }

            return isNewMatrixPoints == true ? new CalculationData(true, matrixPoints) : new CalculationData(false);
            */
            return new CalculationData();
        }
    }
}
