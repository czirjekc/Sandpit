using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class FerretExtComputerGroupBL
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion

        #region Public Methods

        public static List<FerretExtComputerGroupBL> GetAll() 
        {
            List<FerretExtComputerGroupBL> itemBL_List = new List<FerretExtComputerGroupBL>();
            FerretExtComputerGroupBL itemBL;

            LANDeskCustomComputerGroup.GetAll().ForEach(x => 
            {
                itemBL = new FerretExtComputerGroupBL();
                itemBL.MapData(x);
                itemBL_List.Add(itemBL);
            });


            return itemBL_List;
        
        }

        #endregion

        #region Internal Methods

        internal void MapData(LANDeskCustomComputerGroup item) 
        {
            Id = item.Id;
            Name = item.Name;
        }

        #endregion
    }
}
