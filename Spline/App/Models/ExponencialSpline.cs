using Spline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.App.Models
{
    class ExponencialSpline
    {
        public static double[] GetCoeficients(AppMath.BaseFunc F, double x0, double x1)
        {
            double[] a = new double[4];
            double x0_x1 = (x0 - x1);
            double LogF0_F1 = Math.Log(F.Val(x0) / F.Val(x1));


            double d = a[3] = (F.Diff(x1) / F.Val(x1) + F.Diff(x0) / F.Val(x0) + 2 * LogF0_F1 / (x1 - x0)) / Math.Pow(-1 * x0_x1, 2);

            double c = a[2] = ((2 * x0 * x0 - x0 * x1 - x1 * x1) * a[3] - F.Diff(x0) / F.Val(x0) - LogF0_F1 / (-1 * x0_x1)) / (-1 * x0_x1);

            double b = a[1] = 1 / (x1 - x0) * (Math.Log(F.Val(x1) / F.Val(x0)) - a[3] * (Math.Pow(x1, 3) - Math.Pow(x0, 3)) - c * (Math.Pow(x1, 2) - Math.Pow(x0, 2)));
            //A
            a[0] = F.Val(x0) * Math.Exp(-1 * (d * Math.Pow(x0, 3) + c * Math.Pow(x0, 2) + b * x0));

            return a;
        }

        public static double AproximFunc(double x, double[] coef)
        {
            return (coef[0] * Math.Exp(coef[1] * x + coef[2] * Math.Pow(x, 2) + coef[3] * Math.Pow(x, 3)));
        }
    }
}
