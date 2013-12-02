using Spline.App.Utils;
using Spline.Models;
using System;
using System.Collections.Generic;



namespace Spline
{
    class ApproximationWithGivenNumberOfSections : ApproximationAlgorithm
    {

        protected int _numberOfSections = 1;

        public int NumberOfSections
        {
            get { return _numberOfSections; }
            set { _numberOfSections = value; }
        }


        public override List<Section> Compute()
        {
            List<Section> section;
            double muPlus = 0, muMinus = 0;
            int sectionCount;
            var builder = new AlgorithmBuilder();

            builder = builder.SetBorders(_leftBorder, _rightBorder)
                .SetFunction(_func)
                .SetApproximatingFunction(_approximatingFunction);

            do
            {
                IApproximationAlgorithm algorithm = builder.SetObservationalError(_observationalError).Build();

                section = algorithm.Compute();
                sectionCount = section.Count;

                if (sectionCount > _numberOfSections)
                {
                    muMinus = _observationalError;
                    if (!Equals(muPlus, 0.0))
                    {

                        _observationalError = (_observationalError + muPlus) / 2;

                        if (Equals(_observationalError, muMinus))
                        {

                            sectionCount = _numberOfSections;
                            
                        }

                    }
                    else
                    {
                        _observationalError *= 1.1;
                    }

                }
                if (sectionCount < _numberOfSections || (_numberOfSections == sectionCount && (_observationalError - section[sectionCount - 1].Mu) / _observationalError > 0.1))
                {
                    muPlus = _observationalError;
                    sectionCount = -1;
                    if ( !Equals(muMinus, 0.0))
                    {
                        Logger.Info("Start computing observationalError in zadana k-t lanok # observationalError=" + _observationalError + "and muMinus=" + muMinus, "Spline");
                        _observationalError = (_observationalError + muMinus) / 2.0;
                        Logger.Info("Result # _observationalError=" + _observationalError, "Spline");

                        if (Equals(_observationalError, muPlus))
                        {
                            sectionCount = _numberOfSections;
                        }

                    }
                    else
                    {
                        _observationalError *= 0.9;
                    }


                }

            } while (_numberOfSections != sectionCount);

            return section;
        }
    }
}
