using Spline.Models;

namespace Spline.Tests
{
    using NUnit.Framework;
    
    
    public class ExponencialSplineTest : BaseSplineTest
      {
        
        [Test]
        public void TestWithFirstAndThirdParametrs()
        {
            AppMath.BaseFunc exp = new Exp();
            double[] expected = new double[] { 1.00d, 0, -2.00d, 0, 0 };
            exp.SetParametrs(expected);
            double[] actual = new ExponencialSplineWithFourParametrs().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);           
        }

        [Test]
        public void TestWithFirstAndFourthParametrs()
        {
            AppMath.BaseFunc exp = new Exp();
            double[] expected = new double[] { 1.00d, 0, 0, -3.00d, 0 };
            exp.SetParametrs(expected);
            double[] actual = new ExponencialSplineWithFourParametrs().GetCoeficients(exp, 0.1, 0.9);
            
            AssertThatCoeficientsMatches(expected, actual);

        }

        [Test]
        public void TestWithFirstThirdAndFourthParametrs()
        {
            AppMath.BaseFunc exp = new Exp();
            double[] expected = new double[] { 1.00d, 0, 2.00d, -3.00d, 0 };
            exp.SetParametrs(expected);
            double[] actual = new ExponencialSplineWithFourParametrs().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);

        }

        [Test]
        public void TestWithAllParametrs()
        {
            AppMath.BaseFunc exp = new Exp();
            double[] expected = new double[] { 1.00d, -4.00d, 2.00d, -3.00d, 0 };
            exp.SetParametrs(expected);
            double[] actual = new ExponencialSplineWithFourParametrs().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);
        }
    }
}
