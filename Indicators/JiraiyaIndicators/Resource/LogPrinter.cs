using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
using System.Collections.Generic;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators
{
    public class LogPrinter
    {
        private int lastBarIndex = 0;
        private readonly List<NinjaScriptBase> invisibleIndicator = new List<NinjaScriptBase>();

        public void Print(NinjaScriptBase owner, object text)
        {
            if (!IsInvisible(owner))
            {
                if (owner.CurrentBar != lastBarIndex || owner.CurrentBar == 0)
                {
                    Code.Output.Process(owner.CurrentBar + " " + owner.ToString(), PrintTo.OutputTab1);
                    Code.Output.Process(GetStringSpace(owner.CurrentBar) + " " + text, PrintTo.OutputTab1);
                }
                else
                {
                    Code.Output.Process(GetStringSpace(owner.CurrentBar) + " " + text, PrintTo.OutputTab1);
                }

                lastBarIndex = owner.CurrentBar;
            }
        }

        public void PrintError(NinjaScriptBase owner, object text)
        {
            if (!IsInvisible(owner))
            {
                Draw.TextFixed(owner, "Error", text.ToString(), TextPosition.BottomRight);
            }
        }

        /// <summary>
        /// Clear the all tabs of output window.
        /// </summary>
        public static void ResetOuputTabs()
        {
            Code.Output.Reset(PrintTo.OutputTab1);
            Code.Output.Reset(PrintTo.OutputTab2);
        }

        public void SetIndicatorAsInvisible(NinjaScriptBase owner)
        {
            invisibleIndicator.Add(owner);
        }

        private bool IsInvisible(NinjaScriptBase owner)
        {
            foreach(NinjaScriptBase nsb in invisibleIndicator)
            {
                if (nsb == owner)
                {
                    return true;
                }
                break;
            }

            return false;
        }

        private string GetStringSpace(object text)
        {
            // The multiplication number was found doing 7 divided by 3,
            // this means that each number printed equals to 2.3333333333 spaces.
            double charCount = text.ToString().Length * 2.3333333333;

            string space = "";

            for (int i = 0; i < charCount; i++)
                space += " ";

            return space;
        }
    }
}
