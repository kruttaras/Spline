using System;
using Spline.Models;

namespace Spline
{
    public abstract class ApproximatingFunction : ComboBoxBaseItem
    {
       public abstract double[] GetCoeficients(AppMath.BaseFunc func, double x0, double x1);

       public abstract double GetAproximating_function(double x, double[] a);

       protected double ln(double x)
       {
           return Math.Log(x);
       }
       protected double pow(double x, double pow)
       {
           return Math.Pow(x, pow);
       }

       protected double exp(double x)
       {
           return Math.Exp(x);
       }
    }
}
