using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spline.Models;

namespace Spline
{
    class Logarifmical5Spline : AproximatingFunction
    {
        public Logarifmical5Spline()
        {
            Text = "Логарифмічна 5";
        }

        public override double[] GetCoeficients(AppMath.BaseFunc func, double x0, double x1)
        {
            double[] a = new double[5];

            double x2 = x1;
            x1 = (x1 + x0) / 2;
            double x1_x2=x1 - x2;

            double j1, j2, j3, j4;

            j1 = (pow(x2, 2) - pow(x1, 2))/x1_x2;
            j2 = (pow(x2, 3) - pow(x1, 3)) / x1_x2;
            j3 =  (pow(x2, 4) - pow(x1, 4)) / x1_x2;

            return null;
        }

        public override double GetAproximatingFunction(double x, double[] a)
        {
            throw new NotImplementedException();
        }
    }
}
