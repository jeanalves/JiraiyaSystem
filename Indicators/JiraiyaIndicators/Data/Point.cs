namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators
{
    public class Point
    {
        public int Index { get; private set; }
        public double Price { get; set; }
        public int BarIndex { get; set; }
        public SidePoint CurrentSideSwing { get; private set; }

        public Point(int index, double price, int barIndex, SidePoint currentSideSwing)
        {
            Index = index;
            Price = price;
            BarIndex = barIndex;
            CurrentSideSwing = currentSideSwing;
        }

        public enum SidePoint
        {
            High,
            Low,
            Unknow
        }
    }
}
