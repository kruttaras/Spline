using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.Models
{
    public class Exp : AppMath.BaseFunc
    {
        public Exp()
        {
            this.Text = "Exp(x+x^2+x^3+x^4)";
        }

        public override double Val(double x)
        {
            return Params[0] * Math.Exp(Params[1] * x + Params[2] * Math.Pow(x, 2) + Params[3] * Math.Pow(x, 3) + Params[4] * Math.Pow(x, 4));
        }

        public override double Diff(double x, int i = 1)
        {
            double[] p = Params;
            double inner = p[1] + 2 * p[2] * x + 3 * p[3] * Math.Pow(x, 2) + 4 * p[4] * Math.Pow(x, 3);
            double outer = Math.Exp(p[1] * x + p[2] * Math.Pow(x, 2) + p[3] * Math.Pow(x, 3) + p[4] * Math.Pow(x, 4));
            return inner * outer;
        }
    }
}
