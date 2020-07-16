using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
using NinjaTrader.Custom.Indicators.JiraiyaIndicators;
using NinjaTrader.Custom.Indicators.JiraiyaIndicators.PriceActionSwing;

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators.JiraiyaIndicators
{
	public class PriceActionSwingIndicator : Indicator
	{
        DrawingProperties drawingProperties;
        PriceActionSwingClass priceActionSwing;

		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Enter the description for your new custom Indicator here.";
				Name										= "Price Action Swing";
				Calculate									= Calculate.OnEachTick;
				IsOverlay									= true;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= false;
				DrawVerticalGridLines						= false;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				IsSuspendedWhileInactive					= true;

                CalculationType                             = CalculationTypeList.SwingForward;
				Strength					                = 2;
				UseHighLow					                = true;
				ShowPoints					                = true;
                ShowLines                                   = true;

                DotParameters = new DotExpandableParameters()
                {
                    IsDotAutoScale = true,
                    UpDotColor = Brushes.Green,
                    DowDotColor = Brushes.Red,
                    UpDotOutlineColor = Brushes.Green,
                    DownDotOutlineColor = Brushes.Red
                };
			}
			else if (State == State.Configure)
			{
                drawingProperties = new DrawingProperties(DotParameters.IsDotAutoScale, DotParameters.UpDotColor, DotParameters.DowDotColor, DotParameters.UpDotOutlineColor, DotParameters.DownDotOutlineColor,
                                                          true, 15, Brushes.White, new Gui.Tools.SimpleFont("Arial", 11), TextAlignment.Center, Brushes.Transparent, Brushes.Transparent, 100,
                                                          true, Brushes.White, Gui.DashStyleHelper.Solid, 1);
                priceActionSwing = new PriceActionSwingClass(this, drawingProperties, CalculationType, Strength, UseHighLow, ShowPoints, ShowLines);
                

                // Everytime the F5 key is pressed automatically will clear the output window.
                // LogPrinter.ResetOuputTabs();
            }
		}

		protected override void OnBarUpdate()
		{
            try
            {
                priceActionSwing.Compute();
            }
            catch (Exception e)
            {
                Code.Output.Process(CurrentBar + "    " + e.ToString(), PrintTo.OutputTab2);
            }
        }

        #region Properties
        [NinjaScriptProperty]
        [Display(Name = "Calculation type", Order = 0, GroupName = "Parameters")]
        public CalculationTypeList CalculationType
        { get; set; }

        [NinjaScriptProperty]
        [Range(0.1, double.MaxValue)]
        [Display(Name = "Strength", Order = 1, GroupName = "Parameters")]
        public double Strength
        { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Use HighLow", Order = 2, GroupName = "Parameters")]
        public bool UseHighLow
        { get; set; }

        [Display(Name = "Show points", Order = 3, GroupName = "Parameters")]
        public bool ShowPoints
        { get; set; }

        [Display(Name = "Show lines", Order = 4, GroupName = "Parameters")]
        public bool ShowLines
        { get; set; }

        [Display(Name = "Dot parameters", Order = 5, GroupName = "Parameters")]
        public DotExpandableParameters DotParameters
        { get; set; }
        #endregion

    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private JiraiyaIndicators.PriceActionSwingIndicator[] cachePriceActionSwingIndicator;
		public JiraiyaIndicators.PriceActionSwingIndicator PriceActionSwingIndicator(CalculationTypeList calculationType, double strength, bool useHighLow)
		{
			return PriceActionSwingIndicator(Input, calculationType, strength, useHighLow);
		}

		public JiraiyaIndicators.PriceActionSwingIndicator PriceActionSwingIndicator(ISeries<double> input, CalculationTypeList calculationType, double strength, bool useHighLow)
		{
			if (cachePriceActionSwingIndicator != null)
				for (int idx = 0; idx < cachePriceActionSwingIndicator.Length; idx++)
					if (cachePriceActionSwingIndicator[idx] != null && cachePriceActionSwingIndicator[idx].CalculationType == calculationType && cachePriceActionSwingIndicator[idx].Strength == strength && cachePriceActionSwingIndicator[idx].UseHighLow == useHighLow && cachePriceActionSwingIndicator[idx].EqualsInput(input))
						return cachePriceActionSwingIndicator[idx];
			return CacheIndicator<JiraiyaIndicators.PriceActionSwingIndicator>(new JiraiyaIndicators.PriceActionSwingIndicator(){ CalculationType = calculationType, Strength = strength, UseHighLow = useHighLow }, input, ref cachePriceActionSwingIndicator);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.JiraiyaIndicators.PriceActionSwingIndicator PriceActionSwingIndicator(CalculationTypeList calculationType, double strength, bool useHighLow)
		{
			return indicator.PriceActionSwingIndicator(Input, calculationType, strength, useHighLow);
		}

		public Indicators.JiraiyaIndicators.PriceActionSwingIndicator PriceActionSwingIndicator(ISeries<double> input , CalculationTypeList calculationType, double strength, bool useHighLow)
		{
			return indicator.PriceActionSwingIndicator(input, calculationType, strength, useHighLow);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.JiraiyaIndicators.PriceActionSwingIndicator PriceActionSwingIndicator(CalculationTypeList calculationType, double strength, bool useHighLow)
		{
			return indicator.PriceActionSwingIndicator(Input, calculationType, strength, useHighLow);
		}

		public Indicators.JiraiyaIndicators.PriceActionSwingIndicator PriceActionSwingIndicator(ISeries<double> input , CalculationTypeList calculationType, double strength, bool useHighLow)
		{
			return indicator.PriceActionSwingIndicator(input, calculationType, strength, useHighLow);
		}
	}
}

#endregion
