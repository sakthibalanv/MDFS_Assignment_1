using System;

namespace GaussJordan
{
    class Program
    {
        static Random rand = new Random();
        static int rowLength = 3;
        static int colLength = 2 * rowLength;
        static double[,] matrix = new double[rowLength, colLength];
        static bool displayMatrix = false;
        static void Main(string[] args)
        {
            Console.WriteLine("Gauss Jordan Method to find the Inverse of the Matrix");

            // Get User Preferences
            Console.Write("Enter the Matrix Size = ");
            string size_s = Console.ReadLine();

            int size_i = 0;
            int.TryParse(size_s, out size_i);
            if (size_i > 0)
            {
                rowLength = size_i;
                colLength = 2 * rowLength;
                matrix = new double[rowLength, colLength];
            }

            Console.Write("Do you want to display Result ? y/n : ");
            string printPref = Console.ReadLine();
            if (printPref.ToLower() == "y" || printPref.ToLower() == "yes")
            {
                displayMatrix = true;
            }

            // 2x2 eaxmple
            //matrix[0, 0] = 1;
            //matrix[0, 1] = 3;
            //matrix[1, 0] = 2;
            //matrix[1, 1] = 5;
            // A inverse is
            // -5  3
            //  2 -1

            // 3x3 example
            //matrix[0, 0] = 2;
            //matrix[0, 1] = 3;
            //matrix[0, 2] = 0;
            //matrix[1, 0] = 1;
            //matrix[1, 1] = -2;
            //matrix[1, 2] = -1;
            //matrix[2, 0] = 2;
            //matrix[2, 1] = 0;
            //matrix[2, 2] = -1;
            // A inverse is 
            //  2  3 -3
            // -1 -2  2
            //  4  6 -7

            for (int row = 0; row < rowLength; row++)
            {
                for (int col = 0; col < colLength; col++)
                {
                    if (col < rowLength)
                    {
                        matrix[row, col] = Math.Round(rand.NextDouble(), 4);
                    }
                    else
                    {
                        if (row + rowLength == col)
                        {
                            matrix[row, col] = 1;
                        }
                    }
                }
            }
            print_matrix("Augumented Matrix - A");

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    if (j != i)
                    {
                        double val  = matrix[j,i] / matrix[i,i];
                        for (int k = 0; k < colLength; k++)
                        {

                            matrix[j,k] -= matrix[i,k] * val;
                        }
                    }
                }
            }

            for (int i = 0; i < rowLength; i++)
            {
                
                double val = matrix[i,i];
                for (int j = 0; j < colLength; j++)
                {

                    matrix[i,j] = Math.Round(matrix[i,j] / val, 4);
                }
            }

            //print_matrix("Inverse Matrix");

            if (displayMatrix)
            {
                Console.WriteLine("Inverse Matrix");

                for (int i = 0; i < rowLength; i++)
                {
                    Console.Write("[");
                    for (int j = rowLength; j < colLength; j++)
                    {
                        Console.Write(matrix[i, j].ToString() + " ,");
                    }
                    Console.WriteLine("]");
                }
            }
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
