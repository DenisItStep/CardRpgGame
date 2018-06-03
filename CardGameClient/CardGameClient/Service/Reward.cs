using System.Runtime.Serialization;

namespace CardGameServer
{
    [DataContract]
    public class Reward
    {
        /// <summary>
        ///     Опыт
        /// </summary>
        [DataMember]
        public int Exp { get; set; }

        /// <summary>
        ///     Баллы
        /// </summary>
        [DataMember]
        public int Score { get; set; }

        /// <summary>
        ///     новый уровень
        /// </summary>
        [DataMember]
        public bool NewLevel { get; set; }

        /// <summary>
        ///     Новая карта
        /// </summary>
        [DataMember]
        public Card NewCard { get; set; }
    }
}