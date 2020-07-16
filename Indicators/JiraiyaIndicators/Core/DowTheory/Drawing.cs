using NinjaTrader.NinjaScript;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators.DowPivot
{
    public static class Drawing
    {
        public static void DrawPivot(NinjaScriptBase owner, DrawingProperties drawingProperties, MatrixPoints matrixPoints)
        {
            switch(matrixPoints.TrendSideSignal)
            {
                case MatrixPoints.WhichTrendSideSignal.Bullish:
                    DrawWrapper.DrawPathLine(owner, drawingProperties,
                                             matrixPoints.PointsList[3].Index,
                                             matrixPoints.PointsList[3].BarIndex,
                                             matrixPoints.PointsList[3].Price,
                                             matrixPoints.PointsList[2].BarIndex,
                                             matrixPoints.PointsList[2].Price,
                                             System.Windows.Media.Brushes.Green);

                    DrawWrapper.DrawPathLine(owner, drawingProperties,
                                             matrixPoints.PointsList[2].Index,
                                             matrixPoints.PointsList[2].BarIndex,
                                             matrixPoints.PointsList[2].Price,
                                             matrixPoints.PointsList[1].BarIndex,
                                             matrixPoints.PointsList[1].Price,
                                             System.Windows.Media.Brushes.Green);

                    DrawWrapper.DrawPathLine(owner, drawingProperties,
                                             matrixPoints.PointsList[1].Index,
                                             matrixPoints.PointsList[1].BarIndex,
                                             matrixPoints.PointsList[1].Price,
                                             matrixPoints.PointsList[0].BarIndex,
                                             matrixPoints.PointsList[0].Price,
                                             System.Windows.Media.Brushes.Green);
                    break;

                case MatrixPoints.WhichTrendSideSignal.Bearish:
                    DrawWrapper.DrawPathLine(owner, drawingProperties,
                                             matrixPoints.PointsList[3].Index,
                                             matrixPoints.PointsList[3].BarIndex,
                                             matrixPoints.PointsList[3].Price,
                                             matrixPoints.PointsList[2].BarIndex,
                                             matrixPoints.PointsList[2].Price,
                                             System.Windows.Media.Brushes.Red);

                    DrawWrapper.DrawPathLine(owner, drawingProperties,
                                             matrixPoints.PointsList[2].Index,
                                             matrixPoints.PointsList[2].BarIndex,
                                             matrixPoints.PointsList[2].Price,
                                             matrixPoints.PointsList[1].BarIndex,
                                             matrixPoints.PointsList[1].Price,
                                             System.Windows.Media.Brushes.Red);

                    DrawWrapper.DrawPathLine(owner, drawingProperties,
                                             matrixPoints.PointsList[1].Index,
                                             matrixPoints.PointsList[1].BarIndex,
                                             matrixPoints.PointsList[1].Price,
                                             matrixPoints.PointsList[0].BarIndex,
                                             matrixPoints.PointsList[0].Price,
                                             System.Windows.Media.Brushes.Red);
                    break;
            }
        }
    }
}
