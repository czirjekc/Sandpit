using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class FerretComputerBL
    {
        #region Properties 

        public int Id { get; set; }
        public string Name { get; set; }

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
                    MapData(repository.Save<FerretComputer>(MapData(), userLogin));
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        public static List<FerretComputerBL> GetAll() 
        {
            List<FerretComputerBL> itemList_BL = new List<FerretComputerBL>();
            FerretComputerBL itemBL;


            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<FerretComputer>( x => true).ToList().ForEach(x =>
                {
                    itemBL = new FerretComputerBL();
                    itemBL.MapData(x);
                    itemList_BL.Add(itemBL);
                });
            }

            return itemList_BL;
        }

        #endregion

        #region Internal Methods 

        internal void MapData(FerretComputer item) 
        {
            Id = item.Id;
            Name = item.Name;
        }

        internal FerretComputer MapData() 
        {
            FerretComputer item = new FerretComputer();
            item.Name = Name;

            item.EntityKey = new System.Data.EntityKey("GmsrEntities.FerretComputerSet", "Id", 0);

            return item;
        }

        #endregion

        #region Private Methods

        private void ValidateSave(ref ArrayList validationErrorList)
        {

        }

        #endregion

    }
}
