using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Spline
{
    public partial class ParametrsForm : Form
    {
        private MainForm form;
        private  List<List<Control>> Coeficients = new List<List<Control>>();

        public ParametrsForm(MainForm form, int count)
        {
            InitializeComponent();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide(); 
            textBox4.Hide();
            textBox5.Hide();
            Coeficients.Add(new List<Control> {label1, textBox1 });
            Coeficients.Add(new List<Control> { label2, textBox2});
            Coeficients.Add(new List<Control> {label3, textBox3});
            Coeficients.Add(new List<Control> {label4, textBox4});
            Coeficients.Add(new List<Control> {label5, textBox5});

            this.form = form;
            for (int i = 0; i < count; i++)
            {
                ((Label)(Coeficients[i])[0]).Show();
                ((TextBox)(Coeficients[i])[1]).Show();
            }
            
        }

        private void ParametrsForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
