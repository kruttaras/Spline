using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spline.Models;

namespace Spline.Models
{
    public class Sin : AppMath.BaseFunc
    {

        public Sin()
        {
            this.Text = "Sin(x)";
            this.Params = new double[] {1, 1, 0, 0, 0};
        }

        public override double Val(double x)
        {
            return Params[0]*Math.Sin(Params[1]*x) + Params[2];
        }


        public override double Diff(double x, int i = 1)
        {
            switch (i)
            {
                case 1:
                    return Params[0]*Math.Cos(Params[1]*x)*Params[1];
                    break;
                case 2:
                    return -1*Params[0]*Math.Sin(Params[1]*x)*Math.Pow(Params[1], 2);
                    break;
                case 3:
                    return -1*Params[0]*Math.Cos(Params[1]*x)*Math.Pow(Params[1], 3);
                    break;
                case 4:
                    return Params[0]*Math.Sin(Params[1]*x)*Math.Pow(Params[1], 4);
                    break;
                case 5:
                    return Params[0]*Math.Cos(Params[1]*x)*Math.Pow(Params[1], 5);
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}
