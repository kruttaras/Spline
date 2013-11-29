using System;
using System.Collections.Generic;
using Spline.Models;

namespace Spline
{
    public abstract class ApproximationAlgorithm<T> where T : class
    {
        #region Members

      private static readonly Lazy<T> SInstance = new Lazy<T>(CreateInstanceOfT);

        #endregion

        #region Properties

        public static T Instance { get { return SInstance.Value; } }

        #endregion

        #region Methods

       private static T CreateInstanceOfT()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }
        
        public abstract List<Section> Compute(double leftBorder, double rightBorder, AppMath.BaseFunc func, ApproximatingFunction approximatingFunction, double observationalError, int numberOfSections = 1);

        protected static double FindMu(double leftBorder, double rightBorder, AppMath.BaseFunc func, ApproximatingFunction approximatingFunction)
        {
            var coef = approximatingFunction.GetCoeficients(func, leftBorder, rightBorder);

            var accuracy = (rightBorder - leftBorder) / Convert.ToDouble(1000);
            var observationalError = Double.MinValue;

            for (double x = leftBorder; x <= rightBorder; x += accuracy)
            {
                double observationalErrorInPoint = Math.Abs(func.Val(x) - approximatingFunction.GetAproximatingFunction(x, coef));

                if (observationalErrorInPoint > observationalError)
                {
                    observationalError = observationalErrorInPoint;
                }
            }
            return observationalError;
        }

        #endregion
    }
}
