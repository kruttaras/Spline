using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Spline.App.Utils
{
    class Logger
    {
        public static void Error(string message, string module)
        {
            WriteEntry("[ERROR]", message, module);
        }

        public static void Error(Exception ex, string module)
        {
            WriteEntry("[ERROR]", ex.Message, module);
        }

        public static void Warning(string message, string module)
        {
            WriteEntry("[WARNING]",message, module);
        }

        public static void Info(string message, string module)
        {
            WriteEntry("[INFO]", message, module);
        }

        private static void WriteEntry(string type, string message, string module)
        {
            Trace.WriteLine(
                    string.Format("{0}, {1} : {2} ,{3}",
                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                  type,
                                  module,
                                  message));
        }
    }
}

