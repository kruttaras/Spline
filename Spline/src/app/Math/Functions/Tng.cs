using System;

namespace Spline.Models
{
    class Tng: AppMath.BaseFunc
        {
            public Tng()
            {
                this.Text = "Tg(x)";
            }

            public override double Val(double x)
            {
                return System.Math.Tan(x);
            }

            public override double Diff(double x, int i = 1)
            {

                switch (i)
                {
                    case 1:
                        return System.Math.Pow((1/System.Math.Cos(x)),2);
                        break;
                    case 2:
                        return 2 * System.Math.Tan(x) * System.Math.Pow((1 / System.Math.Cos(x)), 2);
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
        }
}
