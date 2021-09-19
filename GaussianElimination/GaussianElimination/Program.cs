using System;
using System.Diagnostics;
using System.Linq;

namespace GaussianElimination
{
    class Program
    {
        
        static Random rand = new Random();
        static int rowLength = 3000;
        static int colLength = rowLength + 1;
        static double[,] matrix = new double[rowLength, colLength];
        static bool displayMatrix = false;
        static void Main(string[] args)
        {
            // Get User Preferences
            Console.WriteLine("Gauss Elimination Method");
            Console.Write("Enter the Matrix Size = ");
            string size_s = Console.ReadLine();

            Console.Write("Do you want to display Result ? y/n : ");
            string printPref = Console.ReadLine();
            if (printPref.ToLower() == "y" || printPref.ToLower() == "yes")
            {
                displayMatrix = true;
            }


            int size_i = 0;
            int.TryParse(size_s, out size_i);
            rowLength = size_i;
            if (rowLength <= 1)
            {
                rowLength = 5;
            }
            colLength = rowLength + 1;
            matrix = new double[rowLength, colLength];

            Console.WriteLine("Matrix of Size " + rowLength.ToString());
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int row = 0; row < rowLength; row++)
            {
                for (int col = 0; col < colLength; col++)
                {
                    matrix[row, col] = Math.Round(rand.NextDouble(), 4);
                }                
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTimeInit = String.Format("{0:00}:{1:00}", ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime Init " + elapsedTimeInit);

            if (displayMatrix)
            {
                print_matrix("Random Augumented Matrix");
            }

            stopWatch = new Stopwatch();
            stopWatch.Start();
            // Partial Pivot Table           

            for (int currentRow = 0; currentRow < rowLength; currentRow++)
            {
                int currentCol = currentRow, maxRowIndex = currentRow;
                double maxVal = 0;
                for (int row = currentRow; row < rowLength; row++)
                {
                    if (matrix[row, currentCol] > maxVal)
                    {
                        maxRowIndex = row;
                        maxVal = matrix[row, currentCol];
                    }
                }
               // Console.WriteLine("Max Row Value = " + maxVal.ToString() + " Max Row Index = " + maxRowIndex.ToString());

                // Swap Max Value Row to Current Row
                double temp = 0;
                for (int col = 0; col < colLength; col++)
                {
                    temp = matrix[currentRow, col];
                    matrix[currentRow, col] = matrix[maxRowIndex, col];
                    matrix[maxRowIndex, col] = temp;
                }
                //print_matrix("Pivoted table");

                // REF
                for (int row = currentRow + 1; row < rowLength; row++)
                {
                    double mj = Math.Round(matrix[row, currentCol] / matrix[currentRow, currentCol], 4);
                    for (int col = currentCol; col < colLength; col++)
                    {
                        if (col == currentCol)
                        {
                            matrix[row, col] = 0;
                        }
                        else
                        {
                            matrix[row, col] = Math.Round(matrix[row, col] - (mj * matrix[currentRow, col]), 4);
                        }
                    }

                    //print_matrix("REF NO = " + row.ToString());
                }
            }

            
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            elapsedTimeInit = String.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime FE " + elapsedTimeInit);

            stopWatch = new Stopwatch();
            stopWatch.Start();
            // Backward Substution
            //Console.WriteLine("Backward Substution");
            double[] vectors = new double[rowLength];
            for (int z = 0; z < rowLength; z++)
            {
                vectors[z] = 1;
            }

            for (int row = rowLength - 1; row >= 0; row--)
            {
                double sum = 0;
                int col = colLength - 2;
                for (; col > row ; col--)
                {
                    sum += matrix[row, col] * vectors[col];
                }                

                vectors[row] = (matrix[row, colLength - 1] - sum) / matrix[row,col];
                
            }

            ts = stopWatch.Elapsed;
            elapsedTimeInit = String.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime BS " + elapsedTimeInit);

            if (displayMatrix)
            {
                for (int i = 0; i < rowLength; i++)
                {
                    Console.WriteLine(Math.Round(vectors[i], 4));
                }
            }

            Console.ReadLine();
        }

        static void print_matrix(string message)
        {
            Console.WriteLine(message);
            for (int row = 0; row < rowLength; row++)
            {
                Console.Write('[');
                for (int col = 0; col < colLength; col++)
                {
                    Console.Write(matrix[row, col].ToString() + ",");
                }
                Console.WriteLine(']');
            }
        }
    }
}
