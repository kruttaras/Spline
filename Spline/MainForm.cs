using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
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
        int numberOfSections;
        private AppMath.BaseFunc _function;

        CancellationTokenSource ts ;

        delegate void SetTextCallback(string text);

        delegate void BehaviourCallback();

        public MainForm()
        {
         
            InitializeComponent();
            comboBox1.Items.AddRange(AppUtils.GetComboboxItemsWith_functions());
            comboBox2.Items.AddRange(AppUtils.GetComboboxItemsWithAproximating_functions());

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            GraphPane functionAndItsAproximationChart = zedGraphControl1.GraphPane;
            functionAndItsAproximationChart.Title.Text = Resources._functionChartTitle;

            GraphPane observationalErrorChart = zedGraphControl2.GraphPane;
            observationalErrorChart.Title.Text = Resources.SplineChartTitle;

            progressIndicator1.CircleSize = 0.7f;
            progressIndicator1.NumberOfCircles = 10;      
            progressIndicator1.Start();

           progressIndicator1.Hide();
           progressIndicator1.BackColor = Color.Transparent;

            button2.Enabled = false;

        }


        private void button2_Click(object sender, EventArgs e)
        {
            ts.Cancel();
            HideProgresIndicator();
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            _function = ((ComboBoxItem)comboBox1.SelectedItem).Get_function();
            ApproximatingFunction aproxFunction = ((ComboBoxItem)comboBox2.SelectedItem).GetAproximating_function();

            GraphPane observationalErrorChart;
            var approximationChart = PrepareFormForNewResult(out observationalErrorChart);

            double xmin = Convert.ToDouble(textBox5.Text);
            double xmax = Convert.ToDouble(textBox6.Text);
            Mu = Convert.ToDouble(textBox7.Text);

            AlgorithmBuilder builder = new AlgorithmBuilder();
            builder.SetBorders(xmin, xmax)
                .SetFunction(_function)
                .SetApproximatingFunction(aproxFunction)
                .SetObservationalError(Mu);

            if (radioButton4.Checked)
            {
               numberOfSections = Convert.ToInt32(textBox1.Text);
               builder.SetNumberOfSections(numberOfSections);
            }
            
              ShowProgresIndicator();

              ts = new CancellationTokenSource();
              CancellationToken ct = ts.Token;

            var tf = new TaskFactory(
                TaskCreationOptions.AttachedToParent,
                TaskContinuationOptions.AttachedToParent);

            IApproximationAlgorithm algorithm = builder.Build();


            Task<List<Section>> approximation = tf.StartNew(() => algorithm.Compute(), ct);
            
            Task<string> outputResultTask = tf.ContinueWhenAll(new Task[] {approximation},
                                       tasks => GetValue(aproxFunction, approximationChart, observationalErrorChart, xmin, xmax, approximation.Result), ct);

            tf.ContinueWhenAll(new Task[] { outputResultTask }, tasks => HideProgresIndicator(),ct);

        }

        private GraphPane PrepareFormForNewResult(out GraphPane observationalErrorChart)
        {
            string currencyDecimalSeparator = CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator;
            textBox5.Text = textBox5.Text.Replace(".", currencyDecimalSeparator).Replace(",", currencyDecimalSeparator);
            textBox6.Text = textBox6.Text.Replace(".", currencyDecimalSeparator).Replace(",", currencyDecimalSeparator);
            textBox7.Text = textBox7.Text.Replace(".", currencyDecimalSeparator).Replace(",", currencyDecimalSeparator);

            GraphPane approximationChart = zedGraphControl1.GraphPane;
            zedGraphControl1.ZoomOutAll(approximationChart);
            approximationChart.CurveList.Clear();

            observationalErrorChart = zedGraphControl2.GraphPane;
            zedGraphControl2.ZoomOutAll(observationalErrorChart);
            observationalErrorChart.CurveList.Clear();
            richTextBox1.Text = "";
            return approximationChart;
        }

        private string GetValue(ApproximatingFunction aprox_function, GraphPane pane, GraphPane pane2, double xmin, double xmax, List<Section> section)
        {
            PointPairList list = AppUtils.GetPointPairsInRange(xmin, xmax, _function);
            var list1 = new PointPairList();
            var aprox = new PointPairList();

            String str = "";
            str += "Ланок побудовано: " + section.Count + "\n";
            str += "_______________________________________________\n";
            for (int i = 0; i < section.Count; i++)
            {
                for (double x = section[i].LeftPoint; x <= section[i].RightPoint; x += 0.001)
                {
                    double fx = aprox_function.GetAproximating_function(x, section[i].Coef);

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


            LineItem myCurve = pane.AddCurve(Resources.Chart_functionName, list, Color.Blue, SymbolType.None);
            LineItem myCurve2 = pane.AddCurve(Resources.ChartSplineName, aprox, Color.Red, SymbolType.None);

            pane.XAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.IsVisible = true;

            zedGraphControl1.AxisChange();


            zedGraphControl1.Invalidate();
            list1.Clear();

            Logger.Info("Mu  _function max value of each section for _function " + _function.ToString(), "MainForm");
            int LogCounter = 1;
            double max = 0;
            foreach (Section sec in section)
            {
                double[] coef = sec.Coef;
                
                for (double x = sec.LeftPoint; x <= sec.RightPoint; x += 0.001)
                {
                    double fx = Math.Abs(_function.Val(x) - aprox_function.GetAproximating_function(x, coef));
                    if (fx > max) max = fx;
                    list1.Add(x, fx);
                }
                Logger.Info("Section#" + LogCounter++ + "Max value = " + max, "MainForm");
            }

            LineItem newCurves = pane2.AddCurve("Ro", list1, Color.Blue, SymbolType.None);
            list1 = new PointPairList();
            if (radioButton4.Checked)
            {
                Mu = max;
            }
            
            list1.Add(xmin, Mu);
            list1.Add(xmax, Mu);
                
            
            
            newCurves = pane2.AddCurve("Mu", list1, Color.Red);
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
            AppMath.BaseFunc selectedFunction = ((ComboBoxItem)comboBox1.SelectedItem).Get_function();
            if (selectedFunction.GetNumberOfParametrs() > 0)
            {
                ParametrsForm parametrsForm = new ParametrsForm(this, selectedFunction.GetNumberOfParametrs());
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

                button1.Enabled = true;
                button2.Enabled = false;
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

            button1.Enabled = false;
            button2.Enabled = true;
        }
    }
}
