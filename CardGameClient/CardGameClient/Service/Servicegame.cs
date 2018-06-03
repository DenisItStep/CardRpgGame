using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CardGameServer
{
    public class ServiceProxy
    {
        public static Servicegame Proxy { get; set; }
    }


    [ServiceContract]
    public interface Servicegame
    {
        /// <summary>
        /// Вход в игру
        /// </summary>
        /// <param name="user">Игрок</param>
        /// <param name="pass">Пароль</param>
        /// <returns></returns>
        [OperationContract]
        int Login(string user, string pass);

        /// <summary>
        /// Если у игрока нету пресонажей
        /// </summary>
        /// <param name="user">Игрок</param>
        /// <returns></returns>
        [OperationContract]
        bool isAccountContainsAnyCharacter(string user);

        /// <summary>
        /// Создание игрока
        /// </summary>
        /// <param name="user">Игрок</param>
        /// <param name="name">Ник</param>
        /// <param name="heroCardId">ID карты игрока</param>
        /// <returns></returns>
        [OperationContract]
        bool createCharacter(string user, string name, int heroCardId);

        /// <summary>
        /// Список стандартных карт игроков
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Card> getHeroesTemplateAvailableList();

        /// <summary>
        /// Вход в игру
        /// </summary>
        /// <param name="user">Игрок</param>
        /// <returns></returns>
        [OperationContract]
        CharInfo EnterWorld(string user);

        /// <summary>
        /// Игрк онлайн
        /// </summary>
        /// <param name="user">Игрок</param>
        [OperationContract]
        void iAmOnline(string user);

        /// <summary>
        /// Получение ранга
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<CharInfo> getRanking();

        /// <summary>
        /// Поиск противника
        /// </summary>
        /// <param name="nickname">Персонаж</param>
        /// <returns></returns>
        [OperationContract]
        Game findGame(string nickname);

        /// <summary>
        /// Создание боя
        /// </summary>
        /// <param name="nickname">Персонаж</param>
        /// <returns></returns>
        [OperationContract]
        Game getGame(string nickname);

        /// <summary>
        /// Конец поиска
        /// </summary>
        /// <param name="nickname">Персонаж</param>
        /// <returns></returns>
        [OperationContract]
        bool cancelSearch(string nickname);

        /// <summary>
        /// Был атакован
        /// </summary>
        /// <param name="nickname">Ник</param>
        /// <param name="myslot">Мои слоты</param>
        /// <param name="enslot">Слоты противника</param>
        /// <returns></returns>
        [OperationContract]
        LastHitInfo DoAttack(string nickname, int myslot, int enslot);

        /// <summary>
        /// Покинуть бой
        /// </summary>
        /// <param name="nickname">Персонаж</param>
        [OperationContract]
        void leaveGame(string nickname);

        /// <summary>
        /// Выход из игры
        /// </summary>
        /// <param name="user">Игрок</param>
        [OperationContract]
        void Logout(string user);

        /// <summary>
        /// Показать все карты
        /// </summary>
        /// <param name="user">Игрок</param>
        /// <param name="page">Страницы</param>
        /// <returns></returns>
        [OperationContract]
        List<Card> GetAllCard(string user, int page = 1);

        /// <summary>
        /// Изменение слотоы карт
        /// </summary>
        /// <param name="user">Карта</param>
        /// <param name="oslot">Старый слот</param>
        /// <param name="nSlot">Новый слот</param>
        /// <returns></returns>
        [OperationContract]
        bool ChangeCardslot(string user, int oslot, int nSlot);

        /// <summary>
        /// Получение слотов под новые карты
        /// </summary>
        /// <param name="user">Игрок</param>
        /// <returns></returns>
        [OperationContract]
        int GetFreeSlotNumberAllCards(string user);

        /// <summary>
        /// Покупка карт
        /// </summary>
        /// <param name="user">Игрок</param>
        /// <param name="number">Колличество</param>
        /// <returns></returns>
        [OperationContract]
        List<Card> BuyCard(string user, int number);

        /// <summary>
        /// Продажа карт
        /// </summary>
        /// <param name="user">Игрок</param>
        /// <param name="slot">Слот</param>
        /// <returns></returns>
        [OperationContract]
        bool SellCard(string user, int slot);

    }
}
