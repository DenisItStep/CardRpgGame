using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CardGameServer
{
    [DataContract]
    public class Game
    {
        /// <summary>
        ///     Количество игроков
        /// </summary>
        [DataMember] public string currUsr;

        /// <summary>
        ///     Первый игрок
        /// </summary>
        [DataMember] public Gamer fGamer;

        /// <summary>
        ///     Карты первого игрока
        /// </summary>
        [DataMember] public List<Card> firstGamerCards;

        /// <summary>
        ///     Список игроков
        /// </summary>
        [DataMember] public List<string> Gamers;

        /// <summary>
        ///     Статистика игры
        /// </summary>
        [DataMember] public int gameState;

        /// <summary>
        ///     Второй игрок
        /// </summary>
        [DataMember] public Gamer tGamer;

        /// <summary>
        ///     Карты второго игрока
        /// </summary>
        [DataMember] public List<Card> twoGamerCards;

        /// <summary>
        ///     Последний удар
        /// </summary>
        [DataMember]
        public LastHitInfo lastHitInfo { get; set; }

        /// <summary>
        ///     Награда выигравшему игроку
        /// </summary>
        [DataMember]
        public Reward WinGamerReward { get; set; }

        /// <summary>
        ///     Награда проигравшему игроку
        /// </summary>
        [DataMember]
        public Reward LooseGamerReward { get; set; }
    }
}