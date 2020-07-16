using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
using System.Windows.Media;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators
{
    public static class DrawWrapper
    {
        public static void DrawDot(NinjaScriptBase owner,
                                   DrawingProperties drawingProperties,
                                   int pointIndex,
                                   int barIndex,
                                   double price,
                                   Brush dotColor)
        {
            Draw.Dot(owner, ("Dot " + pointIndex), drawingProperties.IsDotAutoScale,
                        ConvertBarIndexToBarsAgo(owner, barIndex), price,
                        dotColor).OutlineBrush = drawingProperties.UpDotOutlineColor;
        }

        public static void DrawText(NinjaScriptBase owner,
                                    DrawingProperties drawingProperties,
                                    int pointIndex,
                                    int barIndex,
                                    double price,
                                    int yPixelOffSet)
        {
            Draw.Text(owner, ("Text " + pointIndex), drawingProperties.IsTextAutoScale, pointIndex.ToString(),
                        ConvertBarIndexToBarsAgo(owner, barIndex), price, yPixelOffSet, drawingProperties.TextColor,
                        drawingProperties.TextSimpleFont, drawingProperties.TextAligmentPropertie,
                        drawingProperties.TextOutlineBrush, drawingProperties.TextAreaBrush, drawingProperties.TextAreaOpacity);
        }

        public static void DrawLine(NinjaScriptBase owner,
                                    DrawingProperties drawingProperties,
                                    int pointIndex,
                                    int barIndex1,
                                    double price1,
                                    int barIndex0,
                                    double price0)
        {
            Draw.Line(owner, "Line type 1" + pointIndex, false,
                ConvertBarIndexToBarsAgo(owner, barIndex1), price1,
                ConvertBarIndexToBarsAgo(owner, barIndex0), price0,
                drawingProperties.LineColor, drawingProperties.LineDashStyle, drawingProperties.LineWidth);
        }

        public static void DrawLineForTest(NinjaScriptBase owner,
                                           string text,
                                           Brush color,
                                           int barIndex1,
                                           double price1,
                                           int barIndex0,
                                           double price0)
        {
            Draw.Line(owner, text, false,
                      ConvertBarIndexToBarsAgo(owner, barIndex1), price1, 
                      ConvertBarIndexToBarsAgo(owner, barIndex0), price0, 
                      color, Gui.DashStyleHelper.Solid, 5);
        }

        public static void DrawPathLine(NinjaScriptBase owner,
                                        DrawingProperties drawingProperties,
                                        int pointIndex,
                                        int barIndex1,
                                        double price1,
                                        int barIndex0,
                                        double price0,
                                        Brush color)
        {
            Draw.Line(owner, "Line type 2" + pointIndex, false,
                ConvertBarIndexToBarsAgo(owner, barIndex1), price1,
                ConvertBarIndexToBarsAgo(owner, barIndex0), price0,
                color, drawingProperties.LineDashStyle, drawingProperties.LineWidth);
        }

        private static int ConvertBarIndexToBarsAgo(NinjaScriptBase owner, int barIndex)
        {
            return (barIndex - owner.CurrentBar) < 0 ? (barIndex - owner.CurrentBar) * -1 : barIndex - owner.CurrentBar;
        }
    }
}
