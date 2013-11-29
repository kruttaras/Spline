using System;
using System.Collections.Generic;
using System.Reflection;
using ZedGraph;

namespace Spline.Models
{
    public static class AppUtils
    {
        public static IList<AppMath.BaseFunc> GetAllFunctions()
        {
            IList<AppMath.BaseFunc> functions = new List<AppMath.BaseFunc>();

            var classes= AppDomain.CurrentDomain.GetAssemblies();

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
        public static IList<ApproximatingFunction> GetAllAproximatingFunctions()
        {
            IList<ApproximatingFunction> functions = new List<ApproximatingFunction>();

            Assembly[] classes = AppDomain.CurrentDomain.GetAssemblies();

            Type baseFunc = typeof(ApproximatingFunction);
            for (int i = 0; i < classes.Length; i++)
            {
                foreach (Type type in classes[i].GetTypes())
                {
                    if (type.IsSubclassOf(baseFunc))
                    {
                        functions.Add((ApproximatingFunction)Activator.CreateInstance(type));
                    }
                }

            }
            return functions;
        }

        public static object[] GetComboboxItemsWithFunctions()
        {
            IList<AppMath.BaseFunc> functions = GetAllFunctions();
            var cbItems = new object[functions.Count];
            for (int i = 0; i < cbItems.Length; i++)
            {
                cbItems[i] = new ComboBoxItem(functions[i]);
            }
            return cbItems;
        }

        public static object[] GetComboboxItemsWithAproximatingFunctions()
        {
            IList<ApproximatingFunction> functions = GetAllAproximatingFunctions();
            var cbItems = new object[functions.Count];
            for (int i = 0; i < cbItems.Length; i++)
            {
                cbItems[i] = new ComboBoxItem(functions[i]);
            }
            return cbItems;
        }

        public static PointPairList GetPointPairsInRange(double fromPoint,double toPoint,IFunction func,double h=0.001)
        {
            if (func == null) throw new ArgumentNullException("func");

            var pointsList= new PointPairList();

            for (var x = fromPoint; x <= toPoint; x += h)
            {

                var fx = func.Val(x);

                pointsList.Add(x, fx);

            }
            return pointsList;
        }
    }
}
