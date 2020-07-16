namespace NinjaTrader.NinjaScript.Strategies.JiraiyaStrategies
{
    public class Entry
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double StopPrice { get; set; }
        public double TargetPrice { get; set; }

        public enum EntrySide
        {
            Long,
            Short
        }
    }
}
