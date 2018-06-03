using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CardGameServer
{
    [DataContract]
    public class CharInfo
    {
        /// <summary>
        /// Логин игрока
        /// </summary>
        [DataMember]
        public string nickname { get; set; }

        /// <summary>
        /// Ник игрока
        /// </summary>
        [DataMember]
        public string heroname { get; set; }

        /// <summary>
        /// Уровень игрока
        /// </summary>
        [DataMember]
        public int character_level { get; set; }

        /// <summary>
        /// Опыт игрока
        /// </summary>
        [DataMember]
        public int exp { get; set; }

        /// <summary>
        /// Сыграно игр
        /// </summary>
        [DataMember]
        public int games { get; set; }

        /// <summary>
        /// Кол побед игрока
        /// </summary>
        [DataMember]
        public int wins { get; set; }

        /// <summary>
        /// баллы игрока
        /// </summary>
        [DataMember]
        public int score { get; set; }
    }
}
