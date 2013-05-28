using NCalc;
using ZedGraph;
/*
 * Created by SharpDevelop.
 * User: krut.taras
 * Date: 05.05.2013
 * Time: 13:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Spline
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private Expression expression;

        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            ////
            InitializeComponent();
           //// Expression exp = new Expression("Pow([x],2.1)");
           // exp.Parameters["x"] = 2;
          //  exp.Parameters["y"] = 1;
          //  double x = (double)exp.Evaluate();


            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        protected double[] exp(Func<double, double> F,Func<double, double> FD1, double x0, double x1)
        {
            double[] a = new double[4];
            double x0_x1=(x0 - x1);
            double LogF0_F1=Math.Log(F(x0) / F(x1));
            //C
           // double c=a[2] = (FD1(x0) - FD1(x1)) / (2 * x0_x1) + (F(x1) - F(x0)) * LogF0_F1 / (2 * Math.Pow(x0_x1,2));
            
            //B
           // double b=a[1] = (FD1(x0) - FD1(x1)) / Math.Pow(x0_x1, 2) - (F(x0) + F(x1)) * LogF0_F1 / Math.Pow(x0_x1,3);

            //D
           // double d=a[3] = LogF0_F1 - b * (Math.Pow(x0, 3) - Math.Pow(x1, 3)) - c * (Math.Pow(x0, 2) - Math.Pow(x1, 2));

            double d = a[3] = (FD1(x1) / F(x1) + FD1(x0) / F(x0)+2*LogF0_F1/(x1-x0))/Math.Pow(-1*x0_x1,2);

            double c = a[2] = ((2 * x0 * x0 - x0 * x1 - x1 * x1) * a[3] - FD1(x0) / F(x0) - LogF0_F1 / (-1 * x0_x1)) / (-1 * x0_x1);

            double b = a[1] = 1 / (x1 - x0) * (Math.Log(F(x1) / F(x0)) - a[3] * (Math.Pow(x1, 3) - Math.Pow(x0, 3)) - c * (Math.Pow(x1, 2) - Math.Pow(x0, 2)));

            

            //A
            a[0] = F(x0) * Math.Exp(-1*(d * Math.Pow(x0, 3) + c * Math.Pow(x0, 2) + b * x0));

            return  a;
        }

        protected double Func(double x)
        {
            double A = Convert.ToDouble(textBox1.Text);
            double B = Convert.ToDouble(textBox2.Text);
            double C = Convert.ToDouble(textBox3.Text);
            double D = Convert.ToDouble(textBox4.Text);
            return Math.Sin(x);
            //return (A * Math.Exp(B * x + C * Math.Pow(x, 2) + D * Math.Pow(x, 3)));
        }
        protected double Func1(double x)
        {
            double A = Convert.ToDouble(textBox1.Text);
            double B = Convert.ToDouble(textBox2.Text);
            double C = Convert.ToDouble(textBox3.Text);
            double D = Convert.ToDouble(textBox4.Text);
            double exp = A * Math.Exp(B * x + C * Math.Pow(x, 2) + D * Math.Pow(x, 3));
            double multip = 3 * D * Math.Pow(x, 2) + 2 * C * x + B  ;
            return Math.Cos(x);
            //return multip * exp;
        }

        protected double AproximFunc(double x, double[] coef)
        {
            return (coef[0] * Math.Exp(coef[1] * x + coef[2] * Math.Pow(x, 2) + coef[3] * Math.Pow(x, 3)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                expression = new Expression("String");
               // expression.Parameters["x"] = 2;
                //Convert.ToDouble(expression.Evaluate().ToString());
                //MessageBox.Show("Value= " + expression.Evaluate().ToString());

                GraphPane pane = zedGraphControl1.GraphPane;
                

                // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
                pane.CurveList.Clear();

                // Создадим список точек
                PointPairList list = new PointPairList();
                PointPairList list_aprox = new PointPairList();

                double xmin = Convert.ToDouble(textBox5.Text);
                double xmax = Convert.ToDouble(textBox6.Text);
                double[] coef = exp(Func, Func1, xmin, xmax);
                

                // Заполняем список точек
                for (double x = xmin; x <= xmax; x += 0.001)
                {

                    double fx = Func(x);
                    double faprox = AproximFunc(x, coef);
                    // добавим в список точку
                    list.Add(x, fx);
                    
                    list_aprox.Add(x, faprox);
                }

                // Создадим кривую с названием "Sinc", 
                // которая будет рисоваться голубым цветом (Color.Blue),
                // Опорные точки выделяться не будут (SymbolType.None)
                LineItem myCurve = pane.AddCurve("exp", list, Color.Blue, SymbolType.None);
                LineItem myCurveAprox = pane.AddCurve("Aprox", list_aprox, Color.Red, SymbolType.None);
                // Включим отображение сетки
                pane.XAxis.MajorGrid.IsVisible = true;
                pane.YAxis.MajorGrid.IsVisible = true;
                // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
                // В противном случае на рисунке будет показана только часть графика, 
                // которая умещается в интервалы по осям, установленные по умолчанию
                zedGraphControl1.AxisChange();

                // Обновляем график
                zedGraphControl1.Invalidate();

                GraphPane pane2 = zedGraphControl2.GraphPane;
                pane2.CurveList.Clear();

                // Создадим список точек
                PointPairList list_1 = new PointPairList();
               
                double h = (xmax - xmin) / Convert.ToDouble(100);
                // Заполняем список точек
                double ymax_limit = 0;
                for (double x = xmin; x <= xmax; x += h)
                {

                    double fx = Math.Abs(Func(x) - AproximFunc(x, coef));
                    if (fx > ymax_limit)
                    {
                        ymax_limit = fx;
                    }
                    // добавим в список точку
                    list_1.Add(x, fx);

                }

                // Создадим кривую с названием "Sinc", 
                // которая будет рисоваться голубым цветом (Color.Blue),
                // Опорные точки выделяться не будут (SymbolType.None)
                LineItem newCurves = pane2.AddCurve("Ro", list_1, Color.Blue, SymbolType.None);
                list_1 = new PointPairList();
                list_1.Add(xmin,ymax_limit);
                list_1.Add(xmax, ymax_limit);
                newCurves = pane2.AddCurve("Mu", list_1, Color.Red, SymbolType.None);
                
                // Включим отображение сетки
                pane2.XAxis.MajorGrid.IsVisible = true;
                pane2.YAxis.MajorGrid.IsVisible = true;
                pane2.YAxis.Scale.Max = 2*ymax_limit;
                pane2.YAxis.Scale.Min = 0;
                pane2.XAxis.Scale.Max = xmax;
                pane2.XAxis.Scale.Min = xmin;
                // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
                // В противном случае на рисунке будет показана только часть графика, 
                // которая умещается в интервалы по осям, установленные по умолчанию
                zedGraphControl1.AxisChange();

                // Обновляем график
                zedGraphControl1.Invalidate();

            }
            catch (ArgumentException exception)
            {

                MessageBox.Show("The formula is wrong or empty"+exception.Message);
            }
            catch (NCalc.EvaluationException exception)
            {

                MessageBox.Show("The formula is wrong or empty");
            }

        }
    }
}
