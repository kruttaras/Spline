using System;
using Spline.Models;

namespace Spline
{
    class LogarifmicalSplineWithFourParametrs : ApproximatingFunction
    {
        public LogarifmicalSplineWithFourParametrs()
        {
            Text = "Логарифмічна - 4";
        }

        public override double[] GetCoeficients(AppMath.BaseFunc func, double x0, double x1)
        {
            var a = new double[4];
            double m1, m2, m3, m4, j0, j1, j2;
            double f0, df0, f1, df1;

            f0 = func.Val(x0);
            f1 = func.Val(x1);
            df0 = func.Diff(x0);
            df1 = func.Diff(x1);

            j0 = (Math.Exp(f1) - Math.Exp(f0)) / (x1 - x0);
            j1 = x1 + x0;
            j2 = x1 * x1 + x0 * x1 + x1 * x1;

            m1 = (df0 * Math.Exp(f0) - j0) / (j1 - 2 * x0);
            m2 = (3 * x0 * x0 + j2) / (j1 - 2 * x0);
            m3 = (df1 * Math.Exp(f1) - j0) / (j1 - 2 * x1);
            m4 = (3 * x1 * x1 + j2) / (j1 - 2 * x1);

            a[3] = (m3 - m1) / (m4 - m2);
            a[2] = m1 - a[3] * m2;
            a[1] = j0 + j1 * a[2] + j2 * a[3];
            a[0] = Math.Exp(f0) - a[1] * x0 - a[2] * x0 * x0 - a[3] * Math.Pow(x0, 3);
            return a;
        }

        public override double GetAproximating_function(double x, double[] a)
        {
            return Math.Log(a[0] + a[1] * x + a[2] * Math.Pow(x, 2) + a[3] * Math.Pow(x, 3));
        }
    }
}