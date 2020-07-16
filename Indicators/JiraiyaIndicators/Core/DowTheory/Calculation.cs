using NinjaTrader.Custom.Indicators.JiraiyaIndicators.PriceActionSwing;
using NinjaTrader.NinjaScript;
using System.Collections.Generic;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators.DowPivot
{
    public abstract class Calculation
    {
        // Fields

        protected readonly NinjaScriptBase owner;

        protected List<MatrixPoints> matrixPointsList = new List<MatrixPoints>();
        protected CalculationData currentCalculationData = new CalculationData();

        // Initialization

        protected Calculation(NinjaScriptBase owner)
        {
            this.owner = owner;
        }

        public void Calculate(PriceActionSwingClass priceActionSwingClass)
        {
            currentCalculationData = OnCalculationRequest(priceActionSwingClass);

            if(currentCalculationData.isNewMatrixPoints)
            {
                AddOrUpdateIfNewMatrixPoints(currentCalculationData);
            }
        }

        // Public (methods)

        public MatrixPoints GetMatrixPoints(int matrixPointsAgo)
        {
            return matrixPointsList.Count < matrixPointsAgo + 1 ? null : matrixPointsList[(matrixPointsList.Count - 1) - matrixPointsAgo];
        }

        // Protected (methods)

        protected abstract CalculationData OnCalculationRequest(PriceActionSwingClass priceActionSwingClass);

        protected bool IsNewMatrixTheSameTheLastOne(List<Point> newPointsList)
        {
            if (matrixPointsList.Count <= 0)
            {
                return false;
            }

            MatrixPoints lastMatrix = matrixPointsList[matrixPointsList.Count - 1];

            // Test all points except the last one
            for (int i = 0; i <= newPointsList.Count - 2; i++)
            {
                if (lastMatrix.PointsList[i].Index != newPointsList[i].Index)
                {
                    return false;
                }
            }

            return true;
        }

        // Private (methods)

        private void AddOrUpdateIfNewMatrixPoints(CalculationData calculationData)
        {
            // Here, no code is needed to update the matrix due to the Point's object
            // reference already passed in the list of points, when the last point on
            // the list is updated the Matrix will already have itself updated. 
            // So you just need to redraw the pattern on the chart
            if (!IsNewMatrixTheSameTheLastOne(calculationData.pointsList))
            {
                switch (calculationData.whichTrend)
                {
                    // Add long pattern if the first point was an low
                    case MatrixPoints.WhichTrendSideSignal.Bullish:
                        matrixPointsList.Add(new MatrixPoints(calculationData.pointsList,
                                                              matrixPointsList.Count,
                                                              MatrixPoints.WhichTrendSideSignal.Bullish,
                                                              calculationData.whichGraphic));
                        break;

                    // Add short pattern if the first point was an high
                    case MatrixPoints.WhichTrendSideSignal.Bearish:
                        matrixPointsList.Add(new MatrixPoints(calculationData.pointsList,
                                                              matrixPointsList.Count,
                                                              MatrixPoints.WhichTrendSideSignal.Bearish,
                                                              calculationData.whichGraphic));
                        break;
                }
            }
        }

        // Properties

        public CalculationData CalcData
        {
            get
            {
                return currentCalculationData;
            }
        }

        public MatrixPoints LastMatrixPoints
        {
            get
            {
                return GetMatrixPoints(0);
            }
        }

        // Miscellaneous

        public class CalculationData
        {
            public bool isNewMatrixPoints;
            public List<Point> pointsList;
            public MatrixPoints.WhichTrendSideSignal whichTrend;
            public MatrixPoints.WhichGraphicPatternType whichGraphic;

            public CalculationData()
            {
                isNewMatrixPoints = false;
            }

            public CalculationData(List<Point> pointsList, MatrixPoints.WhichTrendSideSignal whichTrend, MatrixPoints.WhichGraphicPatternType whichGraphic)
            {
                isNewMatrixPoints = true;
                this.pointsList = new List<Point>(pointsList);
                this.whichTrend = whichTrend;
                this.whichGraphic = whichGraphic;
            }
        }
    }
}
