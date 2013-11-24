using System;
using Spline;
using Spline.Models;

namespace Spline
{
    class Exponential5Spline : AproximatingFunction
    {

        public Exponential5Spline()
        {
            Text = "a0*exp(a1*x+a2*x^2+a3*x^3+a4*x^4)";
        }

        public override double[] GetCoeficients(AppMath.BaseFunc func, double x0, double x2)
        {
            double[] a = new double[5] {0, 0, 0, 0, 0};

            double x1;
            x1 = (x0 + x2) / 2;

            double[] f = new[] { func.Val(x0), func.Val(x1), func.Val(x2) };
            double[] df = new[] { func.Diff(x0), func.Diff(x1), func.Diff(x2) };
            double[] x = new[] { x0, x1, x2 };

            double x1_x0 = x1 - x0;
            double x2_x0 = x2 - x0;


            double alpha1, alpha2, alpha3;
            double beta1, beta2, beta3;
            double dzeta1, dzeta2, dzeta3;
            double gamma1, gamma2, gamma3;

            double alpha = ln(f[1]/f[0])/x1_x0;

            alpha1 = alpha - ln(f[2]/f[0])/x2_x0;
            beta1 = (x1 + x0) * (x1 * x1 + x0 * x0) - (x2 + x0) * (x2 * x2 + x0 * x0);
            dzeta1 = pow(x1, 2) + x1*x0 - pow(x2, 2) - x2*x0;
            gamma1 = x1 - x2;

            alpha2 = alpha - df[0]/f[0];
            beta2 = pow(x1, 3) + x0 * x1 * (x0 + x1) - 3 * pow(x0, 3);
            dzeta2 = pow(x1, 2) + x0 * x1 - 2 * pow(x0, 2);
            gamma2 = x1 - x0;


            alpha3 = alpha - df[2]/f[2];
            beta3 = (x0 + x1)*(x0*x0 + x1*x1) - 4*pow(x2,3);
            dzeta3 = pow(x1, 2) + x0*x1 + pow(x0, 2) - 3*pow(x2, 2);
            gamma3 = x1 + x0 - 2*x2;
            
            double chus = (alpha2*beta1 - alpha1*beta2)*(dzeta3*beta1 - dzeta1*beta3) -
                       (dzeta2*beta1 - dzeta1*beta2)*(alpha3*beta1 - alpha1*beta3);

            double znam = (gamma1*beta2 - gamma2*beta1)*(dzeta1*beta3 - dzeta3*beta1) +
                          (dzeta2*beta1 - dzeta1*beta2)*(gamma1*beta3 - gamma3*beta1);
            a[2] = chus/znam;

             a[3] = (alpha2*beta1 - alpha1*beta2 + (gamma1*beta2 - gamma2*beta1)*a[2])/(dzeta2*beta1 - dzeta1*beta2);

            
          //  chus= alpha1*gamma3*(beta2-beta3)-alpha2*(beta1*gamma3-beta3*gamma1)+alpha3*beta1*(gamma2-gamma1);
         //   znam = beta1*(gamma2*dzeta3 - gamma3*dzeta2) - beta2*(gamma1*dzeta3 - gamma3*dzeta1) + beta3*(gamma1*dzeta2 - gamma2*dzeta1);

          //  a[3] =chus/znam;
            a[4] = (alpha1 - dzeta1 * a[3] - gamma1 * a[2]) / beta1;

            a[1] = (1/x1_x0)*
                   (ln(f[1]/f[0]) - a[4]*(pow(x1, 4) - pow(x0, 4)) -
                    a[3]*(pow(x1, 3) - pow(x0, 3)) - a[2]*(pow(x1, 2) - pow(x0, 2)));

            a[0] = f[1] * Math.Exp(-1 * (a[1] * x1 + a[2] * x1 * x1 + a[3] * pow(x1, 3) + a[4] * pow(x1, 4)));

            return a;
        }

        public override double GetAproximatingFunction(double x, double[] a)
        {
            return (a[0] * Math.Exp(a[1] * x + a[2] * Math.Pow(x, 2) + a[3] * Math.Pow(x, 3) + a[4] * Math.Pow(x, 4)));
        }
          
    }
}
