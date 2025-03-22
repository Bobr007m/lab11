using System;
using System.Collections;
using System.Collections.Generic;
using Geometryclass;
using PerformanceTesting;

namespace NonGenericCollections
{
    public class Program
    {
        static void Main()
        {
            // Создание стека
            Stack stack = new Stack();

            // Добавление объектов
            stack.Push(new Rectangle1(5.0, 10.0));
            stack.Push(new Rectangle1(3, 7));
            stack.Push(new Rectangle1(8, 4));
            
            Console.WriteLine("Элементы в стеке:");
            foreach (var item in stack)
            {
                if (item is IShowable showable)
                    showable.Show();
            }

            // Удаление элемента
            Console.WriteLine("\nУдаляем верхний элемент:");
            var removed = stack.Pop();
            Console.WriteLine($"Удален: {removed}");

            // Поиск элемента
            Console.WriteLine("\nПоиск элемента:");
            bool contains = stack.Contains(new Circle1(5));
            Console.WriteLine($"Содержит круг с радиусом 5: {contains}");

            // Клонирование коллекции
            Console.WriteLine("\nКлонирование стека:");
            Stack clonedStack = (Stack)stack.Clone();
            Console.WriteLine("Клонированный стек:");
            foreach (var item in clonedStack)
            {
                if (item is IShowable showableClone)
                    showableClone.Show();
            }

            // Сортировка (не поддерживается в Stack, поэтому преобразуем в List)
            Console.WriteLine("\nСортировка элементов:");
            ArrayList sortedList = new ArrayList(stack);
            sortedList.Sort();
            foreach (var item in sortedList)
            {
                if (item is IShowable showableSorted)
                    showableSorted.Show();
            }
            // Запрос 1: Количество прямоугольников
            int rectangleCount = 0;
            foreach (var item in stack)
            {
                if (item is Rectangle1)
                {
                    rectangleCount++;
                }
            }
            Console.WriteLine($"\nКоличество прямоугольников в стеке: {rectangleCount}");

            // Запрос 2: Печать всех прямоугольников
            Console.WriteLine("\nВсе прямоугольники в стеке:");
            foreach (var item in stack)
            {
                if (item is Rectangle1 rect)
                {
                    Console.WriteLine(rect);
                }
            }

            // Запрос 3: Поиск прямоугольника с максимальной площадью
            Rectangle1 maxAreaRect = null;
            double maxArea = 0;
            foreach (var item in stack)
            {
                if (item is Rectangle1 rect && rect.Area() > maxArea)
                {
                    maxArea = rect.Area();
                    maxAreaRect = rect;
                }
            }
            Console.WriteLine($"\nПрямоугольник с максимальной площадью: {maxAreaRect}");
            // 2 задание
            // Создание словаря
            SortedDictionary<string, IGeometricFigure1> dictionary = new SortedDictionary<string, IGeometricFigure1>();

                // Добавление объектов
                dictionary.Add("Circle1", new Circle1(5));
                dictionary.Add("Parallelepiped1", new Parallelepiped1(10));
                dictionary.Add("Rectangle1", new Rectangle1(4, 6));

                Console.WriteLine("Элементы в словаре:");
                foreach (var kvp in dictionary)
                {
                    Console.WriteLine($"{kvp.Key}:");
                    kvp.Value.Show();
                }

                // Удаление элемента
                Console.WriteLine("\nУдаляем элемент 'Circle1':");
                dictionary.Remove("Circle1");

                // Поиск элемента
                Console.WriteLine("\nПоиск элемента:");
                bool containsKey = dictionary.ContainsKey("Parallelepiped1");
                Console.WriteLine($"Содержит ключ 'Parallelepiped1': {containsKey}");

                // Клонирование коллекции
                Console.WriteLine("\nКлонирование словаря:");
                SortedDictionary<string, IGeometricFigure1> clonedDictionary = new SortedDictionary<string, IGeometricFigure1>(dictionary);
                foreach (var kvp in clonedDictionary)
                {
                    Console.WriteLine($"{kvp.Key}:");
                    kvp.Value.Show();
                }

                // Сортировка (автоматическая в SortedDictionary)
                Console.WriteLine("\nСловарь уже отсортирован по ключам.");
            List<Rectangle1> rectangles = new List<Rectangle1>
            {
                new Rectangle1 (5.0, 10.0),
                new Rectangle1(3, 7),
                new Rectangle1 (8, 4)
            };

            // Перебор элементов
            Console.WriteLine("Элементы списка:");
            foreach (var rect in rectangles)
            {
                Console.WriteLine(rect);
            }
            // Запрос 1: Количество прямоугольников
            Console.WriteLine($"\nКоличество прямоугольников в списке: {rectangles.Count}");

            // Запрос 2: Печать всех прямоугольников
            Console.WriteLine("\nВсе прямоугольники в списке:");
            foreach (var rect in rectangles)
            {
                Console.WriteLine(rect);
            }

            // Запрос 3: Поиск прямоугольника с минимальной площадью
            Rectangle1 minAreaRect = rectangles[0];
            foreach (var rect in dictionary)
            {
                if (rect.Value.Area() < minAreaRect.Area())
                {
                    minAreaRect = rect;
                }
            }
            Console.WriteLine($"\nПрямоугольник с минимальной площадью: {minAreaRect}");
            // 3 задание
            // Создаем тестовые коллекции с 1000 элементами
            TestCollections test = new TestCollections(1000);
            test.MeasureSearchTime();
        }
        }
    }
