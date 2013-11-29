using System;
using Spline.Models;

namespace Spline
{
    class Logarifmical5Spline : ApproximatingFunction
    {
                public Logarifmical5Spline()
        {
            Text = "Логарифмічна - 5";
        }

        public override double[] GetCoeficients(AppMath.BaseFunc func, double x0, double x2)
        {
            var a = new double[] {0, 0, 0, 0, 0};

            double j1, j2, j3, j4, m1, m2, m3, k1, k2, x1;

             x1 = (x2 + x0) / 2;

            var f = new[] { func.Val(x0), func.Val(x1), func.Val(x2) };
            var df = new[] { func.Diff(x0), func.Diff(x1), func.Diff(x2) };

            j1 = (pow(x2,2) - pow(x1,2)) / (x1 - x2);
            j2 = (pow(x2,3) - pow(x1,3)) / (x1 - x2);
            j3 = (pow(x2,4) - pow(x1,4)) / (x1 - x2);
            j4 = (exp(f[1]) - exp(f[2])) / (x1 - x2);

            m1 = (exp(f[2]) - exp(f[0]) + j4 * (x0 - x2)) / (pow(x2, 2) - pow(x0, 2) + j1 * (x2 - x0));
            m2 = (pow(x0, 3) - pow(x2, 3) + j2 * (x0 - x2)) / (pow(x2, 2) - pow(x0, 2) + j1 * (x2 - x0));
            m3 = (pow(x0, 4) - pow(x2, 4)  + j3 * (x0 - x2)) / (pow(x2, 2) - pow(x0, 2) + j1 * (x2 - x0));

            k1 = (exp(f[0]) * df[0] - j4 - m1 * j1 - 2* x0 * m1) / (j2 + 3* pow(x0,2) + j1 * m2 + 2* m2 * x0);
            k2 = (j3 + 4 * pow(x0,3) + j1 * m3 + 2* m3 * x0) / (j2 + 3 * pow(x0,2) + j1 * m2 + 2* m2 * x0);
            
            a[4] = (exp(f[2]) * df[2] - j4 - m1 * j1 - 2* x2 * m1 - k1 * j2 - 3* pow(x2,2) * k1 - k1 * j1 * m2 - 2* m2 * x2 * k1) / (j3 + 4* pow(x2,3) + j1 * m3 + 2* m3 * x2 - k2 * j2 - 3 * pow(x2,2) * k2 - k2* j1 * m2 - 2* m2 * x2 * k2);
            a[3] = k1 - k2 * a[4];
            a[2] = m1 + a[3] * m2 + a[4] * m3;
            a[1] = j4 + a[2] * j1 + a[3] * j2 + a[4] * j3;
            a[0] = exp(f[0]) - a[1] * x0 - a[2] * pow(x0, 2) - a[3] * pow(x0, 3) - a[4] * pow(x0, 4);
            return a;
        }

        public override double GetAproximatingFunction(double x, double[] a)
        {
            return Math.Log(a[0] + a[1] * x + a[2] * Math.Pow(x, 2) + a[3] * Math.Pow(x, 3)+ a[4] * Math.Pow(x, 4));
        }
    }
}
