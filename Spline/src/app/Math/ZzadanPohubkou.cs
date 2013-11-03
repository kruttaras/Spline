using Spline;
using Spline.App.Utils;
using Spline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Spline
{    
    class ZzadanPohubkou
    {
        private readonly AppMath.BaseFunc function;
        private double Mu = 0.005;
        private static IList<Section> section; 
        private double  a;
        private double b;
        private AproximatingFunction aproximatingFunction;

       public IList<Section> Section
       {
           get
           {
               return section;
           }
       }


       public static List<Section> Compute(double a, double b, AppMath.BaseFunc function, AproximatingFunction aproximatingFunction, double Mu)
        {
            section = new List<Section>();
            double zl, zp, xmid, xtemp;
            zl = a;
            zp = b;

            double nextMu, prevMu;
            prevMu = findMu(zl, zp, function, aproximatingFunction);
            while (findMu(zl, b, function, aproximatingFunction) > Mu)
            {

                xmid = (zl + zp) / 2;
                nextMu = findMu(zl, xmid, function, aproximatingFunction);
                if (nextMu < Mu && prevMu > Mu)
                {
                    xtemp = (xmid + zp) / 2.0;
                    prevMu = findMu(zl, xtemp, function, aproximatingFunction);
                    Logger.Info("Start computing Mu for Section# " + section.Count+1, "MainForm");
                    while (Math.Abs(Mu - prevMu) / Mu > 0.01 || prevMu > Mu)
                    {
                        if (prevMu > Mu)
                        {
                            zp = xtemp;
                            xtemp = (xmid + zp) / 2.0;
                        }
                        else
                        {
                            xmid = xtemp;
                            xtemp = (xtemp + zp) / 2.0;
                        }
                        prevMu = findMu(zl, xtemp, function, aproximatingFunction);
                        //TODO fix this
                       // Logger.Info("computed Mu =  " + prevMu, this.GetType().ToString());
                    }

                    zp = xtemp;
                    section.Add(new Section(zl, zp, aproximatingFunction.GetCoeficients(function, zl, zp), prevMu));
                    zl = zp;
                    zp = b;

                }
                else
                {
                    zp = (zl + zp) / 2;
                }
                prevMu = findMu(zl, zp, function, aproximatingFunction);

            }
            section.Add(new Section(zl, b, aproximatingFunction.GetCoeficients(function, zl, b), prevMu));

           return (List<Section>) section;
        }

       private static double findMu(double xL, double xR, AppMath.BaseFunc Function, AproximatingFunction aproximatingFunction)
        {
            double[] coef = aproximatingFunction.GetCoeficients(Function, xL, xR);
            double h = (xR - xL) / Convert.ToDouble(1000);

            double Mu = -999, fx;
            for (double x = xL; x <= xR; x += h)
            {

                fx = Math.Abs(Function.Val(x) - aproximatingFunction.GetAproximatingFunction(x, coef));
                if (fx > Mu)
                {
                    Mu = fx;
                }
            }
            return Mu;
        }
    }
}
