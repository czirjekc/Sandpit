using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class MenuElementUserBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public int MenuElementId { get; set; }
        public int UserId { get; set; }

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
                    MapData(repository.Save<MenuElementUser>(MapData(), userLogin));
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
                    repository.Delete<MenuElementUser>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {
            MenuElementUser item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<MenuElementUser>(id);

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
               
        public static List<MenuElementUserBL> GetByUser(int userId)
        {
            List<MenuElementUserBL> itemBL_List = new List<MenuElementUserBL>();
            List<MenuElementUser> itemList = new List<MenuElementUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<MenuElementUser>(x => x.UserId == userId).ToList();                

                MenuElementUserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new MenuElementUserBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(MenuElementUser item)
        {
            Id = item.Id;
            MenuElementId = item.MenuElementId;
            UserId = item.UserId;
        }

        internal MenuElementUser MapData()
        {
            MenuElementUser item = new MenuElementUser();
            item.Id = Id;
            item.MenuElementId = MenuElementId;
            item.UserId = UserId;
            item.EntityKey = new EntityKey("GmsrEntities.MenuElementUserSet", "Id", Id);

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