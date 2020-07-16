using NinjaTrader.Custom.Indicators.JiraiyaIndicators.PriceActionSwing;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators.DowPivot
{
    public class DowTheoryClass
    {
        // Fields

        private readonly NinjaScriptBase owner;
        private readonly DrawingProperties drawingProperties;
        private readonly PriceActionSwingClass priceActionSwingClass;
        private readonly PivotCalculation pivotCalculation;
        private readonly TrendCalculation trendCalculation;

        public CalculationTypeListDowTheory CalculationType { get; private set; }
        public OrderSideSignal LongShortSignal { get; set; }

        // Initialization

        public DowTheoryClass(NinjaScriptBase owner, DrawingProperties drawingProperties, CalculationTypeListDowTheory calculationTypeListDT,
                              CalculationTypeListPriceActionSwing calculationTypeListPCW, double strength, bool useHighLow, bool showPoints, bool showLines,
                              double maxPercentOfPivotRetraction, double minPercentOfPivotRetraction)
        {
            this.owner = owner;
            this.drawingProperties = drawingProperties;
            CalculationType = calculationTypeListDT;
            LongShortSignal = OrderSideSignal.Flat;

            priceActionSwingClass = new PriceActionSwingClass(owner, drawingProperties, calculationTypeListPCW, strength, 
                                                              useHighLow, showPoints, showLines);
            trendCalculation = new TrendCalculation(owner);
            pivotCalculation = new PivotCalculation(owner, maxPercentOfPivotRetraction, minPercentOfPivotRetraction);

            /*
            if (!ShowLog)
            {
                logPrinter.SetIndicatorAsInvisible(owner);
            }
            */
        }

        // Public (methods)

        public void Compute()
        {
            priceActionSwingClass.Compute();
            GetChosenCalculationObject().Calculate(priceActionSwingClass);


            if (GetChosenCalculationObject().CalcData.isNewMatrixPoints)
            {
                OnCalculationUpdate(GetChosenCalculationObject());
            }
        }

        // Private (methods)

        private void OnCalculationUpdate(Calculation chosenCalculationObject)
        {
            // Code used for Pivots signals and Trend Signals
            if (!chosenCalculationObject.LastMatrixPoints.IsThisMatrixSignalInformed)
            {
                chosenCalculationObject.LastMatrixPoints.IsThisMatrixSignalInformed = true;

                switch (chosenCalculationObject.LastMatrixPoints.TrendSideSignal)
                {
                    case MatrixPoints.WhichTrendSideSignal.Bullish:
                        // Enter a long signal
                        LongShortSignal = OrderSideSignal.Buy;
                        break;

                    case MatrixPoints.WhichTrendSideSignal.Bearish:
                        // Enter a short signal
                        LongShortSignal = OrderSideSignal.Sell;
                        break;
                }
            }

            Drawing.DrawPivot(owner, drawingProperties, chosenCalculationObject.GetMatrixPoints(0));
        }

        public Calculation GetChosenCalculationObject()
        {
            switch(CalculationType)
            {
                case CalculationTypeListDowTheory.Trend:
                    return trendCalculation;
                case CalculationTypeListDowTheory.Pivot:
                    return pivotCalculation;
            }

            return null;
        }
    }
}
