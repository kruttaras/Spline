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

        public ComboBoxItem(ComboBoxBaseItem _func)
        {
            this.Text = _func.Text;
            this.Value = _func;
        }

        public AppMath.BaseFunc Get_function()
        {
            return GetTemplate_function<AppMath.BaseFunc>();  
        }

        public ApproximatingFunction GetAproximating_function()
        {
            return GetTemplate_function<ApproximatingFunction>();
        }

        private T GetTemplate_function<T>()
        {
            if (this.Value is T)
            {
                return (T) Value;
            }
            return default(T);
        }
    }
}
