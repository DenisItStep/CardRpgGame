using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace CardGameClient
{
    public class Rarity
    {
        /// <summary>
        /// Название редкости
        /// </summary>
        public string RarityName { get; set; }
        /// <summary>
        /// Цвет редкости
        /// </summary>
        public Brush RarityColor { get; set; }

        /// <summary>
        /// Редкость/раритет
        /// </summary>
        public Rarity()
        {
        }

        /// <summary>
        /// Редкость
        /// </summary>
        /// <param name="rn">Название редкости</param>
        /// <param name="rc">Цвет редкости</param>
        public Rarity(string rn, Brush rc)
        {
            RarityName = rn;
            RarityColor = rc;
        }


    }
}
