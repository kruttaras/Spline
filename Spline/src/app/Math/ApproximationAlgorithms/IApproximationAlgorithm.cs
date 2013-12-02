using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spline.Models;

namespace Spline
{
    public interface IApproximationAlgorithm
    {
        List<Section> Compute();
    }
}
