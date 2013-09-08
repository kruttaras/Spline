using NCalc;
using ZedGraph;

using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Spline.Models;
using Spline.App.Utils;
using Spline.App.Models;

namespace Spline
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private Expression expression;
        private double Mu=0.005;
        double R;
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

        private void button1_Click(object sender, EventArgs e)
        {
            section = new List<Section>();
          
            Function = ((ComboBoxItem)comboBox1.SelectedItem).GetFunction();

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


                list = FunctionUtil.GetPointPairsInRange(xmin, xmax, Function);     

                PointPairList list_1 = new PointPairList();
                PointPairList aprox = new PointPairList();
           
            ZzadanPohubkou result ;

            if (radioButton4.Checked)
            {
                double muPlus = 0, muMinus = 0;
                int K;

                do
                {
                    result = new ZzadanPohubkou(xmin, xmax, Function, Mu);
                    result.Compute();
                    K = result.Section.Count;
                    if (K > R)
                    {
                        muMinus = Mu;
                        if (muPlus != 0)
                        {
                            Mu = (Mu + muPlus) / 2.0;

                        }
                        else
                        {
                            Mu *= 1.1;
                        }

                    }
                    if (K < R || (R == K && (Mu - result.Section[K - 1].Mu) / Mu > 0.01))
                    {
                        K = -1;
                        muPlus = Mu;
                        if (muMinus != 0)
                        {
                            Mu = (Mu + muMinus) / 2.0;
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
                result = new ZzadanPohubkou(xmin, xmax, Function, Mu);
                result.Compute();
            }
                section = result.Section;

                    for (int i = 0; i < section.Count; i++)
                    {
                        for (double x = section[i].LeftPoint; x <= section[i].RightPoint; x += 0.001)
                        {

                            double fx = ExponencialSpline.AproximFunc(x, section[i].Coef);

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

                Logger.Info("Mu  function max value of each section for function " + Function.ToString(), "MainForm");
                int LogCounter = 1;
                foreach (Section sec in section)
                {

                    double[] coef = sec.Coef;
                    double max = 0;
                    for (double x = sec.LeftPoint; x <= sec.RightPoint; x += 0.001)
                    {

                        double fx = Math.Abs(Function.Val(x) - ExponencialSpline.AproximFunc(x, coef));
                        if (fx > max) max = fx;
                        list_1.Add(x, fx);

                    }
                    Logger.Info("Section#" + LogCounter++ + "Max value = " + max, "MainForm");
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                textBox1.ReadOnly = true;
                textBox1.Text = "";

            }
            else
            {
                textBox1.ReadOnly = false; 
            }
        }


       
    }
}
