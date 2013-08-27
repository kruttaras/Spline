using NCalc;
using ZedGraph;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Spline.Models;

namespace Spline
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private Expression expression;
        private double Mu=0.005;
        private AppMath.BaseFunc Function;
        private IList<Section> section=new List<Section>();
        public MainForm()
        {
         
            InitializeComponent();

            comboBox1.Items.AddRange(FunctionUtil.GetComboboxItemsWithFunctions());
            comboBox1.SelectedIndex = 0;
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "Графік функції та наближення";
            GraphPane pane2 = zedGraphControl2.GraphPane;
            pane2.Title.Text = "Графік функції похибки";
        }

        protected double[] exp(AppMath.BaseFunc F, double x0, double x1)
        {
            double[] a = new double[4];
            double x0_x1=(x0 - x1);
            double LogF0_F1=Math.Log(F.Val(x0) / F.Val(x1));
            

            double d = a[3] = (F.Diff(x1) / F.Val(x1) + F.Diff(x0) / F.Val(x0)+2*LogF0_F1/(x1-x0))/Math.Pow(-1*x0_x1,2);

            double c = a[2] = ((2 * x0 * x0 - x0 * x1 - x1 * x1) * a[3] - F.Diff(x0) / F.Val(x0) - LogF0_F1 / (-1 * x0_x1)) / (-1 * x0_x1);

            double b = a[1] = 1 / (x1 - x0) * (Math.Log(F.Val(x1) / F.Val(x0)) - a[3] * (Math.Pow(x1, 3) - Math.Pow(x0, 3)) - c * (Math.Pow(x1, 2) - Math.Pow(x0, 2)));
            //A
            a[0] = F.Val(x0) * Math.Exp(-1*(d * Math.Pow(x0, 3) + c * Math.Pow(x0, 2) + b * x0));

            return  a;
        }

        protected double AproximFunc(double x, double[] coef)
        {
            return (coef[0] * Math.Exp(coef[1] * x + coef[2] * Math.Pow(x, 2) + coef[3] * Math.Pow(x, 3)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            section = new List<Section>();
          
            Function = ((ComboBoxItem)comboBox1.SelectedItem).GetFunction();

            double xmin = Convert.ToDouble(textBox5.Text);
            double xmax = Convert.ToDouble(textBox6.Text);
            Mu = Convert.ToDouble(textBox7.Text);

              GraphPane pane = zedGraphControl1.GraphPane;
              GraphPane pane2 = zedGraphControl2.GraphPane;
              pane2.CurveList.Clear();
              zedGraphControl2.ZoomOutAll(pane2);
              zedGraphControl1.ZoomOutAll(pane);
              pane.CurveList.Clear();
              richTextBox1.Text = "";
              
              
                PointPairList list = new PointPairList();
                PointPairList list_aprox = new PointPairList();


                list = FunctionUtil.GetPointPairsInRange(xmin, xmax, Function);     

                PointPairList list_1 = new PointPairList();
                PointPairList aprox = new PointPairList();

                    Compute(xmin, xmax);
                    for (int i = 0; i < section.Count; i++)
                    {
                        for (double x = section[i].LeftPoint; x <= section[i].RightPoint; x += 0.001)
                        {

                            double fx = AproximFunc(x, section[i].Coef);

                            aprox.Add(x, fx);

                        }

                        richTextBox1.Text += "Ланка # " + (i + 1) + "\n\nxL= " + section[i].LeftPoint
                            +"\nxR= " + section[i].RightPoint + "\nMu= "
                            + section[i].Mu + "\n\n" + "Коефіцієнти\n\n";
                        for (int ii = 0; ii < section[i].Coef.Length; ii++)
                        {
                            richTextBox1.Text += "a["+ii+"]= "+section[i].Coef[ii]+"\n";

                        }
                        richTextBox1.Text += "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"+"\n\n";

                    }

                 
                LineItem myCurve = pane.AddCurve(Function.Text, list, Color.Blue, SymbolType.None);
                LineItem myCurve2 = pane.AddCurve("aprox", aprox, Color.Red, SymbolType.None);
          
                pane.XAxis.MajorGrid.IsVisible = true;
                pane.YAxis.MajorGrid.IsVisible = true;
         
                zedGraphControl1.AxisChange();

               
                zedGraphControl1.Invalidate();
                list_1.Clear();


                foreach (Section sec in section)
                {
                    double[] coef = exp(Function, sec.LeftPoint, sec.RightPoint);

                    for (double x = sec.LeftPoint; x <= sec.RightPoint; x += 0.001)
                    {

                        double fx = Math.Abs(Function.Val(x) - AproximFunc(x, coef));

                        list_1.Add(x, fx);

                    }
                }
             
                LineItem newCurves = pane2.AddCurve("Ro", list_1, Color.Blue, SymbolType.None);
                list_1 = new PointPairList();
                list_1.Add(xmin,Mu);
                list_1.Add(xmax,Mu);
                newCurves = pane2.AddCurve("Mu", list_1, Color.Red, SymbolType.None);
      
                pane2.XAxis.MajorGrid.IsVisible = true;
                pane2.YAxis.MajorGrid.IsVisible = true;
                pane2.YAxis.Scale.Max = 2 * Mu;
                pane2.YAxis.Scale.Min = 0;
                pane2.XAxis.Scale.Max = xmax;
                pane2.XAxis.Scale.Min = xmin;
    
                zedGraphControl2.AxisChange();

                zedGraphControl2.Invalidate();
        }


        private void Compute(double a, double b)
        {
            double zl, zp,xmid,xtemp;
            zl = a;
            zp=b;
            
            double nextMu, prevMu;
            prevMu = findMu(zl, zp, Function);
            while(findMu(zl,b,Function)>Mu){
            
            xmid = (zl + zp) / 2;
            nextMu=findMu(zl,xmid,Function);
            if(nextMu<Mu && prevMu>Mu )
            {
                xtemp=(xmid+zp)/2.0;
                prevMu=findMu(zl,xtemp,Function);
                while(Math.Abs(Mu-prevMu)/Mu>0.01 || prevMu>Mu)
                {
                    if(prevMu>Mu)
                    {
                        zp=xtemp;
                        xtemp=(xmid+zp)/2.0;
                    }
                    else
                    {
                        xmid=xtemp;
                        xtemp=(xtemp+zp)/2.0;  
                    }
                    prevMu=findMu(zl,xtemp,Function);


                }

                zp=xtemp;
                section.Add(new Section(zl, zp,exp(Function,zl,zp),prevMu));
                zl=zp;
                zp=b;
                
            }
            else
            {
                zp=(zl+zp)/2;
            }
            prevMu=findMu(zl,zp,Function);

            }
            section.Add(new Section(zl, b,exp(Function,zl,b),prevMu));
        }
       
        private double findMu(double xl,double xr,AppMath.BaseFunc Function)
        {
            double[] coef = exp(Function, xl, xr);
            double h = (xr - xl) / Convert.ToDouble(100);
                
                double Mu = 0,fx;
                for (double x = xl; x <= xr; x += h)
                {

                    fx = Math.Abs(Function.Val(x) - AproximFunc(x, coef));
                    if (fx > Mu)
                    {
                        Mu = fx;
                    }
                }
            return Mu;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
           Function=(AppMath.BaseFunc) comboBox1.SelectedValue;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
