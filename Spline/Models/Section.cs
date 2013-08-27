using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.Models
{
    //section of aproximation 
    class Section
    {
        public double LeftPoint;
        public double RightPoint;
        public double[] Coef;
        public double Mu;

        private Section()
        {
        }
        public Section(double LPoint, double RPoint,double[] coef, double Mu)
        {
            if (LPoint < RPoint)
            {
                this.RightPoint = RPoint;
                this.LeftPoint = LPoint;
                this.Coef = coef;
                this.Mu = Mu;
            }
            else
            {
                throw new Exception();
            }
        }

    }
}
