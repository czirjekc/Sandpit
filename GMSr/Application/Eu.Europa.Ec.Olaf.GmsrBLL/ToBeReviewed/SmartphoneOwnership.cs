using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class SmartphoneOwnership
    {
        public string Unit { get; set; }
        public string User { get; set; }
        public string Model { get; set; }
        public string Label { get; set; }
        public int DeliveryYear { get; set; }

        public static List<SmartphoneOwnership> GetAll()
        {
            List<SmartphoneOwnership> itemBL_list = new List<SmartphoneOwnership>();
            SmartphoneOwnership itemBL;
            List<HardwareItem> hardwareItem_List = new List<HardwareItem>();
            List<OlafUser> olafUser_List = new List<OlafUser>();

            List<string> matchList = new List<string>();

            OlafUser olafUser;

            using (IRepository repository = new EntityFrameworkRepository())
            {

                hardwareItem_List = repository.Find<HardwareItem>(x => x.Status == "A" && x.Login != "olaf001" && x.Login != "olaf002" && x.RmtParentCtgNm == "3205 Ordinateurs individuels spéciaux").ToList();
                matchList = hardwareItem_List.Select(x => x.Login).ToList();

                olafUser_List = repository.Find<OlafUser>(y => matchList.Contains(y.Login)).ToList();

                hardwareItem_List.ForEach(x =>
                {
                    itemBL = new SmartphoneOwnership();
                    olafUser = olafUser_List.Where(y => y.Login == x.Login).FirstOrDefault();

                    itemBL.MapData(x, olafUser);
                    itemBL_list.Add(itemBL);
                });


            }


            return itemBL_list;
        }

        internal void MapData(HardwareItem hardwareItem, OlafUser olafUser)
        {
            Unit = olafUser.Unit;
            User = olafUser.LastName + " " + olafUser.FirstName;
            Model = hardwareItem.Model.Replace("HTC0926400  PDA-PHONE", "HTC Touch Pro");
            Label = hardwareItem.InventoryNo;
            DeliveryYear = hardwareItem.DeliveryDate.Value.Year;

        }
    }
}
