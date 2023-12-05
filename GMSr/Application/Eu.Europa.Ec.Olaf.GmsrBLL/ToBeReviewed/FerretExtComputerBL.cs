using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class FerretExtComputerBL
    {
        #region Properties

        public string Name { get; set; }

        #endregion

        #region Public Methods 

        public static List<FerretExtComputerBL> GetByGroupId(int Id) {
            List<FerretExtComputerBL> itemList_BL = new List<FerretExtComputerBL>();
            FerretExtComputerBL itemBL;

            LANDeskComputer.GetByGroupId(Id).ForEach(x => {
                itemBL = new FerretExtComputerBL();
                itemBL.MapData(x);
                itemList_BL.Add(itemBL);
            });

            return itemList_BL;
        }

        #endregion

        #region Internal Methods 

        internal void MapData(LANDeskComputer item) 
        {
            Name = item.Name;
        }

        #endregion
    }
}
