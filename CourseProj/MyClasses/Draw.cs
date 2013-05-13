using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace CourseProj.MyClasses
{
    public class Draw
    {
        public delegate double function(double x);
        public delegate double MuFunc(double x, function f, double[] Coefficients);
        public delegate double SplainFunction(double[] Coef, double x, int power);
        private function f;
        private function fp;
        private function fpp;

        private PictureBox picture;
        private Graphics graphic;
        private Bitmap bitMap;
        private int areaHeight;
        private int areaWidth;
        private Pen areaPen;
        private Color colorOfAxes;
        private const int widthOfNotGradedPart = 120;
        private const int heightOfPartitions = 6;
        private Font font;
        private SolidBrush solidBrush;
        private PointF pointO;
        private PointF pointPositiveX;
        private PointF pointPositiveY;
        private PointF pointNegativeX;
        private PointF pointNegativeY;
        public double multiplyIndexX;
        public double multiplyIndexY;
        private int PolinomPower;
        private double[] Coef;
        public Draw(int width, int height, Color axesColor, PictureBox image,int p,double[] c,function func,function funcp,function funcpp)
        {
            if ((width <= 0) || (height <= 0))
            {
                throw new Exception("Constructor Area: Input parameters are incorrect");
            }
            picture = image;
            areaWidth = width;
            areaHeight = height;
            picture.Width = areaWidth;
            picture.Height = areaHeight;

            colorOfAxes = axesColor;
            bitMap = new Bitmap(areaWidth, areaHeight);
            graphic = Graphics.FromImage(bitMap);
            areaPen = new Pen(colorOfAxes);
            font = new Font("MyFont", 8, FontStyle.Regular);
            solidBrush = new SolidBrush(axesColor);
            PolinomPower = p;
            Coef = c;
            f = new function(func);
            fp = new function(funcp);
            fpp = new function(funcpp);
        }

        public int WidthOfNotGradedPart
        {
            get
            {
                return widthOfNotGradedPart;
            }
        }

        public string getXY(int x, int y)
        {
            string result = "";
            x = x - picture.Location.X;
            y = y - picture.Location.Y;
            result = x.ToString() + " " + y.ToString();
            return result;
        }

        private double maxOfFunction(function f, double xBeg, double xEnd, double step)
        {
            function result = new function(f);
            double max = result(xBeg);
            while (xBeg <= xEnd)
            {
                if (result(xBeg) > max)
                {
                    max = result(xBeg);
                }
                xBeg += step;
            }
            return max;
        }

        private double minOfFunction(function f, double xBeg, double xEnd, double step)
        {
            function result = new function(f);
            double min = result(xBeg);
            while (xBeg <= xEnd)
            {
                if (result(xBeg) < min)
                {
                    min = result(xBeg);
                }
                xBeg += step;
            }
            return min;
        }

        private double specialFunction(double beg, double end)
        {
            if (beg > end)
            {
                double temp = beg;
                beg = end;
                end = temp;
            }
            if ((beg < 0) && (end < 0))
            {
                return Math.Abs(beg);
            }
            else if ((beg < 0) && (end >= 0))
            {
                return (end - beg);
            }
            return end;
        }

        private double setStartingPointX(double beg, double end)
        {

            double value = (beg > 0) ? 0 : Math.Abs(beg);
            return widthOfNotGradedPart / 2 + value / (specialFunction(beg, end) / (double)(areaWidth - widthOfNotGradedPart));

        }

        private double setStartingPointY(double beg, double end)
        {
            double value = (beg > 0) ? specialFunction(beg, end) : end;
            if (value == 0) return widthOfNotGradedPart / 2;
            return widthOfNotGradedPart / 2 +value / (specialFunction(beg, end) / (double)(areaHeight - widthOfNotGradedPart));
        }

        private string doubleToStr(double value, int length)
        {
            if (value.ToString().Length <= length + 5)
            {
                return value.ToString();
            }
            if (value == (int)value)
            {
                return value.ToString();
            }
            return value.ToString("e" + length.ToString());
        }

        public void setAxisValue(double[] array,double xBeg, double xEnd)
        {
            PointF gradPoint = new PointF();
            foreach (double value in array)
            {
                graphic.DrawLine(areaPen, pointO.X - (float)multiplyIndexX  + (float)(value) * (float)multiplyIndexX, pointO.Y - heightOfPartitions, pointO.X - (float)multiplyIndexX + (float)(value) * (float)multiplyIndexX, pointO.Y + heightOfPartitions);
                gradPoint.X = pointO.X- (float)multiplyIndexX + (float)(value) * (float)(multiplyIndexX);
                gradPoint.Y = pointO.Y - heightOfPartitions;
                if ((gradPoint.X != pointNegativeX.X) && (gradPoint.X != pointO.X) && (gradPoint.X != pointPositiveX.X))
                {
                    graphic.DrawString(doubleToStr(value, 2), font, solidBrush, gradPoint.X, gradPoint.Y + heightOfPartitions);
                }
            }

        }

        public void setAxisValueY(double[] array)
        {
            PointF gradPoint = new PointF();
            foreach (double value in array)
            {
                graphic.DrawLine(areaPen, pointO.X - heightOfPartitions, pointO.Y - (float)value * (float)multiplyIndexY, pointO.X + heightOfPartitions, pointO.Y - (float)value * (float)multiplyIndexY);
                gradPoint.X = pointO.X - heightOfPartitions;
                gradPoint.Y = pointO.Y - (float)value * (float)multiplyIndexY;
                if ((gradPoint.X != pointNegativeX.X) && (gradPoint.X != pointO.X) && (gradPoint.X != pointPositiveX.X))
                {
                    graphic.DrawString(doubleToStr(value, 2), font, solidBrush, gradPoint.X - doubleToStr(value, 2).Length * 6, gradPoint.Y);
                }
            }
        }

        public void setAxisValue(double[] array, double xBeg, double xEnd, Color gradsColor)
        {
            PointF gradPoint = new PointF();
            Pen gradPen = (gradsColor != null) ? new Pen(gradsColor) : areaPen;
            foreach (double value in array)
            {
                graphic.DrawLine(areaPen, pointO.X - (float)multiplyIndexX * (float)xBeg + (float)value * (float)multiplyIndexX, pointO.Y - heightOfPartitions, pointO.X - (float)multiplyIndexX * (float)xBeg + (float)value * (float)multiplyIndexX, pointO.Y + heightOfPartitions);
                gradPoint.X = pointO.X - (float)multiplyIndexX * (float)xBeg + (float)value * (float)(multiplyIndexX);
                gradPoint.Y = pointO.Y - heightOfPartitions;
                if ((gradPoint.X != pointNegativeX.X) && (gradPoint.X != pointO.X) && (gradPoint.X != pointPositiveX.X))
                {
                    graphic.DrawString(doubleToStr(value, 2), font, solidBrush, gradPoint.X, gradPoint.Y + heightOfPartitions);
                }
            }

        }

        private void showAxisValues(double xMin, double xMax, double yMin, double yMax)
        {
            graphic.DrawLine(areaPen, pointO.X, pointO.Y - heightOfPartitions, pointO.X, pointO.Y + heightOfPartitions);
            string value = doubleToStr(xMin, 2);
            graphic.DrawString(value, font, solidBrush, pointO.X, pointO.Y);
            if (pointNegativeX.X != pointO.X)
            {
                value = doubleToStr(xMin, 2);
                graphic.DrawLine(areaPen, pointNegativeX.X, pointNegativeX.Y - heightOfPartitions, pointNegativeX.X, pointNegativeX.Y + heightOfPartitions);
                graphic.DrawString(value, font, solidBrush, pointNegativeX.X, pointNegativeX.Y);
            }
            //if (pointNegativeY.Y != pointO.Y)
            //{
            //    value = doubleToStr(yMin, 2);
            //    graphic.DrawLine(areaPen, pointNegativeY.X - heightOfPartitions, pointNegativeY.Y, pointNegativeY.X + heightOfPartitions, pointNegativeY.Y);
            //    graphic.DrawString(value, font, solidBrush, pointNegativeY.X - value.Length * 6, pointNegativeY.Y);
            //}
            if (pointPositiveX.X != pointO.X)
            {
                value = doubleToStr(xMax, 2);
                graphic.DrawLine(areaPen, pointPositiveX.X, pointPositiveX.Y - heightOfPartitions, pointPositiveX.X, pointPositiveX.Y + heightOfPartitions);
                graphic.DrawString(value, font, solidBrush, pointPositiveX.X, pointPositiveX.Y);
            }
            if (pointPositiveY.Y != pointO.Y)
            {
                value = doubleToStr(yMax, 2);
                graphic.DrawLine(areaPen, pointPositiveY.X - heightOfPartitions, pointPositiveY.Y, pointPositiveY.X + heightOfPartitions, pointPositiveY.Y);
                graphic.DrawString(value, font, solidBrush, pointPositiveY.X - value.Length * 6, pointPositiveY.Y);
            }

        }

        private void drawAreaForFunction(function f, double xBeg, double xEnd)
        {
            double minY = minOfFunction(f, xBeg, xEnd, 1e-3);
            double maxY = maxOfFunction(f, xBeg, xEnd, 1e-3);

            pointO.X = (float)setStartingPointX(xBeg, xEnd);
            pointO.Y = (float)setStartingPointY(minY, maxY);

            pointNegativeX.X = widthOfNotGradedPart / 2;
            pointNegativeX.Y = pointO.Y;
            pointNegativeY.X = pointO.X;
            pointNegativeY.Y = areaHeight - widthOfNotGradedPart / 2;

            pointPositiveX.X = areaWidth - widthOfNotGradedPart / 2;
            pointPositiveX.Y = pointO.Y;
            pointPositiveY.X = pointO.X;
            pointPositiveY.Y = widthOfNotGradedPart / 2;

            graphic.DrawLine(areaPen, pointNegativeX, pointPositiveX);
            graphic.DrawLine(areaPen, pointNegativeY, pointPositiveY);

            multiplyIndexX = (double)(pointPositiveX.X - pointNegativeX.X) / (specialFunction(xBeg, xEnd));
            multiplyIndexX *= specialFunction(xBeg, xEnd) / (xEnd - xBeg);
            multiplyIndexY = (double)(pointNegativeY.Y - pointPositiveY.Y) / (specialFunction(minY, maxY));

            showAxisValues(xBeg, xEnd, minY, maxY);

            double[] arr = new double[1];
            arr[0] = minY;
            setAxisValueY(arr);

            picture.Image = bitMap;
        }
       

        public double MaxMinValueOfFunction(double begin, double end, bool Maxflag, MuFunc func,double [] Coefs)
        {
            double result;

            double h = 0.0001f;

            result = func(begin,f,Coefs);
            while (begin <= end)
            {

                if (Maxflag == true)
                {
                    result = Math.Max(result, func(begin, f, Coefs));
                }
                else
                {
                    result = Math.Min(result, func(begin, f, Coefs));
                }
                begin = begin + h;
            }
            return result;
        }

     

       
        double maxValue1 = 0;



        

        private void drawAreaForFunction(MuFunc mf, double xBeg, double xEnd, double[] Coefs, double xr)
        {
            double minY = MaxMinValueOfFunction(xBeg, xr, false, mf, Coefs);
            double maxY = MaxMinValueOfFunction(xBeg, xr, true, mf, Coefs);
            maxValue1 = maxY;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            pointO.X = (float)setStartingPointX(xBeg, xEnd);
            pointO.Y = (float)setStartingPointY(minY, maxY);

            pointNegativeX.X = widthOfNotGradedPart / 2;
            pointNegativeX.Y = pointO.Y;
            pointNegativeY.X = pointO.X;
            pointNegativeY.Y = areaHeight - widthOfNotGradedPart / 2;

            pointPositiveX.X = areaWidth - widthOfNotGradedPart / 2;
            pointPositiveX.Y = pointO.Y;
            pointPositiveY.X = pointO.X;
            pointPositiveY.Y = widthOfNotGradedPart / 2;

            graphic.DrawLine(areaPen, pointNegativeX, pointPositiveX);
            graphic.DrawLine(areaPen, pointNegativeY, pointPositiveY);

            multiplyIndexX = (double)(pointPositiveX.X - pointNegativeX.X) / (specialFunction(xBeg, xEnd));
            multiplyIndexX *= specialFunction(xBeg, xEnd) / (xEnd - xBeg);
            multiplyIndexY = (double)(pointNegativeY.Y - pointPositiveY.Y) / (specialFunction(minY, maxY));

            showAxisValues(xBeg, xEnd, minY, maxY);

            //double[] arr = new double[1];
            //arr[0] = minY;
            //setAxisValueY(arr);

            picture.Image = bitMap;
        }

        public void drawGraphOfFunction(function f, double xBeg, double xEnd, Color color)
        {
            if (xBeg > xEnd)
            {
                double tempValue = xBeg;
                xBeg = xEnd;
                xEnd = tempValue;
            }
            //xBeg = Math.Abs(xBeg);
            Pen graphicPen = new Pen(color);
            PointF p1;
            PointF p2;
            double distance = 1e-3;
            drawAreaForFunction(f, xBeg, xEnd);
            for (double x = xBeg; Math.Abs(xEnd - x) > distance; x += distance)
            {
                if (xBeg < 0 && xEnd > 0)
                {
                    p1 = new PointF((float)pointO.X + (float)(x) * (float)(multiplyIndexX), (float)pointO.Y - (float)(f(x)) * (float)multiplyIndexY);
                    p2 = new PointF((float)pointO.X + (float)(x + distance) * (float)(multiplyIndexX), (float)pointO.Y - (float)(f(x + distance)) * (float)multiplyIndexY);
            
                }
                else
                {
                    p1 = new PointF((float)pointO.X + (float)(x - xBeg) * (float)(multiplyIndexX), (float)pointO.Y - (float)(f(x)) * (float)multiplyIndexY);
                    p2 = new PointF((float)pointO.X + (float)(x - xBeg + distance) * (float)(multiplyIndexX), (float)pointO.Y - (float)(f(x + distance)) * (float)multiplyIndexY);
            
                }
                {
                    graphic.DrawLine(graphicPen, p1, p2);
                }
            }
        }

        
        bool pr = true;

        public void drawGraphOfFunction(MuFunc mf, double xBeg, double xEnd, Color color, double[] Coefs, function ff, double maxRightX, double delta,double minLeftX)
        {
            double maxY1;
            if (xBeg > xEnd)
            {
                double tempValue = xBeg;
                xBeg = xEnd;
                xEnd = tempValue;
            }
            Pen graphicPen = new Pen(color);
            PointF p1;
            PointF p2;
            double distance = 1e-3;
            if (pr)
            {
                drawAreaForFunction(mf, xBeg, maxRightX, Coefs, xEnd);
                pr = false;
            }
            else
            {
                 if ((xEnd == maxRightX))
                {
                    
                    maxY1 = MaxMinValueOfFunction(xBeg, xEnd, true, mf, Coefs);
                    if (maxValue1 / maxY1 >= 10)
                    {
                        multiplyIndexY *= (maxValue1 / maxY1) / (4.0); 
                    }
                    else
                    multiplyIndexY *= (maxValue1 / maxY1) / ((maxValue1 / maxY1));
                }
                else
                {
                    maxY1 = MaxMinValueOfFunction(xBeg, xEnd, true, mf, Coefs);
                    multiplyIndexY *= (maxValue1 / maxY1);
                }
            }

            for (double x = xBeg; Math.Abs(xEnd - x) > distance; x += distance)
            {

                if (minLeftX < 0)
                {
                    p1 = new PointF((float)pointO.X + (float)(x) * (float)multiplyIndexX, (float)pointO.Y - (float)(mf(x, ff, Coefs)) * (float)multiplyIndexY);
                    p2 = new PointF((float)pointO.X + (float)(x + distance) * (float)multiplyIndexX, (float)pointO.Y - (float)(mf(x + distance, ff, Coefs)) * (float)multiplyIndexY);

                }
                else
                {
                    p1 = new PointF((float)pointO.X + (float)(x-delta ) * (float)multiplyIndexX, (float)pointO.Y - (float)(mf(x, ff, Coefs)) * (float)multiplyIndexY);
                    p2 = new PointF((float)pointO.X + (float)(x - delta + distance) * (float)multiplyIndexX, (float)pointO.Y - (float)(mf(x + distance, ff, Coefs)) * (float)multiplyIndexY);
                }

                 
                {
                    graphic.DrawLine(graphicPen, p1, p2);
                }
            }
        }

        ////////////////////////////////////////////////////
        public void drawGraphOfFunction1(MuFunc mf, double xBeg, double xEnd, Color color, double[] Coefs, function ff, double maxRightX, double delta, double minLeftX,double fmax)
        {
            //double maxY1;
            if (xBeg > xEnd)
            {
                double tempValue = xBeg;
                xBeg = xEnd;
                xEnd = tempValue;
            }
            Pen graphicPen = new Pen(color);
            PointF p1;
            PointF p2;
            double distance = 1e-3;
            if (pr)
            {
                drawAreaForFunction1(mf, xBeg, maxRightX, Coefs, xEnd,fmax);
                pr = false;
            }
            //else
            //{
            //    if ((xEnd == maxRightX))
            //    {

            //        maxY1 = MaxMinValueOfFunction(xBeg, xEnd, true, mf, Coefs);
            //        if (maxValue1 / maxY1 >= 10)
            //        {
            //            multiplyIndexY *= (maxValue1 / maxY1) / (4.0);
            //        }
            //        else
            //            multiplyIndexY *= (maxValue1 / maxY1) / ((maxValue1 / maxY1));
            //    }
            //    else
            //    {
            //        maxY1 = MaxMinValueOfFunction(xBeg, xEnd, true, mf, Coefs);
            //        multiplyIndexY *= (maxValue1 / maxY1);
            //    }
            //}

            for (double x = xBeg; Math.Abs(xEnd - x) > distance; x += distance)
            {

                if (minLeftX < 0)
                //    delta = 0;
                {
                    p1 = new PointF((float)pointO.X + (float)(x) * (float)multiplyIndexX, (float)pointO.Y - (float)(mf(x, ff, Coefs)) * (float)multiplyIndexY);
                    p2 = new PointF((float)pointO.X + (float)(x + distance) * (float)multiplyIndexX, (float)pointO.Y - (float)(mf(x + distance, ff, Coefs)) * (float)multiplyIndexY);

                }
                else
                {
                    p1 = new PointF((float)pointO.X + (float)(x - delta) * (float)multiplyIndexX, (float)pointO.Y - (float)(mf(x, ff, Coefs)) * (float)multiplyIndexY);
                    p2 = new PointF((float)pointO.X + (float)(x - delta + distance) * (float)multiplyIndexX, (float)pointO.Y - (float)(mf(x + distance, ff, Coefs)) * (float)multiplyIndexY);
                }


                {
                    graphic.DrawLine(graphicPen, p1, p2);
                }
            }
        }
        ///////////////////////////////////////////////////
     //////////////////////////////////////////////
        private void drawAreaForFunction1(MuFunc mf, double xBeg, double xEnd, double[] Coefs, double xr,double fmax)
        {
            double minY = MaxMinValueOfFunction(xBeg, xr, false, mf, Coefs);
            double maxY = fmax;//MaxMinValueOfFunction(xBeg, xr, true, mf, Coefs);
            //maxValue1 = maxY;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            pointO.X = (float)setStartingPointX(xBeg, xEnd);
            pointO.Y = (float)setStartingPointY(minY, maxY);

            pointNegativeX.X = widthOfNotGradedPart / 2;
            pointNegativeX.Y = pointO.Y;
            pointNegativeY.X = pointO.X;
            pointNegativeY.Y = areaHeight - widthOfNotGradedPart / 2;

            pointPositiveX.X = areaWidth - widthOfNotGradedPart / 2;
            pointPositiveX.Y = pointO.Y;
            pointPositiveY.X = pointO.X;
            pointPositiveY.Y = widthOfNotGradedPart / 2;

            graphic.DrawLine(areaPen, pointNegativeX, pointPositiveX);
            graphic.DrawLine(areaPen, pointNegativeY, pointPositiveY);

            multiplyIndexX = (double)(pointPositiveX.X - pointNegativeX.X) / (specialFunction(xBeg, xEnd));
            multiplyIndexX *= specialFunction(xBeg, xEnd) / (xEnd - xBeg);
            multiplyIndexY = (double)(pointNegativeY.Y - pointPositiveY.Y) / (specialFunction(minY, maxY));

            showAxisValues(xBeg, xEnd, minY, maxY);

            //double[] arr = new double[1];
            //arr[0] = minY;
            //setAxisValueY(arr);

            picture.Image = bitMap;
        }
     /////////////////////////////////////////////

    }

    
}
