using NinjaTrader.NinjaScript;
using System.Windows.Media;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators.PriceActionSwing
{
    public static class Drawing
    {
        public static void DrawPoint(NinjaScriptBase owner, Point point, DrawingProperties drawingProperties)
        {
            switch(point.CurrentSideSwing)
            {
                case Point.SidePoint.High:
                    DrawWrapper.DrawDot(owner, drawingProperties, point.Index, 
                                        point.BarIndex, point.Price, drawingProperties.UpDotColor);
                    DrawWrapper.DrawText(owner, drawingProperties, point.Index, 
                                         point.BarIndex, point.Price, drawingProperties.TextYPixelOffSet);
                    break;

                case Point.SidePoint.Low:
                    DrawWrapper.DrawDot(owner, drawingProperties, point.Index,
                                        point.BarIndex, point.Price, drawingProperties.DownDotColor);
                    DrawWrapper.DrawText(owner, drawingProperties, point.Index,
                                         point.BarIndex, point.Price, drawingProperties.TextYPixelOffSet * -1);
                    break;

                case Point.SidePoint.Unknow:
                    DrawWrapper.DrawDot(owner, drawingProperties, point.Index,
                                        point.BarIndex, point.Price, Brushes.Gray);
                    break;
            }
        }

        public static void DrawZigZag(NinjaScriptBase owner, DrawingProperties drawingProperties, Point pointOne, Point pointTwo)
        {
            DrawWrapper.DrawLine(owner, drawingProperties,
                                 pointTwo.Index,
                                 pointTwo.BarIndex, pointTwo.Price,
                                 pointOne.BarIndex, pointOne.Price);
        }
    }
}
