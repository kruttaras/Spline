using Spline.App.Models;
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
        private AppMath.BaseFunc Function;
        private double Mu = 0.005;
        private IList<Section> section = new List<Section>();
        private double  a;
        private double b;


       public ZzadanPohubkou(double a, double b, AppMath.BaseFunc Function, double Mu)
        {
            this.Function = Function;
            this.a = a;
            this.b = b;
            this.Mu = Mu;
        }


       public IList<Section> Section
       {
           get
           {
               return section;
           }
       }


        public void Compute()
        {
            int LOG_SECTION_COUNTER = 1;

            double zl, zp, xmid, xtemp;
            zl = a;
            zp = b;

            double nextMu, prevMu;
            prevMu = findMu(zl, zp, Function);
            while (findMu(zl, b, Function) > Mu)
            {

                xmid = (zl + zp) / 2;
                nextMu = findMu(zl, xmid, Function);
                if (nextMu < Mu && prevMu > Mu)
                {
                    xtemp = (xmid + zp) / 2.0;
                    prevMu = findMu(zl, xtemp, Function);
                    Logger.Info("Start computing Mu for Section# " + LOG_SECTION_COUNTER++, "MainForm");
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
                        prevMu = findMu(zl, xtemp, Function);

                        Logger.Info("computed Mu =  " + prevMu, "MainForm");
                    }

                    zp = xtemp;
                    section.Add(new Section(zl, zp, new ExponencialSpline().GetCoeficients(Function, zl, zp), prevMu));
                    zl = zp;
                    zp = b;

                }
                else
                {
                    zp = (zl + zp) / 2;
                }
                prevMu = findMu(zl, zp, Function);

            }
            section.Add(new Section(zl, b, new ExponencialSpline().GetCoeficients(Function, zl, b), prevMu));
        }

        private double findMu(double xL, double xR, AppMath.BaseFunc Function)
        {
            double[] coef = new ExponencialSpline().GetCoeficients(Function, xL, xR);
            double h = (xR - xL) / Convert.ToDouble(1000);

            double Mu = -999, fx;
            for (double x = xL; x <= xR; x += h)
            {

                fx = Math.Abs(Function.Val(x) - new ExponencialSpline().GetAproximatingFunction(x, coef));
                if (fx > Mu)
                {
                    Mu = fx;
                }
            }
            return Mu;
        }
    }
}
