using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.Models
{
    //section of aproximation 
    class Section
    {
        private double LeftPoint;
        private double RightPoint;
        private ApproxFunction function;
        private double Mu;

        public void SetRPointToHalfOfLine()
        {
            RightPoint = (LeftPoint + RightPoint) / 2;
        }

        private Section()
        {
        }
        public Section(double LPoint, double RPoint)
        {
            if (LPoint < RPoint)
            {
                this.RightPoint = RPoint;
                this.LeftPoint = LPoint;
            }
            else
            {
                throw new Exception();
            }
        }

    }
}
