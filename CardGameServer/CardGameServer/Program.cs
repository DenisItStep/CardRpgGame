using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading;
using System.IO;

namespace CardGameServer
{
    public class Program
    {
        public static string connectionString;

        static bool useRemoteSQLConnetion = false;

        public static Dictionary<int, int> character_templates = new Dictionary<int,int>();
        public static List<Card> cards = new List<Card>();
        public static List<Card> template_cards = new List<Card>();

        public static List<Gamer> OnlineUsers = new List<Gamer>();
        public static List<Game> OnlineGames = new List<Game>();

        public static ReaderWriterLockSlim GameThreadLock = new ReaderWriterLockSlim();
        public static ReaderWriterLockSlim UserThreadLock = new ReaderWriterLockSlim();

        public static Random Rnd = Formulas.Rnd;

        public static object fs = new object();


        static void detectTimeout()
        {
            while (true)
            {
                UserThreadLock.EnterReadLock();
                List<Gamer> users = new List<Gamer>(OnlineUsers);
                UserThreadLock.ExitReadLock();

                foreach (var user in users)
                {
                    user.lastAcc++;

                    if (user.lastAcc > 20)
                    {
                        GameThreadLock.EnterReadLock();
                        Game game = OnlineGames.Find(g => g.Gamers.Exists(u => u == user.nick));
                        GameThreadLock.ExitReadLock();

                        bool remove = false;

                        try
                        {
                            if (game != null)
                            {
                                lock (game)
                                {
                                    if (game.gameState != 4 || game.gameState != 5)
                                    {
                                        game.getReward(user.nick);
                                        game.Gamers.Remove(user.nick);
                                        game.getReward(game.Gamers[0], true);
                                        Servicegame.setReward(game.Gamers[0], game.WinGamerReward, game, true);
                                        Servicegame.setReward(user.nick, game.LooseGamerReward, game, false);
                                        game.gameState = 5;                                        


                                        if (game.Gamers.Count == 0)
                                        {
                                            remove = true;
                                        }
                                    }
                                    else
                                    {
                                        game.Gamers.Remove(user.nick);

                                        if (game.Gamers.Count == 0)
                                        {
                                            remove = true;
                                        }
                                    }
                                }
                            }
                        }
                        catch 
                        {

                        }

                        UserThreadLock.EnterWriteLock();
                        OnlineUsers.Remove(user);
                        UserThreadLock.ExitWriteLock();

                        if (remove)
                        {
                            GameThreadLock.EnterWriteLock();
                            OnlineGames.Remove(game);
                            GameThreadLock.ExitWriteLock();
                        }
                    }
                }

                Thread.Sleep(1000);
            }
        }


        static void Main(string[] args)
        {
            ServiceHost host = null;
            try
            {
                host = new ServiceHost(typeof(Servicegame));

                if (useRemoteSQLConnetion)
                    connectionString = Properties.Settings.Default.avalon_dbConnectionStringWithSqlAuth;
                else
                    connectionString = Properties.Settings.Default.avalon_dbConnectionString;

                SqlConnection db_connection = new SqlConnection(Program.connectionString);

                //Load all Character Templates from DB
                db_connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM character_templates", db_connection);

                SqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    character_templates.Add((int)res["hero_card"], (int)res["first_card"]);
                }

                Console.WriteLine("INFO: Из базы было загружено " + character_templates.Count + " персонажей");

                res.Close();


                //Load all Card from DB

                cmd = new SqlCommand("SELECT * FROM cards", db_connection);

                res = cmd.ExecuteReader();

                while (res.Read())
                {
                    Card ccc = new Card(
                        (int)res["id"],
                        res["card_name"].ToString(),
                        (int)res["type"],
                        (int)res["hp"],
                        (int)res["dmg"],
                        (int)res["def"],
                        (int)res["rare"]
                        );
                    ccc.min_level = (int)res["min_level"];
                    cards.Add(ccc);
                }

                db_connection.Close();

                //create card Heroes Template list
                foreach (var item in character_templates.Keys)
                {
                    template_cards.Add(cards.Find(c => c.id == item));
                }           

                Console.WriteLine("INFO: Из базы было загружено " + cards.Count + " карт");

                Console.WriteLine("===============================");

                host.Open();


                Thread detectTimeoutThread = new Thread(detectTimeout) { IsBackground = true };
                detectTimeoutThread.Start();


                Uri startedUri = host.Description.Endpoints[0].Address.Uri;

                Console.WriteLine("Сервер запущен по аддресу: " + startedUri.Host + ":" + startedUri.Port);

                Console.WriteLine("===============================");

                Console.WriteLine("Нажмите ESC для выхода...");


                while (Console.ReadKey().Key != ConsoleKey.Escape)
                {
                    
                }

                host.Close();

            }
            catch(Exception exc)
            {
                Console.WriteLine("ERROR: " + exc.Message);
                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey();

            }
        }



        public static void dumpException(Exception exc)
        {
            try
            {
                lock (Program.Rnd)
                {
                    if (!Directory.Exists("Log")) Directory.CreateDirectory("Log");

                    string dir_name = "Log/" + DateTime.Now.ToString("dd_MMM");
                    if (!Directory.Exists(dir_name))
                        Directory.CreateDirectory(dir_name);

                    File.WriteAllText(dir_name + "/error_" + DateTime.Now.ToString("dd_MMM_HH_mm_ss") + ".log", 
                        exc.Message + "\r\n" + exc.StackTrace);
                }
            }
            catch { }
        }

        public static void WriteGameLog(Game game)
        {
            try
            {
                lock (fs)
                {
                    FileStream fileStream = new FileStream("game_" + DateTime.Now.ToString("dd_MMM_HH") + ".log", FileMode.OpenOrCreate,
                        FileAccess.ReadWrite);

                    fileStream.Position = fileStream.Length;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("===================================");
                    sb.AppendLine("==========" + DateTime.Now.ToString("dd_MMM_HH_mm_ss") + "===========");
                    sb.AppendLine("===================================");
                    sb.AppendLine("Количество игроков: " + game.Gamers.Count);
                    sb.AppendLine("Первый игрок: " + game.fGamer.nick);
                    sb.AppendLine("Второй игрок: " + game.tGamer.nick);
                    sb.AppendLine("Победитель: " + game.currUsr);
                    sb.AppendLine("===================================\r\n");

                    fileStream.Write(Encoding.UTF8.GetBytes(sb.ToString()), 0, Encoding.UTF8.GetBytes(sb.ToString()).Count());
                    fileStream.Close();
                }
            }
            catch { }
        }
    }
}
