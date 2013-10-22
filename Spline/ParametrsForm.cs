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
        private static List<List<Control>> Coeficients = new List<List<Control>>();

        private ParametrsForm()
        {
            InitializeComponent();

            Coeficients.Add(new List<Control> { label1, textBox1 });
            Coeficients.Add(new List<Control> { label2, textBox2});
            Coeficients.Add(new List<Control> {label3, textBox3});
            Coeficients.Add(new List<Control> {label4, textBox4});
            Coeficients.Add(new List<Control> {label5, textBox5});
            
        }

        public static ParametrsForm GetParametrsForm(MainForm form, int count)
        {
            ParametrsForm result = new ParametrsForm();
            result.form = form;
            for (int i = 0; i < count; i++ )
            {
                ((Label)(Coeficients[i])[0]).Show();
                ((TextBox)(Coeficients[i])[1]).Show();
            }
            return result;
        }
        private void ParametrsForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
