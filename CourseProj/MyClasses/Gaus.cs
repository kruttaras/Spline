using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseProj.MyClasses
{
    class Gaus
    {
        public double[] GausMethod(int size, double[,] Matrix, double[] Vec, out int counter)
        {
            double temp,max;
            int MaxPosition=0;
            double[] Solution = new double[size];
            for (int i = 0; i < size; i++)
                Solution[i] = 0;   
                for (int k = 0; k < size - 1; k++)
                {
                    double l;
                    max = 0;
                    for (int pos = k; pos < size; pos++)
                    {
                        if (Math.Abs(Matrix[pos, k]) > max)
                        {
                            max = Matrix[pos, k];
                            MaxPosition = pos;
                        }

                    }
                    temp = Matrix[k, k];
                    Matrix[k, k] = Matrix[MaxPosition, k];
                    Matrix[MaxPosition, k] = temp;
                    temp = Vec[k];
                    Vec[k] = Vec[MaxPosition];
                    Vec[MaxPosition] = temp;
                    for (int j = k + 1; j < size; j++)
                    {
                        temp = Matrix[k, j];
                        Matrix[k, j] = Matrix[MaxPosition, j];
                        Matrix[MaxPosition, j] = temp;
                    }
                    for (int i = k + 1; i < size; i++)
                    {

                        l = Matrix[i, k] / Matrix[k, k];
                        for (int j = 1; j < size; j++)
                            Matrix[i, j] = Matrix[i, j] - l * Matrix[k, j];
                        Vec[i] = Vec[i] - l * Vec[k];

                    }
                }

            //Solution:
                for (int count = 1; count < size - 1; count++)
                    for (int i = count; i < size; i++)
                        for (int j = 0; j < count; j++)
                            Matrix[i, j] = 0;

                Solution[size - 1] = Vec[size - 1] / Matrix[size - 1, size - 1];    
            for (int i = size-2; i >=0; i--)
                {
                    Solution[i]=Vec[i];
                    for (int j = 0; j < size; j++)
                    {
                        if (j == i)
                            continue;
                        Solution[i] -= Matrix[i, j] * Solution[j];
                    }
                    Solution[i]=Solution[i]/Matrix[i,i];
                }
               counter = 0;
            return Solution;
        }

    }
}
