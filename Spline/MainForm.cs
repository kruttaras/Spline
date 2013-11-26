using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
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

        delegate void SetTextCallback(string text);

        delegate void BehaviourCallback();

        public MainForm()
        {
         
            InitializeComponent();
            comboBox1.Items.AddRange(AppUtils.GetComboboxItemsWithFunctions());
            comboBox2.Items.AddRange(AppUtils.GetComboboxItemsWithAproximatingFunctions());

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            GraphPane functionAndItsAproximationChart = zedGraphControl1.GraphPane;
            functionAndItsAproximationChart.Title.Text = Resources.FunctionChartTitle;

            GraphPane observationalErrorChart = zedGraphControl2.GraphPane;
            observationalErrorChart.Title.Text = Resources.SplineChartTitle;

            progressIndicator1.CircleSize = 0.7f;
            progressIndicator1.NumberOfCircles = 10;
      
           progressIndicator1.Hide();
           progressIndicator1.BackColor = Color.Transparent; 
            
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

              GraphPane ApproximationChart = zedGraphControl1.GraphPane;
              zedGraphControl1.ZoomOutAll(ApproximationChart);
              ApproximationChart.CurveList.Clear();

              GraphPane observationalErrorChart = zedGraphControl2.GraphPane;
              zedGraphControl2.ZoomOutAll(observationalErrorChart);
              observationalErrorChart.CurveList.Clear();
             
              richTextBox1.Text = "";
              
              
                PointPairList list = new PointPairList();
                PointPairList list_aprox = new PointPairList();


                list = AppUtils.GetPointPairsInRange(xmin, xmax, Function);     

                PointPairList list_1 = new PointPairList();
                PointPairList aprox = new PointPairList();

            var tf = new TaskFactory(
                TaskCreationOptions.AttachedToParent,
                TaskContinuationOptions.AttachedToParent);


            ShowProgresIndicator();
            Task<List<Section>> aproximation;
            if (radioButton4.Checked)
            {
                aproximation = tf.StartNew(() => ApproximationWithGivenNumberOfSections.Instance.Compute(xmin, xmax, Function, aproxFunction, Mu, R));
            }
            else
            {
                aproximation = tf.StartNew(() => ApproximationWithGivenObservationalError.Instance.Compute(xmin, xmax, Function, aproxFunction, Mu));
                
            }
            
            Task<string> outputResultTask = tf.ContinueWhenAll(new Task[] {aproximation},
                    tasks => GetValue(aproxFunction, aprox, ApproximationChart, list, list_1, observationalErrorChart, xmin, xmax, aproximation.Result));

            tf.ContinueWhenAll(new Task[] { outputResultTask }, tasks => HideProgresIndicator());



        }

        private string GetValue(AproximatingFunction aproxFunction, PointPairList aprox, GraphPane pane, PointPairList list,
            PointPairList list_1, GraphPane pane2, double xmin, double xmax, List<Section> section1 )
        {
            String str = "";
            section = section1;
            str += "Ланок побудовано: " + section.Count + "\n";
            str += "_______________________________________________\n";
            for (int i = 0; i < section.Count; i++)
            {
                for (double x = section[i].LeftPoint; x <= section[i].RightPoint; x += 0.001)
                {
                    double fx = aproxFunction.GetAproximatingFunction(x, section[i].Coef);

                    aprox.Add(x, fx);
                }

                str += "Ланка # " + (i + 1) + "\nxL= " + section[i].LeftPoint
                                     + "\nxR= " + section[i].RightPoint + "\nMu= "
                                     + section[i].Mu + "\n\n" + "Коефіцієнти\n";
                for (int ii = 0; ii < section[i].Coef.Length; ii++)
                {
                    str += "a[" + ii + "]= " + section[i].Coef[ii] + "\n";
                }
                str += Resources.Separetor + "\n";
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
            double max = 0;
            foreach (Section sec in section)
            {
                double[] coef = sec.Coef;
                
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
            if (radioButton4.Checked)
            {
                Mu = max;
            }
            
            list_1.Add(xmin, Mu);
            list_1.Add(xmax, Mu);
                
            
            
            newCurves = pane2.AddCurve("Mu", list_1, Color.Red);
            newCurves.Line.Style = DashStyle.Custom;
            newCurves.Line.Width = 2;
            newCurves.Line.DashOn = 5;
            newCurves.Line.DashOff = 8;
            newCurves.Symbol.Size = 0;

            pane2.XAxis.MajorGrid.IsVisible = true;
            pane2.YAxis.MajorGrid.IsVisible = true;
            pane2.YAxis.Scale.Max = 2*Mu;
            pane2.YAxis.Scale.Min = 0;
            pane2.XAxis.Scale.Max = xmax;
            pane2.XAxis.Scale.Min = xmin;

            zedGraphControl2.AxisChange();

            zedGraphControl2.Invalidate();
            AddText(str);
            return str;
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
        private void AddText(string text)
        {

            if (this.richTextBox1.InvokeRequired)
            {
                var d = new SetTextCallback(AddText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBox1.Text += text;
            }
        }

        private void HideProgresIndicator()
        {
            if (this.progressIndicator1.InvokeRequired)
            {
                var d = new BehaviourCallback(HideProgresIndicator);
                this.Invoke(d);
            }
            else
            {
                zedGraphControl1.Show();
                zedGraphControl2.Show();
                progressIndicator1.Stop();
                progressIndicator2.Stop();
                progressIndicator3.Stop();
                progressIndicator1.Hide();
                progressIndicator2.Hide();
                progressIndicator3.Hide();
            }
           
        }

        private void ShowProgresIndicator()
        {
            zedGraphControl1.Hide();
            zedGraphControl2.Hide();
            progressIndicator1.Show();
            progressIndicator2.Show();
            progressIndicator3.Show();
            progressIndicator1.Start();
            progressIndicator2.Start();
            progressIndicator3.Start(); 
           
        }
       
    }
}
