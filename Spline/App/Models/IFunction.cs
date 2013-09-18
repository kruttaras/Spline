using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spline.Models
{
    public interface IFunction
    {
        double Val(double x);
    }

    public abstract class ComboBoxBaseItem
    {
       public string Text { get; set; }
    }
}
