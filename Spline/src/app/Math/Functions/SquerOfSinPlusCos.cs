using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.Models
{
    class SquerOfSinPlusCos : AppMath.BaseFunc
    {

        public SquerOfSinPlusCos()
        {
            this.Text = "(Sin(x) + Cos(x))^2+1";
            this.Params = new double[] {1, 1, 0, 0, 0};
        }

        public override double Val(double x)
        {
            return Math.Pow((Math.Sin(x)+Math.Cos(x)),2)+1;
        }


        public override double Diff(double x, int i = 1)
        {
            switch (i)
            {
                case 1:
                    return 2 * (Math.Sin(x) + Math.Cos(x)) * (Math.Cos(x) - Math.Sin(x));
                    break;
                case 2:
                    return 2 * Math.Pow(Math.Cos(x) - Math.Sin(x),2) + 2 * (Math.Sin(x) + Math.Cos(x))*(-Math.Sin(x) - Math.Cos(x));
                    break;
               default:
                    throw new Exception();
            }
        }
    }
}
