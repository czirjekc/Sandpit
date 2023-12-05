using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class HardwareItemOrderFormBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public int HardwareItemId { get; set; }
        public int OrderFormId { get; set; }

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
                    MapData(repository.Save<HardwareItemOrderForm>(MapData(), userLogin));
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
                    repository.Delete<HardwareItemOrderForm>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {
            HardwareItemOrderForm item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<HardwareItemOrderForm>(id);
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

        public static List<HardwareItemOrderFormBL> GetAll()
        {
            List<HardwareItemOrderFormBL> itemBL_List = new List<HardwareItemOrderFormBL>();
            List<HardwareItemOrderForm> itemList = new List<HardwareItemOrderForm>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<HardwareItemOrderForm>(x => 1 == 1).ToList();
            }            

            HardwareItemOrderFormBL itemBL;
            itemList.ForEach(item =>
            {
                itemBL = new HardwareItemOrderFormBL();
                itemBL.MapData(item);
                itemBL_List.Add(itemBL);
            });

            return itemBL_List;
        }

        public static List<HardwareItemOrderFormBL> GetByOrderForm(int orderFormId)
        {
            List<HardwareItemOrderFormBL> itemBL_List = new List<HardwareItemOrderFormBL>();
            List<HardwareItemOrderForm> itemList = new List<HardwareItemOrderForm>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<HardwareItemOrderForm>(x => x.OrderFormId == orderFormId).ToList();

                HardwareItemOrderFormBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new HardwareItemOrderFormBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(HardwareItemOrderForm item)
        {
            Id = item.Id;
            HardwareItemId = item.HardwareItemId;
            OrderFormId = item.OrderFormId;
        }

        internal HardwareItemOrderForm MapData()
        {
            HardwareItemOrderForm item = new HardwareItemOrderForm();
            item.Id = Id;
            item.HardwareItemId = HardwareItemId;
            item.OrderFormId = OrderFormId;

            item.EntityKey = new EntityKey("GmsrEntities.HardwareItemOrderFormSet", "Id", Id);

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