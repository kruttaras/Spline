using System.Drawing.Drawing2D;
using NCalc;
using Spline.Properties;
using ZedGraph;

using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Spline.Models;
using Spline.App.Utils;
using Spline;

namespace Spline
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private Expression expression;
        private double Mu=0.005;
        int R;
        private AppMath.BaseFunc Function;
        private IList<Section> section=new List<Section>();
        public MainForm()
        {
         
            InitializeComponent();

            comboBox1.Items.AddRange(AppUtils.GetComboboxItemsWithFunctions());
            comboBox2.Items.AddRange(AppUtils.GetComboboxItemsWithAproximatingFunctions());
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = Resources.FunctionChartTitle;
            GraphPane pane2 = zedGraphControl2.GraphPane;
            pane2.Title.Text = Resources.SplineChartTitle;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            section = new List<Section>();
          
            Function = ((ComboBoxItem)comboBox1.SelectedItem).GetFunction();
            AproximatingFunction aproxFunction = ((ComboBoxItem)comboBox2.SelectedItem).GetAproximatingFunction();

            double xmin = Convert.ToDouble(textBox5.Text);
            double xmax = Convert.ToDouble(textBox6.Text);
            Mu = Convert.ToDouble(textBox7.Text);
            if (radioButton4.Checked)
            {
                R = Convert.ToInt32(textBox1.Text);
            }

              GraphPane pane = zedGraphControl1.GraphPane;
              GraphPane pane2 = zedGraphControl2.GraphPane;
              pane2.CurveList.Clear();
              zedGraphControl2.ZoomOutAll(pane2);
              zedGraphControl1.ZoomOutAll(pane);
              pane.CurveList.Clear();
              richTextBox1.Text = "";
              
              
                PointPairList list = new PointPairList();
                PointPairList list_aprox = new PointPairList();


                list = AppUtils.GetPointPairsInRange(xmin, xmax, Function);     

                PointPairList list_1 = new PointPairList();
                PointPairList aprox = new PointPairList();
           
            List<Section> result ;

            if (radioButton4.Checked)
            {
                double muPlus = 0, muMinus = 0;
                int K;

                do
                {
                    section = ZzadanPohubkou.Compute(xmin, xmax, Function, aproxFunction, Mu);
                    K = section.Count;
                    if (K > R)
                    {
                        muMinus = Mu;
                        if (muPlus != 0)
                        {
                            Mu = (Mu + muPlus) / 2;

                            if (Equals(Mu, muMinus))
                            {
                                K = R;
                            }

                        }
                        else
                        {
                            Mu *= 1.1;
                        }

                    }
                    if (K < R || (R == K && (Mu - section[K - 1].Mu) / Mu > 0.01))
                    {
                        muPlus = Mu;
                        K = -1;
                       if (muMinus != 0)
                            {
                                Logger.Info("Start computing Mu in zadana k-t lanok # Mu=" + Mu + "and muMinus=" + muMinus, "Spline");
                                Mu = (Mu + muMinus) / 2.0d;
                                Logger.Info("Result # Mu=" + Mu, "Spline");

                                if (Equals(Mu, muPlus))
                                {
                                    K = R;
                                }

                            }
                            else
                            {
                                Mu *= 0.9;
                            }
                        
                        
                    }

                } while (R != K);
            }
            else
            {
                section = ZzadanPohubkou.Compute(xmin, xmax, Function, aproxFunction, Mu);
                
            }
            richTextBox1.Text += "Ланок побудовано: " + section.Count +"\n";
            richTextBox1.Text += "_______________________________________________\n";
                    for (int i = 0; i < section.Count; i++)
                    {
                        for (double x = section[i].LeftPoint; x <= section[i].RightPoint; x += 0.001)
                        {

                            double fx = aproxFunction.GetAproximatingFunction(x, section[i].Coef);

                            aprox.Add(x, fx);

                        }

                        richTextBox1.Text += "Ланка # " + (i + 1) + "\nxL= " + section[i].LeftPoint
                            +"\nxR= " + section[i].RightPoint + "\nMu= "
                            + section[i].Mu + "\n\n" + "Коефіцієнти\n";
                        for (int ii = 0; ii < section[i].Coef.Length; ii++)
                        {
                            richTextBox1.Text += "a["+ii+"]= "+section[i].Coef[ii]+"\n";

                        }
                        richTextBox1.Text += Resources.Separetor+"\n";

                    }

                 
                LineItem myCurve = pane.AddCurve(Resources.ChartFunctionName, list, Color.Blue, SymbolType.None);
                LineItem myCurve2 = pane.AddCurve(Resources.ChartSplineName, aprox, Color.Red, SymbolType.None);
          
                pane.XAxis.MajorGrid.IsVisible = true;
                pane.YAxis.MajorGrid.IsVisible = true;
         
                zedGraphControl1.AxisChange();

               
                zedGraphControl1.Invalidate();
                list_1.Clear();

                Logger.Info("Mu  function max value of each section for function " + Function.ToString(), "MainForm");
                int LogCounter = 1;
                foreach (Section sec in section)
                {

                    double[] coef = sec.Coef;
                    double max = 0;
                    for (double x = sec.LeftPoint; x <= sec.RightPoint; x += 0.001)
                    {

                        double fx = Math.Abs(Function.Val(x) - aproxFunction.GetAproximatingFunction(x, coef));
                        if (fx > max) max = fx;
                        list_1.Add(x, fx);

                    }
                    Logger.Info("Section#" + LogCounter++ + "Max value = " + max, "MainForm");
                }

                LineItem newCurves = pane2.AddCurve("Ro", list_1, Color.Blue, SymbolType.None);
                list_1 = new PointPairList();
          /*      for (double i = xmin; i < xmax; i+=Math.Abs(xmax - xmin)/20)
            {
                list_1.Add(i, Mu);   
            }*/
                list_1.Add(xmin,Mu);
                list_1.Add(xmax,Mu);
                newCurves = pane2.AddCurve("Mu", list_1, Color.Red);
            newCurves.Line.Style =DashStyle.Custom;
            newCurves.Line.Width = 2;
            newCurves.Line.DashOn = 5;
            newCurves.Line.DashOff = 8;
            newCurves.Symbol.Size = 0;
      
                pane2.XAxis.MajorGrid.IsVisible = true;
                pane2.YAxis.MajorGrid.IsVisible = true;
                pane2.YAxis.Scale.Max = 2 * Mu;
                pane2.YAxis.Scale.Min = 0;
                pane2.XAxis.Scale.Max = xmax;
                pane2.XAxis.Scale.Min = xmin;
    
                zedGraphControl2.AxisChange();

                zedGraphControl2.Invalidate();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            AppMath.BaseFunc SelectedFunction = ((ComboBoxItem)comboBox1.SelectedItem).GetFunction();
            if (SelectedFunction.GetNumberOfParametrs() > 0)
            {
                ParametrsForm parametrsForm = new ParametrsForm(this, SelectedFunction.GetNumberOfParametrs());
                parametrsForm.Show();
                
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                label5.Text = "Похибка";
                textBox1.ReadOnly = true;
                textBox1.Text = "";
                label1.ForeColor = System.Drawing.Color.DarkGray;

            }
            else
            {
                label5.Text = "Точність побудови";
                textBox1.ReadOnly = false;
                label1.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void comboBox2_VisibleChanged(object sender, EventArgs e)
        {
         
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }


       
    }
}
