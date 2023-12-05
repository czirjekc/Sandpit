using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

using System.Transactions;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class SoftwareProductBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }
        public string Version { get; set; }
        public string CompanyName { get; set; }
        public string Other { get; set; }
        public string Comment { get; set; }
        public string Source { get; set; }

        public int? SoftwareProductStatusId { get; set; }
        public string SoftwareProductStatusName { get; set; }

        public string Concatenation
        {
            get
            {
                return Name + " | " + Version + " | " + CompanyName + " | " + SoftwareProductStatusName + " | (" + Id + ")";
            }
        }

        public bool HasMatchWithDetectedSoftwareProduct
        {
            get
            {
                //checks whether the product has at least one match with a detected product
                return DetectedSoftwareProductMatchBL.GetBySoftwareProduct(Id).Count != 0;
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
                using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
                {
                    MapData(repository.Save<SoftwareProduct>(MapData(), userLogin));
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
                using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
                {
                    repository.Delete<SoftwareProduct>(MapData(), userLogin);
                }
            }
        }

        public bool Select(int id)
        {
            SoftwareProduct item = null;
            using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
            {
                item = repository.Get<SoftwareProduct>(id);

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

        public static List<SoftwareProductBL> GetAll()
        {
            List<SoftwareProductBL> itemBL_List = new List<SoftwareProductBL>();
            List<SoftwareProduct> itemList = new List<SoftwareProduct>();

            using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
            {
                itemList = repository.Find<SoftwareProduct>(x => 1 == 1).ToList();

                itemList = itemList.OrderBy(x => x.Name).ThenBy(x => x.Version).ThenBy(x => x.Id).ToList();

                SoftwareProductBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareProductBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareProductBL> GetByInfix(string infix)
        {
            List<SoftwareProductBL> itemBL_List = new List<SoftwareProductBL>();
            List<SoftwareProduct> itemList = new List<SoftwareProduct>();

            using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
            {
                itemList = repository.Find<SoftwareProduct>(x => (x.Name + " " + x.Version + " " + x.CompanyName).Contains(infix)
                                                                && x.Source == "GMSr"
                                                           ).ToList();

                SoftwareProductBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareProductBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareProductBL> GetByKeyword(string keyword)
        {
            List<SoftwareProductBL> itemBL_List = new List<SoftwareProductBL>();
            List<SoftwareProduct> itemList = new List<SoftwareProduct>();

            using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
            {

                if (keyword == "")
                {
                    itemList = repository.Find<SoftwareProduct>(x => 1 == 1).ToList();
                }
                else
                {

                    itemList = repository.Find<SoftwareProduct>(x =>
                                                                  x.Name.Contains(keyword) ||
                                                                  x.Version.Contains(keyword) ||
                                                                  x.CompanyName.Contains(keyword) ||
                                                                  x.Other.Contains(keyword) ||
                                                                  x.Comment.Contains(keyword) ||
                                                                  x.Source.Contains(keyword) ||
                                                                  x.SoftwareProductStatus.Name.Contains(keyword)
                                                                  ).ToList();
                }

                itemList = itemList.OrderBy(x => x.Name).ThenBy(x => x.Version).ThenBy(x => x.Id).ToList();

                SoftwareProductBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareProductBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        public static List<SoftwareProductBL> GetByParameters(string name,
                                                              string version,
                                                              string company,
                                                              string other,
                                                              int? softwareProductStatusId,
                                                              string comment,
                                                              string source
                                                             )
        {
            List<SoftwareProductBL> itemBL_List = new List<SoftwareProductBL>();
            List<SoftwareProduct> itemList = new List<SoftwareProduct>();

            using (IRepository repository = RepositoryFactory.CreateWithGmsrEntities())
            {
                itemList = repository.Find<SoftwareProduct>(x =>
                                                              (name == "" || x.Name.Contains(name)) &&
                                                              (version == "" || x.Version.Contains(version)) &&
                                                              (company == "" || x.CompanyName.Contains(company)) &&
                                                              (other == "" || x.Other.Contains(other)) &&
                                                              (softwareProductStatusId == null || x.SoftwareProductStatusId == softwareProductStatusId) &&
                                                              (comment == "" || x.Comment.Contains(comment)) &&
                                                              (source == "" || x.Source.Contains(source))
                                                              ).ToList();

                itemList = itemList.OrderBy(x => x.Id).ToList();

                SoftwareProductBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new SoftwareProductBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(SoftwareProduct item)
        {
            Id = item.Id;

            Name = item.Name;
            Version = item.Version;
            CompanyName = item.CompanyName;
            Other = item.Other;
            Comment = item.Comment;
            Source = item.Source;

            SoftwareProductStatusId = item.SoftwareProductStatus == null ? null : (int?)item.SoftwareProductStatus.Id;
            SoftwareProductStatusName = item.SoftwareProductStatus == null ? null : item.SoftwareProductStatus.Name;
        }

        internal SoftwareProduct MapData()
        {
            SoftwareProduct item = new SoftwareProduct();
            item.Id = Id;
            item.Name = Name;
            item.Version = Version;
            item.CompanyName = CompanyName;
            item.Other = Other;
            item.Comment = Comment;
            item.Source = Source;
            item.SoftwareProductStatusId = SoftwareProductStatusId;

            item.EntityKey = new EntityKey("GmsrEntities.SoftwareProductSet", "Id", Id);

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