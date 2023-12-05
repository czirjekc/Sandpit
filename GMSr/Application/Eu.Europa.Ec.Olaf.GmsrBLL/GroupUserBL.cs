using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class GroupUserBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        #endregion

        #region Public Methods

        #region Standard

        public bool Save(ref ArrayList validationErrorList, string userLogin)
        {
            ValidateSave(ref validationErrorList);

            if (validationErrorList.Count == 0)
            {
                using (IRepository repository = new EntityFrameworkRepository())
                {
                    MapData(repository.Save<GroupUser>(MapData(), userLogin));
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete(ref ArrayList validationErrorList, string userLogin)
        {
            ValidateDelete(ref validationErrorList);

            if (validationErrorList.Count == 0)
            {
                using (IRepository repository = new EntityFrameworkRepository())
                {
                    repository.Delete<GroupUser>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {
            GroupUser item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<GroupUser>(id);

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

        public static List<GroupUserBL> GetByGroup(int groupId)
        {
            List<GroupUserBL> itemBL_List = new List<GroupUserBL>();
            List<GroupUser> itemList = new List<GroupUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<GroupUser>(x => x.GroupId == groupId).ToList();                

                GroupUserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new GroupUserBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<GroupUserBL> GetByUser(int userId)
        {
            List<GroupUserBL> itemBL_List = new List<GroupUserBL>();
            List<GroupUser> itemList = new List<GroupUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<GroupUser>(x => x.UserId == userId).ToList();

                GroupUserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new GroupUserBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(GroupUser item)
        {
            Id = item.Id;
            UserId = item.UserId;
            GroupId = item.GroupId;
        }

        internal GroupUser MapData()
        {
            GroupUser item = new GroupUser();
            item.Id = Id;
            item.UserId = UserId;
            item.GroupId = GroupId;
            item.EntityKey = new EntityKey("GmsrEntities.GroupUserSet", "Id", Id);

            return item;
        }

        #endregion

        #region Private Methods

        private void ValidateSave(ref ArrayList validationErrorList)
        {
            //todo: validation
            //if (FirstName.Trim() == "")
            //{
            //    validationErrorList.Add("The First Name is required.");
            //}

            //if (LastName.Trim() == "")
            //{
            //    validationErrorList.Add("The Last Name is required.");
            //}
        }

        private void ValidateDelete(ref ArrayList validationErrorList)
        {
            //todo: Check for referential integrity.
        }

        #endregion
    }
}