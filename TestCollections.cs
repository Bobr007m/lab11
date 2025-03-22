using Geometryclass;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PerformanceTesting
{
    public class TestCollections
    {
        public SortedSet<Rectangle1> Collection1 { get; } = new SortedSet<Rectangle1>();
        public SortedSet<string> Collection2 { get; } = new SortedSet<string>();
        public HashSet<Geometryfigure1> Collection3 { get; } = new HashSet<Geometryfigure1>();
        public HashSet<string> Collection4 { get; } = new HashSet<string>();

        // Конструктор с генерацией 1000 элементов
        public TestCollections(int size)
        {
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                var rect = new Rectangle1(rnd.Next(1, 100), rnd.Next(1, 100));

                // Заполнение коллекций
                Collection1.Add(rect);
                Collection2.Add(rect.ToString());
                Collection3.Add(rect.BaseFigure); // Используем свойство BaseFigure
                Collection4.Add(rect.BaseFigure.ToString());
            }
        }

        // Метод измерения времени поиска
        public void MeasureSearchTime()
        {
            // Выбор элементов для поиска
            var first = Collection1.Min;
            var middle = GetElementAt(Collection1, Collection1.Count / 2);
            var last = Collection1.Max;
            var nonExistent = new Rectangle1(999, 999);

            // Замеры времени
            Console.WriteLine("Поиск первого элемента:");
            Measure(() => Collection1.Contains(first), "Collection1");
            Measure(() => Collection3.Contains(first.BaseFigure), "Collection3");

            Console.WriteLine("\nПоиск центрального элемента:");
            Measure(() => Collection1.Contains(middle), "Collection1");
            Measure(() => Collection3.Contains(middle.BaseFigure), "Collection3");

            Console.WriteLine("\nПоиск последнего элемента:");
            Measure(() => Collection1.Contains(last), "Collection1");
            Measure(() => Collection3.Contains(last.BaseFigure), "Collection3");

            Console.WriteLine("\nПоиск несуществующего элемента:");
            Measure(() => Collection1.Contains(nonExistent), "Collection1");
            Measure(() => Collection3.Contains(nonExistent.BaseFigure), "Collection3");
        }

        // Вспомогательный метод для получения элемента по индексу
        private T GetElementAt<T>(SortedSet<T> set, int index)
        {
            int count = 0;
            foreach (var item in set)
            {
                if (count == index) return item;
                count++;
            }
            return default;
        }

        // Метод для многократного замера времени
        private void Measure(Action action, string collectionName)
        {
            Stopwatch sw = new Stopwatch();
            long totalTime = 0;
            int iterations = 1000;

            for (int i = 0; i < iterations; i++)
            {
                sw.Restart();
                action();
                sw.Stop();
                totalTime += sw.ElapsedTicks;
            }

            Console.WriteLine($"{collectionName}: {totalTime / iterations} тиков (среднее)");
        }
    }
}