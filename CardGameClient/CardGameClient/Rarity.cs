using System.Windows.Media;

namespace CardGameClient
{
    public class Rarity
    {
        /// <summary>
        ///     Редкость/раритет
        /// </summary>
        public Rarity()
        {
        }

        /// <summary>
        ///     Редкость
        /// </summary>
        /// <param name="rn">Название редкости</param>
        /// <param name="rc">Цвет редкости</param>
        public Rarity(string rn, Brush rc)
        {
            RarityName = rn;
            RarityColor = rc;
        }

        /// <summary>
        ///     Название редкости
        /// </summary>
        public string RarityName { get; set; }

        /// <summary>
        ///     Цвет редкости
        /// </summary>
        public Brush RarityColor { get; set; }
    }
}