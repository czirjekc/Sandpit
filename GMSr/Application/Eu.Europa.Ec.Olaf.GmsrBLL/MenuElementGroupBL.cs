using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class MenuElementGroupBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public int MenuElementId { get; set; }
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
                    MapData(repository.Save<MenuElementGroup>(MapData(), userLogin));
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
                    repository.Delete<MenuElementGroup>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {
            MenuElementGroup item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<MenuElementGroup>(id);

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

        public static List<MenuElementGroupBL> GetByGroup(int groupId)
        {
            List<MenuElementGroupBL> itemBL_List = new List<MenuElementGroupBL>();
            List<MenuElementGroup> itemList = new List<MenuElementGroup>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<MenuElementGroup>(x => x.GroupId == groupId).ToList();                

                MenuElementGroupBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new MenuElementGroupBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(MenuElementGroup item)
        {
            Id = item.Id;
            MenuElementId = item.MenuElementId;
            GroupId = item.GroupId;
        }

        internal MenuElementGroup MapData()
        {
            MenuElementGroup item = new MenuElementGroup();
            item.Id = Id;
            item.MenuElementId = MenuElementId;
            item.GroupId = GroupId;
            item.EntityKey = new EntityKey("GmsrEntities.MenuElementGroupSet", "Id", Id);

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