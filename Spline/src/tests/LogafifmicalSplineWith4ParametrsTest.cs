using System.Collections.Generic;
using Spline.Models;

namespace Spline.Tests
{
    using NUnit.Framework;

    internal class LogafifmicalSplineWith4ParametrsTest : BaseSplineTest
    {

        private List<double[]> TEST_PLANS = new List<double[]>();
       
    [SetUp]
    public void Init()
    {
        TEST_PLANS.Add(new[] { 0, 0, 1.0, 0, 0 });
        TEST_PLANS.Add(new[] { 0, 0, 0, 3.0, 0 });
        //TEST_PLANS.Add(new []{ 1.00d, 0, 0, 3.0d, 0 });
      //  TEST_PLANS.Add(new []{ 0, 0, 4.00d, 5.0d, 0 });
     //   TEST_PLANS.Add(new [] { 0, 2.00d, 4.00d, 5.0d, 0 });
     //   TEST_PLANS.Add(new []{ 1.00d, 0, 4.00d, 5.0d, 0 });
    }

        [Test]
        public void TestParametrs()
        {
            foreach (var expected in TEST_PLANS)
            {
                AppMath.BaseFunc ln = new Ln();
                ln.SetParametrs(expected);
                double[] actual = new LogarifmicalSplineWithFourParametrs().GetCoeficients(ln, 1.1, 1.9);

                AssertThatCoeficientsMatches(expected, actual);
            }
        }
    }
}
