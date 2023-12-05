using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class GroupBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        #endregion
        
        #region Constructors
        
        public GroupBL()
        {
            IsActive = true;
        }
        
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
                    MapData(repository.Save<Group>(MapData(), userLogin));
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
                    repository.Delete<Group>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {

            Group item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<Group>(id);
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

        public static List<GroupBL> GetAll()
        {
            List<GroupBL> itemBL_List = new List<GroupBL>();
            List<Group> itemList = new List<Group>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<Group>(x => 1 == 1 /*x => x.IsActive == true*/).ToList();
            }

            itemList = itemList.OrderBy(x => x.Name).ToList();

            GroupBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new GroupBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        public static List<GroupBL> GetByKeyword(string keyword)
        {
            List<GroupBL> itemBL_List = new List<GroupBL>();
            List<Group> itemList = new List<Group>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                int number = 0;
                if (int.TryParse(keyword, out number))
                {
                    itemList = repository.Find<Group>(x =>
                                                      x.Id == number ||

                                                      x.Name.Contains(keyword)                                                     
                                                      ).ToList();
                }
                else if (keyword == "")
                {
                    itemList = repository.Find<Group>(x => 1 == 1).ToList();
                }
                else
                {
                    itemList = repository.Find<Group>(x =>
                                                      x.Name.Contains(keyword)                                                                                                          
                                                      ).ToList();
                }

                itemList = itemList.OrderBy(x => x.Name).ToList();

                GroupBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new GroupBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<GroupBL> GetByUser(int userId)
        {
            List<GroupBL> itemBL_List = new List<GroupBL>();
            List<GroupUser> itemList = new List<GroupUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<GroupUser>(x => x.UserId == userId).ToList();

                GroupBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new GroupBL();
                    itemBL.MapData(item.Group);
                    itemBL_List.Add(itemBL);
                });
            }

            itemBL_List = itemBL_List.OrderBy(x => x.Name).ToList();

            return itemBL_List;
        }

        public static List<GroupBL> GetNotMemberOfByUser(int userId)
        {
            List<GroupBL> itemBL_List = GroupBL.GetAll();

            List<GroupBL> userItemBL_List = GroupBL.GetByUser(userId);

            // Remove the groups of the user
            userItemBL_List.ForEach(i =>
            {
                itemBL_List.RemoveAll(j => j.Id == i.Id);
            });

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(Group item)
        {
            Id = item.Id;
            Name = item.Name;
            IsActive = item.IsActive;
        }

        internal Group MapData()
        {
            Group item = new Group();
            item.Id = Id;
            item.Name = Name;
            item.IsActive = IsActive;

            item.EntityKey = new EntityKey("GmsrEntities.GroupSet", "Id", Id);

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