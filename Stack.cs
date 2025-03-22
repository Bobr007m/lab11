using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace lab11
{
    // класс-коллекция
    internal class Stack<T> : IIni, IComparable<Geometryfigure1>, ICloneable, IShowable
    {

        T[] array;
        int current; // текущее количество
        static int delta; // приращение

        // только для чтения
        public int GetLength => array.Length;
        public int Current => current;
        public static int Delta => delta;

        // конструкторы
        public Stack ()
        {
            array = new T[0];
            current = 0;
        }
        public Stack (T[] array)
        {
            this.array = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                this.array[i] = array[i];
            }
            current = GetLength;
        }
        public Stack( int length)
        {
            array = new T [length];
            for (int i = 0;i < array.Length;i++)
            {
                array[i] = default (T); // сгенерировать и заполнить Random
            }
            current = 0;
        }
        
        // Печать
        public void PrintArray ()
        {
            for (int i = 0; i< current; i++)
            {
                Console.WriteLine(array[i]);
            }
        }
        // Индексатор
        public T this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }      
}
}
