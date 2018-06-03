using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CardGameServer
{
    [DataContract]
    public class Game
    {
        [DataMember]
        public LastHitInfo lastHitInfo { get; set; }

        [DataMember] public Gamer fGamer;

        [DataMember] public Gamer tGamer;

        //debug
        public bool Init1 = true;
        public bool Init2 = true;


        public int Level { get; set; }

        [DataMember] public List<string> Gamers = new List<string>();

        [DataMember] public List<Card> firstGamerCards;

        [DataMember] public List<Card> twoGamerCards;

        [DataMember] public int gameState;

        [DataMember] public string currUsr;

        [DataMember]
        public Reward WinGamerReward { get; set; }

        [DataMember]
        public Reward LooseGamerReward { get; set; }

        public Game(Game oldGame)
        {
            Gamers = new List<string>(oldGame.Gamers);
            firstGamerCards = new List<Card>(oldGame.firstGamerCards);
            twoGamerCards = new List<Card>(oldGame.twoGamerCards);
            gameState = oldGame.gameState;
            currUsr = oldGame.currUsr;
            WinGamerReward = oldGame.WinGamerReward;
            LooseGamerReward = oldGame.LooseGamerReward;
        }


        public Game(string user, List<Card> fgc)
        {
            //Rnd = new Random();

            lastHitInfo = new LastHitInfo();

            Gamers.Add(user);
            currUsr = user;
            firstGamerCards = new List<Card>(fgc);

            Program.UserThreadLock.EnterReadLock();
            fGamer = Program.OnlineUsers.Find(u => u.nick == user);
            Program.UserThreadLock.ExitReadLock();

            gameState = 1;
        }

        public void AddSecondUser(string user, List<Card> tgc)
        {
            Program.UserThreadLock.EnterReadLock();
            var gamer = Program.OnlineUsers.Find(u => u.nick == user);
            Program.UserThreadLock.ExitReadLock();


            var dmg1 = firstGamerCards.Sum(ccc => ccc.dmg);
            var dmg2 = tgc.Sum(ccc => ccc.dmg);


            var def1 = firstGamerCards.Sum(ccc => ccc.def);
            var def2 = tgc.Sum(ccc => ccc.def);


            var hp1 = firstGamerCards.Sum(ccc => ccc.hp);
            var hp2 = tgc.Sum(ccc => ccc.hp);


            if (dmg2 < dmg1 && def2 < def1 && hp2 < hp1)
            {
                Gamers.Insert(0, user);
                twoGamerCards = firstGamerCards;
                firstGamerCards = new List<Card>(tgc);
                currUsr = user;
                tGamer = fGamer;
                fGamer = gamer;
            }
            /*int initatv1 = firstGamerCards.Sum(ccc => ccc.Initiative);
            int initatv2 = tgc.Sum(ccc => ccc.Initiative);


            if (initatv2 > initatv1)
            {
                Gamers.Insert(0, user);
                twoGamerCards = firstGamerCards;
                firstGamerCards = new List<Card>(tgc);
                currUsr = user;
                tGamer = fGamer;
                fGamer = gamer;
            }*/
            else
            {
                Gamers.Add(user);
                twoGamerCards = new List<Card>(tgc);
                tGamer = gamer;
            }

            gameState = 2;
        }

        public bool CheckWinner()
        {
            if (firstGamerCards.FindAll(c => c.hp > 0).Count == 0
                || twoGamerCards.FindAll(c => c.hp > 0).Count == 0)
            {
                if (currUsr == Gamers[0])
                {
                    getReward(Gamers[0], true);
                    getReward(Gamers[1]);
                }
                else
                {
                    getReward(Gamers[0]);
                    getReward(Gamers[1], true);
                }

                gameState = 4;
                return true;
            }

            return false;
        }

        public void getReward(string nickname, bool isWinner = false)
        {
            var reward = new Reward();

            if (isWinner)
            {
                reward.Exp = Formulas.RndNext(245, 255);
                reward.Score = Formulas.RndNext(123, 133);
                if (Formulas.RndNext(0, 3) == 1) //drop card % winner
                {
                    var clst = Card.GetAllavailableCardsByNickName(nickname);
                    if (clst.Count > 1) reward.NewCard = clst[Formulas.RndNext(0, clst.Count)];
                }

                WinGamerReward = reward;
            }
            else
            {
                reward.Exp = Formulas.RndNext(123, 133);
                reward.Score = Formulas.RndNext(62, 72);
                if (Formulas.RndNext(0, 6) == 1) //drop card % looser
                {
                    var clst = Card.GetAllavailableCardsByNickName(nickname);
                    if (clst.Count > 1) reward.NewCard = clst[Formulas.RndNext(0, clst.Count)];
                }

                LooseGamerReward = reward;
            }
        }
    }
}