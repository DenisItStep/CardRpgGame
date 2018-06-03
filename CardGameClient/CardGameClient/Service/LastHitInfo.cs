using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CardGameServer
{
    [DataContract]
    public class LastHitInfo
    {
        /// <summary>
        /// Слот карты
        /// </summary>
        [DataMember]
        public int slot { get; set; }

        /// <summary>
        /// Урон карты
        /// </summary>
        [DataMember]
        public int dmg { get; set; }

        /// <summary>
        /// Получен крит
        /// </summary>
        [DataMember]
        public bool IsCritical { get; set; }

        /// <summary>
        /// Промах
        /// </summary>
        [DataMember]
        public bool isMissed { get; set; }
    }
}