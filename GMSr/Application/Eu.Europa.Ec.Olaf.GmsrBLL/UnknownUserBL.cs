using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class UnknownUserBL : BaseBL
    {
        #region Properties

        public string UserName { get; set; }
        public string SourceName { get; set; }
        public DateTime FoundOn { get; set; }

        #endregion

        #region Public Methods

        public static List<UnknownUserBL> GetAll()
        {
            List<UnknownUserBL> itemBL_List = new List<UnknownUserBL>();
            List<UnknownUser> itemList = new List<UnknownUser>();

            using (IRepository repository = new EntityFrameworkRepository())
            {
                itemList = repository.Find<UnknownUser>(x => 1 == 1).ToList();
                itemList = itemList.OrderBy(x => x.UserName).ToList();

                UnknownUserBL itemBL;
                itemList.ForEach(item =>
                {
                    itemBL = new UnknownUserBL();
                    itemBL.MapData(item);
                    itemBL_List.Add(itemBL);
                });
            }

            return itemBL_List;

        }

        #endregion

        #region Internal Methods
        internal void MapData(UnknownUser item) 
        {
            UserName = item.UserName;
            SourceName = item.SourceName;
            FoundOn = item.FoundOn;
        }
        #endregion
    }
}
