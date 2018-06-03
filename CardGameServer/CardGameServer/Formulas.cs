using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameServer
{
    public class Formulas
    {
        public static Random Rnd;

        static Formulas()
        {
            Rnd = new Random();
            
        }

        public static int RndNext()
        {
            lock (Rnd)
            {
                return Rnd.Next();
            }
        }

        public static int RndNext(int max)
        {
            lock (Rnd)
            {
                return Rnd.Next(max);
            }
        }

        public static int RndNext(int min, int max)
        {
            lock (Rnd)
            {
                return Rnd.Next(min, max);
            }
        }



        public static int CalculateDamage(int dmgStat, int defStat)
        {
            return dmgStat - (int)(defStat / 1.5); //def div
        }

        public static int CalculateCritDamage(int dmg)
        {
            double tmp = dmg * 1.4; //crit mul
            int result = (int)tmp;
            if ((tmp % 1) >= 0.5) result += 1;

            return result;
        }

        public static int CalculateCritDamage(int dmg, double critMul)
        {
            double tmp = dmg * critMul; //crit mul
            int result = (int)tmp;
            if ((tmp % 1) >= 0.5) result += 1;

            return result;
        }

        public static bool IsMiss()
        {
            return RndNext(0, 10) == 9; //10% miss
        }

        public static bool IsCrit()
        {
            return RndNext(0, 6) == 1; //15%
        }
    }
}
