using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class OlafUserBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Unit { get; set; }
        public string Building { get; set; }
        public string Office { get; set; }
        public string Status { get; set; }
        public string Fl { get; set; }
        public string Act { get; set; }
        public bool? Local { get; set; }
        public decimal? PeoId { get; set; }
        public string Phone { get; set; }

        public string Concatenation
        {
            get
            {
                return LastName + " " + FirstName + " | (" + Id + ")";
            }
        }

        #endregion

        #region Public Methods

        #region Standard

        public bool Select(int id)
        {

            OlafUser item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<OlafUser>(id);
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

        public static List<OlafUserBL> GetAll()
        {
            List<OlafUserBL> itemBL_List = new List<OlafUserBL>();
            List<OlafUser> itemList = new List<OlafUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OlafUser>(x => 1 == 1).ToList();
            }

            itemList = itemList.OrderBy(x => x.LastName).ToList();

            OlafUserBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new OlafUserBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        public static List<OlafUserBL> GetByInfix(string infix)
        {
            List<OlafUserBL> itemBL_List = new List<OlafUserBL>();
            List<OlafUser> itemList = new List<OlafUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OlafUser>(x => (x.LastName + " " + x.FirstName).Contains(infix)).ToList();

                itemList = itemList.OrderBy(x => x.LastName).Take(50).ToList();

                OlafUserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OlafUserBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OlafUserBL> GetByKeyword(string keyword)
        {
            List<OlafUserBL> itemBL_List = new List<OlafUserBL>();
            List<OlafUser> itemList = new List<OlafUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                if (keyword == "")
                {
                    itemList = repository.Find<OlafUser>(x => 1 == 1).ToList();
                }
                else
                {
                    itemList = repository.Find<OlafUser>(x => x.LastName.Contains(keyword) ||
                                                              x.FirstName.Contains(keyword) ||
                                                              x.Title.Contains(keyword) ||
                                                              x.Login.Contains(keyword) ||
                                                              x.Email.Contains(keyword) ||
                                                              x.Building.Contains(keyword) ||
                                                              x.Office.Contains(keyword) ||
                                                              x.Status.Contains(keyword) ||
                                                              x.Fl.Contains(keyword) ||
                                                              x.Act.Contains(keyword) ||
                                                              x.Phone.Contains(keyword)).ToList();
                }

                itemList = itemList.OrderBy(x => x.LastName).ToList();

                OlafUserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OlafUserBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OlafUserBL> GetByParameters(string lastName,
                                                       string firstName,
                                                       string title,
                                                       string login,
                                                       string email,
                                                       string unit,
                                                       string building,
                                                       string office,
                                                       string status,
                                                       string fl,
                                                       string act,
                                                       bool? local,
                                                       string phone
                                                       )
        {
            List<OlafUserBL> itemBL_List = new List<OlafUserBL>();
            List<OlafUser> itemList = new List<OlafUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OlafUser>(x => (lastName == "" || x.LastName.Contains(lastName)) &&
                                                          (firstName == "" || x.FirstName.Contains(firstName)) &&
                                                          (title == "" || x.Title.Contains(title)) &&
                                                          (login == "" || x.Login.Contains(login)) &&
                                                          (email == "" || x.Email.Contains(email)) &&
                                                          (unit == "" || x.Unit.Contains(unit)) &&
                                                          (building == "" || x.Building.Contains(building)) &&
                                                          (office == "" || x.Office.Contains(office)) &&
                                                          (status == "" || x.Status.Contains(status)) &&
                                                          (fl == "" || x.Fl.Contains(fl)) &&
                                                          (act == "" || x.Act.Contains(act)) &&
                                                          (local == null || x.Local == local) &&
                                                          (phone == "" || x.Phone.Contains(phone))
                                                          ).ToList();

                itemList = itemList.OrderBy(x => x.LastName).ToList();

                OlafUserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OlafUserBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OlafUserBL> GetByFullNamePrefix(string prefix, int count)
        {
            List<OlafUserBL> itemBL_List = new List<OlafUserBL>();
            //List<OlafUser> itemList = new List<OlafUser>();

            //using (IRepository repository = new EntityFrameworkRepository())
            //{
            //    if (keyword == "*")
            //    {
            //        itemList = repository.Find<OlafUser>(x => 1 == 1).ToList();
            //    }
            //    else
            //    {
            //        itemList = repository.Find<OlafUser>(x => x.LastName == keyword ||
            //                                                  x.FirstName == keyword ||
            //                                                  x.Title == keyword ||
            //                                                  x.Login == keyword ||
            //                                                  x.Email == keyword ||
            //                                                  x.Building == keyword ||
            //                                                  x.Office == keyword ||
            //                                                  x.Status == keyword ||
            //                                                  x.Fl == keyword ||
            //                                                  x.Act == keyword).ToList();
            //    }

            //    itemList = itemList.OrderBy(x => x.LastName).ToList();

            //    OlafUserBL itemBL;
            //    itemList.ForEach(item =>
            //    {
            //        itemBL = new OlafUserBL();
            //        itemBL.MapData(item);
            //        itemBL_List.Add(itemBL);
            //    });
            //}

            return itemBL_List;
        }    

        #endregion

        #region Internal Methods

        internal void MapData(OlafUser item)
        {
            Id = item.Id;
            LastName = item.LastName;
            FirstName = item.FirstName;
            Title = item.Title;
            Login = item.Login;
            Email = item.Email;
            Unit = item.Unit;
            Building = item.Building;
            Office = item.Office;
            Status = item.Status;
            Fl = item.Fl;
            Act = item.Act;
            Local = item.Local;
            PeoId = item.PeoId;
            Phone = item.Phone;
        }

        internal OlafUser MapData()
        {
            OlafUser item = new OlafUser();
            item.Id = Id;
            item.LastName = LastName;
            item.FirstName = FirstName;
            item.Title = Title;
            item.Login = Login;
            item.Email = Email;
            item.Unit = Unit;
            item.Building = Building;
            item.Office = Office;
            item.Status = Status;
            item.Fl = Fl;
            item.Act = Act;
            item.Local = Local;
            item.PeoId = PeoId;
            item.Phone = Phone;

            item.EntityKey = new EntityKey("GmsrEntities.OlafUserSet", "Id", Id);

            return item;
        }

        #endregion
    }
}