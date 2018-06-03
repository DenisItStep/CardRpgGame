using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace CardGameServer
{
    [DataContract]
    public class Card
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string card_name { get; set; }

        public int type { get; set; }

        [DataMember]
        public int hp { get; set; }

        [DataMember]
        public int dmg { get; set; }

        [DataMember]
        public int def { get; set; }

        [DataMember]
        public int slot { get; set; }

        [DataMember]
        public bool Enabled { get; set; }

        [DataMember]
        public int cardRarity { get; set; }

        [DataMember]
        public bool IsInjury { get; set; } //effect

        [DataMember]
        public bool IsAttacked { get; set; }

        public bool Use = false;

        [DataMember] public int min_level = 1;

        public Card()
        {
        }

        public Card(int id, string cn, int t, int hp, int dmg, int def, int r, int sl = -1)
        {
            this.id = id;
            card_name = cn;
            type = t;
            this.hp = hp;
            this.dmg = dmg;
            this.def = def;
            slot = sl;
            cardRarity = r;
            Enabled = true;
            IsInjury = false;
            IsAttacked = false;
        }

        public void TryEnjured(double dmg, double temp)
        {
            if (!IsInjury && dmg >= temp / 3) //Injury effect
            {
                //test
                IsInjury = true;
                var tmp = this.dmg * 0.3;
                this.dmg -= (int) tmp;
                if (tmp % 1 >= 0.5) this.dmg -= 1;

                if (this.dmg < 1) this.dmg = 1;
            }
        }


        public static List<Card> GetAllavailableCards(string user)
        {
            var char_level = 1;
            var tmp = new List<Card>();

            var db_connection = new SqlConnection(Program.connectionString);
            try
            {
                db_connection.Open();

                var cmd = new SqlCommand("SELECT character_level FROM characters where account='" + user + "'",
                    db_connection);

                var res = cmd.ExecuteReader();

                if (res.Read())
                {
                    char_level = (int) res["character_level"];
                    res.Close();

                    if (char_level < 3)
                        tmp = Program.cards.FindAll(ccc => ccc.type != 0 && ccc.min_level <= char_level);
                    else
                        tmp = Program.cards.FindAll(ccc =>
                            ccc.type != 0 && ccc.min_level >= char_level - 2 && ccc.min_level <= char_level
                        );
                }
                else
                {
                    res.Close();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            db_connection.Close();

            return tmp;
        }


        public static List<Card> GetAllavailableCardsByNickName(string nickname)
        {
            var char_level = 1;
            var tmp = new List<Card>();

            var db_connection = new SqlConnection(Program.connectionString);
            try
            {
                db_connection.Open();

                var cmd = new SqlCommand("SELECT character_level FROM characters where name='" + nickname + "'",
                    db_connection);

                var res = cmd.ExecuteReader();

                if (res.Read())
                {
                    char_level = (int) res["character_level"];
                    res.Close();

                    if (char_level < 3)
                        tmp = Program.cards.FindAll(ccc => ccc.type != 0 && ccc.min_level <= char_level);
                    else
                        tmp = Program.cards.FindAll(ccc =>
                            ccc.type != 0 && ccc.min_level >= char_level - 2 && ccc.min_level <= char_level
                        );
                }
                else
                {
                    res.Close();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            db_connection.Close();

            return tmp;
        }
    }
}