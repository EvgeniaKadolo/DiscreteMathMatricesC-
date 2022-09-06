using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4_4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите матрицу инцидентности: ");

                ConsoleKeyInfo key = default(ConsoleKeyInfo);
                List<int[]> B = new List<int[]>();
                int length = 0;
                int count = 0;

                while (key.Key != ConsoleKey.Escape)
                {
                    int[] input = Console.ReadLine()
                                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(Int32.Parse).ToArray();

                    if (count == 0) length = input.Length;
                    if (input.Length != length) throw new Exception();

                    for (int i = 0; i < length; i++)
                    {
                        if (input[i] != 0 && input[i] != 1 && input[i] != -1 && input[i] != 2) throw new Exception();
                    }

                    B.Add(input);
                    count++;
                    key = Console.ReadKey(true);
                }

                bool flagO = false;
                bool flagN = false;

                bool flag2;
                bool flag1;
                bool flag_1;
                int count2;
                int count1;
                int count_1;
                for (int i = 0; i < length; i++)
                {
                    flag2 = false;
                    flag1 = false;
                    flag_1 = false;
                    count2 = 0;
                    count1 = 0;
                    count_1 = 0;
                    for (int j = 0; j < B.Count; j++)
                    {
                        if (B[j][i] == 2)
                        {
                            flag2 = true;
                            count2++;
                        }
                        else if (B[j][i] == -1)
                        {
                            flag_1 = true;
                            count_1++;
                        }
                        else if (B[j][i] == 1)
                        {
                            flag1 = true;
                            count1++;
                        }
                    }
                    if (!flag2 && !flag_1 && !flag1 || !flag2 && !flag_1 && flag1 ||
                        !flag2 && flag_1 && !flag1 || flag2 && flag_1 && flag1 ||
                        flag2 && flag_1 && !flag1 || flag2 && !flag_1 && flag1)
                    {
                        flagO = true;
                        break;
                    }
                    else if (count2 > 1 || count1 > 1 || count_1 > 1)
                    {
                        flagO = true;
                        break;
                    }
                }

                for (int i = 0; i < length; i++)
                {
                    flag2 = false;
                    flag1 = false;
                    count2 = 0;
                    count1 = 0;
                    for (int j = 0; j < B.Count; j++)
                    {
                        if (B[j][i] == 2)
                        {
                            flag2 = true;
                            count2++;
                        }
                        else if (B[j][i] == 1)
                        {
                            flag1 = true;
                            count1++;
                        }
                        else if (B[j][i] == -1)
                        {
                            flagN = true;
                            break;
                        }
                    }
                    if (flagN) break;
                    if (!flag2 && !flag1 || flag2 && flag1)
                    {
                        flagN = true;
                        break;
                    }
                    else if (count2 == 1 && count1 > 0 || count2 > 0 && count1 == 2 || count1 > 2 || count2 > 1)
                    {
                        flagN = true;
                        break;
                    }
                }

                if (flagO && flagN) throw new Exception();

                int[,] A = new int[B.Count, B.Count];
                int start = 0;
                int end = 0;
                bool flagStart;
                bool flagEnd;
                bool flag = false;

                if (!flagO)
                {
                    for (int i = 0; i < length; i++)
                    {
                        flagStart = false;
                        flagEnd = false;
                        for (int j = 0; j < B.Count; j++)
                        {
                            if (B[j][i] == 2)
                            {
                                A[j, j] = 1;
                            }
                            else if (B[j][i] == -1)
                            {
                                start = j;
                                flagStart = true;
                            }
                            else if (B[j][i] == 1)
                            {
                                end = j;
                                flagEnd = true;
                            }
                        }
                        if (flagStart && flagEnd)
                        {
                            A[start, end] = 1;
                        }
                        else if (!flagStart && flagEnd || flagStart && !flagEnd) throw new Exception();
                    }
                }
                else if (!flagN)
                {
                    for (int i = 0; i < length; i++)
                    {
                        flagStart = false;
                        flagEnd = false;
                        flag = false;
                        for (int j = 0; j < B.Count; j++)
                        {
                            if (B[j][i] == 2)
                            {
                                A[j, j] = 1;
                                break;
                            }
                            else if (B[j][i] == 1 && !flag)
                            {
                                start = j;
                                flagStart = true;
                                flag = true;
                            }
                            else if (B[j][i] == 1 && flag)
                            {
                                end = j;
                                flagEnd = true;
                            }
                        }
                        if (flagStart && flagEnd)
                        {
                            A[start, end] = 1;
                            A[end, start] = 1;
                        }
                    }
                }

                int[,] P = new int[length, length];
                P = Copy(A);
                int[,] A1 = new int[length, length];
                int[,] A2 = new int[length, length];
                for (int i = 1; i < length; i++)
                {
                    if (i == 1) A1 = Multiplication(A, A);
                    else if (i % 2 != 0) A1 = Multiplication(A, A2);
                    else if (i % 2 == 0) A2 = Multiplication(A, A1);
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
            catch
            {
                Console.WriteLine("Ошибка");
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
