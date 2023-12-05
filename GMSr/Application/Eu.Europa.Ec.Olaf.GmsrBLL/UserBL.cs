using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class UserBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Login { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public string FullName
        {
            get
            {
                string fullName = null;

                if (Surname != null)
                    fullName += Surname.ToUpper();
                if (FirstName != null)
                    fullName += " " + FirstName;

                return fullName;
            }
        }

        #endregion

        #region Constructors

        public UserBL()
        {
            IsActive = true;
        }

        public UserBL(int id, string login, string firstName, string surName, string email, bool isActive)
        {
            Id = id;
            Login = login;
            FirstName = firstName;
            Surname = surName;
            Email = email;
            IsActive = isActive;
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
                    MapData(repository.Save<User>(MapData(), userLogin));
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
                    repository.Delete<User>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {
            User item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<User>(id);

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

        public bool SelectByUser(string login)
        {
            User item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<User>(x => x.Login == login);

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

        public static List<UserBL> GetAll()
        {
            List<UserBL> itemBL_List = new List<UserBL>();
            List<User> itemList = new List<User>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<User>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.Surname).ToList();

                UserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new UserBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<UserBL> GetByKeyword(string keyword)
        {
            List<UserBL> itemBL_List = new List<UserBL>();
            List<User> itemList = new List<User>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                int number = 0;
                if (int.TryParse(keyword, out number))
                {
                    itemList = repository.Find<User>(x =>
                                                     x.Id == number ||

                                                     x.Surname.Contains(keyword) ||
                                                     x.FirstName.Contains(keyword) ||
                                                     x.Login.Contains(keyword) ||
                                                     x.Email.Contains(keyword)
                                                     ).ToList();
                }
                else if (keyword == "")
                {
                    itemList = repository.Find<User>(x => 1 == 1).ToList();
                }
                else
                {
                    itemList = repository.Find<User>(x =>
                                                     x.Surname.Contains(keyword) ||
                                                     x.FirstName.Contains(keyword) ||
                                                     x.Login.Contains(keyword) ||
                                                     x.Email.Contains(keyword)
                                                     ).ToList();
                }

                itemList = itemList.OrderBy(x => x.Surname).ThenBy(x => x.FirstName).ToList();

                UserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new UserBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<UserBL> GetByGroup(int groupId)
        {
            List<UserBL> itemBL_List = new List<UserBL>();
            List<GroupUser> itemList = new List<GroupUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<GroupUser>(x => x.GroupId == groupId).ToList();

                UserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new UserBL();
                    itemBL.MapData(item.User);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List.OrderBy(x => x.Surname).ToList();
        }

        public static void UpdateUser(UserBL item)
        {
            ArrayList validationErrors = new ArrayList();
            item.Save(ref validationErrors, "test");
        }

        #endregion

        #region Internal Methods

        internal void MapData(User item)
        {
            Id = item.Id;
            Login = item.Login;
            FirstName = item.FirstName;
            Surname = item.Surname;
            Email = item.Email;
            IsActive = item.IsActive;
        }

        internal User MapData()
        {
            User item = new User();
            item.Id = Id;
            item.Login = Login;
            item.FirstName = FirstName;
            item.Surname = Surname;
            item.Email = Email;
            item.IsActive = IsActive;

            item.EntityKey = new EntityKey("GmsrEntities.UserSet", "Id", Id);

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