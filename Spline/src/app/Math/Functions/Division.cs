using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spline.Models;

namespace Spline.Models
{
    public class Division : AppMath.BaseFunc
    {
        public Division()
        {
            this.Text = "1/(1+x^2)";
        }

        public override double Val(double x)
        {
            return (1 / (1 + x * x));
        }

        public override double Diff(double x, int i = 1)
        {
            switch (i)
            {
                case 1:
                    return (-2 * x / System.Math.Pow((x * x + 1), 2));
                    break;
                case 2:
                    return ((6 * x * x - 2) / System.Math.Pow((x * x + 1), 3));
                    break;
                default:
                    throw new Exception();
                    break;
            }
        }
    }
}
