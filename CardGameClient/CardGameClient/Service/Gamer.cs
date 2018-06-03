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
        /// <summary>
        /// Ник игрока
        /// </summary>
        [DataMember]
        public string nick { get; set; }

        /// <summary>
        /// Уровень
        /// </summary>
        [DataMember]
        public int level { get; set; }
    }
}
