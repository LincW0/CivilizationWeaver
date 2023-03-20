using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class NumberDisplay
    {
        public static string UnitAbbreviation(long num)
        {
            if (num < 1000) return num.ToString();
            float numDisplaying = (float)num / 1000;
            if (numDisplaying < 1000)
            {
                return numDisplaying.ToString("0.0") + "K"; //Thousand
            }
            numDisplaying /= 1000;
            if (numDisplaying < 1000)
            {
                return numDisplaying.ToString("0.0") + "M"; //Million
            }
            numDisplaying /= 1000;
            if (numDisplaying < 1000)
            {
                return numDisplaying.ToString("0.0") + "B"; //Billion
            }
            numDisplaying /= 1000;
            if (numDisplaying < 1000)
            {
                return numDisplaying.ToString("0.0") + "T"; //Trillion
            }
            numDisplaying /= 1000;
            if (numDisplaying < 1000)
            {
                return numDisplaying.ToString("0.0") + "Q"; //Quadrillion
            }
            return ((long)numDisplaying).ToString() + "Q"; //Seriously???
        }
        public static string UnitAbbreviation(float num)
        {
            if (num < 1000) return num.ToString("0.00");
            float numDisplaying = num / 1000;
            if (numDisplaying < 1000)
            {
                return numDisplaying.ToString("0.0") + "K"; //Thousand
            }
            numDisplaying /= 1000;
            if (numDisplaying < 1000)
            {
                return numDisplaying.ToString("0.0") + "M"; //Million
            }
            numDisplaying /= 1000;
            if (numDisplaying < 1000)
            {
                return numDisplaying.ToString("0.0") + "B"; //Billion
            }
            numDisplaying /= 1000;
            if (numDisplaying < 1000)
            {
                return numDisplaying.ToString("0.0") + "T"; //Trillion
            }
            numDisplaying /= 1000;
            if (numDisplaying < 1000)
            {
                return numDisplaying.ToString("0.0") + "Q"; //Quadrillion
            }
            return ((long)numDisplaying).ToString() + "Q"; //What the
        }
    }
}
