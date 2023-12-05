using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class HardwareItemBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public string Status { get; set; }
        public bool? Local { get; set; }
        public string OlafName { get; set; }
        public string InventoryNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Building { get; set; }
        public string Office { get; set; }
        public string Model { get; set; }
        public string LongName { get; set; }
        public string Description { get; set; }
        public string Serial { get; set; }
        public decimal? RmtMatId { get; set; }
        public string Unit { get; set; }
        public string RmtCtgNm { get; set; }
        public string RmtParentCtgNm { get; set; }
        public DateTime? MaintenanceEndDate { get; set; }

        public string Concatenation
        {
            get
            {
                return OlafName + " | " + InventoryNo + " | " + Model + " | Status: " + Status + " | Local: " + Local + " | (" + Id + ")";
            }
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
                    MapData(repository.Save<HardwareItem>(MapData(), userLogin));

                    //if the OlafName is in the UnknownHardwareItem list -> delete the corresponding item from the UnknownHardwareItem list
                    UnknownHardwareItem unknownHardwareItem = repository.Get<UnknownHardwareItem>(x => x.MachineName == OlafName);
                    if(unknownHardwareItem != null)
                    {
                        repository.Delete<UnknownHardwareItem>(unknownHardwareItem, userLogin);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Select(int id)
        {

            HardwareItem item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<HardwareItem>(id);
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

        public static List<HardwareItemBL> GetAll()
        {
            List<HardwareItemBL> itemBL_List = new List<HardwareItemBL>();
            List<HardwareItem> itemList = new List<HardwareItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<HardwareItem>(x => 1 == 1).ToList();
            }

            itemList = itemList.OrderBy(x => x.Model).ToList();

            HardwareItemBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new HardwareItemBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        public static List<HardwareItemBL> GetByInfix(string infix)
        {
            List<HardwareItemBL> itemBL_List = new List<HardwareItemBL>();
            List<HardwareItem> itemList = new List<HardwareItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<HardwareItem>(x => x.OlafName.Contains(infix) ||
                                                              x.InventoryNo.Contains(infix)
                                                              ).ToList();

                itemList = itemList.OrderBy(x => x.OlafName).ThenBy(x => x.Model).ThenBy(x => x.Status).ThenBy(x => x.Local).ThenBy(x => x.Id).Take(50).ToList();

                HardwareItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new HardwareItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<HardwareItemBL> GetNotLinkedWithOrderByPrefix(string prefix)
        {
            List<HardwareItemBL> itemBL_List = GetNotLinkedWithOrder();

            string prefixUpperCase = prefix.ToUpper();

            itemBL_List = (itemBL_List.FindAll(x => (x.OlafName == null) ? false : x.OlafName.Contains(prefix) || x.OlafName.Contains(prefixUpperCase)));

            return itemBL_List.OrderBy(x => x.OlafName).Take(50).ToList();
        }

        public static List<HardwareItemBL> GetByKeyword(string keyword)
        {
            List<HardwareItemBL> itemBL_List = new List<HardwareItemBL>();
            List<HardwareItem> itemList = new List<HardwareItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                int number = 0;
                if (int.TryParse(keyword, out number))
                {
                    itemList = repository.Find<HardwareItem>(x =>
                                                              x.Id == number ||

                                                              x.Model == keyword ||
                                                              x.LongName == keyword
                                                              ).ToList();
                }
                else
                {
                    itemList = repository.Find<HardwareItem>(x =>
                                                              x.Model == keyword ||
                                                              x.LongName.Contains(keyword)
                                                              ).ToList();
                }

                itemList = itemList.OrderBy(x => x.Model).ToList();

                HardwareItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new HardwareItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<HardwareItemBL> GetByParameters(ref ArrayList validationErrorList,
                                                           string status,
                                                           bool? local,
                                                           string olafName,
                                                           string inventoryNo,
                                                           string lastName,
                                                           string firstName,
                                                           string phone,
                                                           string building,
                                                           string office,
                                                           string model,
                                                           string description,
                                                           string serial,
                                                           string unit,
                                                           string rmtCtgNm,
                                                           string rmtParentCtgNm,
                                                           DateTime? maintenanceEndDateFrom,
                                                           DateTime? maintenanceEndDateTo
                                                           )
        {
            List<HardwareItemBL> itemBL_List = new List<HardwareItemBL>();
            List<HardwareItem> itemList = new List<HardwareItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<HardwareItem>(x =>
                                                              (status == "" || x.Status.Contains(status)) &&
                                                              (local == null || x.Local == local) &&
                                                              (olafName == "" || x.OlafName.Contains(olafName)) &&
                                                              (inventoryNo == "" || x.InventoryNo.Contains(inventoryNo)) &&
                                                              (lastName == "" || x.LastName.Contains(lastName)) &&
                                                              (firstName == "" || x.FirstName.Contains(firstName)) &&
                                                              (phone == "" || x.Phone.Contains(phone)) &&
                                                              (building == "" || x.Building.Contains(building)) &&
                                                              (office == "" || x.Office.Contains(office)) &&
                                                              (model == "" || x.Model.Contains(model)) &&
                                                              (description == "" || x.Description.Contains(description)) &&
                                                              (serial == "" || x.Serial.Contains(serial)) &&
                                                              (unit == "" || x.Unit.Contains(unit)) &&
                                                              (rmtCtgNm == "" || x.RmtCtgNm.Contains(rmtCtgNm)) &&
                                                              (rmtParentCtgNm == "" || x.RmtParentCtgNm.Contains(rmtParentCtgNm)) &&
                                                              (maintenanceEndDateFrom == null || x.MaintenanceEndDate == null || x.MaintenanceEndDate >= maintenanceEndDateFrom) &&
                                                              (maintenanceEndDateTo == null || x.MaintenanceEndDate <= maintenanceEndDateTo)
                                                              ).Take(1000).ToList(); // todo: temporary limit the results amount to 1000

                // todo: temporary check whether 'too much' results
                if (itemList.Count == 1000)
                {
                    validationErrorList.Add("There are too many results (>1000). Use stricter search conditions. Only a subset of 1000 items is currently displayed.");
                }

                itemList = itemList.OrderBy(x => x.Id).ToList();

                HardwareItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new HardwareItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<HardwareItemBL> GetByOrderForm(int orderFormId)
        {
            List<HardwareItemBL> itemBL_List = new List<HardwareItemBL>();
            List<HardwareItemOrderForm> itemList = new List<HardwareItemOrderForm>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<HardwareItemOrderForm>(x => x.OrderFormId == orderFormId).ToList();

                itemList = itemList.OrderBy(x => x.HardwareItemId).ToList();

                HardwareItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new HardwareItemBL();
                    itemBL.MapData(item.HardwareItem);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List.OrderBy(x => x.Model).ThenBy(x => x.Status).ThenBy(x => x.Local).ThenBy(x => x.Id).ToList();
        }

        public static List<HardwareItemBL> GetActiveByUserLogin(string userLogin)
        {
            List<HardwareItemBL> itemBL_List = new List<HardwareItemBL>();
            List<HardwareItem> itemList = new List<HardwareItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<HardwareItem>(x => x.Login == userLogin && x.Status == "A").ToList();

                itemList = itemList.OrderBy(x => x.OlafName).ThenBy(x => x.Model).ThenBy(x => x.Local).ThenBy(x => x.Id).ToList();

                HardwareItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new HardwareItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<HardwareItemBL> GetNotLinkedWithOrder()
        {
            List<HardwareItemBL> itemBL_List = GetAll().ToList();
            List<HardwareItemOrderForm> itemList = new List<HardwareItemOrderForm>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<HardwareItemOrderForm>(x => 1 == 1).ToList();

                HardwareItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new HardwareItemBL();
                    itemBL.MapData(item.HardwareItem);
                    itemBL_List.Remove(itemBL);
                });
            }

            return itemBL_List.OrderBy(x => x.Model).ThenBy(x => x.Status).ThenBy(x => x.Local).ThenBy(x => x.Id).ToList();
        }

        #endregion

        #region Internal Methods

        internal void MapData(HardwareItem item)
        {
            Id = item.Id;
            Status = item.Status;
            Local = item.Local;
            OlafName = item.OlafName;
            InventoryNo = item.InventoryNo;
            LastName = item.LastName;
            FirstName = item.FirstName;
            Login = item.Login;
            Phone = item.Phone;
            Building = item.Building;
            Office = item.Office;
            Model = item.Model;
            LongName = item.LongName;
            Description = item.Description;
            Serial = item.Serial;
            RmtMatId = item.RmtMatId;
            Unit = item.Unit;
            RmtCtgNm = item.RmtCtgNm;
            RmtParentCtgNm = item.RmtParentCtgNm;
            MaintenanceEndDate = item.MaintenanceEndDate;
        }

        internal HardwareItem MapData()
        {
            HardwareItem item = new HardwareItem();
            item.Id = Id;
            item.Status = Status;
            item.Local = Local;
            item.OlafName = OlafName;
            item.InventoryNo = InventoryNo;
            item.LastName = LastName;
            item.FirstName = FirstName;
            item.Login = Login;
            item.Phone = Phone;
            item.Building = Building;
            item.Office = Office;
            item.Model = Model;
            item.LongName = LongName;
            item.Description = Description;
            item.Serial = Serial;
            item.RmtMatId = RmtMatId;
            item.Unit = Unit;
            item.RmtParentCtgNm = RmtParentCtgNm;
            item.RmtMatId = RmtMatId;
            item.MaintenanceEndDate = MaintenanceEndDate;

            item.EntityKey = new EntityKey("GmsrEntities.HardwareItemSet", "Id", Id);

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

        #endregion
    }
}
