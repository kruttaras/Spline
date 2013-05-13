using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.IO;
using CourseProj.MyClasses;

namespace CourseProj.MyClasses
{
    class UniformApproximation
    {
        private double a;
        private double b;
        private int PowerOfPolinom;
        private bool GausFlag;
        public double e;
        public delegate double function(double x);
        private Label Error;
        private Label Error1;
        public PictureBox FuncGraph;
        public PictureBox MuFuncGraph;
        private FileStream ResOut;
        private FileStream DiscrOut;
        private PictureBox MuCheckGraph;
        private FileStream CheckOut;
        

        public UniformApproximation(double StartPoint,double EndPoint,int power,bool flag,double eps,PictureBox fpbox,PictureBox mupbox,Label err,RichTextBox discrepancy,PictureBox mucheckpbox,Label err1)
        {
            a = StartPoint;
            b = EndPoint;
            PowerOfPolinom=power;
            GausFlag = flag;
            e = eps;
            FuncGraph = fpbox;
            MuFuncGraph = mupbox;
            MuCheckGraph = mucheckpbox;
            Error = err;
            Error1 = err1;
            ResOut = new FileStream("Балансне наближення.txt",FileMode.Create);
            DiscrOut = new FileStream("Вектор нев'язки.txt",FileMode.Create);
            CheckOut = new FileStream("Наближення при розбитті інтервалу на рівні частини.txt",FileMode.Create);
        }

        public double[] Discrepancy(int size, double[,] CoefsMatrix,double[] FreeMembers,double[] Solution)
        {
            double[] Discrepancy = new double[size];
            for (int i = 0; i < size; i++)
            {
                double sum = 0;
                for (int j = 0; j < size; j++)
                {
                    sum += CoefsMatrix[i, j] * Solution[j];
                }
                Discrepancy[i] = sum - FreeMembers[i];
            }
                return Discrepancy;
        }

        public double[] CoefFind(int PolinomPow,bool GausFlag,double x0, double x1,function f,function fp,function fpp,out double[] discr)
        {
            int size=0,IterationCounter;
            double[,] Matrix=new double[1,1];
            double[] Vector = new double[1];
            switch (PolinomPow)
            {
                case 3:
                    {   
                        size = PolinomPow+1;
                        Matrix = new double[,]{{3.0*Math.Pow(x1,2.0), 2.0*x1,           1.0, 0.0},
                                               {Math.Pow(x1,3.0),     Math.Pow(x1,2.0), x1,  1.0},                       
                                               {3.0*Math.Pow(x0,2.0), 2.0*x0,           1.0, 0.0},
                                               {Math.Pow(x0,3.0),     Math.Pow(x0,2.0), x0,  1.0}};
                        Vector = new double[] { fp(x1),f(x1), fp(x0), f(x0)};
                        break;
                    }
                case 4:
                    {
                      double xs = (x0 + x1 )/ 2;
                      size = PolinomPow + 1;
                      Matrix = new double[,] {{4*Math.Pow(x1,3),3*Math.Pow(x1,2),2*x1,1.0,0.0},
                                              {Math.Pow(x1,4),Math.Pow(x1,3),Math.Pow(x1,2),x1,1.0},
                                              {Math.Pow(xs,4),Math.Pow(xs,3),Math.Pow(xs,2),xs,1.0},
                                              {4*Math.Pow(x0,3),3*Math.Pow(x0,2),2*x0,1.0,0.0},
                                              {Math.Pow(x0,4),Math.Pow(x0,3),Math.Pow(x0,2),x0,1.0}};
                      Vector = new double[] { fp(x1), f(x1),  f(xs), fp(x0),f(x0)};
                      break;
                    }
                case 5:
                    {
                        size = PolinomPow + 1;
                        Matrix = new double[,] {{20*Math.Pow(x1,3),12*Math.Pow(x1,2),6*x1,2.0,0.0,0.0},
                                                {5*Math.Pow(x1,4),4*Math.Pow(x1,3),3*Math.Pow(x1,2),2*x1,1.0,0.0}, 
                                                {Math.Pow(x1,5),Math.Pow(x1,4),Math.Pow(x1,3),Math.Pow(x1,2),x1,1.0},
                                                {20*Math.Pow(x0,3),12*Math.Pow(x0,2),6*x0,2.0,0.0,0.0},
                                                {5*Math.Pow(x0,4),4*Math.Pow(x0,3),3*Math.Pow(x0,2),2*x0,1.0,0.0},
                                                {Math.Pow(x0,5),Math.Pow(x0,4),Math.Pow(x0,3),Math.Pow(x0,2),x0,1.0}};
                        Vector = new double[] { fpp(x1), fp(x1), f(x1), fpp(x0), fp(x0), f(x0) };
                        break;

                    }

            }
            double[] Coefficients = new double[size];
            if (GausFlag == true)
            {
                Gaus Gob = new Gaus();
                Coefficients=Gob.GausMethod(size, Matrix, Vector, out IterationCounter);
            }
            else
            {
                Zeydel Zob = new Zeydel();
                Coefficients = Zob.ZeydMethod(size, Matrix, Vector, e, out IterationCounter);
            }
      
                discr=Discrepancy(size, Matrix, Vector, Coefficients);
                return Coefficients;
        }


        public double Splain(double[] Coef, double x,int power)
        {
            double S=0;
            switch(power)
            {
                case 3:
                    {
                        S=Coef[0] * Math.Pow(x, 3) + Coef[1] * Math.Pow(x, 2) + Coef[2] * x + Coef[3];
                        break;
                    }
                case 4:
                    {
                        S = Coef[0] * Math.Pow(x, 4) + Coef[1] * Math.Pow(x, 3) + Coef[2] * Math.Pow(x, 2) + Coef[3] * x + Coef[4];
                        break;
                    }
                case 5:
                    {
                        S = Coef[0] * Math.Pow(x, 5) + Coef[1] * Math.Pow(x, 4) + Coef[2] * Math.Pow(x, 3) + Coef[3] * Math.Pow(x, 2) + Coef[4] * x + Coef[5];
                        break;
                    }

            }
            return S;
        }

        public double Ro(double x, Draw.function f, double[] Coefficients)
        {
            return Math.Abs( f(x) -  Splain(Coefficients, x, PowerOfPolinom)); 
        }

        public double muFindingFunction(double a, double b,function f,function fp,function fpp)
        {
            
            double mu = 0;
            double[] d;
            double[] Coefs=CoefFind(PowerOfPolinom, GausFlag, a, b, f, fp,fpp,out d);
            double h = (b - a) / Convert.ToDouble(100);
            do
            {

                mu = Math.Max(mu, Math.Abs(f(a) - Splain(Coefs, a,PowerOfPolinom)));
                a = a + h;

            } while (a <= b);
            return mu;
        }

        public void Approximate(double a, double b, double MuConst, function f, function fp, function fpp, RichTextBox richTextBox, RichTextBox DiscrepancyRichTextBox,RichTextBox CheckTextBox,string FuncName)
        {
            string CoefsMethodName;
            if (GausFlag)
                CoefsMethodName = "Гауса";
            else
                CoefsMethodName = "Зейделя";
            Error.Text = "";
            Error1.Text = "";
            MuFuncGraph.Show();
            MuCheckGraph.Show();
            double[] Coefs=new double[PowerOfPolinom+1];
            Draw dr1 = new Draw(MuFuncGraph.Width, MuFuncGraph.Height, Color.Black, MuFuncGraph, PowerOfPolinom, Coefs, new Draw.function(f), new Draw.function(fp), new Draw.function(fpp));
            Draw dr2 = new Draw(MuCheckGraph.Width, MuCheckGraph.Height, Color.Black, MuCheckGraph, PowerOfPolinom, Coefs, new Draw.function(f), new Draw.function(fp), new Draw.function(fpp));
            double start = a;
            Draw dr = new Draw(FuncGraph.Width, FuncGraph.Height, Color.Black, FuncGraph, PowerOfPolinom, Coefs, new Draw.function(f), new Draw.function(fp), new Draw.function(fpp));
            double xl = a;
            double xr = b;
            double xrtemp = 0;
            double xmid;
            double MuPrev;
            double MuNext = 0;
            double deltax;
            double[] discrepancy;
            int LankaCount = 0;
            bool noError=false; 
            StreamWriter fstr_out = new StreamWriter(ResOut);
            StreamWriter disc_out = new StreamWriter(DiscrOut);
            StreamWriter check_out = new StreamWriter(CheckOut);
            richTextBox.AppendText("Результати балансного наближення:");
            disc_out.Write("Вектор нев'язки при обчисленні коефіцієнтів \r\nмногочленного ермітового сплайна "+PowerOfPolinom.ToString()+"-го степеня "+"методом "+CoefsMethodName+":\r\n");
            fstr_out.Write("Результати балансного наближення функції "+FuncName+"\r\nмногочленним ермітовим сплайном "+PowerOfPolinom.ToString()+"-го степеня"+"\r\nна інтервалі ["+a.ToString()+","+b.ToString()+"] з похибкою наближення "+MuConst.ToString());
            MuPrev = muFindingFunction(xl, xr,f,fp,fpp);
            dr.drawGraphOfFunction(new Draw.function(f), a, b, Color.Red);
            if (MuPrev < MuConst)
            {
                
                fstr_out.Write("\r\nxl=" + xl.ToString() + "\r\nxr=" + xr.ToString());
                richTextBox.AppendText("\nxl=" + xl + "\nxr=" + xr);
                Coefs = CoefFind(PowerOfPolinom, GausFlag, xl, xr, f, fp,fpp,out discrepancy);
                DiscrepancyRichTextBox.AppendText("\nxl=" + xl + "\nxr=" + xr + "\nВектор нев'язки:\n");
                check_out.Write("\rxl="+xl.ToString()+"\rxr="+xr.ToString()+"\rMu="+MuPrev.ToString());
                for (int i = 0; i <= PowerOfPolinom; i++)
                {
                    richTextBox.AppendText("\na" + i + "=" + Coefs[i]);
                    DiscrepancyRichTextBox.AppendText(discrepancy[i].ToString() + "\n");
                    fstr_out.Write("\r\na" + i + "=" + Coefs[i].ToString());
                    disc_out.Write(discrepancy[i].ToString() + "\r\n");
                }
               richTextBox.AppendText("\nMu=" + MuPrev.ToString());
               fstr_out.Write("\r\nMu=" + MuPrev.ToString());
               
               if (MuPrev <= 9e-12)
               {
                   MuFuncGraph.Hide();
                   Error.Text = "Похибка відсутня";
                   noError = true;
               }
               else
               {
                   dr1.drawGraphOfFunction(new Draw.MuFunc(Ro), xl, xr, Color.Gray, Coefs, new Draw.function(f), b,xl,a);
                   double[] arr12 = new double[1];
                   arr12[0] = xl;
                   dr1.setAxisValue(arr12,xl,xr);
                   noError = false;
               }
               
               
            }
            else
            {
                while (true)
                {
                    while (MuPrev > MuConst)
                    {
                        xmid = (xl + xr) / 2;
                        MuNext = muFindingFunction(xl, xmid,f,fp,fpp);
                        if (MuNext < MuConst & MuPrev > MuConst)
                        {
                            xrtemp = (xmid + xr) / 2.0;
                            MuPrev = muFindingFunction(xl, xrtemp,f,fp,fpp);
                            while (Math.Abs(MuConst - MuPrev) / MuConst > 0.01)
                            {

                                if (MuPrev > MuConst)
                                {
                                    xr = xrtemp;
                                    xrtemp = (xmid + xr) / 2.0;
                                    MuPrev = muFindingFunction(xl, xrtemp, f, fp, fpp);
                                }
                                else
                                {
                                    xmid = xrtemp;
                                    xrtemp = (xrtemp + xr) / 2.0;
                                    MuPrev = muFindingFunction(xl, xrtemp, f, fp, fpp);

                                }

                            }
                            xr = xrtemp;

                        }
                        else
                        {

                            xr = (xl + xr) / 2;
                        

                        }
                    
                        MuPrev = muFindingFunction(xl, xr, f, fp, fpp);
                    }
                    LankaCount++;
                    richTextBox.AppendText("\n---------------------------" + "\n Ланка #" + LankaCount.ToString() + " :");
                    fstr_out.Write("\r\n---------------------------" + "\r\n Ланка #" + LankaCount.ToString() + " :");
                    richTextBox.AppendText("\nxl=" + xl + "\nxr=" + xr);
                    fstr_out.Write("\r\nxl=" + xl.ToString() + "\r\nxr=" + xr.ToString());
                    Coefs = CoefFind(PowerOfPolinom, GausFlag, xl, xr, f, fp, fpp,out discrepancy);
                    deltax = xr - xl;
                    if (LankaCount == 1)
                    {
                        dr1.drawGraphOfFunction(new Draw.MuFunc(Ro), xl, xr, Color.Gray, Coefs, new Draw.function(f), b,Math.Abs(xl),a);
                        double[] arr111 = new double[1];
                        arr111[0] = xl;
                        dr1.setAxisValue(arr111, xl, xr);

                    }
                    else  
                    dr1.drawGraphOfFunction(new Draw.MuFunc(Ro), xl, xr, Color.Gray, Coefs, new Draw.function(f),b,Math.Abs(a),a);
                    double[] arr11 = new double[1];
                    arr11[0] = xl;
                    dr1.setAxisValue(arr11,xl,xr);
                    DiscrepancyRichTextBox.AppendText("---------------------------" + "\n Ланка #" + LankaCount.ToString() +  ":\nxl=" + xl + "\nxr=" + xr+"\nВектор нев'язки:\n");
                    disc_out.Write("---------------------------" + "\r\n Ланка #" + LankaCount.ToString() + " :\r\nxl=" + xl + "\r\nxr=" + xr+"\r\nВектор нев'язки:\r\n");
                    for (int i = 0; i <= PowerOfPolinom; i++)
                    {
                        richTextBox.AppendText("\na" + i + "=" + Coefs[i]);
                        fstr_out.Write("\r\na" + i.ToString() + "=" + Coefs[i].ToString());
                        DiscrepancyRichTextBox.AppendText(discrepancy[i].ToString() + "\n");
                        disc_out.Write(discrepancy[i].ToString() + "\r\n");
                    }
                    richTextBox.AppendText("\nMu=" + MuPrev.ToString());
                    
                    richTextBox.AppendText("\ndeltax=" + deltax.ToString());
                    fstr_out.Write("\r\nMu=" + MuPrev.ToString() + "\r\ndeltax=" + deltax.ToString());
                    xl = xr;
                    xr = b;
                    if (xl == xr)
                        break;
                    MuPrev = muFindingFunction(xl, xr, f, fp, fpp);
                    if(((xr-xl)<=deltax)&&(MuPrev<=MuConst))
                    {
                        LankaCount++;
                        richTextBox.AppendText("\n---------------------------" + "\n Ланка #" + LankaCount.ToString() + " :");
                        fstr_out.Write("\r\n---------------------------" + "\r\n Ланка #" + LankaCount.ToString() + " :");
                        richTextBox.AppendText("\nxl=" + xl + "\nxr=" + xr);
                        fstr_out.Write("\r\nxl=" + xl.ToString() + "\r\nxr=" + xr.ToString());
                        Coefs = CoefFind(PowerOfPolinom, GausFlag, xl, xr, f, fp, fpp, out discrepancy);
                        deltax = xr - xl;
                        dr1.drawGraphOfFunction(new Draw.MuFunc(Ro), xl, xr, Color.Gray, Coefs, new Draw.function(f), b, Math.Abs(a),a);
                        double[] arr1 = new double[1];
                        arr1[0] = xl;
                        dr1.setAxisValue(arr1,xl,xr);
                        DiscrepancyRichTextBox.AppendText("---------------------------" + "\n Ланка #" + LankaCount.ToString() + ":\nxl=" + xl + "\nxr=" + xr+"\nВектор нев'язки:\n");
                        disc_out.Write("---------------------------" + "\r\n Ланка #" + LankaCount.ToString() + " :\r\nxl=" + xl + "\r\nxr=" + xr+"\r\nВектор нев'язки:\r\n");
                        for (int i = 0; i <= PowerOfPolinom; i++)
                        {
                            richTextBox.AppendText("\na" + i + "=" + Coefs[i]);
                            DiscrepancyRichTextBox.AppendText(discrepancy[i].ToString() + "\n");
                            fstr_out.Write("\r\na" + i.ToString() + "=" + Coefs[i].ToString());
                            disc_out.Write(discrepancy[i].ToString() + "\r\n");
                        }
                        richTextBox.AppendText("\nMu=" + MuPrev.ToString());
                        
                        richTextBox.AppendText("\ndeltax=" + deltax.ToString());
                         fstr_out.Write("\r\nMu=" + MuPrev.ToString() + "\r\ndeltax=" + deltax.ToString());
                      
                        break;
                    }

                }
            }

            check_out.Write("Результати наближення функції " + FuncName + "\r\nмногочленним ермітовим сплайном " + PowerOfPolinom.ToString() + "-го степеня" + " при розбитті \r\nінтервалу [" + a.ToString() + "," + b.ToString() + "] " + "на рівні частини(к-сть частин = " + LankaCount.ToString() + "):");
            double MuForCheck=0;
            if (LankaCount == 0)
                LankaCount++;
            double h = (b - a) / LankaCount;
            int count=1;
            double max = 0;
            double[] Yarray = new double[LankaCount];
            double p1=0, p2=0;
            CheckTextBox.AppendText("Результати наближення при розбитті інтервалу на рівні частини:");
            int lanka=0;
            for (double point = a; point < b; point += h)
            {
                MuForCheck = muFindingFunction(point, point + h, f, fp, fpp);
                if (MuForCheck > max)
                {
                    max = MuForCheck;
                    p1 = point;
                    p2 = point + h;
                }
                lanka++;

            }
            for (double point = a; point <b; point += h)
            {
                MuForCheck=muFindingFunction(point,point+h,f,fp,fpp);
                Coefs = CoefFind(PowerOfPolinom, GausFlag, point, point+h, f, fp, fpp, out discrepancy);
                CheckTextBox.AppendText("\n------------------\nЛанка#"+(count).ToString()+"\nxl="+point.ToString()+"\nxr="+(point+h).ToString()+"\nMu="+MuForCheck.ToString());
                check_out.Write("\r\n------------------\r\nЛанка#" + (count).ToString() + "\r\nxl=" + point.ToString() + "\r\nxr=" + (point + h).ToString() + "\r\nMu=" + MuForCheck.ToString());
                count ++;
                if (noError)
                {
                    MuCheckGraph.Hide();
                    Error1.Text = "Похибка відсутня";

                }
                else
                {
                    dr2.drawGraphOfFunction1(new Draw.MuFunc(Ro), point, point + h, Color.Brown, Coefs, new Draw.function(f), b, Math.Abs(a), a, max);
                    double[] arr0 = new double[1];
                    arr0[0] = point;
                    dr2.setAxisValue(arr0, point, point + h);
                }
                
            }
           
                fstr_out.Close();
                disc_out.Close();
                check_out.Close();
        }

    }
}
