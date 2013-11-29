using System;
using Spline.Models;

namespace Spline
{
    class Multivalue5Spline : ApproximatingFunction
    {
        public Multivalue5Spline()
        {
            Text = "Многочленна - 5";
        }

        public override double[] GetCoeficients(AppMath.BaseFunc func, double x0, double x1)
        {
            var a = new double[5];

            double x2 = x1;
            x1 = (x1 + x0)/2;

            var f = new[] {func.Val(x0), func.Val(x1), func.Val(x2)};
            var df = new[] {func.Diff(x0), func.Diff(x1), func.Diff(x2)};
            var x = new[] { x0, x1, x2 };

            double  p, q, r;
            
            p = x0 + x1 + x2;
            q = x0*x0 + x1*x1 + x2*x2 + x0*x1 + x1*x2 + x0*x2;

            double x1_x0 = x1 - x0;
            double x2_x1 = x2 - x1;
            r = 1 / (x2 - x0) * ((f[2] - f[1]) / x2_x1 - (f[1] - f[0]) / x1_x0);
            

            double alpha1, alpha2;
            alpha1 = df[0] - (f[1] - f[0])/x1_x0 + r*(x1*x1 - x0*x0)/x1_x0 - 2 * r * x0;
            alpha2 = df[2] - (f[2] - f[1]) / x2_x1 + r * (x2 * x2 - x1 * x1) / x2_x1 - 2 * r * x2;

            double beta1, beta2;
            beta1 = p * diffOf(1,0,x,2)/(x1_x0) - diffOf(1,0,x,3) /(x1_x0) - 2*q*x0 + 3*x0*x0;
            beta2 = p * diffOf(2, 1, x, 2) / (x2_x1) - diffOf(2, 1, x, 3) / (x2_x1) - 2 * q * x2 + 3 * x2 * x2;

            double miu1, miu2;
            miu1 = q * diffOf(1, 0, x, 2)/x1_x0 - diffOf(1, 0, x, 4)/x1_x0 - 2*q*x0 + (double) (4*Math.Pow((double) x0, 3));
            miu2 = q * diffOf(2, 1, x, 2) / x2_x1 - diffOf(2, 1, x, 4) / x2_x1 - 2 * q * x2 + (double) (4 * Math.Pow((double) x2, 3));

            a[4] = (beta1*alpha2 - beta2*alpha1)/(beta1*miu2 - beta2*miu1);
            a[3] = (alpha1-miu1*a[4])/beta1;
            a[2] = r-p*a[3]-q*a[4];
            a[1] = (f[1] - f[0] - a[2]*diffOf(1, 0, x, 2) - a[3]*diffOf(1, 0, x, 3) - a[4]*diffOf(1, 0, x, 4))/x1_x0;
            a[0] = func.Val(x0) - a[1] * x0 - a[2] * x0 * x0 - a[3] * Math.Pow(x0, 3) - a[4] * Math.Pow(x0, 4);
           
            return a;
        }

        public override double GetAproximatingFunction(double x, double[] a)
        {
            return (a[0] + a[1] * x + a[2] * Math.Pow(x, 2) + a[3] * Math.Pow(x, 3) + a[4] * Math.Pow(x, 4));
        }

        private double diffOf(int first, int second, double[] mass, int pow = 1)
        {
            return (double) (Math.Pow((double) mass[first], pow) - Math.Pow((double) mass[second], pow));
        }
    }
}
