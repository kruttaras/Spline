using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Spline.Models;
using ZedGraph;

namespace Spline.Models
{
    public static class AppUtils
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
        //TODO refactor this and that
        public static IList<AproximatingFunction> GetAllAproximatingFunctions()
        {
            IList<AproximatingFunction> functions = new List<AproximatingFunction>();

            Assembly[] classes = AppDomain.CurrentDomain.GetAssemblies();

            Type baseFunc = typeof(AproximatingFunction);
            for (int i = 0; i < classes.Length; i++)
            {
                foreach (Type type in classes[i].GetTypes())
                {
                    if (type.IsSubclassOf(baseFunc))
                    {
                        functions.Add((AproximatingFunction)Activator.CreateInstance(type));
                    }
                }

            }
            return functions;
        }

        public static object[] GetComboboxItemsWithFunctions()
        {
            IList<AppMath.BaseFunc> functions = GetAllFunctions();
            object[] cbItems = new object[functions.Count];
            for (int i = 0; i < cbItems.Length; i++)
            {
                cbItems[i] = new ComboBoxItem(functions[i]);
            }
            return cbItems;
        }

        public static object[] GetComboboxItemsWithAproximatingFunctions()
        {
            IList<AproximatingFunction> functions = GetAllAproximatingFunctions();
            object[] cbItems = new object[functions.Count];
            for (int i = 0; i < cbItems.Length; i++)
            {
                cbItems[i] = new ComboBoxItem(functions[i]);
            }
            return cbItems;
        }

        public static PointPairList GetPointPairsInRange(double fromPoint,double toPoint,IFunction func,double h=0.001)
        {
            if (func == null) throw new ArgumentNullException("func");

            PointPairList pointsList= new PointPairList();

            for (double x = fromPoint; x <= toPoint; x += h)
            {

                double fx = func.Val(x);

                pointsList.Add(x, fx);

            }
            return pointsList;
        }
    }
}
