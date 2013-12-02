using System;
using Spline.Models;

namespace Spline
{

    internal class AlgorithmBuilder
    {
        private double _leftBorder = 0;
        private double _rightBorder = 0;
        private AppMath.BaseFunc _func;
        private ApproximatingFunction _approximatingFunction;
        private double _observationalError = 0;
        private int _numberOfSections = 0;

        public AlgorithmBuilder()
        {

        }

        public AlgorithmBuilder SetBorders(double x1, double x2)
        {
            this._leftBorder = x1;
            this._rightBorder = x2;
            return this;
        }

        public AlgorithmBuilder SetFunction(AppMath.BaseFunc func)
        {
            this._func = func;
            return this;
        }

        public AlgorithmBuilder SetApproximatingFunction(ApproximatingFunction approximatingFunctionfunc)
        {
            this._approximatingFunction = approximatingFunctionfunc;
            return this;
        }

        public AlgorithmBuilder SetObservationalError(double error)
        {
            this._observationalError = error;
            return this;
        }

        public AlgorithmBuilder SetNumberOfSections(int count)
        {
            this._numberOfSections = count;
            return this;
        }

        public IApproximationAlgorithm Build()
        {
            ApproximationAlgorithm algorithm;

            if (_numberOfSections < 1)
            {
                algorithm = new ApproximationWithGivenObservationalError();
                
            }
            else
            {
                ApproximationWithGivenNumberOfSections approximationWithGivenNumberOfSections = new ApproximationWithGivenNumberOfSections();
                approximationWithGivenNumberOfSections.NumberOfSections = _numberOfSections;
                algorithm = approximationWithGivenNumberOfSections;
            }

            SetPropretis(algorithm);
            return algorithm;
        }

        private void SetPropretis(ApproximationAlgorithm algorithm)
        {
            algorithm.LeftBorder = _leftBorder;
            algorithm.RightBorder = _rightBorder;
            algorithm.Func = _func;
            algorithm.ApproximatingFunction = _approximatingFunction;
            algorithm.ObservationalError = _observationalError;
        }

    }
}
