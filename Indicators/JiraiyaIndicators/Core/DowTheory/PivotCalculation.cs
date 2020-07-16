using NinjaTrader.Custom.Indicators.JiraiyaIndicators.PriceActionSwing;
using NinjaTrader.NinjaScript;
using System.Collections.Generic;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators.DowPivot
{
    public class PivotCalculation : Calculation
    {
        private List<Point> pointsList = new List<Point>();
        private bool isNewPivot = true;
        private MatrixPoints.WhichTrendSideSignal whichTrend = MatrixPoints.WhichTrendSideSignal.None;
        private double maxPercentOfPivotRetraction = 0;
        private double minPercentOfPivotRetraction = 0;

        public PivotCalculation(NinjaScriptBase owner, 
                                double maxPercentOfPivotRetraction,
                                double minPercentOfPivotRetraction) : base(owner)
        {
            this.maxPercentOfPivotRetraction = maxPercentOfPivotRetraction;
            this.minPercentOfPivotRetraction = minPercentOfPivotRetraction;
        }

        protected override CalculationData OnCalculationRequest(PriceActionSwingClass priceActionSwingClass)
        {
            if (priceActionSwingClass.GetPoint(3) == null)
            {
                return new CalculationData();
            }

            pointsList.Clear();
            isNewPivot = false;

            //----Bearish----|---Bullish---
            //----3----------|----------0--
            //-----\---1-----|-----2---/---
            //------\-/-\----|----/-\-/----
            //-------2---\---|---/---1-----
            //------------0--|--3----------

            pointsList.Add(priceActionSwingClass.GetPoint(0)); // Last point or   pointsList[0]
            pointsList.Add(priceActionSwingClass.GetPoint(1)); // 1 point ago or  pointsList[1]
            pointsList.Add(priceActionSwingClass.GetPoint(2)); // 2 points ago or pointsList[2]
            pointsList.Add(priceActionSwingClass.GetPoint(3)); // 3 points ago or pointsList[3]

            // Test a long pivot
            if ((pointsList[3].CurrentSideSwing == Point.SidePoint.Low && whichTrend != MatrixPoints.WhichTrendSideSignal.Bullish) ||
                (pointsList[3].CurrentSideSwing == Point.SidePoint.Low && IsNewMatrixTheSameTheLastOne(pointsList)))
            {
                bool isLongPivot = pointsList[3].Price < pointsList[1].Price &&
                                   pointsList[2].Price < pointsList[0].Price;
                
                bool isBetweenLongPercentRetracement = IsLowerThanMaxPercentPivotRetracement(pointsList, 
                                                                                             maxPercentOfPivotRetraction, 
                                                                                             MatrixPoints.WhichTrendSideSignal.Bullish) &&
                                                       IsHigherThanMinPercentPivotRetracement(pointsList, 
                                                                                              minPercentOfPivotRetraction,
                                                                                              MatrixPoints.WhichTrendSideSignal.Bullish);

                isNewPivot = isLongPivot && isBetweenLongPercentRetracement;

                if (isNewPivot)
                {
                    whichTrend = MatrixPoints.WhichTrendSideSignal.Bullish;
                }
            }
            // Test a short pivot
            else if ((pointsList[3].CurrentSideSwing == Point.SidePoint.High && whichTrend != MatrixPoints.WhichTrendSideSignal.Bearish) ||
                     (pointsList[3].CurrentSideSwing == Point.SidePoint.High && IsNewMatrixTheSameTheLastOne(pointsList)))
            {
                bool isShortPivot = pointsList[3].Price > pointsList[1].Price &&
                                    pointsList[2].Price > pointsList[0].Price;

                bool isBetweenShortPercentRetracement = IsLowerThanMaxPercentPivotRetracement(pointsList, 
                                                                                              maxPercentOfPivotRetraction, 
                                                                                              MatrixPoints.WhichTrendSideSignal.Bearish) &&
                                                        IsHigherThanMinPercentPivotRetracement(pointsList,
                                                                                               minPercentOfPivotRetraction,
                                                                                               MatrixPoints.WhichTrendSideSignal.Bearish);

                isNewPivot = isShortPivot && isBetweenShortPercentRetracement;

                if (isNewPivot)
                {
                    whichTrend = MatrixPoints.WhichTrendSideSignal.Bearish;
                }
            }

            return isNewPivot == true ? new CalculationData(pointsList, whichTrend, MatrixPoints.WhichGraphicPatternType.Pivot) : 
                new CalculationData();
        }

        private bool IsLowerThanMaxPercentPivotRetracement(List<Point> pointsList, double maxPercent, MatrixPoints.WhichTrendSideSignal whichTrendSideSignal)
        {
            switch(whichTrendSideSignal)
            {
                case MatrixPoints.WhichTrendSideSignal.Bullish:
                    
                    double bullishRangeToMeasure = (pointsList[3].Price - pointsList[2].Price) * -1;
                    double upLimit = (maxPercent / 100) * bullishRangeToMeasure;
                    upLimit -= (pointsList[3].Price * -1);

                    if (pointsList[1].Price < upLimit)
                    {
                        /*DrawWrapper.DrawLineForTest(owner, "Up test " + owner.CurrentBar, System.Windows.Media.Brushes.Gray,
                                                pointsList[3].BarIndex, upLimit, pointsList[0].BarIndex, upLimit);*/
                        return true;
                    }
                    break;

                case MatrixPoints.WhichTrendSideSignal.Bearish:

                    double bearishRangeToMeasure = pointsList[3].Price - pointsList[2].Price;
                    double downLimit = (maxPercent / 100) * bearishRangeToMeasure;
                    downLimit += pointsList[2].Price;

                    if(pointsList[1].Price < downLimit)
                    {
                        /*DrawWrapper.DrawLineForTest(owner, "Down test " + owner.CurrentBar, System.Windows.Media.Brushes.Gray,
                                                pointsList[3].BarIndex, downLimit, pointsList[0].BarIndex, downLimit);*/
                        return true;
                    }
                    break;
            }
            return false;
        }

        private bool IsHigherThanMinPercentPivotRetracement(List<Point> pointsList, double minPercent, MatrixPoints.WhichTrendSideSignal whichTrendSideSignal)
        {
            switch(whichTrendSideSignal)
            {
                case MatrixPoints.WhichTrendSideSignal.Bullish:
                    
                    double bullishRangeToMeasure = (pointsList[3].Price - pointsList[2].Price) * -1;
                    double upLimit = (minPercent / 100) * bullishRangeToMeasure;
                    upLimit -= (pointsList[3].Price * -1);

                    if (pointsList[1].Price > upLimit)
                    {
                        /*DrawWrapper.DrawLineForTest(owner, "Up min test " + owner.CurrentBar, System.Windows.Media.Brushes.LightGray,
                                                        pointsList[3].BarIndex, upLimit, pointsList[0].BarIndex, upLimit);*/
                        return true;
                    }
                    break;

                case MatrixPoints.WhichTrendSideSignal.Bearish:

                    double bearishRangeToMeasure = pointsList[3].Price - pointsList[2].Price;
                    double downLimit = (minPercent / 100) * bearishRangeToMeasure;
                    downLimit += pointsList[2].Price;

                    if (pointsList[1].Price > downLimit)
                    {
                        /*DrawWrapper.DrawLineForTest(owner, "Down min test " + owner.CurrentBar, System.Windows.Media.Brushes.LightGray,
                                                pointsList[3].BarIndex, downLimit, pointsList[0].BarIndex, downLimit);*/
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}
