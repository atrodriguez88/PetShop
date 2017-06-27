using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    class Program
    {
        static int[] solve(int[] grades)
        {            
            int[] res = new int[grades.Length];
            for (int i = 0; i < grades.Length; i++)
            {
                if (grades[i] >= 38)
                {
                    int rounded = (int)Math.Ceiling(grades[i] / 5.0) * 5;
                    if ((rounded - grades[i]) < 3)
                    {
                        res[i] = rounded;                        
                    }
                    else
                    {
                        res[i] = grades[i];
                    }
                }
                else
                {
                    res[i] = grades[i];
                }
            }
            return res;
        }
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[] grades = new int[n];
            for (int grades_i = 0; grades_i < n; grades_i++)
            {
                grades[grades_i] = Convert.ToInt32(Console.ReadLine());
            }
            int[] result = solve(grades);
            Console.WriteLine(String.Join("\n", result));
            Console.ReadLine();
        }
    }
}
