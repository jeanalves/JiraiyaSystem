using NinjaTrader.NinjaScript;
using System.Collections.Generic;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators.PriceActionSwing
{
    public class PriceActionSwingClass
    {
        // Fields

        private readonly NinjaScriptBase owner;
        private readonly DrawingProperties drawingProperties;
        private readonly TickCalculation tickCalculation;
        private readonly SwingForwardCalculation swingForwardCalculation;
        private readonly SwingForwardCalculationOld swingForwardCalculationOld;

        public CalculationTypeListPriceActionSwing CalculationType { get; private set; }
        public double Strength { get; private set; }
        public bool UseHighLow { get; private set; }
        public bool ShowPoints { get; private set; }
        public bool ShowLines { get; private set; }

        // Initialization

        public PriceActionSwingClass(NinjaScriptBase owner, DrawingProperties drawingProperties, CalculationTypeListPriceActionSwing calculationType,
                                     double strength, bool useHighLow, bool showPoints, bool showLines)
        {
            this.owner = owner;
            this.drawingProperties = drawingProperties;
            tickCalculation = new TickCalculation(owner, this);
            swingForwardCalculation = new SwingForwardCalculation(owner, this);
            swingForwardCalculationOld = new SwingForwardCalculationOld(owner, this);

            CalculationType = calculationType;
            Strength = strength;
            UseHighLow = useHighLow;
            ShowPoints = showPoints;
            ShowLines = showLines;
        }

        public void Compute()
        {
            GetChosenCalculationObject().Calculate();

            // Every time a new point event happens, it will be drawn in this 3 lines code
            if (GetChosenCalculationObject().CalcData.isNewSwing)
            {
                OnCalculationUpdate(GetChosenCalculationObject());
            }
        }

        // Public (methods)

        public Point GetPoint(int pointsAgo)
        {
            GetChosenCalculationObject().Calculate();
            return GetChosenCalculationObject().GetPoint(pointsAgo);
        }

        // Private (methods)

        private void OnCalculationUpdate(Calculation ChosenCalculationObject)
        {
            if (ShowPoints)
            {
                Drawing.DrawPoint(owner, ChosenCalculationObject.GetPoint(0), drawingProperties);
            }

            // Test if there is more than two points to be able in draw a line
            if (ShowLines)
            {
                if (ChosenCalculationObject.GetPointsList().Count > 1)
                {
                    Drawing.DrawZigZag(owner, drawingProperties,
                                       ChosenCalculationObject.GetPoint(1),
                                       ChosenCalculationObject.GetPoint(0));
                }
            }
        }

        private Calculation GetChosenCalculationObject()
        {
            switch (CalculationType)
            {
                case CalculationTypeListPriceActionSwing.Tick:
                    return tickCalculation;

                case CalculationTypeListPriceActionSwing.SwingForward:
                    return swingForwardCalculation;

                case CalculationTypeListPriceActionSwing.SwingForwardOld:
                    return swingForwardCalculationOld;
            }

            return null;
        }

        // Properties

        public List<Point> GetPointsList
        {
            get
            {
                GetChosenCalculationObject().Calculate();
                return GetChosenCalculationObject().GetPointsList();
            }
            private set { }
        }

        public Point GetLastPoint
        {
            get
            {
                GetChosenCalculationObject().Calculate();
                return GetChosenCalculationObject().GetPoint(0);
            }
            private set { }
        }
    }
}
