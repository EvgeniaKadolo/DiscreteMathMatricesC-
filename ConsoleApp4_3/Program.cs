using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите матрицу смежности: ");
            int count = 0;
            int length = 0;
            int[,] A = new int[length, length];
            int count1 = 0;

            do
            {
                var input = Console.ReadLine()
                            .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(Int32.Parse);
                if (count == 0)
                {
                    length = input.ToArray().Length;
                    A = new int[length, length];
                }
                if (input.ToArray().Length != length) throw new Exception();

                for (int i = 0; i < length; i++)
                {
                    if (input.ToArray()[i] != 0 && input.ToArray()[i] != 1) throw new Exception();
                    if (input.ToArray()[i] == 1) count1++;
                    A[count, i] = input.ToArray()[i];
                }

                count++;
            }
            while (count != length);


            int[,] P = new int[length, length];
            P = Copy(A);
            int[,] A1 = new int[length, length];
            int[,] A2 = new int[length, length];
            for (int i = 1; i < length; i++)
            {
                if (i == 1) A1 = Multiplication(A, A);
                else if (i % 2 != 0) A1 = Multiplication(A, A2);
                else if(i % 2 == 0) A2 = Multiplication(A, A1);
                for (int j = 0; j < P.GetLength(0); j++)
                {
                    for (int k = 0; k < P.GetLength(1); k++)
                    {
                        if (i % 2 != 0 && A1[j, k] == 1 && P[j, k] == 0)
                        {
                            P[j, k] = 1;
                        }
                        else if (i % 2 == 0 && A2[j, k] == 1 && P[j, k] == 0)
                        {
                            P[j, k] = 1;
                        }
                    }
                } 
            }

            Console.WriteLine("\nМатрица достижимости:");
            for (int i = 0; i < P.GetLength(0); i++)
            {
                for (int j = 0; j < P.GetLength(1); j++)
                {
                    Console.Write(P[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static int[,] Multiplication(int[,] A, int[,] A2)
        {
            int[,] A3 = new int[A.GetLength(1), A.GetLength(1)];
            int count_;
            for (int i = 0; i < A.GetLength(1); i++)
            {
                count_ = 0;
                for (int j = 0; j < A.GetLength(1) * A.GetLength(1); j++)
                {
                    if (A2[i, j % A.GetLength(1)] == 1 && A[j % A.GetLength(1), count_] == 1)
                    {
                        A3[i, count_] = 1;
                    }
                    if (j % A.GetLength(1) == 2) count_++;
                    if (count_ == A.GetLength(1)) count_ = 0;
                }
            }

            return A3;
        }

        static T[,] Copy<T>(T[,] array)
        {
            T[,] newArray = new T[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    newArray[i, j] = array[i, j];
            return newArray;
        }
    }
}
