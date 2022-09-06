using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
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


                int[,] B = new int[length, count1];
                int count_ = 0;
                int countSymm = 0;
                int countP = 0;
                bool symmetry = false;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (A[i, j] == A[j, i]) countSymm++;
                        if (A[i, j] == A[j, i] && A[i, j] == 1 && i == j) countP++;
                    }
                }
                if (countSymm == length * length) symmetry = true;


                if (!symmetry)
                {
                    B = new int[length, count1];
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            if (A[i, j] == 1)
                            {
                                if (i == j) B[i, count_] = 2;
                                else
                                {
                                    B[i, count_] = -1;
                                    B[j, count_] = 1;
                                }
                                count_++;
                            }
                            if (count_ == count1) break;
                        }
                        if (count_ == count1) break;
                    }
                }
                else if (symmetry)
                {
                    B = new int[length, (count1-countP)/2+countP];
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = i; j < length; j++)
                        {
                            if (A[i, j] == 1)
                            {
                                if (i == j) B[i, count_] = 2;
                                else
                                {
                                    B[i, count_] = 1;
                                    B[j, count_] = 1;
                                }
                                count_++;
                            }
                            if (count_ == count1) break;
                        }
                        if (count_ == count1) break;
                    }
                }
                

                Console.WriteLine("\nМатрица инцидентности:");
                for (int i = 0; i < B.GetLength(0); i++)
                {
                    for (int j = 0; j < B.GetLength(1); j++)
                    {
                        Console.Write(B[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
            }
            catch
            {
                Console.WriteLine("Ошибка");
            }
        }
    }
}
