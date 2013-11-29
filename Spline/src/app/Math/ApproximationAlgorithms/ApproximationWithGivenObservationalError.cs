using Spline.App.Utils;
using Spline.Models;
using System;
using System.Collections.Generic;


namespace Spline
{
    class ApproximationWithGivenObservationalError : ApproximationAlgorithm<ApproximationWithGivenObservationalError>
    {


        public override List<Section> Compute(double leftBorder, double rightBorder, AppMath.BaseFunc func, ApproximatingFunction approximatingFunction, double observationalError, int numberOfSections = 1)
        {
            var section = new List<Section>();
            double zl, zp, xmid, xtemp;
            zl = leftBorder;
            zp = rightBorder;

            double nextMu, prevMu;
            prevMu = FindMu(zl, zp, func, approximatingFunction);
            while (FindMu(zl, rightBorder, func, approximatingFunction) > observationalError)
            {

                xmid = (zl + zp) / 2;
                nextMu = FindMu(zl, xmid, func, approximatingFunction);
                if (nextMu < observationalError && prevMu > observationalError)
                {
                    xtemp = (xmid + zp) / 2.0;
                    prevMu = FindMu(zl, xtemp, func, approximatingFunction);
                    Logger.Info("Start computing observationalError for Section# " + section.Count + 1, "MainForm");
                    while (Math.Abs(observationalError - prevMu) / observationalError > 0.01 || prevMu > observationalError)
                    {
                        if (prevMu > observationalError)
                        {
                            zp = xtemp;
                            xtemp = (xmid + zp) / 2.0;
                        }
                        else
                        {
                            xmid = xtemp;
                            xtemp = (xtemp + zp) / 2.0;
                        }
                        prevMu = FindMu(zl, xtemp, func, approximatingFunction);
                        //TODO fix this
                       // Logger.Info("computed observationalError =  " + prevMu, this.GetType().ToString());
                    }

                    zp = xtemp;
                    section.Add(new Section(zl, zp, approximatingFunction.GetCoeficients(func, zl, zp), prevMu));
                    zl = zp;
                    zp = rightBorder;

                }
                else
                {
                    zp = (zl + zp) / 2;
                }
                prevMu = FindMu(zl, zp, func, approximatingFunction);

            }
            section.Add(new Section(zl, rightBorder, approximatingFunction.GetCoeficients(func, zl, rightBorder), prevMu));

            return (List<Section>)section;
        }

    }
}
