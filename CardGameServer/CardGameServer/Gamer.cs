using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CardGameServer
{
    [DataContract]
    public class Gamer
    {
        public string login { get; set; }

        [DataMember]
        public string nick { get; set; }

        [DataMember]
        public int level = 1;

        public int lastAcc { get; set; }

        public Gamer(string l)
        {
            login = l;
            nick = "";
            lastAcc = 0;
        }
    }
}
