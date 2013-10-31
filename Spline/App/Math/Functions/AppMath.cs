using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.Models
{
    public class AppMath
    {
        public abstract class BaseFunc : ComboBoxBaseItem, IFunction
        {
            protected double[] parametrs = { 1, 1, 1, 1, 1 };
            public override string ToString()
            {
                return Text;
            }

            public abstract double Val(double x);

            public abstract double Diff(double x, int i = 1);

            public virtual int GetNumberOfParametrs()
            {
                return 0;
            }

            public void setParametrs(double[] parametrs)
            {
                this.parametrs = parametrs;
            }

            //TODO: make abstract
            //public bool ValidatePoints(double a, double b);
            
        }

        public class Sin : BaseFunc
        {
       
            public Sin()
            {
                this.Text = "Sin(x)";
            }

            public override double Val(double x)
            {
                return Math.Sin(x);
            }


            public override double Diff(double x, int i = 1)
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
                }
            }
        }


        public class Ln : BaseFunc
        {

            public Ln()
            {
                this.Text = "Ln(x)";
            }

            public override double Val(double x)
            {
                double[] p = parametrs;
                return Math.Log(p[4] * Math.Pow(x, 4) + p[3] * Math.Pow(x, 3) + p[2] * Math.Pow(x, 2) + p[1] * x + p[0]);
            }

            public override double Diff(double x, int i = 1)
            {
                double[] p = parametrs;
                if (x == 0)
                {
                    throw new Exception();
                }
                switch (i)
                {
                    case 1:
                        return (4 * p[4] * x * x * x + 3 * p[3] * x * x + 2 * p[2] * x + p[1]) / (p[4] * Math.Pow(x, 4) + p[3] * Math.Pow(x, 3) + p[2] * Math.Pow(x, 2)+p[1]*x+p[0]);
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
            public Exp()
            {
                this.Text = "Exp(x)";
            }

            public override double Val(double x)
            {
                return parametrs[0] * Math.Exp(parametrs[1] * x + parametrs[2] * Math.Pow(x, 2) + parametrs[3] * Math.Pow(x, 3) + parametrs[4] * Math.Pow(x, 4));
            }

            public override double Diff(double x, int i = 1)
            {
                double[] p = parametrs;
                double inner = p[1] + 2 * p[2] * x + 3 * p[3] * Math.Pow(x, 2) + 4 * p[4] * Math.Pow(x, 3);
                double outer = Math.Exp(p[1] * x + p[2] * Math.Pow(x, 2) + p[3] * Math.Pow(x, 3) + p[4] * Math.Pow(x, 4));
                return inner * outer;
            }
        }

        public class Exp5 : BaseFunc
        {
            public Exp5()
            {
                this.Text = "Exp(x+x^2+x^3+x^4)";
            }

            public override double Val(double x)
            {
                return Math.Exp(x + x * x + Math.Pow(x, 3) + Math.Pow(x, 4));
            }

            public override double Diff(double x, int i = 1)
            {
                return (1 + 2*x + 3*x*x + 4*Math.Pow(x, 3))*Math.Exp(x + x*x + Math.Pow(x, 3) + Math.Pow(x, 4));
            }
        }

        public class Division : BaseFunc
        {
            public Division()
            {
                this.Text = "1/(1+x^2)";
            }

            public override double Val(double x)
            {
                return (1/(1+x*x));
            }

            public override double Diff(double x, int i = 1)
            {
                switch (i)
                {
                    case 1:
                        return (-2*x/Math.Pow((x*x+1),2));
                        break;
                    case 2:
                        return ((6*x*x-2)/ Math.Pow((x * x + 1), 3));
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
        }


        public class Tangens : BaseFunc
        {
            public Tangens()
            {
                this.Text = "Tg(x)";
            }

            public override double Val(double x)
            {
                return Math.Tan(x);
            }

            public override double Diff(double x, int i = 1)
            {

                switch (i)
                {
                    case 1:
                        return Math.Pow((1/Math.Cos(x)),2);
                        break;
                    case 2:
                        return 2 * Math.Tan(x) * Math.Pow((1 / Math.Cos(x)), 2);
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
        }

        public class Polinom3 : BaseFunc
        {
            public Polinom3()
            {
                this.Text = "A+B*x+C*x^2+D*x^3";
            }
            public override int GetNumberOfParametrs()
            {
                return 4;
            }

            public override double Val(double x)
            {
                return (1+x+x*x+x*x*x);
            }

            public override double Diff(double x, int i = 1)
            {

                switch (i)
                {
                    case 1:
                        return (1+2*x+3*x*x);
                        break;
                    case 2:
                        return ( 2 + 6 * x);
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
        }



    }
}
