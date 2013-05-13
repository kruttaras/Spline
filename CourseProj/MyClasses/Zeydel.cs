using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace CourseProj.MyClasses
{
    class Zeydel
    {

        public double[] ZeydMethod(int size,double[,] Matrix,double[] Vec,double eps,out int counter)
        {
	       
            counter=0;
	        double sum1,sum2=0;
            double[] Rozv = new double[size];
            double[] Rozv_previous = new double[size];
            for (int i = 0; i < size; i++)
	        {
	    	Rozv[i]=0;
	        }
	        do
	        {
                for (int i = 0; i < size; i++)
		        {
		    	Rozv_previous[i]=Rozv[i];
		        }
                for (int i = 0; i < size; i++)
		        {
		        	sum1 = 0;
			        for(int j=0;j<i;j++)
			        {
			        	sum1=sum1+((Matrix[i,j]/Matrix[i,i])*Rozv[j]);
		        	}	
		        	sum2=0;
                    for (int j = i+1; j < size; j++)
			        {
				        sum2=sum2+((Matrix[i,j]/Matrix[i,i])*Rozv_previous[j]);
			        }
                    Rozv[i] = -sum1 - sum2 + Vec[i] / Matrix[i, i];
		       }
		       counter++;
            } while (Norm(size, Rozv, Rozv_previous) > eps);

          
            return Rozv;
        }

        public  double Norm(int size,double[] Next,double[] Previous)
        {
	         double norma=0.0;
	         double a;
	         for(int i=0;i<size;i++)
	         {
	
		         a=Math.Abs(Previous[i]-Next[i]);
		         norma=Math.Max(a,norma);
		 
	         }
	         return norma;
        }      
    }
}
