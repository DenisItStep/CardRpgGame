using System.Runtime.Serialization;

namespace CardGameServer
{
    [DataContract]
    public class CharInfo
    {
        [DataMember]
        public string nickname { get; set; }

        [DataMember]
        public string heroname { get; set; }

        [DataMember]
        public int character_level { get; set; }

        [DataMember]
        public int exp { get; set; }

        [DataMember]
        public int games { get; set; }

        [DataMember]
        public int wins { get; set; }

        [DataMember]
        public int score { get; set; }

        public CharInfo()
        {
        }

        public CharInfo(string n, string h, int cl, int e, int g, int w, int r = 0)
        {
            nickname = n;
            heroname = h;
            character_level = cl;
            exp = e;
            games = g;
            wins = w;
            score = r;
        }
    }
}