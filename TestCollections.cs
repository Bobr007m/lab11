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

        // Конструктор с генерацией 1000 элементов и обработкой исключений
        public TestCollections(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Размер коллекции должен быть положительным числом", nameof(size));

            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании коллекций: {ex.Message}");
                throw;
            }
        }

        // Метод измерения времени поиска
        public void MeasureSearchTime()
        {
            try
            {
                // Выбор элементов для поиска
                var first = Collection1.Min;
                var middle = GetElementAt(Collection1, Collection1.Count / 2);
                var last = Collection1.Max;
                var nonExistent = new Rectangle1(999, 999);

                // Замеры времени для всех коллекций
                Console.WriteLine("Поиск первого элемента:");
                MeasureSearch(Collection1, first, "Collection1");
                MeasureSearch(Collection2, first.ToString(), "Collection2");
                MeasureSearch(Collection3, first.BaseFigure, "Collection3");
                MeasureSearch(Collection4, first.BaseFigure.ToString(), "Collection4");

                Console.WriteLine("\nПоиск центрального элемента:");
                MeasureSearch(Collection1, middle, "Collection1");
                MeasureSearch(Collection2, middle.ToString(), "Collection2");
                MeasureSearch(Collection3, middle.BaseFigure, "Collection3");
                MeasureSearch(Collection4, middle.BaseFigure.ToString(), "Collection4");

                Console.WriteLine("\nПоиск последнего элемента:");
                MeasureSearch(Collection1, last, "Collection1");
                MeasureSearch(Collection2, last.ToString(), "Collection2");
                MeasureSearch(Collection3, last.BaseFigure, "Collection3");
                MeasureSearch(Collection4, last.BaseFigure.ToString(), "Collection4");

                Console.WriteLine("\nПоиск несуществующего элемента:");
                MeasureSearch(Collection1, nonExistent, "Collection1");
                MeasureSearch(Collection2, nonExistent.ToString(), "Collection2");
                MeasureSearch(Collection3, nonExistent.BaseFigure, "Collection3");
                MeasureSearch(Collection4, nonExistent.BaseFigure.ToString(), "Collection4");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при измерении времени поиска: {ex.Message}");
            }
        }

        // Вспомогательный метод для получения элемента по индексу
        private T GetElementAt<T>(SortedSet<T> set, int index)
        {
            if (set == null)
                throw new ArgumentNullException(nameof(set));
            if (index < 0 || index >= set.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            int count = 0;
            foreach (var item in set)
            {
                if (count == index) return item;
                count++;
            }
            return default;
        }

        // Обобщенный метод для измерения времени поиска
        private void MeasureSearch<T>(IEnumerable<T> collection, T item, string collectionName)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            Stopwatch sw = new Stopwatch();
            long totalTime = 0;
            int iterations = 1000;

            for (int i = 0; i < iterations; i++)
            {
                sw.Restart();
                bool found = false;
                foreach (var element in collection)
                {
                    if (EqualityComparer<T>.Default.Equals(element, item))
                    {
                        found = true;
                        break;
                    }
                }
                sw.Stop();
                totalTime += sw.ElapsedTicks;
            }

            Console.WriteLine($"{collectionName}: {totalTime / iterations} тиков (среднее)");
        }
    }
}