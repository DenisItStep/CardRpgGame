using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using CardGameServer.Security;

namespace CardGameServer
{
    [ServiceContract]
    public class Servicegame
    {
        [OperationContract]
        public int Login(string user, string pass)
        {
            bool isError = false;

            //0 success
            //1 incorrect pass
            //2 already online 
            //3 hacking attempt

            Program.UserThreadLock.EnterReadLock();
            Gamer gamer = Program.OnlineUsers.Find(u => u.login == user);
            Program.UserThreadLock.ExitReadLock();

            if (gamer != null)
                return 2;


            //check for sqlInjection
            if (sqlInjection.Words.Any(word => user.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0 ||
                pass.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)) return 3;

            SqlConnection db_connection = new SqlConnection(Program.connectionString);   
         
            try
            {
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT password FROM accounts where account='" + user + "'", db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                if (res.Read())
                {
                    string password = res[0].ToString();
                    res.Close();
                    db_connection.Close();

                    if (password == pass)
                    {
                        Program.UserThreadLock.EnterWriteLock();
                        Program.OnlineUsers.Add(new Gamer(user));
                        Program.UserThreadLock.ExitWriteLock();
                        return
                            0;
                    }
                    return 1;
                }

                res.Close();

                cmd = new SqlCommand("INSERT INTO accounts VALUES('" + user + "', '" + pass + "')", db_connection);

                cmd.ExecuteNonQuery();
            }

            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
                isError = true;
            }

            db_connection.Close();

            if (!isError)
            {
                Program.UserThreadLock.EnterWriteLock();
                Program.OnlineUsers.Add(new Gamer(user));
                Program.UserThreadLock.ExitWriteLock();
                return 0;
            }
            else return 4;
        }

        [OperationContract]
        public bool isAccountContainsAnyCharacter(string user)
        {
            bool contains = true;

            //check for sqlInjection
            if (sqlInjection.Words.Any(word => user.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)) return false;

            SqlConnection db_connection = new SqlConnection(Program.connectionString);

            try
            {
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT account FROM characters where account='" + user + "'", db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                contains = res.Read();

                res.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            db_connection.Close();

            return contains;
        }

        /// <summary>
        /// Создание персонажей
        /// </summary>
        [OperationContract]
        public bool createCharacter(string user, string name, int heroCardId)
        {
            //check for sqlInjection
            if (sqlInjection.Words.Any(word => user.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)) return false;

            SqlConnection db_connection = new SqlConnection(Program.connectionString);

            try
            {
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT name FROM characters where name='" + name + "'", db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                bool exists = res.Read();

                res.Close();

                if (exists)
                {
                    db_connection.Close();
                    return false;
                }

                Card card = Program.cards.Find(c => c.id == heroCardId);

                cmd = new SqlCommand("INSERT INTO characters(account, name, hero_name) VALUES('" + user + "', '" + name + "', '"
                        + card.card_name + "')", db_connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT id FROM characters where name='" + name + "'", db_connection);
                res = cmd.ExecuteReader();

                if (res.Read())
                {
                    int char_id = (int)res[0];
                    res.Close();

                    cmd = new SqlCommand("INSERT INTO character_cards(char_id, card_id, slot) VALUES" +
                            "(" + char_id + ", " + heroCardId + ", 0)", db_connection);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("INSERT INTO character_cards(char_id, card_id, slot) VALUES" +
                            "(" + char_id + ", " + Program.character_templates[heroCardId] + ", 1)", db_connection);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    res.Close();
                    db_connection.Close();
                    return false;
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
                return false;
            }

            db_connection.Close();

            return true;
        }

        [OperationContract]
        public List<Card> getHeroesTemplateAvailableList()
        {
            try
            {
                return Program.template_cards;
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
                return new List<Card>();
            }
        }

        /// <summary>
        /// Войти в игру
        /// </summary>
        [OperationContract]
        public CharInfo EnterWorld(string user)
        {
            CharInfo chInfo = null;

            //check for sqlInjection
            if (sqlInjection.Words.Any(word => user.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0))
                return chInfo;

            SqlConnection db_connection = new SqlConnection(Program.connectionString);

            try
            {
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM characters where account='" + user + "'", db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                if (res.Read())
                {
                    chInfo = new CharInfo(res["name"].ToString(), res["hero_name"].ToString(), (int)res["character_level"],
                                (int)res["exp"], (int)res["games"], (int)res["wins"], (int)res["score"]);
                }

                res.Close();

                Program.UserThreadLock.EnterReadLock();
                Gamer gamer = Program.OnlineUsers.Find(u => u.login == user);
                Program.UserThreadLock.ExitReadLock();
                if (gamer != null)
                {
                    gamer.level = chInfo.character_level;
                    gamer.nick = chInfo.nickname;
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            db_connection.Close();

            return chInfo;
        }

        [OperationContract]
        public void iAmOnline(string user)
        {
            Program.UserThreadLock.EnterReadLock();
            Gamer gamer = Program.OnlineUsers.Find(u => u.login == user);
            Program.UserThreadLock.ExitReadLock();
            if (gamer != null) gamer.lastAcc = 0;
        }


        [OperationContract]
        public List<CharInfo> getRanking()
        {
            List<CharInfo> ch = new List<CharInfo>();

            SqlConnection db_connection = new SqlConnection(Program.connectionString);

            try
            {
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT TOP 10 * FROM characters ORDER BY character_level DESC, wins DESC, games DESC",
                    db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    ch.Add(new CharInfo(res["name"].ToString(), res["hero_name"].ToString(), (int)res["character_level"],
                                (int)res["exp"], (int)res["games"], (int)res["wins"]));
                }

                res.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            db_connection.Close();

            return ch;
        }

        /// <summary>
        /// Поиск боя
        /// </summary>
        [OperationContract]
        public Game findGame(string nickname)
        {
            //check for sqlInjection
            if (sqlInjection.Words.Any(word => nickname.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)) return null;

            int char_id = -1;
            int character_level = -1;

            List<Card> gamerCard = new List<Card>();
            Game game = null;

            bool sqlConClose = false;

            SqlConnection db_connection = new SqlConnection(Program.connectionString);
            try
            {
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT id, character_level FROM characters where name='" + nickname + "'", db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                if (res.Read())
                {
                    char_id = (int)res["id"];
                    character_level = (int)res["character_level"];
                    res.Close();

                    cmd = new SqlCommand("SELECT card_id, slot FROM character_cards where char_id=" + char_id +
                        " AND slot >= 0 AND slot <= 8", db_connection);

                    res = cmd.ExecuteReader();

                    while (res.Read())
                    {
                        Card currcard = Program.cards.Find(ccc => ccc.id == (int)res["card_id"]);

                        if (currcard != null)
                        {
                            int hp_bust, dmg_bust, def_bust;

                            //test boost
                            hp_bust = currcard.type == 0 ? (character_level > 1 ? character_level / 2 : 0) : 0;
                            dmg_bust = currcard.type == 0 ? (character_level > 4 ? character_level / 2 - 1 : 0) : 0;
                            def_bust = currcard.type == 0 ? (character_level > 5 ? character_level / 2 - 2 : 0) : 0;

                            gamerCard.Add(new Card(currcard.id, currcard.card_name, currcard.type, currcard.hp + hp_bust,
                                currcard.dmg + dmg_bust, currcard.def + def_bust, currcard.cardRarity, (int)res["slot"])
                            {
                                min_level = currcard.min_level
                            });
                        }
                    }
                }
                res.Close();

                db_connection.Close();

                sqlConClose = true;

                bool find = false;

                while (!find)
                {
                    Program.GameThreadLock.EnterReadLock();

                    // Интеллектуальный подбор игры
                    // state == 1
                    // level +- 1
                    // кол-во карт равное
                    /*game = Program.OnlineGames.Find(g => g.gameState == 1 && g.Level >= (character_level -1) &&
                        g.Level <= (character_level + 1) && g.firstGamerCards.Count == gamerCard.Count);*/

                    game = Program.OnlineGames.Find(g => g.gameState == 1);

                    Program.GameThreadLock.ExitReadLock();

                    if (game != null)
                    {
                        lock (game)
                        {
                            if (game.gameState != 1) continue;

                            game.AddSecondUser(nickname, gamerCard);
                            return game;
                        }
                    }
                    else find = true;
                }


                game = new Game(nickname, gamerCard);
                game.Level = character_level;


                Program.GameThreadLock.EnterWriteLock();
                Program.OnlineGames.Add(game);

                Program.GameThreadLock.ExitWriteLock();

            }

            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);

                if (!sqlConClose)
                    db_connection.Close();
            }


            return game;
        }


        /// <summary>
        /// Создание Игры
        /// </summary>
        [OperationContract]
        public Game getGame(string nickname)
        {
            Game game = null;

            try
            {
                Program.GameThreadLock.EnterReadLock();

                game = Program.OnlineGames.Find(g => g.Gamers.Contains(nickname));

                Program.GameThreadLock.ExitReadLock();

                bool remove = false;

                if (game != null)
                {
                    lock (game)
                    {
                        if (nickname == game.Gamers[0])
                            game.fGamer.lastAcc = 0;
                        else game.tGamer.lastAcc = 0;


                        if (game.gameState == 4 || game.gameState == 5 || game.gameState == 7)
                        {
                            game.Gamers.Remove(nickname);
                            if (game.Gamers.Count == 0)
                            {
                                remove = true;
                            }
                        }
                        else if (game.gameState == 2)
                        {
                            if (game.Init1 || game.Init2)
                                Program.WriteGameLog(game);


                            try
                            {
                                if (nickname == game.Gamers[0]) game.Init1 = false;
                                if (nickname == game.Gamers[1]) game.Init2 = false;
                            }
                            catch { }
                        }

                    }

                    if (remove)
                    {
                        Program.GameThreadLock.EnterWriteLock();
                        Program.OnlineGames.Remove(game);
                        Program.GameThreadLock.ExitWriteLock();
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            return game;
        }

        /// <summary>
        /// окончание поиска
        /// </summary>
        [OperationContract]
        public bool cancelSearch(string nickname)
        {
            bool remove = false;

            try
            {
                Program.GameThreadLock.EnterReadLock();
                Game game = Program.OnlineGames.Find(g => g.Gamers.Contains(nickname));
                Program.GameThreadLock.ExitReadLock();

                if (game != null)
                {
                    lock (game)
                    {
                        if (game.gameState == 1)
                        {
                            game.gameState = 7; //canceled
                            remove = true;
                        }
                    }
                    if (remove)
                    {
                        //    Program.GameThreadLock.EnterWriteLock();
                        //   Program.OnlineGames.Remove(game);
                        //   Program.GameThreadLock.ExitWriteLock();
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            return remove;
        }


        /// <summary>
        /// Атаковать
        /// </summary>
        [OperationContract]
        public LastHitInfo DoAttack(string nickname, int myslot, int enslot)
        {
            int dmg = -1;
            LastHitInfo lhi = null;
            try
            {
                Program.GameThreadLock.EnterReadLock();
                Game game = Program.OnlineGames.Find(g => g.Gamers.Contains(nickname));
                Program.GameThreadLock.ExitReadLock();

                bool isWin = false;

                if (game != null)
                {
                    lock (game)
                    {
                        if (game.currUsr != nickname) return lhi;

                        foreach (var item in game.firstGamerCards)
                        {
                            item.IsAttacked = false;
                        }

                        foreach (var item in game.twoGamerCards)
                        {
                            item.IsAttacked = false;
                        }

                        game.lastHitInfo.isMissed = false;
                        game.lastHitInfo.IsCritical = false;


                        if (game.gameState == 2)
                        {
                            Card myC = game.firstGamerCards.Find(cc => cc.slot == myslot);
                            Card enC = game.twoGamerCards.Find(cc => cc.slot == enslot);

                            //myC.Enabled = false;

                            if (!Formulas.IsMiss()) //miss?
                            {
                                //calc real damage
                                dmg = Formulas.CalculateDamage(myC.dmg, enC.def);

                                if (dmg <= 0) dmg = 1;
                                else
                                {
                                    if (Formulas.IsCrit()) //crit damage
                                    {
                                        dmg = Formulas.CalculateCritDamage(dmg);
                                        game.lastHitInfo.IsCritical = true;
                                    }
                                }


                                int temp = enC.hp; //save curr hp                          

                                enC.hp = enC.hp - dmg;

                                //test
                                //enC.dmg = enC.dmg - dmg/2;
                                //if (enC.dmg < 1) enC.dmg = 1;

                                if (enC.hp <= 0)
                                {
                                    if (enC.hp < 0) dmg = temp; //if overhit -> change hp
                                    enC.hp = 0;
                                    enC.Enabled = false;
                                }
                                else enC.TryEnjured((double)dmg, (double)temp);
                            }
                            else game.lastHitInfo.isMissed = true;


                            game.lastHitInfo.slot = myslot;
                            game.lastHitInfo.dmg = dmg;

                            lhi = game.lastHitInfo;

                            enC.IsAttacked = true;


                            isWin = game.CheckWinner();

                            //if (!isWin && game.firstGamerCards.Find(cc => cc.Enabled == true) == null)

                            if (!isWin) //3 or < cards per turn... test
                            {
                                /* int cardsPerTurn = 3;

                                 if (game.firstGamerCards.FindAll(cc=> cc.hp > 0).Count >= 3)
                                     cardsPerTurn = 3;
                                 else if (game.firstGamerCards.FindAll(cc => cc.hp > 0).Count == 2)
                                     cardsPerTurn = 2;
                                 else if (game.firstGamerCards.FindAll(cc => cc.hp > 0).Count == 1)
                                     cardsPerTurn = 1;


                                 if (game.firstGamerCards.FindAll(cc => cc.Enabled == false && cc.hp > 0).Count >= cardsPerTurn)
                                 {
                                     foreach (var item in game.firstGamerCards)
                                     {
                                         if (item.hp > 0) item.Enabled = true;
                                     }
                                 */

                                game.currUsr = game.Gamers[1];
                                game.gameState = 3;
                                //}
                            }

                            else if (isWin)
                            {
                                setReward(game.Gamers[0], game.WinGamerReward, game, true);
                                setReward(game.Gamers[1], game.LooseGamerReward, game, false);
                            }
                        }
                        else if (game.gameState == 3)
                        {
                            Card myC = game.twoGamerCards.Find(cc => cc.slot == myslot);
                            Card enC = game.firstGamerCards.Find(cc => cc.slot == enslot);

                            //myC.Enabled = false;

                            if (!Formulas.IsMiss()) //miss?
                            {
                                //calc real damage
                                dmg = Formulas.CalculateDamage(myC.dmg, enC.def);

                                if (dmg <= 0) dmg = 1;
                                else
                                {
                                    if (Formulas.IsCrit()) //crit damage
                                    {
                                        dmg = Formulas.CalculateCritDamage(dmg);
                                        game.lastHitInfo.IsCritical = true;
                                    }
                                }

                                int temp = enC.hp; //save curr hp                          

                                enC.hp = enC.hp - dmg;

                                //test
                                //enC.dmg = enC.dmg - dmg/2;
                                //if (enC.dmg < 1) enC.dmg = 1;

                                if (enC.hp <= 0)
                                {
                                    if (enC.hp < 0) dmg = temp; //if overhit -> change hp
                                    enC.hp = 0;
                                    enC.Enabled = false;
                                }
                                else enC.TryEnjured((double)dmg, (double)temp);
                            }
                            else game.lastHitInfo.isMissed = true;

                            game.lastHitInfo.slot = myslot;
                            game.lastHitInfo.dmg = dmg;

                            lhi = game.lastHitInfo;

                            enC.IsAttacked = true;

                            isWin = game.CheckWinner();

                            //if (!isWin && game.twoGamerCards.Find(cc => cc.Enabled == true) == null)

                            if (!isWin) //3 or < cards per turn... test
                            {
                                /*int cardsPerTurn = 3;

                                if (game.twoGamerCards.FindAll(cc => cc.hp > 0).Count >= 3)
                                    cardsPerTurn = 3;
                                else if (game.twoGamerCards.FindAll(cc => cc.hp > 0).Count == 2)
                                    cardsPerTurn = 2;
                                else if (game.twoGamerCards.FindAll(cc => cc.hp > 0).Count == 1)
                                    cardsPerTurn = 1;


                                if (game.twoGamerCards.FindAll(cc => cc.Enabled == false && cc.hp > 0).Count >= cardsPerTurn)
                                {
                                    foreach (var item in game.twoGamerCards)
                                    {
                                        if (item.hp > 0) item.Enabled = true;
                                    }
                                */
                                game.currUsr = game.Gamers[0];
                                game.gameState = 2;
                                // }
                            }

                            else if (isWin)
                            {
                                setReward(game.Gamers[1], game.WinGamerReward, game, true);
                                setReward(game.Gamers[0], game.LooseGamerReward, game, false);
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }
            return lhi;
        }

        [OperationContract]
        public void leaveGame(string nickname)
        {
            try
            {
                Program.GameThreadLock.EnterReadLock();
                Game game = Program.OnlineGames.Find(g => g.Gamers.Contains(nickname));
                Program.GameThreadLock.ExitReadLock();

                bool remove = false;

                if (game != null)
                {
                    lock (game)
                    {
                        if (game.gameState != 4 && game.gameState != 5)
                        {
                            game.Gamers.Remove(nickname);
                            game.gameState = 5;

                            if (game.fGamer.nick == nickname)
                                game.fGamer = game.tGamer;

                            game.getReward(nickname);
                            game.getReward(game.Gamers[0], true);

                            setReward(game.Gamers[0], game.WinGamerReward, game, true);
                            setReward(nickname, game.LooseGamerReward, game, false);


                            if (game.Gamers.Count == 0)
                            {
                                remove = true;
                            }
                        }
                    }

                    if (remove)
                    {
                        Program.GameThreadLock.EnterWriteLock();
                        Program.OnlineGames.Remove(game);
                        Program.GameThreadLock.ExitWriteLock();
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }


        }

        [OperationContract]
        public void Logout(string user)
        {
            try
            {
                Program.GameThreadLock.EnterReadLock();
                Game game = Program.OnlineGames.Find(g => g.Gamers.Contains(user));
                Program.GameThreadLock.ExitReadLock();

                if (game != null) leaveGame(user);

                Program.UserThreadLock.EnterReadLock();
                Gamer gamer = Program.OnlineUsers.Find(u=>u.login == user);
                Program.UserThreadLock.ExitReadLock();

                if (gamer != null)
                {
                    Program.UserThreadLock.EnterWriteLock();
                    Program.OnlineUsers.Remove(gamer);
                    Program.UserThreadLock.ExitWriteLock();
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }
        }

        public static void setReward(string nickname, Reward reward, Game game, bool Winner = false)
        {
            SqlConnection db_connection = new SqlConnection(Program.connectionString);

            try
            {
                db_connection.Open();

                int c_id;
                int level;
                int exp;
                int games;
                int wins;
                int score;

                SqlCommand cmd = new SqlCommand("SELECT * FROM characters WHERE name='" + nickname + "'", db_connection);

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    c_id = (int)rd["id"];
                    level = (int)rd["character_level"];
                    exp = (int)rd["exp"];
                    games = (int)rd["games"];
                    wins = (int)rd["wins"];
                    score = (int)rd["score"];

                    rd.Close();

                    exp += reward.Exp;

                    score += reward.Score;

                    if (exp >= Level.Levels[level + 1])
                    {
                        reward.NewLevel = true;
                        level += 1;
                    }

                    if (Winner) wins++;

                    games++;

                    cmd = new SqlCommand("UPDATE characters SET exp=" + exp + ", character_level=" + level + ", games="
                        + games + ", wins=" + wins + ", score=" + score + " WHERE id=" + c_id, db_connection);

                    cmd.ExecuteNonQuery();


                    if (reward.NewCard != null)
                    {
                        int slot = -1;

                        /*if (nickname == game.Gamers[0])
                        {
                            List<Card> cList1 = game.firstGamerCards.FindAll(c => c.slot <= 8);
                            if (cList1 != null)
                                slot = cList1.Max(ccc => ccc.slot);
                        }
                        else
                        {
                            List<Card> cList2 = game.twoGamerCards.FindAll(c => c.slot <= 8);
                            if (cList2 != null)
                                slot = cList2.Max(ccc => ccc.slot);
                        }

                        if (slot == 8)
                        {*/
                            cmd = new SqlCommand("SELECT MAX(slot) FROM character_cards WHERE char_id=" + c_id + "", db_connection);

                            rd = cmd.ExecuteReader();

                            if (rd.Read())
                            {
                                slot = (int)rd[0];

                                if (slot < 10) slot = 9;
                            }

                            rd.Close();

                        //}
                        
                        slot++;


                        cmd = new SqlCommand("INSERT INTO character_cards(char_id, card_id, slot) VALUES (" + c_id + ", "
                            + reward.NewCard.id + ", " + slot +")", db_connection);

                        cmd.ExecuteNonQuery();
                    }

                }
                else rd.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            db_connection.Close();
        }

        /// <summary>
        /// Получить все карты
        /// </summary>
        [OperationContract]
        public List<Card> GetAllCard(string user, int page = 1)
        {
            //check for sqlInjection
            if (sqlInjection.Words.Any(word => user.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)) return null;

            int char_id = -1;
            int character_level = -1;

            const int cnt_perpage = 24;

            List<Card> gamerCard = new List<Card>();

            SqlConnection db_connection = new SqlConnection(Program.connectionString);
            try
            {
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT id, character_level FROM characters where account='" + user + "'", db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                if (res.Read())
                {
                    char_id = (int)res["id"];
                    character_level = (int)res["character_level"];
                    res.Close();


                    int l_index = page * cnt_perpage + 10;
                    int f_index = (l_index - 24);

                    cmd = new SqlCommand("SELECT card_id, slot FROM character_cards where char_id=" + char_id +
                        " AND slot>=" + f_index + " AND slot <" + l_index, db_connection);

                    res = cmd.ExecuteReader();

                    while (res.Read())
                    {
                        Card currcard = Program.cards.Find(ccc => ccc.id == (int)res["card_id"]);

                        if (currcard != null)
                        {

                            gamerCard.Add(new Card(currcard.id, currcard.card_name, currcard.type, currcard.hp,
                                currcard.dmg, currcard.def, currcard.cardRarity, (int)res["slot"])
                            {
                                min_level = currcard.min_level
                            });
                        }
                    }

                    res.Close();


                    cmd = new SqlCommand("SELECT card_id, slot FROM character_cards where char_id=" + char_id +
                        " AND slot>=1 AND slot<=8", db_connection);

                    res = cmd.ExecuteReader();

                    while (res.Read())
                    {
                        Card currcard = Program.cards.Find(ccc => ccc.id == (int)res["card_id"]);
                        if (currcard != null)
                        {
                            gamerCard.Add(new Card(currcard.id, currcard.card_name, currcard.type, currcard.hp,
                                 currcard.dmg, currcard.def, currcard.cardRarity, (int)res["slot"])
                            {
                                min_level = currcard.min_level
                            });
                        }
                    }

                }
                res.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            db_connection.Close();

            return gamerCard;
        }

        /// <summary>
        /// Изменение слотов для карт
        /// </summary>
        [OperationContract]
        public bool ChangeCardslot(string user, int oslot, int nSlot)
        {
            if (sqlInjection.Words.Any(word => user.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)) return false;

            int char_id = -1;

            SqlConnection db_connection = new SqlConnection(Program.connectionString);
            try
            {
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT id FROM characters where account='" + user + "'", db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                if (res.Read())
                {
                    char_id = (int)res["id"];
                    res.Close();

                    // int oslot_cardid = -1;
                    // int nslot_cardid = -1;


                    // cmd = new SqlCommand("SELECT card_id FROM character_cards where char_id=" + char_id + " AND slot=" + oslot, db_connection);
                    // res = cmd.ExecuteReader();

                    //if (res.Read())
                    // {
                    //    oslot_cardid = (int)res[0];
                    //    res.Close();                         

                    cmd = new SqlCommand("UPDATE character_cards SET slot=" + nSlot + " where char_id=" + char_id + " AND slot=" + oslot,
                        db_connection);
                    cmd.ExecuteNonQuery();

                    //cmd = new SqlCommand("UPDATE character_cards SET slot=" + oslot + " where char_id=" + char_id + " AND card_id=" +
                    // + nslot_cardid, db_connection);
                    //cmd.ExecuteNonQuery();
                    // }                      
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            db_connection.Close();

            return true;

        }

        /// <summary>
        /// Создание пустых слотов для всех карт
        /// </summary>
        [OperationContract]
        public int GetFreeSlotNumberAllCards(string user)
        {
            if (sqlInjection.Words.Any(word => user.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)) return -1;

            int char_id = -1;
            int slot = -1;

            List<Card> gamerCard = new List<Card>();

            SqlConnection db_connection = new SqlConnection(Program.connectionString);
            try
            {
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT id FROM characters where account='" + user + "'", db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                if (res.Read())
                {
                    char_id = (int)res["id"];
                    res.Close();

                    cmd = new SqlCommand("SELECT MAX(slot) FROM character_cards where char_id=" + char_id +
                        " AND slot >= 8", db_connection);

                    res = cmd.ExecuteReader();

                    if (res.Read())
                    {
                        try
                        {
                            slot = (int)res[0];

                            if (slot == 8)
                            {
                                slot = 9;
                            }
                            slot++;
                        }
                        catch { }
                    }

                    if (slot == -1) slot = 10;
                }
                res.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            db_connection.Close();


            return slot;
        }

        /// <summary>
        /// Покупка карт
        /// </summary>
        [OperationContract]
        public List<Card> BuyCard(string user, int number)
        {
            if (sqlInjection.Words.Any(word => user.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)) return null;

            int char_id = -1;
            bool udovlet = true;
            List<Card> result = new List<Card>();

            SqlConnection db_connection = new SqlConnection(Program.connectionString);
            try
            {
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT id, character_level, score FROM characters where account='" + user + "'", db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                if (res.Read())
                {
                    char_id = (int)res["id"];
                    int score = (int)res["score"];
                    int char_level = (int)res["character_level"];
                    res.Close();

                    if (char_level < 3 && number > 1)
                        udovlet = false;



                    if (udovlet && ((score >= 2000 && number == 1) || (score >= 4000 && number == 2) || (score >= 8000 && number == 3)))
                    {
                        int slot = GetFreeSlotNumberAllCards(user);

                        List<Card> cclist;

                        int card_id1 = -1;

                        int card_id2 = -1;

                        int card_id3 = -1;

                        bool find = false;

                        cclist = Card.GetAllavailableCards(user);

                        if (cclist.Count > 0)
                        {
                            if (number == 1)
                            {
                                result.Add(cclist[Formulas.RndNext(0, cclist.Count)]);
                                result.Add(cclist[Formulas.RndNext(0, cclist.Count)]);
                                result.Add(cclist[Formulas.RndNext(0, cclist.Count)]);

                                card_id1 = result[0].id;
                                card_id2 = result[1].id;
                                card_id3 = result[2].id;

                                score -= 2000;
                            }
                            else if (number == 2)
                            {
                                find = false;
                                while (!find)
                                {
                                    Card cc = cclist[Formulas.RndNext(0, cclist.Count)];
                                    if (cc.cardRarity >= 2)
                                    {
                                        result.Add(cc);
                                        find = true;
                                    }
                                }

                                result.Add(cclist[Formulas.RndNext(0, cclist.Count)]);
                                result.Add(cclist[Formulas.RndNext(0, cclist.Count)]);

                                card_id1 = result[0].id;
                                card_id2 = result[1].id;
                                card_id3 = result[2].id;

                                score -= 4000;
                            }
                            else if (number == 3)
                            {
                                find = false;
                                while (!find)
                                {
                                    Card cc = cclist[Formulas.RndNext(0, cclist.Count)];
                                    if (cc.cardRarity >= 2)
                                    {
                                        result.Add(cc);
                                        find = true;
                                    }
                                }

                                find = false;
                                while (!find)
                                {
                                    Card cc = cclist[Formulas.RndNext(0, cclist.Count)];
                                    if (cc.cardRarity >= 2)
                                    {
                                        result.Add(cc);
                                        find = true;
                                    }
                                }

                                result.Add(cclist[Formulas.RndNext(0, cclist.Count)]);

                                card_id1 = result[0].id;
                                card_id2 = result[1].id;
                                card_id3 = result[2].id;

                                score -= 8000;
                            }

                            if (card_id1 != -1 && card_id2 != -1 && card_id3 != -1)
                            {
                                cmd = new SqlCommand("UPDATE characters SET score=" + score + " WHERE id=" + char_id, db_connection);
                                cmd.ExecuteNonQuery();

                                cmd = new SqlCommand("INSERT INTO character_cards(char_id, card_id, slot) VALUES (" + char_id + ", " +
                                    card_id1 + ", " + slot + ")", db_connection);
                                cmd.ExecuteNonQuery();

                                cmd = new SqlCommand("INSERT INTO character_cards(char_id, card_id, slot) VALUES (" + char_id + ", " +
                                    card_id2 + ", " + (slot + 1) + ")", db_connection);
                                cmd.ExecuteNonQuery();

                                cmd = new SqlCommand("INSERT INTO character_cards(char_id, card_id, slot) VALUES (" + char_id + ", " +
                                    card_id3 + ", " + (slot + 2) + ")", db_connection);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                }
                res.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            db_connection.Close();

            return result;
        }

        [OperationContract]
        public bool SellCard(string user, int slot)
        {
            if (sqlInjection.Words.Any(word => user.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)) return false;

            int char_id = -1;
            int score = -1;
            bool result = false;

            SqlConnection db_connection = new SqlConnection(Program.connectionString);
            try
            {
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT id, score FROM characters where account='" + user + "'", db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                if (res.Read())
                {
                    char_id = (int)res["id"];
                    score = (int)res["score"];
                    res.Close();

                    cmd = new SqlCommand("DELETE FROM character_cards where char_id=" + char_id + " AND slot=" + slot, db_connection);
                    cmd.ExecuteNonQuery();

                    score += 350;

                    cmd = new SqlCommand("UPDATE characters SET score=" + score + " WHERE id=" + char_id, db_connection);
                    cmd.ExecuteNonQuery();

                    result = true;

                }
                else 
                    res.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message + "\r\n" + exc.StackTrace);
                Program.dumpException(exc);
            }

            db_connection.Close();

            return result;
        }
    }
}
