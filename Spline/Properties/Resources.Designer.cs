﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Spline.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Spline.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Баланснее наближення функцій нелінійними ермітовими сплайнами.
        /// </summary>
        internal static string AppName {
            get {
                return ResourceManager.GetString("AppName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to F( x ).
        /// </summary>
        internal static string ChartFunctionName {
            get {
                return ResourceManager.GetString("ChartFunctionName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to S( x ).
        /// </summary>
        internal static string ChartSplineName {
            get {
                return ResourceManager.GetString("ChartSplineName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Графік функції та сплайна.
        /// </summary>
        internal static string FunctionChartTitle {
            get {
                return ResourceManager.GetString("FunctionChartTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~.
        /// </summary>
        internal static string Separetor {
            get {
                return ResourceManager.GetString("Separetor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Функція похибки.
        /// </summary>
        internal static string SplineChartTitle {
            get {
                return ResourceManager.GetString("SplineChartTitle", resourceCulture);
            }
        }
    }
}
