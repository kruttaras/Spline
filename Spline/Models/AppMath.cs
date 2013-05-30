using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.Models
{
    public class AppMath
    {
        public abstract class BaseFunc
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public abstract double Val(double x);

            public abstract double diff(double x, int i = 1);

            public bool ValidPoints(double a, double b)
            {
                if (a > b && (a <= 0 & b >= 0))
                {
                    return false;
                }

                return true;
            }


        }
        public class Sin : BaseFunc
        {
      

            public override string ToString()
            {
                return Text;
            }
            public override double Val(double x)
            {
                return Math.Sin(x);
            }


            public override double diff(double x, int i = 1)
            {
                switch (i)
                {
                    case 1:
                        return Math.Cos(x);
                        break;
                    case 2:
                        return (-1 * Math.Sin(x));
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
        }

        public class Ln : BaseFunc
        {
            public override double Val(double x)
            {
                return Math.Log(x);
            }

            public override double diff(double x, int i = 1)
            {
                if (x == 0)
                {
                    throw new Exception();
                }
                switch (i)
                {
                    case 1:
                        return (1 / x);
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

        public class Exp : BaseFunc
        {
            public override double Val(double x)
            {
                return Math.Log(x);
            }

            public override double diff(double x, int i = 1)
            {
                if (x == 0)
                {
                    throw new Exception();
                }
                switch (i)
                {
                    case 1:
                        return (1 / x);
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
}
