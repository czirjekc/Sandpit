using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class MenuElementBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public int Location { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        #endregion

        #region Public Methods

        #region Standard

        public bool Select(int id)
        {
            MenuElement item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<MenuElement>(id);

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

        #region Override

        //public override bool Equals(object obj)
        //{
        //    if (obj == null) return false;
        //    if (this.GetType() != obj.GetType()) return false;

        //    // safe because of the GetType check
        //    MenuElementEO item = (MenuElementEO)obj;

        //    if (!Id.Equals(item.Id)) return false;

        //    return true;
        //}

        //public override int GetHashCode()
        //{
        //    return Id;
        //}

        #endregion

        public static List<MenuElementBL> GetAll()
        {
            List<MenuElementBL> itemBL_List = new List<MenuElementBL>();
            List<MenuElement> itemList = new List<MenuElement>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<MenuElement>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.Location).ToList();

                MenuElementBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new MenuElementBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<MenuElementBL> GetAllPageList()
        {
            return FillPathsAndRemoveContainers(GetAll());
        }

        public static List<MenuElementBL> GetByUser(int userId)
        {
            List<MenuElementBL> itemBL_List = new List<MenuElementBL>();
            List<MenuElementBL> containerBL_List = new List<MenuElementBL>();
            List<MenuElementUser> menuElementUserList = new List<MenuElementUser>();
            List<MenuElement> containerList = new List<MenuElement>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                menuElementUserList = repository.Find<MenuElementUser>(x => x.UserId == userId).ToList();

                MenuElementBL itemBL;
                menuElementUserList.ForEach(item =>
                {
                    itemBL = new MenuElementBL();
                    itemBL.MapData(item.MenuElement);
                    itemBL_List.Add(itemBL);
                });

                //get all containers
                containerList = repository.Find<MenuElement>(x => x.Url == null).ToList();

                MenuElementBL containerBL;
                containerList.ForEach(item =>
                {
                    containerBL = new MenuElementBL();
                    containerBL.MapData(item);
                    containerBL_List.Add(containerBL);
                });
            }

            return ((List<MenuElementBL>)itemBL_List.Union(containerBL_List).ToList()).OrderBy(i => i.Location).ToList();
        }

        public static List<MenuElementBL> GetByGroup(int groupId)
        {
            List<MenuElementBL> itemBL_List = new List<MenuElementBL>();
            List<MenuElementBL> containerBL_List = new List<MenuElementBL>();
            List<MenuElementGroup> menuElementGroupList = new List<MenuElementGroup>();
            List<MenuElement> containerList = new List<MenuElement>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                menuElementGroupList = repository.Find<MenuElementGroup>(x => x.GroupId == groupId).ToList();

                MenuElementBL itemBL;
                menuElementGroupList.ForEach(item =>
                {
                    itemBL = new MenuElementBL();
                    itemBL.MapData(item.MenuElement);
                    itemBL_List.Add(itemBL);
                });

                //get all containers
                containerList = repository.Find<MenuElement>(x => x.Url == null).ToList();

                MenuElementBL containerBL;
                containerList.ForEach(item =>
                {
                    containerBL = new MenuElementBL();
                    containerBL.MapData(item);
                    containerBL_List.Add(containerBL);
                });
            }

            return ((List<MenuElementBL>)itemBL_List.Union(containerBL_List).ToList()).OrderBy(i => i.Location).ToList();
        }

        public static List<MenuElementBL> GetFullByUser(int userId)
        {
            List<MenuElementBL> itemBL_List = new List<MenuElementBL>();

            bool isDeveloper = false;
            List<GroupBL> groupBL_List = new List<GroupBL>();

            #region Check if special user (e.g. local developer who needs full access)

            if (userId == -1)
            {
                isDeveloper = true;
            }
            else
            {
                groupBL_List = GroupBL.GetByUser(userId);
                // Check if the user belongs to the 'developer' group    
                isDeveloper = groupBL_List.Find(item => item.Id == 1) != null ? true : false;
            }

            #endregion

            // a developer gets all existing menuElements            
            if (isDeveloper)
            {
                itemBL_List = GetAll();
            }
            else
            {
                // get the menuElements for the user
                itemBL_List = GetByUser(userId);

                // get the menuElements for the groups
                groupBL_List.ForEach(group =>
                {
                    itemBL_List.AddRange(GetByGroup(group.Id));
                });

                // make sure every menu element appears only once
                itemBL_List = (itemBL_List.GroupBy(i => i.Id).Select(j => j.First()).ToList()).OrderBy(i => i.Location).ToList();
            }

            return itemBL_List;
        }

        public static List<MenuElementBL> GetPageListByUser(int userId)
        {
            List<MenuElementBL> itemBL_List = new List<MenuElementBL>();

            itemBL_List = GetByUser(userId);

            return FillPathsAndRemoveContainers(itemBL_List);
        }

        public static List<MenuElementBL> GetPageListByGroup(int groupId)
        {
            List<MenuElementBL> itemBL_List = new List<MenuElementBL>();

            // a developer gets all existing menuElements
            if (groupId == 1) //developer group
            {
                itemBL_List = GetAll();
            }
            else
            {
                itemBL_List = GetByGroup(groupId);
            }

            return FillPathsAndRemoveContainers(itemBL_List);
        }

        public static List<MenuElementBL> GetNotInheritedPageListByUser(int userId)
        {
            // get the pages for the user
            List<MenuElementBL> pageList = MenuElementBL.GetPageListByUser(userId);

            // get the inherited pages
            List<MenuElementBL> inheritedList = MenuElementBL.GetInheritedPageListByUser(userId);

            // Remove the inherited pages from the list                                    
            inheritedList.ForEach(item =>
            {
                pageList.RemoveAll(i => i.Id == item.Id);
            });

            return pageList;
        }

        public static List<MenuElementBL> GetInheritedPageListByUser(int userId)
        {
            // get the groups the user belongs to
            List<GroupBL> groupBL_List = GroupBL.GetByUser(userId);

            // get the pages for the groups
            List<MenuElementBL> groupItemBL_List = new List<MenuElementBL>();
            groupBL_List.ForEach(groupBL =>
            {
                groupItemBL_List.AddRange(MenuElementBL.GetPageListByGroup(groupBL.Id));
            });

            // make sure every page appears only once
            return groupItemBL_List.GroupBy(i => i.Id).Select(j => j.First()).ToList();
        }

        public static List<MenuElementBL> GetNotAccessiblePageListByGroup(int groupId)
        {
            List<MenuElementBL> itemEO_List = MenuElementBL.GetAllPageList();

            List<MenuElementBL> groupItemEO_List = MenuElementBL.GetPageListByGroup(groupId);

            // Remove group menu elements from the list
            groupItemEO_List.ForEach(i =>
            {
                itemEO_List.RemoveAll(j => j.Id == i.Id);
            });

            return itemEO_List;
        }

        public static List<MenuElementBL> GetNotAccessiblePageListByUser(int userId)
        {
            // get all pages
            List<MenuElementBL> itemEO_List = MenuElementBL.GetAllPageList();

            // get the not inherited accessible pages by userId
            List<MenuElementBL> userItemEO_List = MenuElementBL.GetNotInheritedPageListByUser(userId);

            // get the inherited accessible pages by userId
            userItemEO_List.AddRange(MenuElementBL.GetInheritedPageListByUser(userId));

            // Remove user menu elements from the list
            userItemEO_List.ForEach(i =>
            {
                itemEO_List.RemoveAll(j => j.Id == i.Id);
            });

            return itemEO_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(MenuElement item)
        {
            Id = item.Id;
            Name = item.Name;
            Location = item.Location;
            Url = item.Url;
        }

        internal MenuElement MapData()
        {
            MenuElement item = new MenuElement();
            item.Id = Id;
            item.Name = Name;
            item.Location = Location;
            item.Url = Url;

            return item;
        }

        #endregion

        #region Private Methods

        public static List<MenuElementBL> FillPathsAndRemoveContainers(List<MenuElementBL> itemList)
        {
            // Fill menu element paths
            string path = "► ";
            string[] parts;
            itemList.ForEach(item =>
            {
                parts = path.Split('►');
                if (item.Url == null && item.Location % 1000000 == 0) //level 1
                    path = "► " + item.Name + " ► ";
                else if (item.Url == null && item.Location % 10000 == 0) //level 2
                    path = "►" + parts[1] + "► " + item.Name + " ► ";
                else if (item.Url == null && item.Location % 100 == 0) //level 3
                    path = "►" + parts[1] + "►" + parts[2] + "► " + item.Name + " ► ";
                else
                    item.Path = path + item.Name;
            });

            return itemList.FindAll(item => item.Url != null); //Remove menu containers           
        }

        #endregion
    }
}
