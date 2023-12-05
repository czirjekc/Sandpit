using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class FerretScriptBL
    {
        #region Properties

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreationDate { get; set; }

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
                    MapData(repository.Save<FerretScript>(MapData(), userLogin));
                }

                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #endregion

        #region Internal Methods 

        internal void MapData(FerretScript item) 
        {
            Id = item.Id;
            Description = item.Description;
            IsEnabled = item.IsEnabled;
            CreationDate = item.CreationDate;
        }

        internal FerretScript MapData() {
            FerretScript item = new FerretScript();

            item.Id = Id;
            item.Description = Description;
            item.IsEnabled = IsEnabled;
            item.CreationDate = DateTime.Now;

            item.EntityKey = new System.Data.EntityKey("GmsrEntities.FerretScriptSet", "Id", Id);

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
