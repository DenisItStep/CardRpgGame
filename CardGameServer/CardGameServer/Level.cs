using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameServer
{
    public class Level
    {
        public static Dictionary<int, int> Levels = new Dictionary<int, int>();

        static Level()
        {
            Levels.Add(1, 0);
            Levels.Add(2, 245);
            Levels.Add(3, 863);
            Levels.Add(4, 1965);
            Levels.Add(5, 3278);
            Levels.Add(6, 5456);
            Levels.Add(7, 8456);
            Levels.Add(8, 13456);
            Levels.Add(9, 19456);
            Levels.Add(10, 30654);
            Levels.Add(11, 47567);
            Levels.Add(12, 65975);
            Levels.Add(13, 88673);
            Levels.Add(14, 110346);
            Levels.Add(15, 130346);
            Levels.Add(16, 150137);
            Levels.Add(17, 171246);
            Levels.Add(18, 200795);
            Levels.Add(19, 235446);
            Levels.Add(20, 279395);
        }
    }
}
