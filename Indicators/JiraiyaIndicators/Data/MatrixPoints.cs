using System.Collections.Generic;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators
{
    public class MatrixPoints
    {
        private readonly List<Point> pointsList = new List<Point>();
        public int Index { get; private set; }
        public bool IsThisMatrixSignalInformed { get; set; }
        public WhichTrendSideSignal TrendSideSignal { get; set; }
        public WhichGraphicPatternType GraphicPatternType { get; set; }

        // Initialization

        public MatrixPoints(WhichGraphicPatternType graphicPatternType)
        {
            GraphicPatternType = graphicPatternType;
        }

        public MatrixPoints(List<Point> pointsList, int index, WhichTrendSideSignal trendSideSignal, WhichGraphicPatternType graphicPatternType)
        {
            this.pointsList = pointsList;
            IsThisMatrixSignalInformed = false;
            Index = index;
            TrendSideSignal = trendSideSignal;
            GraphicPatternType = graphicPatternType;
        }

        // Public (methods)

        public void AddPoint(Point point)
        {
            pointsList.Add(point);
        }

        // Properties

        public List<Point> PointsList
        {
            get
            {
                return pointsList;
            }
        }

        // Miscellaneous

        public enum WhichTrendSideSignal
        {
            Bullish,
            Bearish,
            Both,
            None
        }

        public enum WhichGraphicPatternType
        {
            Trend,
            Pivot
        }
    }
}
