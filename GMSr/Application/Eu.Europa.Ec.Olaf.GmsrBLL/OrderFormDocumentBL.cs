using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class OrderFormDocumentBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public int? OrderFormId { get; set; }
        public string OrderFormName { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public byte[]  File { get; set; }
        public string Path { get; set; }
        public string SoftwareProductSource { get; set; }

        public string FileSize
        {
            get
            {
                if (File != null)
                {
                    return File.Count() + " Bytes";
                }
                else
                {
                    return "";
                }
            }
        }

        public string FileInfo
        {
            get
            {
                if (File != null)
                {
                    return Filename + " (" + File.Count() + " Bytes)";
                }
                else
                {
                    return "";
                }
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
                    MapData(repository.Save<OrderFormDocument>(MapData(), userLogin));
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
                    repository.Delete<OrderFormDocument>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {
            OrderFormDocument item = null;
            using (IRepository repository = new EntityFrameworkRepository())
            {
                item = repository.Get<OrderFormDocument>(id);

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

        public static List<OrderFormDocumentBL> GetAll()
        {
            List<OrderFormDocumentBL> itemBL_List = new List<OrderFormDocumentBL>();
            List<OrderFormDocument> itemList = new List<OrderFormDocument>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OrderFormDocument>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.OrderFormId).ToList();

                OrderFormDocumentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OrderFormDocumentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OrderFormDocumentBL> GetByOrderForm(int orderFormId)
        {
            List<OrderFormDocumentBL> itemBL_List = new List<OrderFormDocumentBL>();
            List<OrderFormDocument> itemList = new List<OrderFormDocument>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<OrderFormDocument>(x => x.OrderFormId == orderFormId).ToList();

                OrderFormDocumentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OrderFormDocumentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<OrderFormDocumentBL> GetByKeyword(string keyword)
        {
            List<OrderFormDocumentBL> itemBL_List = new List<OrderFormDocumentBL>();
            List<OrderFormDocument> itemList = new List<OrderFormDocument>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                if (keyword == "")
                {
                    itemList = repository.Find<OrderFormDocument>(x => 1 == 1).ToList();
                }
                else
                {
                    itemList = repository.Find<OrderFormDocument>(x =>
                                                              x.Path.Contains(keyword) ||
                                                              x.OrderForm.Name.Contains(keyword) ||
                                                              x.OrderForm.SoftwareOrderItem.FirstOrDefault().SoftwareProduct.Source.Contains(keyword)
                                                              ).ToList();
                }

                itemList = itemList.OrderBy(x => x.Id).ToList();

                OrderFormDocumentBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new OrderFormDocumentBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }     

        #endregion

        #region Internal Methods

        internal void MapData(OrderFormDocument item)
        {
            Id = item.Id;
            OrderFormId = item.OrderFormId;
            OrderFormName = item.OrderForm == null ? null : item.OrderForm.Name;
            Description = item.Description;
            Filename = item.Filename;
            File = item.File;
            Path = item.Path;
            SoftwareProductSource = (item.OrderForm == null || item.OrderForm.SoftwareOrderItem == null || item.OrderForm.SoftwareOrderItem.Count == 0 || item.OrderForm.SoftwareOrderItem.First().SoftwareProduct == null) ? null : item.OrderForm.SoftwareOrderItem.First().SoftwareProduct.Source;
        }

        internal OrderFormDocument MapData()
        {
            OrderFormDocument item = new OrderFormDocument();
            item.Id = Id;
            item.OrderFormId = OrderFormId;
            item.Description = Description;
            item.Filename = Filename;
            item.File = File;
            item.Path = Path;
            item.EntityKey = new EntityKey("GmsrEntities.OrderFormDocumentSet", "Id", Id);

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