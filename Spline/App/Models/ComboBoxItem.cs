using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.Models
{
    class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public ComboBoxItem()
        {
        }

        public ComboBoxItem(ComboBoxBaseItem func)
        {
            this.Text = func.Text;
            this.Value = func;
        }

        public AppMath.BaseFunc GetFunction()
        {
            return (AppMath.BaseFunc)this.Value;
        }

    }
}
