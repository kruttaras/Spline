using Spline.App.Utils;
using Spline.Models;
using System;
using System.Collections.Generic;


namespace Spline
{
    class ApproximationWithGivenObservationalError : ApproximationAlgorithm
    {
       
       public override List<Section> Compute()
        {

            var section = new List<Section>();
            double zl, zp, xmid, xtemp;
            zl = _leftBorder;
            zp = _rightBorder;

            double nextMu, prevMu;
            prevMu = FindMu(zl, zp, _func, _approximatingFunction);
            while (FindMu(zl, _rightBorder, _func, _approximatingFunction) > _observationalError)
            {

                xmid = (zl + zp) / 2;
                nextMu = FindMu(zl, xmid, _func, _approximatingFunction);
                if (nextMu < _observationalError && prevMu > _observationalError)
                {
                    xtemp = (xmid + zp) / 2.0;
                    prevMu = FindMu(zl, xtemp, _func, _approximatingFunction);
                    Logger.Info("Start computing observationalError for Section# " + section.Count + 1, "MainForm");
                    while (Math.Abs(_observationalError - prevMu) / _observationalError > 0.01 || prevMu > _observationalError)
                    {
                        if (prevMu > _observationalError)
                        {
                            zp = xtemp;
                            xtemp = (xmid + zp) / 2.0;
                        }
                        else
                        {
                            xmid = xtemp;
                            xtemp = (xtemp + zp) / 2.0;
                        }
                        prevMu = FindMu(zl, xtemp, _func, _approximatingFunction);
                        //TODO fix this
                       // Logger.Info("computed observationalError =  " + prevMu, this.GetType().ToString());
                    }

                    zp = xtemp;
                    section.Add(new Section(zl, zp, _approximatingFunction.GetCoeficients(_func, zl, zp), prevMu));
                    zl = zp;
                    zp = _rightBorder;

                }
                else
                {
                    zp = (zl + zp) / 2;
                }
                prevMu = FindMu(zl, zp, _func, _approximatingFunction);

            }
            section.Add(new Section(zl, _rightBorder, _approximatingFunction.GetCoeficients(_func, zl, _rightBorder), prevMu));

            return section;
        }

    }
}
