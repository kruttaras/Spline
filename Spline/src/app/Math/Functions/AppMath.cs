using System;

namespace Spline.Models
{
    public class AppMath
    {
        public abstract class BaseFunc : ComboBoxBaseItem, IFunction
        {
            protected double[] Params = { 1, 1, 1, 1, 1 };
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

            public void SetParametrs(double[] parametrs)
            {
                this.Params = parametrs;
            }
            //TODO: make abstract
            //public bool ValidatePoints(double a, double b);
            
        }
    }
}
