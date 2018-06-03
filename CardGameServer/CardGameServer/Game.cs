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
        [DataMember]
        public LastHitInfo lastHitInfo { get; set; }

        [DataMember]
        public Gamer fGamer = null;

        [DataMember]
        public Gamer tGamer = null;

        //debug
        public bool Init1 = true;
        public bool Init2 = true;


        public int Level { get; set; }

        [DataMember]
        public List<string> Gamers = new List<string>();

        [DataMember]
        public List<Card> firstGamerCards;

        [DataMember]
        public List<Card> twoGamerCards;

        [DataMember]
        public int gameState;

        [DataMember]
        public string currUsr;

        [DataMember]
        public Reward WinGamerReward { get; set; }

        [DataMember]
        public Reward LooseGamerReward { get; set; }

        public Game(Game oldGame)
        {
            this.Gamers = new List<string>(oldGame.Gamers);
            this.firstGamerCards = new List<Card>(oldGame.firstGamerCards);
            this.twoGamerCards = new List<Card>(oldGame.twoGamerCards);
            this.gameState = oldGame.gameState;
            this.currUsr = oldGame.currUsr;
            this.WinGamerReward = oldGame.WinGamerReward;
            this.LooseGamerReward = oldGame.LooseGamerReward;
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
            Gamer gamer = Program.OnlineUsers.Find(u => u.nick == user);
            Program.UserThreadLock.ExitReadLock();


            int dmg1 = firstGamerCards.Sum(ccc => ccc.dmg);
            int dmg2 = tgc.Sum(ccc => ccc.dmg);


            int def1 = firstGamerCards.Sum(ccc => ccc.def);
            int def2 = tgc.Sum(ccc => ccc.def);


            int hp1 = firstGamerCards.Sum(ccc => ccc.hp);
            int hp2 = tgc.Sum(ccc => ccc.hp);



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
            Reward reward = new Reward();

            if (isWinner)
            {
                reward.Exp = Formulas.RndNext(245, 255);
                reward.Score = Formulas.RndNext(123, 133);
                if (Formulas.RndNext(0, 3) == 1) //drop card % winner
                {
                    List<Card> clst = Card.GetAllavailableCardsByNickName(nickname);
                    if (clst.Count > 1)
                    {
                        reward.NewCard = clst[Formulas.RndNext(0, clst.Count)];
                    }
                }
                WinGamerReward = reward;
            }
            else
            {
                reward.Exp = Formulas.RndNext(123, 133);
                reward.Score = Formulas.RndNext(62, 72);
                if (Formulas.RndNext(0, 6) == 1) //drop card % looser
                {
                    List<Card> clst = Card.GetAllavailableCardsByNickName(nickname);
                    if (clst.Count > 1)
                    {
                        reward.NewCard = clst[Formulas.RndNext(0, clst.Count)];
                    }
               }
                LooseGamerReward = reward;
            }
        }

    }
}
