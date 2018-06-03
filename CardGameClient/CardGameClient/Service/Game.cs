using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CardGameServer
{
    [DataContract]
    public class Game
    {
        /// <summary>
        /// Список игроков
        /// </summary>
        [DataMember]
        public List<string> Gamers;

        /// <summary>
        /// Последний удар
        /// </summary>
        [DataMember]
        public LastHitInfo lastHitInfo { get; set; }

        /// <summary>
        /// Первый игрок
        /// </summary>
        [DataMember]
        public Gamer fGamer;

        /// <summary>
        /// Второй игрок
        /// </summary>
        [DataMember]
        public Gamer tGamer;

        /// <summary>
        /// Карты первого игрока
        /// </summary>
        [DataMember]
        public List<Card> firstGamerCards;

        /// <summary>
        /// Карты второго игрока
        /// </summary>
        [DataMember]
        public List<Card> twoGamerCards;

        /// <summary>
        /// Статистика игры
        /// </summary>
        [DataMember]
        public int gameState;

        /// <summary>
        /// Количество игроков
        /// </summary>
        [DataMember]
        public string currUsr;

        /// <summary>
        /// Награда выигравшему игроку
        /// </summary>
        [DataMember]
        public Reward WinGamerReward { get; set; }

        /// <summary>
        /// Награда проигравшему игроку
        /// </summary>
        [DataMember]
        public Reward LooseGamerReward { get; set; }
    }
}
