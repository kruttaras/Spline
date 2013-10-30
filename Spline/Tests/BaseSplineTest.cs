
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
                    Assert.True(expected[i] - eps < actual[i] && actual[i] < expected[i] + eps, "expected  {0} but was {1} .index = {2}",Stringify(expected),Stringify(actual), i);
                    Console.WriteLine("expected {0} and actual {1} in index {2}", Stringify(expected), Stringify(actual), i);
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

        protected String Stringify(double[] x)
        {
            string str = "[ ";
            for (int index = 0; index < x.Length-1; index++)
            {
                str += x[index];
                str += "; ";
            }
            str += x[x.Length - 1];
            str += " ]";
            return str;
        }
    }
}
