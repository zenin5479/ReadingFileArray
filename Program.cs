using System;
using System.IO;
using System.Text;

namespace ReadingFileArray
{
   internal class Program
   {
      static void Main()
      {
         // Переводит (,) в (.)
         //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

         //string filePath = AppContext.BaseDirectory + "a.txt";
         string filePath = AppContext.BaseDirectory + "b.txt";
         Console.BackgroundColor = ConsoleColor.DarkBlue;
         // Чтение файла за одну операцию
         string[] allLines = File.ReadAllLines(filePath);
         if (allLines == null)
         {
            Console.WriteLine("Ошибка при открытии файла для чтения");
         }
         else
         {
            Console.WriteLine("Исходный массив строк");
            string[] arrayLines = new string[allLines.Length];
            for (int i = 0; i < allLines.Length; i++)
            {
               arrayLines[i] = allLines[i];
               Console.WriteLine(arrayLines[i]);
            }
            // Разделение строки на подстроки по пробелу для определения количества столбцов в строке
            Console.ResetColor();
            int[] sizeArray = new int[arrayLines.Length];
            char symbolSpace = ' ';
            int countRow = 0;
            int countSymbol = 0;
            int countСolumn = 0;
            while (countRow < arrayLines.Length)
            {
               string line = arrayLines[countRow];
               while (countSymbol < line.Length)
               {
                  if (symbolSpace == line[countSymbol])
                  {
                     countСolumn++;
                  }

                  if (countSymbol == line.Length - 1)
                  {
                     countСolumn++;
                  }

                  countSymbol++;
               }

               sizeArray[countRow] = countСolumn;
               //Console.WriteLine("В строке {0} количество столбцов {1}", countRow, countСolumn);
               countСolumn = 0;
               countRow++;
               countSymbol = 0;
            }

            // Проверка количества столбцов для определения размерности двухмерного массива (прямоугольный/ступенчатый)
            int min = sizeArray[0];
            int max = sizeArray[0];
            int k = 0;
            while (k < sizeArray.Length)
            {
               if (sizeArray[k] < min)
               {
                  min = sizeArray[k];
               }

               if (sizeArray[k] > max)
               {
                  max = sizeArray[k];
               }

               k++;
            }

            Console.ResetColor();
            Console.WriteLine("Количество строк {0}", arrayLines.Length);
            Console.WriteLine("Минимальное количество столбцов: {0}", min);
            Console.WriteLine("Максимальное количество столбцов: {0}", max);
            if (min == max)
            {
               Console.ForegroundColor = ConsoleColor.Green;
               Console.WriteLine("Массив имеет одинаковое количество столбцов - прямоугольный");
            }
            else
            {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine("Массив имеет разное количество столбцов - ступенчатый");
            }

            Console.ResetColor();
            Console.WriteLine();

            // Разделение строки на подстроки по пробелу и конвертация подстрок в double
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Двухмерный числовой массив");
            StringBuilder stringModified = new StringBuilder();
            double[,] arrayDouble = new double[arrayLines.Length, max];
            char spaceCharacter = ' ';
            int row = 0;
            int column = 0;
            int countCharacter = 0;
            while (row < arrayDouble.GetLength(0))
            {
               string line = arrayLines[row];
               while (column < sizeArray[row])
               {
                  while (countCharacter < line.Length)
                  {
                     if (spaceCharacter != line[countCharacter])
                     {
                        stringModified.Append(line[countCharacter]);
                     }
                     else
                     {
                        string subLine = stringModified.ToString();
                        arrayDouble[row, column] = Convert.ToDouble(subLine);
                        Console.Write(arrayDouble[row, column] + " ");
                        stringModified.Clear();
                        column++;
                     }

                     if (countCharacter == line.Length - 1)
                     {
                        string subLine = stringModified.ToString();
                        arrayDouble[row, column] = Convert.ToDouble(subLine);
                        Console.Write(arrayDouble[row, column] + " ");
                        stringModified.Clear();
                        column++;
                     }

                     countCharacter++;
                  }

                  countCharacter = 0;
               }

               Console.WriteLine();
               column = 0;
               row++;
            }

            Console.ResetColor();
            Console.WriteLine();
            // Проверка всех имеющихся элементов в строке
            int lines = 2;
            int range = arrayDouble.GetLength(1);
            int iterator = 0;
            while (iterator < range)
            {
               Console.WriteLine("Элемент {0} строки по индексу {1} равен: {2}  ", lines, iterator, arrayDouble[lines, iterator] + " ");
               iterator++;
            }
         }

         Console.ReadKey();
      }
   }
}