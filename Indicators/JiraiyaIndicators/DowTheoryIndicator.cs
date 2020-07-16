using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Custom.Indicators.JiraiyaIndicators;
using NinjaTrader.Custom.Indicators.JiraiyaIndicators.DowPivot;

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators.JiraiyaIndicators
{
	public class DowTheoryIndicator : Indicator
	{
        DrawingProperties drawingProperties;
        DowTheoryClass dowTheory;

		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Enter the description for your new custom Indicator here.";
				Name										= "Dow Theory";
				Calculate									= Calculate.OnEachTick;
				IsOverlay									= true;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= false;
				DrawVerticalGridLines						= false;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;

                CalculationTypeDT                           = CalculationTypeListDowTheory.Pivot;
                CalculationTypePCW                          = CalculationTypeList.SwingForward;
                Strength                                    = 2;
                UseHighLow                                  = true;
                ShowPoints                                  = true;
                ShowLines                                   = true;
                MaxPercentOfPivotRetraction                 = 80;
                MinPercentOfPivotRetraction                 = 20;

                AddPlot(Brushes.Transparent, "Long Short Signal");
			}
			else if (State == State.Configure)
			{
			}
            else if(State == State.DataLoaded)
            {
                drawingProperties = new DrawingProperties(true, Brushes.Green, Brushes.Red, Brushes.Transparent, Brushes.White,
                                                          true, 15, Brushes.White, new Gui.Tools.SimpleFont("Arial", 11), TextAlignment.Center, Brushes.Transparent, Brushes.Transparent, 100,
                                                          true, Brushes.White, Gui.DashStyleHelper.Solid, 3);
                dowTheory = new DowTheoryClass(this, drawingProperties, CalculationTypeDT, CalculationTypePCW, Strength, UseHighLow, ShowPoints, ShowLines,
                                               MaxPercentOfPivotRetraction, MinPercentOfPivotRetraction);

                // Everytime the F5 key is pressed automatically will clear the output window.
                // LogPrinter.ResetOuputTabs();
            }
		}

        protected override void OnBarUpdate()
        {
            try
            { 
                dowTheory.Compute();
            }
            catch (Exception e)
            {
                Code.Output.Process(CurrentBar + "    " + e.ToString(), PrintTo.OutputTab2);
            }
        }

        public void ResetLongShortSignal()
        {
            Value[0] = 0;
        }

        // Other properties

        [Browsable(false)]
        public MatrixPoints LastMatrix
        {
            get
            {
                return dowTheory.GetChosenCalculationObject().LastMatrixPoints;
            }
        }

        #region Properties
        [NinjaScriptProperty]
        [Display(Name = "Dow theory calculation type", Order = 0, GroupName = "Parameters")]
        public CalculationTypeListDowTheory CalculationTypeDT
        { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Price action swing calculation type", Order = 1, GroupName = "Parameters")]
        public CalculationTypeList CalculationTypePCW
        { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Strengh", Order = 2, GroupName = "Parameters")]
        public double Strength { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Use HighLow", Order = 3, GroupName = "Parameters")]
        public bool UseHighLow
        { get; set; }

        [Display(Name = "Show points", Order = 4, GroupName = "Parameters")]
        public bool ShowPoints
        { get; set; }

        [Display(Name = "Show lines", Order = 5, GroupName = "Parameters")]
        public bool ShowLines
        { get; set; }

        [NinjaScriptProperty]
        [Range(0, 100)]
        [Display(Name = "Max percent of pivot retraction", Order = 6, GroupName = "Parameters")]
        public double MaxPercentOfPivotRetraction
        { get; set; }

        [NinjaScriptProperty]
        [Range(0, 100)]
        [Display(Name = "Min percent of pivot retraction", Order = 7, GroupName = "Parameters")]
        public double MinPercentOfPivotRetraction
        { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> LongShortSignal
        {
            get { return Values[0]; }
        }
        #endregion
    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private JiraiyaIndicators.DowTheoryIndicator[] cacheDowTheoryIndicator;
		public JiraiyaIndicators.DowTheoryIndicator DowTheoryIndicator(CalculationTypeListDowTheory calculationTypeDT, CalculationTypeList calculationTypePCW, double strength, bool useHighLow, double maxPercentOfPivotRetraction, double minPercentOfPivotRetraction)
		{
			return DowTheoryIndicator(Input, calculationTypeDT, calculationTypePCW, strength, useHighLow, maxPercentOfPivotRetraction, minPercentOfPivotRetraction);
		}

		public JiraiyaIndicators.DowTheoryIndicator DowTheoryIndicator(ISeries<double> input, CalculationTypeListDowTheory calculationTypeDT, CalculationTypeList calculationTypePCW, double strength, bool useHighLow, double maxPercentOfPivotRetraction, double minPercentOfPivotRetraction)
		{
			if (cacheDowTheoryIndicator != null)
				for (int idx = 0; idx < cacheDowTheoryIndicator.Length; idx++)
					if (cacheDowTheoryIndicator[idx] != null && cacheDowTheoryIndicator[idx].CalculationTypeDT == calculationTypeDT && cacheDowTheoryIndicator[idx].CalculationTypePCW == calculationTypePCW && cacheDowTheoryIndicator[idx].Strength == strength && cacheDowTheoryIndicator[idx].UseHighLow == useHighLow && cacheDowTheoryIndicator[idx].MaxPercentOfPivotRetraction == maxPercentOfPivotRetraction && cacheDowTheoryIndicator[idx].MinPercentOfPivotRetraction == minPercentOfPivotRetraction && cacheDowTheoryIndicator[idx].EqualsInput(input))
						return cacheDowTheoryIndicator[idx];
			return CacheIndicator<JiraiyaIndicators.DowTheoryIndicator>(new JiraiyaIndicators.DowTheoryIndicator(){ CalculationTypeDT = calculationTypeDT, CalculationTypePCW = calculationTypePCW, Strength = strength, UseHighLow = useHighLow, MaxPercentOfPivotRetraction = maxPercentOfPivotRetraction, MinPercentOfPivotRetraction = minPercentOfPivotRetraction }, input, ref cacheDowTheoryIndicator);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.JiraiyaIndicators.DowTheoryIndicator DowTheoryIndicator(CalculationTypeListDowTheory calculationTypeDT, CalculationTypeList calculationTypePCW, double strength, bool useHighLow, double maxPercentOfPivotRetraction, double minPercentOfPivotRetraction)
		{
			return indicator.DowTheoryIndicator(Input, calculationTypeDT, calculationTypePCW, strength, useHighLow, maxPercentOfPivotRetraction, minPercentOfPivotRetraction);
		}

		public Indicators.JiraiyaIndicators.DowTheoryIndicator DowTheoryIndicator(ISeries<double> input , CalculationTypeListDowTheory calculationTypeDT, CalculationTypeList calculationTypePCW, double strength, bool useHighLow, double maxPercentOfPivotRetraction, double minPercentOfPivotRetraction)
		{
			return indicator.DowTheoryIndicator(input, calculationTypeDT, calculationTypePCW, strength, useHighLow, maxPercentOfPivotRetraction, minPercentOfPivotRetraction);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.JiraiyaIndicators.DowTheoryIndicator DowTheoryIndicator(CalculationTypeListDowTheory calculationTypeDT, CalculationTypeList calculationTypePCW, double strength, bool useHighLow, double maxPercentOfPivotRetraction, double minPercentOfPivotRetraction)
		{
			return indicator.DowTheoryIndicator(Input, calculationTypeDT, calculationTypePCW, strength, useHighLow, maxPercentOfPivotRetraction, minPercentOfPivotRetraction);
		}

		public Indicators.JiraiyaIndicators.DowTheoryIndicator DowTheoryIndicator(ISeries<double> input , CalculationTypeListDowTheory calculationTypeDT, CalculationTypeList calculationTypePCW, double strength, bool useHighLow, double maxPercentOfPivotRetraction, double minPercentOfPivotRetraction)
		{
			return indicator.DowTheoryIndicator(input, calculationTypeDT, calculationTypePCW, strength, useHighLow, maxPercentOfPivotRetraction, minPercentOfPivotRetraction);
		}
	}
}

#endregion
