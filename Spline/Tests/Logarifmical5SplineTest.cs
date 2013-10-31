using Spline.Models;

namespace Spline.Tests
{
    using NUnit.Framework;

    class Logarifmical5SplineTest : BaseSplineTest
    {
        [Test]
        public void TestWithFirst1()
        {
            AppMath.BaseFunc exp = new AppMath.Ln();
            double[] expected = new double[] { 1.00d, 0, 2.00d, 3.00d, 4.00d};
            exp.setParametrs(expected);
            double[] actual = new Logarifmical5Spline().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);

        }

        [Test]
        public void TestWithFirst11()
        {
            AppMath.BaseFunc exp = new AppMath.Ln();
            double[] expected = new double[] { 1.00d, -4.00d, 2.00d, 3.00d, 4.00d };
            exp.setParametrs(expected);
            double[] actual = new Logarifmical5Spline().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);

        }

        [Test]
        public void TestWithFirst2()
        {
            AppMath.BaseFunc exp = new AppMath.Ln();
            double[] expected = new double[] { 0, 0, 2.00d, 0, 0 };
            exp.setParametrs(expected);
            double[] actual = new Logarifmical5Spline().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);

        }

        [Test]
        public void TestWithFirst3()
        {
            AppMath.BaseFunc exp = new AppMath.Ln();
            double[] expected = new double[] { 0, 0, 0, 3.00d, 0 };
            exp.setParametrs(expected);
            double[] actual = new Logarifmical5Spline().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);

        }

        [Test]
        public void TestWithFirst4()
        {
            AppMath.BaseFunc exp = new AppMath.Ln();
            double[] expected = new double[] { 0, 0, 0, 0, 3.00d };
            exp.setParametrs(expected);
            double[] actual = new Logarifmical5Spline().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);

        }
    }
}
