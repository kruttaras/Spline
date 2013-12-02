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
                    return 1/x;
                    break;
                case 2:
                    return SecondDiff(x);
                    break;
                default:
                    throw new Exception();
                    break;
            }
        }

        private double SecondDiff(double x)
        {
            double[] p = Params;
            double chus = p[4]*Math.Pow(x, 4) + p[3]*Math.Pow(x, 3) + p[2]*Math.Pow(x, 2) + p[1]*x + p[0];
            double res1 = 12*p[4]*Math.Pow(x, 2) + 6*p[3]*x + 2*p[2];
            double res2 = 4*p[4]*Math.Pow(x, 3) + 3*p[3]*Math.Pow(x, 2) + 2*p[2]*x + p[1];
            
            return (res1/chus-Math.Pow(res2,2)/Math.Pow(chus,2));
        }
    }
}
