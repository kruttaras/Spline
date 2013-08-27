using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Spline.Models;
using ZedGraph;

namespace Spline.Models
{
    static class FunctionUtil
    {
        public static IList<AppMath.BaseFunc> GetAllFunctions()
        {
            IList<AppMath.BaseFunc> functions = new List<AppMath.BaseFunc>();

            Assembly[] classes= AppDomain.CurrentDomain.GetAssemblies();

            Type baseFunc = typeof(AppMath.BaseFunc);
            for (int i = 0; i < classes.Length; i++)
            {
                foreach(Type type in classes[i].GetTypes())
                {
                      if (type.IsSubclassOf(baseFunc))
                      {
                        functions.Add((AppMath.BaseFunc) Activator.CreateInstance(type));
                      }
                }
              
            }
            return functions;
        }

        public static object[] GetComboboxItemsWithFunctions()
        {
            IList<AppMath.BaseFunc> functions = GetAllFunctions();
            object[] CBItems = new object[functions.Count];
            for (int i = 0; i < CBItems.Length; i++)
            {
                CBItems[i] = new ComboBoxItem(functions[i]);
            }
            return CBItems;
        }

        public static PointPairList GetPointPairsInRange(double FromPoint,double ToPoint,IFunction func,double h=0.001)
        {
            PointPairList PointsList= new PointPairList();

            for (double x = FromPoint; x <= ToPoint; x += h)
            {

                double fx = func.Val(x);

                PointsList.Add(x, fx);

            }
            return PointsList;
        }
    }
}
