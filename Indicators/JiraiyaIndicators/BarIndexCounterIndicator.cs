#region Using declarations
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Gui;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators.JiraiyaIndicators
{
	public class BarIndexCounterIndicator : Indicator
	{
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Used to print the index of each bar.";
				Name										= "BarIndexCounter";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= true;
				DisplayInDataBox							= false;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= false;
				DrawVerticalGridLines						= false;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				BarsInterval					            = 5;
				YOffSet					                    = 15;
				DrawPosition					            = EDrawPosition.Up;
				Color					                    = Brushes.White;
			}
			else if (State == State.Configure)
			{
			}
		}

		protected override void OnBarUpdate()
		{
			if(CurrentBar % BarsInterval == 0)
            {
                switch(DrawPosition)
                {
                    case EDrawPosition.Up:
                        Draw.Text(this, "Up text " + CurrentBar, true, CurrentBar.ToString(), 0, High[0], YOffSet, Color,
                            new Gui.Tools.SimpleFont("Arial", 11), System.Windows.TextAlignment.Center,
                            Brushes.Transparent, Brushes.Transparent, 100);
                        break;

                    case EDrawPosition.Low:
                        Draw.Text(this, "Low text " + CurrentBar, true, CurrentBar.ToString(), 0, Low[0], (-1 * YOffSet), Color,
                            new Gui.Tools.SimpleFont("Arial", 11), System.Windows.TextAlignment.Center,
                            Brushes.Transparent, Brushes.Transparent, 100);
                        break;

                    case EDrawPosition.Both:
                        Draw.Text(this, "Up text " + CurrentBar, true, CurrentBar.ToString(), 0, High[0], YOffSet, Color,
                            new Gui.Tools.SimpleFont("Arial", 11), System.Windows.TextAlignment.Center,
                            Brushes.Transparent, Brushes.Transparent, 100);

                        Draw.Text(this, "Low text " + CurrentBar, true, CurrentBar.ToString(), 0, Low[0], (-1 * YOffSet), Color,
                            new Gui.Tools.SimpleFont("Arial", 11), System.Windows.TextAlignment.Center,
                            Brushes.Transparent, Brushes.Transparent, 100);
                        break;
                }
                
            }
		}

		#region Properties
		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="BarsInterval", Order=1, GroupName="Parameters")]
		public int BarsInterval
		{ get; set; }

		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="YOffSet", Order=2, GroupName="Parameters")]
		public int YOffSet
		{ get; set; }

		[NinjaScriptProperty]
		[Display(Name="DrawPosition", Description="Define if the text will be above or over price.", Order=3, GroupName="Parameters")]
		public EDrawPosition DrawPosition
		{ get; set; }

		[NinjaScriptProperty]
		[XmlIgnore]
		[Display(Name="Color", Order=4, GroupName="Parameters")]
		public Brush Color
		{ get; set; }

		[Browsable(false)]
		public string ColorSerializable
		{
			get { return Serialize.BrushToString(Color); }
			set { Color = Serialize.StringToBrush(value); }
		}			
		#endregion

	}
}

public enum EDrawPosition
{
    Up,
    Low,
    Both
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private JiraiyaIndicators.BarIndexCounterIndicator[] cacheBarIndexCounterLauncher;
		public JiraiyaIndicators.BarIndexCounterIndicator BarIndexCounterLauncher(int barsInterval, int yOffSet, EDrawPosition drawPosition, Brush color)
		{
			return BarIndexCounterLauncher(Input, barsInterval, yOffSet, drawPosition, color);
		}

		public JiraiyaIndicators.BarIndexCounterIndicator BarIndexCounterLauncher(ISeries<double> input, int barsInterval, int yOffSet, EDrawPosition drawPosition, Brush color)
		{
			if (cacheBarIndexCounterLauncher != null)
				for (int idx = 0; idx < cacheBarIndexCounterLauncher.Length; idx++)
					if (cacheBarIndexCounterLauncher[idx] != null && cacheBarIndexCounterLauncher[idx].BarsInterval == barsInterval && cacheBarIndexCounterLauncher[idx].YOffSet == yOffSet && cacheBarIndexCounterLauncher[idx].DrawPosition == drawPosition && cacheBarIndexCounterLauncher[idx].Color == color && cacheBarIndexCounterLauncher[idx].EqualsInput(input))
						return cacheBarIndexCounterLauncher[idx];
			return CacheIndicator<JiraiyaIndicators.BarIndexCounterIndicator>(new JiraiyaIndicators.BarIndexCounterIndicator(){ BarsInterval = barsInterval, YOffSet = yOffSet, DrawPosition = drawPosition, Color = color }, input, ref cacheBarIndexCounterLauncher);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.JiraiyaIndicators.BarIndexCounterIndicator BarIndexCounterLauncher(int barsInterval, int yOffSet, EDrawPosition drawPosition, Brush color)
		{
			return indicator.BarIndexCounterLauncher(Input, barsInterval, yOffSet, drawPosition, color);
		}

		public Indicators.JiraiyaIndicators.BarIndexCounterIndicator BarIndexCounterLauncher(ISeries<double> input , int barsInterval, int yOffSet, EDrawPosition drawPosition, Brush color)
		{
			return indicator.BarIndexCounterLauncher(input, barsInterval, yOffSet, drawPosition, color);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.JiraiyaIndicators.BarIndexCounterIndicator BarIndexCounterLauncher(int barsInterval, int yOffSet, EDrawPosition drawPosition, Brush color)
		{
			return indicator.BarIndexCounterLauncher(Input, barsInterval, yOffSet, drawPosition, color);
		}

		public Indicators.JiraiyaIndicators.BarIndexCounterIndicator BarIndexCounterLauncher(ISeries<double> input , int barsInterval, int yOffSet, EDrawPosition drawPosition, Brush color)
		{
			return indicator.BarIndexCounterLauncher(input, barsInterval, yOffSet, drawPosition, color);
		}
	}
}

#endregion
