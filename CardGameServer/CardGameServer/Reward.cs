using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CardGameServer
{
    [DataContract]
    public class Reward
    {
        [DataMember]
        public int Exp { get; set; }

        [DataMember]
        public int Score { get; set; }

        [DataMember]
        public bool NewLevel { get; set; }

        [DataMember]
        public Card NewCard { get; set; }
    }
}
