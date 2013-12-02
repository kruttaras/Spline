using System;
using Spline.Models;

namespace Spline.Tests
{
    using NUnit.Framework;

    class Exponencial5SplineTest : BaseSplineTest
    {
        [Test]
        public void TestWithFirstAndThirdParametrs()
        {
            AppMath.BaseFunc exp = new Exp();
            double[] expected = new double[] { 1.00d, 0, -7.00d, 0, 0 };
            exp.SetParametrs(expected);
            double[] actual = new ExponentialSplineWithFiveParametrs().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);
        }

        [Test]
        public void TestWithFirstAndFourthParametrs()
        {
            AppMath.BaseFunc exp = new Exp();
            double[] expected = new double[] { 1.00d, 0, 0, -7.00d, 0 };
            exp.SetParametrs(expected);
            double[] actual = new ExponentialSplineWithFiveParametrs().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);

        }

        [Test]
        public void TestWithFirstThirdAndFourthParametrs()
        {
            AppMath.BaseFunc exp = new Exp();
            double[] expected = new double[] { 1.00d, 0, 2.00d, -3.00d, 0 };
            exp.SetParametrs(expected);
            double[] actual = new ExponentialSplineWithFiveParametrs().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);

        }

        [Test]
        public void TestWith4FirstParametrs()
        {
            AppMath.BaseFunc exp = new Exp();
            double[] expected = new double[] { 1.00d, -4.00d, 2.00d, -3.00d, 0 };
            exp.SetParametrs(expected);
            double[] actual = new ExponentialSplineWithFiveParametrs().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);
        }

        [Test]
        public void TestWithAllParametrs()
        {
            AppMath.BaseFunc exp = new Exp();
            double[] expected = new double[] { 1.00d, -4.00d, 2.00d, -3.00d, 5.00d };
            exp.SetParametrs(expected);
            double[] actual = new ExponentialSplineWithFiveParametrs().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);
        }

        [Test]
        public void TestWithParametrs()
        {
            AppMath.BaseFunc exp = new Exp();
            double[] expected = new double[] { 1.00d, 0, 0, 0, 5.00d };

            for (Int16 i = -9; i < 10; i++)
            {
                expected[4] = Convert.ToDouble(i);
                exp.SetParametrs(expected);
                double[] actual = new ExponentialSplineWithFiveParametrs().GetCoeficients(exp, 0.1, 0.9);

                AssertThatCoeficientsMatches(expected, actual);
            }
        }

        [Test]
        public void TestWith2Parametrs()
        {
            AppMath.BaseFunc exp = new Exp();
            double[] expected = new double[] { 1.00d, 2.00d, 0, 0, 0};
            exp.SetParametrs(expected);
            double[] actual = new ExponentialSplineWithFiveParametrs().GetCoeficients(exp, 0.1, 0.9);

            AssertThatCoeficientsMatches(expected, actual);
        }
    }
}
