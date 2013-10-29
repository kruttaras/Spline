
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public abstract class BaseSplineTest
    {
        protected const double INFLECITY = 1E-8;

        protected void AssertThatCoeficientsMatches(double[] expected, double[] actual, double eps)
        {
            for (int i = 0; i <expected.Length; i++)
            {
                if (expected[i] != actual[i])
                {
                    Assert.True(expected[i] - eps < actual[i] && actual[i] < expected[i] + eps, "expected  {0} but was {1} .index = {2}\n {3}", expected.ToString(), actual.ToString(), i);
                    Console.WriteLine("expected {0} and actual {1} in index {2}", expected.ToString(), actual.ToString(), i);
                }
                else
                {
                    Assert.AreEqual(expected[i], actual[i]);
                }
            }
        }

        protected void AssertThatCoeficientsMatches(double[] expected, double[] actual)
        {
            AssertThatCoeficientsMatches(expected, actual, INFLECITY);
        }

    }
}
