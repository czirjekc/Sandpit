using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class HardwareLoanStatusBL
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion

        #region Public Methods

        public static List<HardwareLoanStatusBL> GetAll()
        {
            List<HardwareLoanStatusBL> itemList_BL = new List<HardwareLoanStatusBL>();
            HardwareLoanStatusBL item;

            using (IRepository repository = new EntityFrameworkRepository(new GmsrEntities()))
            {
                repository.Find<GLoanStatus>(x => true).ToList().ForEach(x =>
                {
                    item = new HardwareLoanStatusBL();
                    item.MapData(x);
                    itemList_BL.Add(item);
                }
                );
            }

            return itemList_BL;
        }

        #endregion

        #region Internal Methods

        internal void MapData(GLoanStatus item)
        {
            Id = item.Id;
            Name = item.Name;
        }

        #endregion
    }
}
