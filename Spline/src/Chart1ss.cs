/*
 * Created by SharpDevelop.
 * User: krut.taras
 * Date: 05.05.2013
 * Time: 14:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Spline
{
	/// <summary>
	/// Description of SingletonClass1.
	/// </summary>
	public sealed class ChartUtil
	{
		private static ChartUtil instance = new ChartUtil();
		
		public static ChartUtil Instance {
			get {
				return instance;
			}
		}
		
		private ChartUtil()
		{
		}
	}
}
