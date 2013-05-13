using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CourseProj.MyClasses;

namespace CourseProj
{
    public partial class Form1 : Form
    {
        private UniformApproximation SplainApprox;
        public int power;
        public bool Gflag=true;
        public string FunctionName;
        public Form1()
        {
            InitializeComponent();
        }
       
        public bool  EnterChecking(TextBox text)
        {
            if (text.Text == "")
            {
               
                throw new Exception("Введіть дані  в необхідні поля...");
              
            }
            return true;
        }

        public bool Check(RadioButton r1,RadioButton r2,RadioButton r3,RadioButton r4,RadioButton r5,RadioButton r6,RadioButton r7, RadioButton r8,RadioButton r9)
        {
            if (!r1.Checked & !r2.Checked & !r3.Checked&!r4.Checked&!r5.Checked&!r6.Checked&!r7.Checked&!r8.Checked&!r9.Checked)
                MessageBox.Show("Оберіть функцію!!!");
            return true; 
        }

        public double f(double value)
        {

            if (ExpRadioButton.Checked)
            {
                
                return Math.Exp(value);
            }
            if (SinRadioButton.Checked)
            {
                
                return Math.Sin(value);
            }
            if (LnRadioButton.Checked)
            {
                
                return Math.Log(value);
            }
            if(TanRadioButton.Checked)
            {
                
                return Math.Tan(value);
            }
            if (FractionalFunctionRadioButton.Checked)
            {
            
                return 1.0 / (1.0+value*value);
            }
            if(AtanRadioButton.Checked)
            {
               
                return Math.Atan(value);
            }
            if (ThirdPowRadioButton.Checked)
            {
                if (EnterChecking(CubeTextBox0) & EnterChecking(CubeTextBox1) &
                    EnterChecking(CubeTextBox2) & EnterChecking(CubeTextBox3))
                {
                    double a0 = Convert.ToDouble(CubeTextBox3.Text);
                    double a1 = Convert.ToDouble(CubeTextBox2.Text);
                    double a2 = Convert.ToDouble(CubeTextBox1.Text);
                    double a3 = Convert.ToDouble(CubeTextBox0.Text);
                                        return a0 * Math.Pow(value, 3) + a1 * Math.Pow(value, 2) + a2 * value + a3;
                }
            }
            if (FourthPowRadioButton.Checked)
            {
                if(EnterChecking(FourthTextBox0)&EnterChecking(FourthTextBox1)&
                    EnterChecking(FourthTextBox2)&EnterChecking(FourthTextBox3)&
                    EnterChecking(FourthTextBox4))
                {
                double a0 = Convert.ToDouble(FourthTextBox4.Text);
                double a1 = Convert.ToDouble(FourthTextBox3.Text);
                double a2 = Convert.ToDouble(FourthTextBox2.Text);
                double a3 = Convert.ToDouble(FourthTextBox1.Text);
                double a4 = Convert.ToDouble(FourthTextBox0.Text);
                return a0 * Math.Pow(value, 4) + a1 * Math.Pow(value, 3) + a2 * Math.Pow(value, 2) + a3 * value + a4;
                }

            }
            if (FifthPowRadioButton.Checked)
            {
                if (EnterChecking(FifthTextBox0) & EnterChecking(FifthTextBox1) &
                    EnterChecking(FifthTextBox2) & EnterChecking(FifthTextBox3) &
                    EnterChecking(FifthTextBox4) & EnterChecking(FifthTextBox5))
                {
                    double a0 = Convert.ToDouble(FifthTextBox5.Text);
                    double a1 = Convert.ToDouble(FifthTextBox4.Text);
                    double a2 = Convert.ToDouble(FifthTextBox3.Text);
                    double a3 = Convert.ToDouble(FifthTextBox2.Text);
                    double a4 = Convert.ToDouble(FifthTextBox1.Text);
                    double a5 = Convert.ToDouble(FifthTextBox0.Text);
                    return a0 * Math.Pow(value, 5) + a1 * Math.Pow(value, 4) + a2 * Math.Pow(value, 3) + a3*Math.Pow(value,2) + a4*value+a5;
                
                }
            }
            return 0;
        }

        public double fp(double value)
        {
            if (ExpRadioButton.Checked)
            {
                return Math.Exp(value);
            }
            if (SinRadioButton.Checked)
            {
                return Math.Cos(value);
            }
            if (LnRadioButton.Checked)
            {
                return 1.0 / value;
            }
            if (TanRadioButton.Checked)
            {
                return 1.0 / Math.Pow(Math.Cos(value),2.0);
            }
            if(FractionalFunctionRadioButton.Checked)
            {
                return -(2.0 * value) / Math.Pow((1+value*value),2.0);
            }
            if(AtanRadioButton.Checked)
            {
                return 1.0/(1+value*value);
            }
            if (ThirdPowRadioButton.Checked)
            {
                if (EnterChecking(CubeTextBox0) & EnterChecking(CubeTextBox1) &
                    EnterChecking(CubeTextBox2) & EnterChecking(CubeTextBox3))
                {
                    double a0 = Convert.ToDouble(CubeTextBox3.Text);
                    double a1 = Convert.ToDouble(CubeTextBox2.Text);
                    double a2 = Convert.ToDouble(CubeTextBox1.Text);
                    double a3 = Convert.ToDouble(CubeTextBox0.Text);
                    return a0 * 3 * Math.Pow(value, 2) + a1 * 2 * value + a2;
                }
            }
            if (FourthPowRadioButton.Checked)
            {
                if (EnterChecking(FourthTextBox0) & EnterChecking(FourthTextBox1) &
                    EnterChecking(FourthTextBox2) & EnterChecking(FourthTextBox3) &
                    EnterChecking(FourthTextBox4))
                {
                    double a0 = Convert.ToDouble(FourthTextBox4.Text);
                    double a1 = Convert.ToDouble(FourthTextBox3.Text);
                    double a2 = Convert.ToDouble(FourthTextBox2.Text);
                    double a3 = Convert.ToDouble(FourthTextBox1.Text);
                    return a0 * 4 * Math.Pow(value, 3) + a1 * 3 * Math.Pow(value, 2) + a2 * 2 * Math.Pow(value, 1) + a3;
                }

            }
            if (FifthPowRadioButton.Checked)
            {
                if (EnterChecking(FifthTextBox0) & EnterChecking(FifthTextBox1) &
                    EnterChecking(FifthTextBox2) & EnterChecking(FifthTextBox3) &
                    EnterChecking(FifthTextBox4) & EnterChecking(FifthTextBox5))
                {
                    double a0 = Convert.ToDouble(FifthTextBox5.Text);
                    double a1 = Convert.ToDouble(FifthTextBox4.Text);
                    double a2 = Convert.ToDouble(FifthTextBox3.Text);
                    double a3 = Convert.ToDouble(FifthTextBox2.Text);
                    double a4 = Convert.ToDouble(FifthTextBox1.Text);
                    return a0*5* Math.Pow(value, 4) + a1 *4* Math.Pow(value, 3) + a2*3* Math.Pow(value, 2) + a3*2* value + a4;

                }
            }
            return 0;
        }

        public double fpp(double value)
        {
            if (SinRadioButton.Checked)
            {
                return -Math.Sin(value);
            }
            if (ExpRadioButton.Checked)
            {
                return Math.Exp(value);
            }
            if (LnRadioButton.Checked)
            {
                return -1.0/(value*value);
            }
            if (TanRadioButton.Checked)
            {
                return 2.0*Math.Sin(value)/(Math.Pow(Math.Cos(value),3.0));
            }
            if(FractionalFunctionRadioButton.Checked)
            {
                return (8.0*value*value-2.0*(1+value*value))/Math.Pow((1.0+value*value),3.0);
            }
            if(AtanRadioButton.Checked)
            {
                return -(2.0 * value) / Math.Pow((1 + value * value), 2.0);
            }
            if (FifthPowRadioButton.Checked)
            {
                if (EnterChecking(FifthTextBox0) & EnterChecking(FifthTextBox1) &
                    EnterChecking(FifthTextBox2) & EnterChecking(FifthTextBox3) &
                    EnterChecking(FifthTextBox4) & EnterChecking(FifthTextBox5))
                {
                    double a0 = Convert.ToDouble(FifthTextBox5.Text);
                    double a1 = Convert.ToDouble(FifthTextBox4.Text);
                    double a2 = Convert.ToDouble(FifthTextBox3.Text);
                    double a3 = Convert.ToDouble(FifthTextBox2.Text);
                    return a0 * 20 * Math.Pow(value, 3) + a1 * 12 * Math.Pow(value, 2) + a2 * 6 *value + a3 * 2;

                }
            }
            return 0;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (ThirdPowRadioButton.Checked)
            {
                CoefsEnterLabel.Show();
                CubeTextBox3.Show();
                CubeTextBox2.Show();
                CubeTextBox1.Show();
                CubeTextBox0.Show();
                
                CubeLabel3.Show();
                CubeLabel2.Show();
                CubeLabel1.Show();
                FunctionName = "многочлен 3-го степеня";
               
                
            }
            else
            {
                CoefsEnterLabel.Hide();
                CubeTextBox3.Hide();
                CubeTextBox2.Hide();
                CubeTextBox1.Hide();
                CubeTextBox0.Hide();
               
                CubeLabel3.Hide();
                CubeLabel2.Hide();
                CubeLabel1.Hide();
               
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {


            NevRichTextBox.Hide();
            NevRichTextBox.Clear();
            richTextBox1.Clear();
            CheckRichTextBox.Clear();
            double a=0, b=0, eps=0,Zeps=0;
            if(EnterChecking(textbox1)&EnterChecking(textBox2)&EnterChecking(textBox3)&
                Check(SinRadioButton,ExpRadioButton,ThirdPowRadioButton,LnRadioButton ,TanRadioButton,FractionalFunctionRadioButton,AtanRadioButton ,FourthPowRadioButton,FifthPowRadioButton)
               )
            {
                if (CubeSplRadioButton.Checked)
                    power = 3;
                if (FourthSplRadioButton.Checked)
                    power = 4;
                if (FifthSplRadioButton.Checked)
                    power = 5;
                a = Convert.ToDouble(textbox1.Text);
                b = Convert.ToDouble(textBox2.Text);
                eps = Convert.ToDouble(textBox3.Text);
                if (ZeydelRadioButton.Checked)
                {
                    Zeps = Convert.ToDouble(EpsTextBox.Text);
                }
                SplainApprox = new UniformApproximation(a, b,power,Gflag,Zeps,FunctionPictureBox,MuFuncPictureBox,ELabel,NevRichTextBox,CheckPictureBox,Clabel);
                SplainApprox.Approximate(a, b, eps, new UniformApproximation.function(f), new UniformApproximation.function(fp),new UniformApproximation.function(fpp), richTextBox1,NevRichTextBox,CheckRichTextBox,FunctionName);
                tabControl1.SelectTab(tabPage2);
            }
            
            
            
            

            
        }

        private void Nextbutton1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage3);
            
            
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        private void FourthPowRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FourthPowRadioButton.Checked)
            {
                CoefsEnterLabel.Show();
                FourthTextBox4.Show();
                FourthTextBox3.Show();
                FourthTextBox2.Show();
                FourthTextBox1.Show();
                FourthTextBox0.Show();

                FourthLabel4.Show();
                FourthLabel3.Show();
                FourthLabel2.Show();
                FourthLabel1.Show();
                FunctionName = "многочлен 4-го порядку";
            }
            else
            {
                CoefsEnterLabel.Hide();
                FourthTextBox4.Hide();
                FourthTextBox3.Hide();
                FourthTextBox2.Hide();
                FourthTextBox1.Hide();
                FourthTextBox0.Hide();

                FourthLabel4.Hide();
                FourthLabel3.Hide();
                FourthLabel2.Hide();
                FourthLabel1.Hide();
            }
        }

        private void FifthPowRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FifthPowRadioButton.Checked)
            {
                CoefsEnterLabel.Show();
                FifthTextBox5.Show();
                FifthTextBox4.Show();
                FifthTextBox3.Show();
                FifthTextBox2.Show();
                FifthTextBox1.Show();
                FifthTextBox0.Show();

                FifthLabel5.Show();
                FifthLabel4.Show();
                FifthLabel3.Show();
                FifthLabel2.Show();
                FifthLabel1.Show();
                FunctionName = "многочлен 5-го степеня";
            }
            else
            {
                CoefsEnterLabel.Hide();
                FifthTextBox5.Hide();
                FifthTextBox4.Hide();
                FifthTextBox3.Hide();
                FifthTextBox2.Hide();
                FifthTextBox1.Hide();
                FifthTextBox0.Hide();

                FifthLabel5.Hide();
                FifthLabel4.Hide();
                FifthLabel3.Hide();
                FifthLabel2.Hide();
                FifthLabel1.Hide();
            }
        }

        private void GausRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (GausRadioButton.Checked)
            {
                Gflag = true;
            }
        }

        private void ZeydelRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ZeydelRadioButton.Checked)
            {
                Gflag = false;
                EpsLabel.Show();
                EpsTextBox.Show();
            }
            else 
            {
                EpsLabel.Hide();
                EpsTextBox.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        private void NevButton_Click(object sender, EventArgs e)
        {
           
           NevRichTextBox.Show();
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        private void SinRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SinRadioButton.Checked)
                FunctionName = "sin(x)";
        }

        private void ExpRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ExpRadioButton.Checked)
                FunctionName = "exp(x)";
        }

        private void LnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (LnRadioButton.Checked)
                FunctionName = "ln(x)";
        }

        private void TanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(TanRadioButton.Checked)
             FunctionName = "tg(x)";
        }

        private void FractionalFunctionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FractionalFunctionRadioButton.Checked)
                FunctionName = "1/(1+x*x)";
        }

        private void AtanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AtanRadioButton.Checked)
                FunctionName = "arctg(x)";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage3);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage4);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage5);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage5);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage6);
        }
    
    }
}
