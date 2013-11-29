using Spline.App.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.Models
{
    //section of aproximation 
    public class Section
    {
        public double LeftPoint;
        public double RightPoint;
        public double[] Coef;
        public double Mu;

        private Section()
        {
        }
        public Section(double lPoint, double rPoint,double[] coef, double mu)
        {
            if (lPoint < rPoint)
            {
                this.RightPoint = rPoint;
                this.LeftPoint = lPoint;
                this.Coef = coef;
                this.Mu = mu;
            }
            else
            {
                throw new Exception();
            }
            Logger.Info("saving section with params \n x = " + LeftPoint + ", y = " + RightPoint + ", " + coef.ToString() + ", Mu = " + mu, "Section");
        }

    }
}
