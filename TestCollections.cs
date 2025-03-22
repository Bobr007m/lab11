using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PerformanceTesting
{
    public class TestCollections
    {
        public SortedSet<IGeometricFigure> Collection1 { get; set; }
        public SortedSet<string> Collection2 { get; set; }
        public HashSet<IGeometricFigure> Collection3 { get; set; }
        public HashSet<string> Collection4 { get; set; }

        public TestCollections(int size)
        {
            Collection1 = new SortedSet<IGeometricFigure>();
            Collection2 = new SortedSet<string>();
            Collection3 = new HashSet<IGeometricFigure>();
            Collection4 = new HashSet<string>();

            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                var figure = new Circle(rand.Next(1, 100));
                Collection1.Add(figure);
                Collection2.Add(figure.ToString());
                Collection3.Add(figure);
                Collection4.Add(figure.ToString());
            }
        }

        public void MeasurePerformance()
        {
            Stopwatch sw = new Stopwatch();

            // Первый элемент
            var firstElement = Collection1.Min;
            MeasureTime(Collection1, firstElement, "SortedSet<T>");
            MeasureTime(Collection2, firstElement.ToString(), "SortedSet<string>");
            MeasureTime(Collection3, firstElement, "HashSet<T>");
            MeasureTime(Collection4, firstElement.ToString(), "HashSet<string>");

            // Центральный элемент
            var middleElement = GetMiddleElement(Collection1);
            MeasureTime(Collection1, middleElement, "SortedSet<T>");
            MeasureTime(Collection2, middleElement.ToString(), "SortedSet<string>");
            MeasureTime(Collection3, middleElement, "HashSet<T>");
            MeasureTime(Collection4, middleElement.ToString(), "HashSet<string>");

            // Последний элемент
            var lastElement = Collection1.Max;
            MeasureTime(Collection1, lastElement, "SortedSet<T>");
            MeasureTime(Collection2, lastElement.ToString(), "SortedSet<string>");
            MeasureTime(Collection3, lastElement, "HashSet<T>");
            MeasureTime(Collection4, lastElement.ToString(), "HashSet<string>");

            // Элемент, которого нет
            var nonExistent = new Circle(1000);
            MeasureTime(Collection1, nonExistent, "SortedSet<T>");
            MeasureTime(Collection2, nonExistent.ToString(), "SortedSet<string>");
            MeasureTime(Collection3, nonExistent, "HashSet<T>");
            MeasureTime(Collection4, nonExistent.ToString(), "HashSet<string>");
        }

        private void MeasureTime<T>(ICollection<T> collection, T element, string collectionName)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool contains = collection.Contains(element);
            sw.Stop();
            Console.WriteLine($"{collectionName}: поиск элемента {(contains ? "найден" : "не найден")} за {sw.ElapsedTicks}");
        }

        private IGeometricFigure GetMiddleElement(SortedSet<IGeometricFigure> set)
        {
            int index = set.Count / 2;
            int i = 0;
            foreach (var item in set)
            {
                if (i == index) return item;
                i++;
            }
            return null;
        }
    }
}