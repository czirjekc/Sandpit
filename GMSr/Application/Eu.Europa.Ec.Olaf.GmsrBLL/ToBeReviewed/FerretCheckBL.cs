using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class FerretCheckBL
    {
        #region Properties

        public int Id { get; set; }
        public string Type { get; set; }
        public bool Result { get; set; }
        public DateTime LastTryDate { get; set; }
        public int FerretComputerId { get; set; }
        public int FerretScriptId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Path { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public string KeyName { get; set; }

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
                    MapData(repository.Save<FerretCheck>(MapData(), userLogin));
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

        internal void MapData(FerretCheck item) 
        {
            Id = item.Id;
            Type = item.Type;
            Result = item.Result;
            LastTryDate = item.LastTryDate ?? DateTime.MinValue;
            FerretComputerId = item.FerretComputerId;
            FerretScriptId = item.FerretScriptId;
            CreationDate = item.CreationDate;
            Path = item.Path;
            Value = item.Value;
            Comment = item.Comment;
            KeyName = item.KeyName;
        }

        internal FerretCheck MapData() {

            FerretCheck item = new FerretCheck();

            item.Id = Id;
            item.Type = Type;
            item.Result = Result;
            item.LastTryDate = LastTryDate;
            item.FerretComputerId = FerretComputerId;
            item.FerretScriptId = FerretScriptId;
            item.CreationDate = CreationDate;
            item.Path = Path;
            item.Value = Value;
            item.Comment = Comment;
            item.KeyName = KeyName;

            item.EntityKey = new System.Data.EntityKey("GmsrEntities.FerretCheckSet", "Id", Id);
            
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
