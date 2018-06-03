using System.Runtime.Serialization;

namespace CardGameServer
{
    [DataContract]
    public class Card
    {
        /// <summary>
        ///     ИД карты
        /// </summary>
        [DataMember]
        public int id { get; set; }

        /// <summary>
        ///     Название карты
        /// </summary>
        [DataMember]
        public string card_name { get; set; }

        /// <summary>
        ///     ХП карты
        /// </summary>
        [DataMember]
        public int hp { get; set; }

        /// <summary>
        ///     Урон карты
        /// </summary>
        [DataMember]
        public int dmg { get; set; }

        /// <summary>
        ///     Защита карты
        /// </summary>
        [DataMember]
        public int def { get; set; }

        /// <summary>
        ///     Слот карты
        /// </summary>
        [DataMember]
        public int slot { get; set; }

        /// <summary>
        ///     Активная карта
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }

        /// <summary>
        ///     Ранг карты
        /// </summary>
        [DataMember]
        public int cardRarity { get; set; }

        /// <summary>
        ///     Под бафами
        /// </summary>
        [DataMember]
        public bool IsInjury { get; set; }

        /// <summary>
        ///     Был атакован
        /// </summary>
        [DataMember]
        public bool IsAttacked { get; set; }

        /// <summary>
        ///     Начальный уровень
        /// </summary>
        [DataMember]
        public int min_level { get; set; }
    }
}