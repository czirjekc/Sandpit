using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class SessionBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public string SessionID { get; set; }
        public string UserLogin { get; set; }
        public string UserFullName { get; set; }
        public DateTime DateStart { get; set; }

        #endregion

        #region Constructors

        public SessionBL()
        {
        }

        public SessionBL(string sessionID, string userLogin, string userFullName, DateTime dateStart)
        {
            SessionID = sessionID;
            UserLogin = userLogin;
            UserFullName = userFullName;
            DateStart = dateStart;
        }

        #endregion

        #region Public Methods

        #region Standard

        public void Save()
        {
            using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
            {
                MapData(repository.Save<Session>(MapData(), null));
            }
        }

        public bool Select(int id)
        {
            Session item = null;
            using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
            {
                item = repository.Get<Session>(id);

                if (item != null)
                {
                    MapData(item);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        public static List<SessionBL> GetByParameters(DateTime? dateStartFrom,
                                                      DateTime? dateStartTo,
                                                      string userName
                                                      )
        {
            List<SessionBL> itemBL_List = new List<SessionBL>();
            List<Session> itemList = new List<Session>();

            using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
            {
                itemList = repository.Find<Session>(x =>
                                                       (dateStartFrom == null || x.DateStart >= dateStartFrom) &&
                                                       (dateStartTo == null || x.DateStart <= dateStartTo) &&
                                                       (userName == "" || x.UserFullName.Contains(userName))
                                                       ).ToList();

                itemList = itemList.OrderByDescending(x => x.DateStart).ToList();

                SessionBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SessionBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(Session item)
        {
            Id = item.Id;
            SessionID = item.SessionID;
            UserLogin = item.UserLogin;
            UserFullName = item.UserFullName;
            DateStart = item.DateStart;
        }

        internal Session MapData()
        {
            Session item = new Session();
            item.Id = Id;
            item.SessionID = SessionID;
            item.UserLogin = UserLogin;
            item.UserFullName = UserFullName;
            item.DateStart = DateStart;

            item.EntityKey = new EntityKey("GmsrEntities.SessionSet", "Id", Id);

            return item;
        }

        #endregion
    }
}