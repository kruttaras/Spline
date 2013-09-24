﻿using Spline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.App.Models
{
    class ExponencialSpline : AproximatingFunction
    {
        public ExponencialSpline()
        {
            Text = "Експоненціальна";
        }
        public override double[] GetCoeficients(AppMath.BaseFunc func, double x0, double x1)
        {
            double[] a = new double[4];
            double x0_x1 = (x0 - x1);
            double LogF0_F1 = Math.Log(func.Val(x0) / func.Val(x1));


            double d = a[3] = (func.Diff(x1) / func.Val(x1) + func.Diff(x0) / func.Val(x0) + 2 * LogF0_F1 / (x1 - x0)) / Math.Pow(-1 * x0_x1, 2);

            double c = a[2] = ((2 * x0 * x0 - x0 * x1 - x1 * x1) * a[3] - func.Diff(x0) / func.Val(x0) - LogF0_F1 / (-1 * x0_x1)) / (-1 * x0_x1);

            double b = a[1] = 1 / (x1 - x0) * (Math.Log(func.Val(x1) / func.Val(x0)) - a[3] * (Math.Pow(x1, 3) - Math.Pow(x0, 3)) - c * (Math.Pow(x1, 2) - Math.Pow(x0, 2)));
            //A
            a[0] = func.Val(x0) * Math.Exp(-1 * (d * Math.Pow(x0, 3) + c * Math.Pow(x0, 2) + b * x0));

            return a;
        }

        public override double GetAproximatingFunction(double x, double[] a)
        {
            return (a[0] * Math.Exp(a[1] * x + a[2] * Math.Pow(x, 2) + a[3] * Math.Pow(x, 3)));
        }
    }

    class Multivalue3Spline : AproximatingFunction
    {
        public Multivalue3Spline()
        {
            Text = "Многочлен 3 порядку";
        }

        public override double[] GetCoeficients(AppMath.BaseFunc func, double x0, double x1)
        {
            double[] a = new double[4];
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

    class LogarifmicalSpline : AproximatingFunction
    {
        public LogarifmicalSpline()
        {
            Text = "Логарифмічна";
        }

        public override double[] GetCoeficients(AppMath.BaseFunc func, double x0, double x1)
        {
            double[] a = new double[4];
            double m1, m2, m3, m4, j0, j1, j2;
            double f0, df0, f1, df1;

            f0 = func.Val(x0);
            f1 = func.Val(x1);
            df0 = func.Diff(x0);
            df1 = func.Diff(x1);

            j0 = (Math.Exp(f1) - Math.Exp(f0))/(x1 - x0);
            j1 = x1 + x0;
            j2 = x1*x1 + x0*x1 + x1*x1;

            m1 = (df0*Math.Exp(f0) - j0)/(j1 - 2*x0);
            m2 = (3*x0*x0 + j2)/(j1 - 2*x0);
            m3 = (df1*Math.Exp(f1) - j0)/(j1 - 2*x1);
            m4 = (3*x1*x1 + j2)/(j1 - 2*x1);

            a[3] = (m3 - m1)/(m4 - m2);
            a[2] = m1 - a[3]*m2;
            a[1] = j0 + j1*a[2] + j2*a[3];
            a[0] = Math.Exp(f0) - a[1]*x0 - a[2]*x0*x0 - a[3]*Math.Pow(x0,3);
            return a;
        }

        public override double GetAproximatingFunction(double x, double[] a)
        {
            return Math.Log(a[0] + a[1] * x + a[2] * Math.Pow(x, 2) + a[3] * Math.Pow(x, 3));
        }
    }
}
