using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInfoSystem
{
    public static class Tools
    {
        public static int[] GetBirthFromString(string str)
        {
            int[] birth = new int[3];
            if(!(str.Contains("年")&& str.Contains("月")&& str.Contains("日")))
            {
                return null;
            }
            try
            {
                string[] split = new string[]
                {
                    "年","月","日"
                };
                string[] date = str.Split(split, StringSplitOptions.RemoveEmptyEntries);
                birth[0] = int.Parse(date[0]);
                birth[1] = int.Parse(date[1]);
                birth[2] = int.Parse(date[2]);
            }
            catch
            {
                return null;
            }
            return birth;
        }

        public static string ReturnSex(int sex)
        {
            if (sex == 1)
                return "男";
            else
                return "女";
        }
    }
}
