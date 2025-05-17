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
            DemonstrateStackOperations();
            DemonstrateDictionaryOperations();
            RunPerformanceTest();
        }

        static void DemonstrateStackOperations()
        {
            try
            {
                // 1. Создание и заполнение стека
                Stack stack = CreateAndFillStack();

                // 2. Вывод элементов стека
                DisplayStackContents(stack, "Исходный стек:");

                // 3. Удаление элемента
                RemoveFromStack(stack);

                // 4. Поиск элемента
                SearchInStack(stack, new Rectangle1(3, 7));

                // 5. Глубокое клонирование
                Stack clonedStack = DeepCloneStack(stack);
                DisplayStackContents(clonedStack, "Клонированный стек:");

                // 6. Сортировка и возврат в стек
                stack = SortStack(stack);
                DisplayStackContents(stack, "Отсортированный стек:");

                // 7. Поиск в отсортированном стеке
                SearchInStack(stack, new Rectangle1(8, 4));

                // 8. Выполнение запросов
                ExecuteStackQueries(stack);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при работе со стеком: {ex.Message}");
            }
        }

        static Stack CreateAndFillStack()
        {
            Stack stack = new Stack();
            stack.Push(new Rectangle1(5.0, 10.0));
            stack.Push(new Rectangle1(3, 7));
            stack.Push(new Rectangle1(8, 4));
            return stack;
        }

        static void DisplayStackContents(Stack stack, string title)
        {
            Console.WriteLine($"\n{title}");
            foreach (var item in stack)
            {
                if (item is IShowable showable)
                    showable.Show();
                else
                    Console.WriteLine(item.ToString());
            }
        }

        static void RemoveFromStack(Stack stack)
        {
            if (stack.Count == 0)
            {
                Console.WriteLine("\nСтек пуст, удаление невозможно");
                return;
            }

            Console.WriteLine("\nУдаляем верхний элемент:");
            var removed = stack.Pop();
            Console.WriteLine($"Удален: {removed}");
        }

        static void SearchInStack(Stack stack, object itemToFind)
        {
            Console.WriteLine($"\nПоиск элемента {itemToFind}:");
            bool contains = false;
            foreach (var item in stack)
            {
                if (item.Equals(itemToFind))
                {
                    contains = true;
                    break;
                }
            }
            Console.WriteLine($"Содержит элемент: {contains}");
        }

        static Stack DeepCloneStack(Stack original)
        {
            Stack cloned = new Stack();
            foreach (var item in original)
            {
                if (item is ICloneable cloneable)
                    cloned.Push(cloneable.Clone());
                else
                    cloned.Push(item);
            }
            return cloned;
        }

        static Stack SortStack(Stack original)
        {
            ArrayList tempList = new ArrayList(original);
            tempList.Sort();

            Stack sortedStack = new Stack();
            foreach (var item in tempList)
                sortedStack.Push(item);

            return sortedStack;
        }

        static void ExecuteStackQueries(Stack stack)
        {
            // Запрос 1: Количество прямоугольников
            int rectangleCount = CountRectangles(stack);
            Console.WriteLine($"\nКоличество прямоугольников в стеке: {rectangleCount}");

            // Запрос 2: Печать всех прямоугольников
            PrintAllRectangles(stack);

            // Запрос 3: Поиск прямоугольника с максимальной площадью
            FindRectangleWithMaxArea(stack);
        }

        static int CountRectangles(Stack stack)
        {
            int count = 0;
            foreach (var item in stack)
                if (item is Rectangle1) count++;
            return count;
        }

        static void PrintAllRectangles(Stack stack)
        {
            Console.WriteLine("\nВсе прямоугольники в стеке:");
            foreach (var item in stack)
                if (item is Rectangle1 rect)
                    Console.WriteLine(rect);
        }

        static void FindRectangleWithMaxArea(Stack stack)
        {
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

            Console.WriteLine($"\nПрямоугольник с максимальной площадью: {maxAreaRect?.ToString() ?? "не найден"}");
        }

        static void DemonstrateDictionaryOperations()
        {
            try
            {
                // 1. Создание и заполнение словаря
                var dictionary = CreateAndFillDictionary();

                // 2. Вывод элементов словаря
                DisplayDictionaryContents(dictionary, "Исходный словарь:");

                // 3. Удаление элемента по значению
                RemoveFromDictionaryByValue(dictionary, new Circle1(5));

                // 4. Поиск элемента по значению
                SearchInDictionaryByValue(dictionary, new Parallelepiped1(10));

                // 5. Глубокое клонирование
                var clonedDictionary = DeepCloneDictionary(dictionary);
                DisplayDictionaryContents(clonedDictionary, "Клонированный словарь:");

                // 6. Добавление элемента
                AddToDictionary(dictionary, "NewRectangle", new Rectangle1(2, 3));

                // 7. Выполнение запросов
                ExecuteDictionaryQueries(dictionary);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при работе со словарем: {ex.Message}");
            }
        }

        static SortedDictionary<string, IGeometricFigure1> CreateAndFillDictionary()
        {
            var dictionary = new SortedDictionary<string, IGeometricFigure1>();
            dictionary.Add("Circle1", new Circle1(5));
            dictionary.Add("Parallelepiped1", new Parallelepiped1(10));
            dictionary.Add("Rectangle1", new Rectangle1(4, 6));
            return dictionary;
        }

        static void DisplayDictionaryContents(SortedDictionary<string, IGeometricFigure1> dictionary, string title)
        {
            Console.WriteLine($"\n{title}");
            foreach (var kvp in dictionary)
            {
                Console.WriteLine($"{kvp.Key}:");
                kvp.Value.Show();
            }
        }

        static void RemoveFromDictionaryByValue(SortedDictionary<string, IGeometricFigure1> dictionary, IGeometricFigure1 valueToRemove)
        {
            string keyToRemove = null;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value.Equals(valueToRemove))
                {
                    keyToRemove = kvp.Key;
                    break;
                }
            }

            if (keyToRemove != null)
            {
                dictionary.Remove(keyToRemove);
                Console.WriteLine($"\nУдален элемент со значением: {valueToRemove}");
            }
            else
            {
                Console.WriteLine($"\nЭлемент со значением {valueToRemove} не найден");
            }
        }

        static void SearchInDictionaryByValue(SortedDictionary<string, IGeometricFigure1> dictionary, IGeometricFigure1 valueToFind)
        {
            bool found = false;
            foreach (var kvp in dictionary)
            {
                if (kvp.Value.Equals(valueToFind))
                {
                    found = true;
                    break;
                }
            }
            Console.WriteLine($"\nСловарь {(found ? "содержит" : "не содержит")} элемент: {valueToFind}");
        }

        static SortedDictionary<string, IGeometricFigure1> DeepCloneDictionary(SortedDictionary<string, IGeometricFigure1> original)
        {
            var cloned = new SortedDictionary<string, IGeometricFigure1>();
            foreach (var kvp in original)
            {
                if (kvp.Value is ICloneable cloneable)
                    cloned.Add(kvp.Key, (IGeometricFigure1)cloneable.Clone());
                else
                    cloned.Add(kvp.Key, kvp.Value);
            }
            return cloned;
        }

        static void AddToDictionary(SortedDictionary<string, IGeometricFigure1> dictionary, string key, IGeometricFigure1 value)
        {
            try
            {
                dictionary.Add(key, value);
                Console.WriteLine($"\nДобавлен новый элемент: {key}");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"\nЭлемент с ключом {key} уже существует");
            }
        }

        static void ExecuteDictionaryQueries(SortedDictionary<string, IGeometricFigure1> dictionary)
        {
            // Запрос 1: Количество прямоугольников
            int rectangleCount = CountRectanglesInDictionary(dictionary);
            Console.WriteLine($"\nКоличество прямоугольников в словаре: {rectangleCount}");

            // Запрос 2: Печать всех прямоугольников
            PrintAllRectanglesFromDictionary(dictionary);

            // Запрос 3: Поиск фигуры с минимальной площадью
            FindFigureWithMinArea(dictionary);
        }

        static int CountRectanglesInDictionary(SortedDictionary<string, IGeometricFigure1> dictionary)
        {
            int count = 0;
            foreach (var kvp in dictionary)
                if (kvp.Value is Rectangle1) count++;
            return count;
        }

        static void PrintAllRectanglesFromDictionary(SortedDictionary<string, IGeometricFigure1> dictionary)
        {
            Console.WriteLine("\nВсе прямоугольники в словаре:");
            foreach (var kvp in dictionary)
                if (kvp.Value is Rectangle1 rect)
                    Console.WriteLine($"{kvp.Key}: {rect}");
        }

        static void FindFigureWithMinArea(SortedDictionary<string, IGeometricFigure1> dictionary)
        {
            IGeometricFigure1 minAreaFigure = null;
            double minArea = double.MaxValue;

            foreach (var kvp in dictionary)
            {
                if (kvp.Value.Area() < minArea)
                {
                    minArea = kvp.Value.Area();
                    minAreaFigure = kvp.Value;
                }
            }

            Console.WriteLine($"\nФигура с минимальной площадью: {minAreaFigure?.ToString() ?? "не найдена"}");
        }

        static void RunPerformanceTest()
        {
            try
            {
                Console.WriteLine("\nЗапуск теста производительности...");
                TestCollections test = new TestCollections(1000);
                test.MeasureSearchTime();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении теста производительности: {ex.Message}");
            }
        }
    }
}