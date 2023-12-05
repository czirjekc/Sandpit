using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class TicketFunctionalService
    {
        #region Properties

        public int Id { get; set; }
        public string Code { get; set; }
        public string Valid { get; set; }
        public string test { get; set; }

        #endregion

        #region Public Methods

        public static List<TicketFunctionalService> GetValid() 
        {
            List<TicketFunctionalService> itemBL_List = new List<TicketFunctionalService>();
            TicketFunctionalService itemBL;

            GetSmcFunctionalService.GetAll().Where(x => x.Valid== "Y").ToList().ForEach(item =>
                {
                    itemBL = new TicketFunctionalService();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                }
            );


            return itemBL_List;

        }

        #endregion

        #region Internal Methods

        internal void MapData(GetSmcFunctionalService item)
        {
            Id = item.Id;
            Code = item.Code;
            Valid = item.Valid;
        }

        #endregion
    }
}
