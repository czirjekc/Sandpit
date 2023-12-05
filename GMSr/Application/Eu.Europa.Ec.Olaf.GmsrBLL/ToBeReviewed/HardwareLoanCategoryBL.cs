using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class HardwareLoanCategoryBL
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion

        #region Public Methods

        public static List<HardwareLoanCategoryBL> GetAll()
        {
            List<HardwareLoanCategoryBL> itemList_BL = new List<HardwareLoanCategoryBL>();
            HardwareLoanCategoryBL itemBL;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanCategory>(x => true).ToList().ForEach(x =>
                {
                    itemBL = new HardwareLoanCategoryBL();
                    itemBL.MapData(x);
                    itemList_BL.Add(itemBL);

                });
            }

            return itemList_BL;
        }

        #endregion

        #region Internal Methods

        internal void MapData(GLoanCategory item)
        {
            Id = item.Id;
            Name = item.Name;
        }

        #endregion

    }
}
