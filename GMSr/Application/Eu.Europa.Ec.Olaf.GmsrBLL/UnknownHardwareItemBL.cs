using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class UnknownHardwareItemBL : BaseBL
    {
        #region Properties

        public int Id { get; set; }
        public string ComputerName { get; set; }
        public string Source { get; set; }
        public DateTime FoundOn { get; set; }

        #endregion

        #region Public Methods

        public static List<UnknownHardwareItemBL> GetAll()
        {
            List<UnknownHardwareItemBL> itemBL_List = new List<UnknownHardwareItemBL>();
            List<UnknownHardwareItem> itemList = new List<UnknownHardwareItem>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<UnknownHardwareItem>(x => 1 == 1).ToList();
                itemList = itemList.OrderBy(x => x.MachineName).ToList();

                UnknownHardwareItemBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new UnknownHardwareItemBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;
        }

        #endregion

        #region Internal Methods

        internal void MapData(UnknownHardwareItem item)
        {
            Id = item.Id;
            ComputerName = item.MachineName;
            Source = item.SourceName;
            FoundOn = item.FoundOn;
        }

        #endregion

        #region Private Methods

        private void ValidateDelete(ref ArrayList validationErrorList)
        {
            //todo: Check for referential integrity.
        }

        #endregion
    }
}
