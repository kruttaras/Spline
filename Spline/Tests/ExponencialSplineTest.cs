using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spline.Models;

namespace Spline
{
    using NUnit.Framework;
    
    [TestFixture]
    public  class ExponencialSplineTest
      {

        private const double INFLECITY = 1E-8;

        [Test]
        public void TransferFunds()
        {
            AppMath.BaseFunc exp = new AppMath.Exp();
            double[] expected = new double[] { 1.00d, 0, -2.00d, 0, 0 };
            exp.setParametrs(expected);
            double[] actual = new ExponencialSpline().GetCoeficients(exp, 0.1, 0.9);

            for (int i = 0; i < expected.Length; i++)
            {
                if (expected[i] != actual[i])
                {
                    Assert.True(expected[i] - INFLECITY < actual[i] && actual[i] < expected[i] + INFLECITY);
                }
                else
                {
                    Assert.True(true);
                }
            }
                
        }

        [Test]
        public void TransferFunds2()
        {
            AppMath.BaseFunc exp = new AppMath.Exp();
            double[] expected = new double[] { 1.00d, 0, 0, -3.00d, 0 };
            exp.setParametrs(expected);
            double[] actual = new ExponencialSpline().GetCoeficients(exp, 0.1, 0.9);

            for (int i = 0; i < expected.Length; i++)
            {
                if (expected[i] != actual[i])
                {
                    Assert.True(expected[i] - INFLECITY < actual[i] && actual[i] < expected[i] + INFLECITY);
                }
                else
                {
                    Assert.True(true);
                }
            }

        }

        [Test]
        public void TransferFunds3()
        {
            AppMath.BaseFunc exp = new AppMath.Exp();
            double[] expected = new double[] { 1.00d, 0, 2.00d, -3.00d, 0 };
            exp.setParametrs(expected);
            double[] actual = new ExponencialSpline().GetCoeficients(exp, 0.1, 0.9);

            for (int i = 0; i < expected.Length; i++)
            {
                if (expected[i] != actual[i])
                {
                    Assert.True(expected[i] - INFLECITY < actual[i] && actual[i] < expected[i] + INFLECITY);
                }
                else
                {
                    Assert.True(true);
                }
            }

        }

        [Test]
        public void TransferFunds4()
        {
            AppMath.BaseFunc exp = new AppMath.Exp();
            double[] expected = new double[] { 1.00d, -4.00d, 2.00d, -3.00d, 0 };
            exp.setParametrs(expected);
            double[] actual = new ExponencialSpline().GetCoeficients(exp, 0.1, 0.9);

            for (int i = 0; i < expected.Length; i++)
            {
                if (expected[i] != actual[i])
                {
                    Assert.True(expected[i] - INFLECITY < actual[i] && actual[i] < expected[i] + INFLECITY);
                }
                else
                {
                    Assert.True(true);
                }
            }

        }
    }
}
