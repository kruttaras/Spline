using System;

namespace Spline.Models
{
    public class Polinom : AppMath.BaseFunc
    {
        public Polinom()
        {
            this.Text = "1+x+x^2+x^3";
        }
        public override int GetNumberOfParametrs()
        {
            return 4;
        }

        public override double Val(double x)
        {
            return (1 + x + x * x + x * x * x);
        }

        public override double Diff(double x, int i = 1)
        {

            switch (i)
            {
                case 1:
                    return (1 + 2 * x + 3 * x * x);
                    break;
                case 2:
                    return (2 + 6 * x);
                    break;
                default:
                    throw new Exception();
                    break;
            }
        }
    }
}
