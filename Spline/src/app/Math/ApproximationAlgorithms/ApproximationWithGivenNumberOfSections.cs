using Spline.App.Utils;
using Spline.Models;
using System;
using System.Collections.Generic;



namespace Spline
{
    class ApproximationWithGivenNumberOfSections: ApproximationAlgorithm<ApproximationWithGivenNumberOfSections>
    {
        public override List<Section> Compute(double leftBorder, double rightBorder, AppMath.BaseFunc func, ApproximatingFunction approximatingFunction, double observationalError, int numberOfSections = 1)
        {

            List<Section> section;
            double muPlus = 0, muMinus = 0;
            int sectionCount;

            do
            {
                section = ApproximationWithGivenObservationalError.Instance.Compute(leftBorder, rightBorder, func, approximatingFunction, observationalError);
                sectionCount = section.Count;
                if (sectionCount > numberOfSections)
                {
                    muMinus = observationalError;
                    if (!Equals(muPlus, 0.0))
                    {
                        
                        observationalError = (observationalError + muPlus) / 2;

                        if (Equals(observationalError, muMinus))
                        {

                            sectionCount = numberOfSections;
                            
                        }

                    }
                    else
                    {
                        observationalError *= 1.1;
                    }

                }
                if (sectionCount < numberOfSections || (numberOfSections == sectionCount && (observationalError - section[sectionCount - 1].Mu) / observationalError > 0.1))
                {
                    muPlus = observationalError;
                    sectionCount = -1;
                    if ( !Equals(muMinus, 0.0))
                    {
                        Logger.Info("Start computing observationalError in zadana k-t lanok # observationalError=" + observationalError + "and muMinus=" + muMinus, "Spline");
                        observationalError = (observationalError + muMinus) / 2.0;
                        Logger.Info("Result # observationalError=" + observationalError, "Spline");

                        if (Equals(observationalError, muPlus))
                        {
                            sectionCount = numberOfSections;
                        }

                    }
                    else
                    {
                        observationalError *= 0.9;
                    }


                }

            } while (numberOfSections != sectionCount);

            return section;
        }
    }
}
