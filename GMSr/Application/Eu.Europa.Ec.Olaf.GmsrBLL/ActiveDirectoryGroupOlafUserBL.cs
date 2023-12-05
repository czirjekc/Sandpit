using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class ActiveDirectoryGroupOlafUserBL : BaseBL
    {
        #region Properties

        public string Group { get; set; }
        public string User { get; set; }

        #endregion

        #region Public Methods

        #region Standard

        public bool Select(int id)
        {
            ActiveDirectoryGroupOlafUser item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<ActiveDirectoryGroupOlafUser>(id);
            }

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

        #endregion

        public static List<ActiveDirectoryGroupOlafUserBL> GetAll()
        {
            List<ActiveDirectoryGroupOlafUserBL> itemBL_List = new List<ActiveDirectoryGroupOlafUserBL>();
            List<ActiveDirectoryGroupOlafUser> itemList = new List<ActiveDirectoryGroupOlafUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<ActiveDirectoryGroupOlafUser>(x => 1 == 1).ToList();
            }

            itemList = itemList.OrderBy(x => x.Group).ToList();

            ActiveDirectoryGroupOlafUserBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new ActiveDirectoryGroupOlafUserBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        public static List<ActiveDirectoryGroupOlafUserBL> GetByOlafUser(string userLogin)
        {
            List<ActiveDirectoryGroupOlafUserBL> itemBL_List = new List<ActiveDirectoryGroupOlafUserBL>();
            List<ActiveDirectoryGroupOlafUser> itemList = new List<ActiveDirectoryGroupOlafUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<ActiveDirectoryGroupOlafUser>(x => x.User == userLogin).ToList();

                itemList = itemList.OrderBy(x => x.Group).ToList();

                ActiveDirectoryGroupOlafUserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new ActiveDirectoryGroupOlafUserBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<ActiveDirectoryGroupOlafUserBL> GetByActiveDirectoryGroupNameList(List<string> groupNameList)
        {
            List<ActiveDirectoryGroupOlafUserBL> itemBL_List = new List<ActiveDirectoryGroupOlafUserBL>();
            List<ActiveDirectoryGroupOlafUser> itemList = new List<ActiveDirectoryGroupOlafUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<ActiveDirectoryGroupOlafUser>(x => groupNameList.Contains(x.Group.ToLower())).ToList();

                itemList = itemList.OrderBy(x => x.Group).ToList();

                ActiveDirectoryGroupOlafUserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new ActiveDirectoryGroupOlafUserBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(ActiveDirectoryGroupOlafUser item)
        {
            Group = item.Group;
            User = item.User;
        }

        internal ActiveDirectoryGroupOlafUser MapData()
        {
            ActiveDirectoryGroupOlafUser item = new ActiveDirectoryGroupOlafUser();

            item.Group = Group;
            item.User = User;

            return item;
        }

        #endregion
    }
}