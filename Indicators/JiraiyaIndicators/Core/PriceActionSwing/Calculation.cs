using NinjaTrader.NinjaScript;
using System.Collections.Generic;
using System.Linq;

namespace NinjaTrader.Custom.Indicators.JiraiyaIndicators.PriceActionSwing
{
    public abstract class Calculation
    {
        // Fields

        protected Series<double> highs;
        protected Series<double> lows;
        protected List<Point> pointsList = new List<Point>();

        protected readonly NinjaScriptBase owner;
        protected readonly PriceActionSwingClass priceActionSwingClass;

        private CalculationData calculationData = new CalculationData();
        private CalculationStage calculationStage = CalculationStage.FirstPoint;

        // Initialization

        protected Calculation(NinjaScriptBase owner, PriceActionSwingClass priceActionSwingClass)
        {
            this.owner = owner;
            this.priceActionSwingClass = priceActionSwingClass;

            highs = new Series<double>(owner);
            lows = new Series<double>(owner);
        }

        public void Calculate()
        {
            SetValues();

            calculationData = new CalculationData();

            if (pointsList.Count == 0)
            {
                calculationData = OnCalculationOfFirstSwingPointRequest();
            }
            else if (owner.State == State.Historical)
            {
                calculationData = OnCalculationOfEachBarSwingPointRequest();
                calculationStage = CalculationStage.EachBarSwingPoint;
            }
            else if (owner.State == State.Realtime)
            {
                calculationData = OnCalculationOFEachTickSwingPointRequest();
                calculationStage = CalculationStage.EachTickSwingPoint;
            }

            if (calculationData.isNewSwing)
            {
                AddOrUpdatePointsIfNewSwing(calculationData, calculationStage);
            }
        }

        // Public (methods)

        public Point.SidePoint LastSideTrend()
        {
            return pointsList[pointsList.Count - 1].CurrentSideSwing;
        }

        public double LastPrice()
        {
            return pointsList.Count < 0 ? 0 : pointsList[pointsList.Count - 1].Price;
        }

        public Point LastHigh()
        {
            foreach (Point point in pointsList.AsEnumerable().Reverse())
            {
                if (point.CurrentSideSwing == Point.SidePoint.High)
                {
                    return point;
                }
            }
            return null;
        }

        public Point LastLow()
        { 
            foreach (Point point in pointsList.AsEnumerable().Reverse())
            {
                if (point.CurrentSideSwing == Point.SidePoint.Low)
                {
                    return point;
                }
            }
            return null;
        }

        public List<Point> GetPointsList()
        {
            return pointsList;
        }

        public Point GetPoint(int pointsAgo)
        {
            return pointsList.Count < pointsAgo + 1 ? null : pointsList[(pointsList.Count - 1) - pointsAgo];
        }

        // Protected (methods)

        protected virtual CalculationData OnCalculationOfFirstSwingPointRequest()
        {
            //logPrinter.Print(owner, "Virtual Calculation.OnCalculationOfFirstSwingPointRequest()");

            Point.SidePoint sideSwing = owner.Close.GetValueAt(0) > owner.Open.GetValueAt(0) ?
                Point.SidePoint.Low : Point.SidePoint.High;

            if (priceActionSwingClass.UseHighLow)
            {
                switch (sideSwing)
                {
                    case Point.SidePoint.High:
                        return new CalculationData(owner.High.GetValueAt(0), 0, sideSwing);

                    case Point.SidePoint.Low:
                        return new CalculationData(owner.Low.GetValueAt(0), 0, sideSwing);
                }
            }
            else
            {
                return new CalculationData(owner.Open.GetValueAt(0), 0, sideSwing);
            }

            return new CalculationData();
        }

        protected abstract CalculationData OnCalculationOfEachBarSwingPointRequest();

        protected virtual CalculationData OnCalculationOFEachTickSwingPointRequest()
        {
            //logPrinter.Print(owner, "virtual Calculation.CalculateEachTickSwing()");

            return new CalculationData();
        }

        // Private (methods)

        private void SetValues()
        {
            if (priceActionSwingClass.UseHighLow)
            {
                highs[0] = owner.High[0];
                lows[0] = owner.Low[0];
            }
            else
            {
                highs[0] = owner.Close[0];
                lows[0] = owner.Close[0];
            }
        }

        private void AddOrUpdatePointsIfNewSwing(CalculationData calculationData, CalculationStage calculationStage)
        {
            switch (calculationStage)
            {
                case CalculationStage.FirstPoint:

                    if (calculationData.sideSwing == Point.SidePoint.High)
                    {
                        AddHigh(pointsList.Count, calculationData.price, calculationData.barIndex, calculationData.sideSwing);
                    }
                    else if (calculationData.sideSwing == Point.SidePoint.Low)
                    {
                        AddLow(pointsList.Count, calculationData.price, calculationData.barIndex, calculationData.sideSwing);
                    }
                    else if (calculationData.sideSwing == Point.SidePoint.Unknow && pointsList.Count == 0)
                    {
                        AddUnknow(pointsList.Count, owner.Open.GetValueAt(0), 0, calculationData.sideSwing);
                    }

                    break;

                case CalculationStage.EachBarSwingPoint:

                    DefaultAddUpdatePointsManagement(calculationData);

                    break;

                case CalculationStage.EachTickSwingPoint:

                    DefaultAddUpdatePointsManagement(calculationData);

                    break;
            }
        }

        private void DefaultAddUpdatePointsManagement(CalculationData calculationData)
        {
            if (calculationData.sideSwing == Point.SidePoint.High && LastSideTrend() != Point.SidePoint.High)
            {
                AddHigh(pointsList.Count, calculationData.price, calculationData.barIndex, calculationData.sideSwing);
            }
            else if (calculationData.sideSwing == Point.SidePoint.Low && LastSideTrend() != Point.SidePoint.Low)
            {
                AddLow(pointsList.Count, calculationData.price, calculationData.barIndex, calculationData.sideSwing);
            }
            else if (calculationData.sideSwing == Point.SidePoint.High && LastSideTrend() == Point.SidePoint.High &&
                calculationData.price > LastPrice())
            {
                UpdateHigh(calculationData.price, calculationData.barIndex);
            }
            else if (calculationData.sideSwing == Point.SidePoint.Low && LastSideTrend() == Point.SidePoint.Low &&
                calculationData.price < LastPrice())
            {
                UpdateLow(calculationData.price, calculationData.barIndex);
            }
        }

        private void AddHigh(int index, double price, int barIndex, Point.SidePoint sideSwing)
        {
            //logPrinter.Print(owner, "Calculation.AddHigh()");

            pointsList.Add(new Point(index, price, barIndex, sideSwing));
        }

        private void AddLow(int index, double price, int barIndex, Point.SidePoint sideSwing)
        {
            //logPrinter.Print(owner, "Calculation.AddLow()");

            pointsList.Add(new Point(index, price, barIndex, sideSwing));
        }

        private void AddUnknow(int index, double price, int barIndex, Point.SidePoint sideSwing)
        {
            //logPrinter.Print(owner, "Calculation.AddUnknow()");

            pointsList.Add(new Point(index, price, barIndex, sideSwing));
        }

        private void UpdateHigh(double price, int barIndex)
        {
            //logPrinter.Print(owner, "Calculation.UpdateHigh()");

            Point temp = pointsList[pointsList.Count - 1];

            temp.Price = price;
            temp.BarIndex = barIndex;

            pointsList[pointsList.Count - 1] = temp;
        }

        private void UpdateLow(double price, int barIndex)
        {
            //logPrinter.Print(owner, "Calculation.UpdateLow()");

            Point temp = pointsList[pointsList.Count - 1];

            temp.Price = price;
            temp.BarIndex = barIndex;

            pointsList[pointsList.Count - 1] = temp;
        }

        // Properties

        public CalculationData CalcData
        {
            get
            {
                return calculationData;
            }
        }

        public CalculationStage CalcStage
        {
            get
            {
                return calculationStage;
            }
        }

        // Miscellaneous

        public class CalculationData
        {
            public bool isNewSwing;
            public double price;
            public int barIndex;
            public Point.SidePoint sideSwing;

            public CalculationData()
            {
                isNewSwing = false;
            }

            public CalculationData(double price, int barIndex, Point.SidePoint sideSwing)
            {
                isNewSwing = true;
                this.price = price;
                this.barIndex = barIndex;
                this.sideSwing = sideSwing;
            }
        }

        public enum CalculationStage
        {
            FirstPoint,
            EachBarSwingPoint,
            EachTickSwingPoint
        }
    }
}
