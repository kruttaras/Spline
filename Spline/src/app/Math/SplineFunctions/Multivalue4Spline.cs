using System;
using Spline.Models;

namespace Spline
{
    class Multivalue4Spline : ApproximatingFunction
    {
        public Multivalue4Spline()
        {
            Text = "Многочленна - 4";
        }

        public override double[] GetCoeficients(AppMath.BaseFunc func, double x0, double x1)
        {
            var a = new double[4];
            double  alpha1, alpha2, beta1, beta2, miu1, miu2;

            double alpha = (func.Val(x1) - func.Val(x0))/(x1 - x0);
            alpha1 = alpha - func.Diff(x0);
            alpha2 = alpha - func.Diff(x1);

            double beta = (x1*x1 - x0*x0)/(x1 - x0);
            beta1 = beta - 2*x0;
            beta2 = beta - 2*x1;

            double miu = (Math.Pow(x1, 3) - Math.Pow(x0, 3))/(x1 - x0);
            miu1 = miu - 3*x0*x0;
            miu2 = miu - 3 * x1 * x1;

            a[3] = (beta1*alpha2 - beta2*alpha1)/(beta1*miu2 - beta2*miu1);
            a[2] = (alpha1 - miu1*a[3])/beta1;
            a[1] = alpha - a[2]*beta - a[3]*miu;
            a[0] = func.Val(x0) - a[1]*x0 - a[2]*x0*x0 - a[3]*Math.Pow(x0, 3);
            return a;
        }

        public override double GetAproximatingFunction(double x, double[] a)
        {
            return (a[0] + a[1]*x + a[2]*Math.Pow(x, 2) + a[3]*Math.Pow(x, 3));
        }
    }
}