using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spline.Models;

namespace Spline
{
    public abstract class ApproximationAlgorithm : IApproximationAlgorithm
    {
        protected double _leftBorder;
        protected double _rightBorder;
        protected AppMath.BaseFunc _func;
        protected ApproximatingFunction _approximatingFunction;
        protected double _observationalError;

        public double LeftBorder
        {
            get { return _leftBorder; }
            set { _leftBorder = value; }
        }

        public double RightBorder
        {
            get { return _rightBorder; }
            set { _rightBorder = value; }
        }

        public AppMath.BaseFunc Func
        {
            get { return _func; }
            set { _func = value; }
        }

        public ApproximatingFunction ApproximatingFunction
        {
            get { return _approximatingFunction; }
            set { _approximatingFunction = value; }
        }

        public double ObservationalError
        {
            get { return _observationalError; }
            set { _observationalError = value; }
        }

        public abstract List<Section> Compute();

        protected static double FindMu(double _leftBorder, double _rightBorder, AppMath.BaseFunc _func,
            ApproximatingFunction approximating_function)
        {
            var coef = approximating_function.GetCoeficients(_func, _leftBorder, _rightBorder);

            var accuracy = (_rightBorder - _leftBorder)/Convert.ToDouble(1000);
            var observationalError = Double.MinValue;

            for (double x = _leftBorder; x <= _rightBorder; x += accuracy)
            {
                double observationalErrorInPoint =
                    Math.Abs(_func.Val(x) - approximating_function.GetAproximating_function(x, coef));

                if (observationalErrorInPoint > observationalError)
                {
                    observationalError = observationalErrorInPoint;
                }
            }
            return observationalError;
        }
    }
}
