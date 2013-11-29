using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.Models
{
    public class Ln : AppMath.BaseFunc
    {

        public Ln()
        {
            this.Text = "Ln(1+x+x^2+x^3+x^4)";
        }

        public override double Val(double x)
        {
            double[] p = Params;
            return Math.Log(p[4] * Math.Pow(x, 4) + p[3] * Math.Pow(x, 3) + p[2] * Math.Pow(x, 2) + p[1] * x + p[0]);
        }

        public override double Diff(double x, int i = 1)
        {
            double[] p = Params;
            if (x == 0)
            {
                throw new Exception();
            }
            switch (i)
            {
                case 1:
                    return (4 * p[4] * x * x * x + 3 * p[3] * x * x + 2 * p[2] * x + p[1]) / (p[4] * Math.Pow(x, 4) + p[3] * Math.Pow(x, 3) + p[2] * Math.Pow(x, 2) + p[1] * x + p[0]);
                    break;
                case 2:
                    return (-1 / Math.Pow(x, 2));
                    break;
                default:
                    throw new Exception();
                    break;
            }
        }
    }
}
