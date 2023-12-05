using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class UnitAndActiveDirectoryAssociationBL
    {
        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Unit { get; set; }
        public bool BaseGroup { get; set; }
        public string OtherGroups { get; set; }


        #endregion

        #region Public Methods

        public static List<UnitAndActiveDirectoryAssociationBL> GetAll()
        {
            List<ActiveDirectoryGroupOlafUserBL> adMembershipList = new List<ActiveDirectoryGroupOlafUserBL>();
            List<OlafUserBL> activeUserList = new List<OlafUserBL>();

            List<UnitAndActiveDirectoryAssociationBL> itemBL_list = new List<UnitAndActiveDirectoryAssociationBL>();
            UnitAndActiveDirectoryAssociationBL itemBL;

            List<string> adGroupList = new List<string>();
            adGroupList.Add("olaf_fs_dirgen_usr");
            adGroupList.Add("olaf_fs_cds_usr");
            adGroupList.Add("olaf_fs_dira_dir_usr");
            adGroupList.Add("olaf_fs_dirb_dir_usr");
            adGroupList.Add("olaf_fs_dirc_dir_usr");
            adGroupList.Add("olaf_fs_dird_dir_usr");
            adGroupList.Add("olaf_fs_dira_a1_usr");
            adGroupList.Add("olaf_fs_dira_a2_usr");
            adGroupList.Add("olaf_fs_dira_a3_usr");
            adGroupList.Add("olaf_fs_dira_a4_usr");
            adGroupList.Add("olaf_fs_dirb_b1_usr");
            adGroupList.Add("olaf_fs_dirb_b2_usr");
            adGroupList.Add("olaf_fs_dirb_b3_usr");
            adGroupList.Add("olaf_fs_dirb_b4_usr");
            adGroupList.Add("olaf_fs_dirc_c1_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c4_usr");
            adGroupList.Add("olaf_fs_dirc_c5_usr");
            adGroupList.Add("olaf_fs_dird_d1_usr");
            adGroupList.Add("olaf_fs_dird_d2_usr");
            adGroupList.Add("olaf_fs_dird_d3_usr");
            adGroupList.Add("olaf_fs_dird_d4_usr");
            adGroupList.Add("olaf_fs_dird_d5_usr");
            adGroupList.Add("olaf_fs_dird_d6_usr");
            adGroupList.Add("olaf_fs_dird_d7_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");

            adMembershipList = ActiveDirectoryGroupOlafUserBL.GetByActiveDirectoryGroupNameList(adGroupList);
            activeUserList = OlafUserBL.GetByParameters("", "", "", "", "", "", "", "", "", "Y", "A", false, "").Where(x => x.Unit != null).ToList();



            activeUserList.ForEach(user =>
            {
                itemBL = new UnitAndActiveDirectoryAssociationBL();
                itemBL.Username = user.Login;
                itemBL.Unit = user.Unit;
                itemBL.BaseGroup = (adMembershipList.Where(x => x.User.ToLower() == user.Login.ToLower()).Any(x => x.Group.ToLower() == TransformUnitToGroup(user.Unit)));
                itemBL.OtherGroups = "";
                adMembershipList.Where(x => (x.User.ToLower() == user.Login.ToLower()) && (x.Group.ToLower() != TransformUnitToGroup(user.Unit))).ToList().ForEach(x =>
                {
                    itemBL.OtherGroups += x.Group + "; ";
                });

                itemBL_list.Add(itemBL);
            });


            return itemBL_list;
        }

        public static List<UnitAndActiveDirectoryAssociationBL> GetNegatives()
        {
            List<ActiveDirectoryGroupOlafUserBL> adMembershipList = new List<ActiveDirectoryGroupOlafUserBL>();
            List<OlafUserBL> activeUserList = new List<OlafUserBL>();

            List<UnitAndActiveDirectoryAssociationBL> itemBL_list = new List<UnitAndActiveDirectoryAssociationBL>();
            UnitAndActiveDirectoryAssociationBL itemBL;

            List<string> adGroupList = new List<string>();
            adGroupList.Add("olaf_fs_dirgen_usr");
            adGroupList.Add("olaf_fs_cds_usr");
            adGroupList.Add("olaf_fs_dira_dir_usr");
            adGroupList.Add("olaf_fs_dirb_dir_usr");
            adGroupList.Add("olaf_fs_dirc_dir_usr");
            adGroupList.Add("olaf_fs_dird_dir_usr");
            adGroupList.Add("olaf_fs_dira_a1_usr");
            adGroupList.Add("olaf_fs_dira_a2_usr");
            adGroupList.Add("olaf_fs_dira_a3_usr");
            adGroupList.Add("olaf_fs_dira_a4_usr");
            adGroupList.Add("olaf_fs_dirb_b1_usr");
            adGroupList.Add("olaf_fs_dirb_b2_usr");
            adGroupList.Add("olaf_fs_dirb_b3_usr");
            adGroupList.Add("olaf_fs_dirb_b4_usr");
            adGroupList.Add("olaf_fs_dirc_c1_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c2_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c3_usr");
            adGroupList.Add("olaf_fs_dirc_c4_usr");
            adGroupList.Add("olaf_fs_dirc_c5_usr");
            adGroupList.Add("olaf_fs_dird_d1_usr");
            adGroupList.Add("olaf_fs_dird_d2_usr");
            adGroupList.Add("olaf_fs_dird_d3_usr");
            adGroupList.Add("olaf_fs_dird_d4_usr");
            adGroupList.Add("olaf_fs_dird_d5_usr");
            adGroupList.Add("olaf_fs_dird_d6_usr");
            adGroupList.Add("olaf_fs_dird_d7_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");
            adGroupList.Add("olaf_fs_dird_d8_usr");

            adMembershipList = ActiveDirectoryGroupOlafUserBL.GetByActiveDirectoryGroupNameList(adGroupList);
            activeUserList = OlafUserBL.GetByParameters("", "", "", "", "", "", "", "", "", "Y", "A", false, "").Where(x => x.Unit != null).ToList();



            activeUserList.ForEach(user =>
            {

                itemBL = new UnitAndActiveDirectoryAssociationBL();
                itemBL.FirstName = user.FirstName;
                itemBL.LastName = user.LastName;
                itemBL.Username = user.Login;
                itemBL.Unit = user.Unit;
                itemBL.BaseGroup = (adMembershipList.Where(x => x.User.ToLower() == user.Login.ToLower()).Any(x => x.Group.ToLower() == TransformUnitToGroup(user.Unit)));
                itemBL.OtherGroups = "";
                adMembershipList.Where(x => (x.User.ToLower() == user.Login.ToLower()) && (x.Group.ToLower() != TransformUnitToGroup(user.Unit))).ToList().ForEach(x =>
                {
                    itemBL.OtherGroups += x.Group + "; ";
                });

                itemBL_list.Add(itemBL);
            });


            return itemBL_list.Where(x => x.BaseGroup == false).ToList();
        }


        #endregion

        #region Private Methods

        private static string TransformUnitToGroup(string unit)
        {
            switch (unit)
            {
                case "OLAF": return "olaf_fs_dirgen_usr";
                case "OLAF.SUPCOM": return "olaf_fs_cds_usr";
                case "OLAF.A": return "olaf_fs_dira_dir_usr";
                case "OLAF.B": return "olaf_fs_dirb_dir_usr";
                case "OLAF.C": return "olaf_fs_dirc_dir_usr";
                case "OLAF.D": return "olaf_fs_dird_dir_usr";
                case "OLAF.A.1": return "olaf_fs_dira_a1_usr";
                case "OLAF.A.2": return "olaf_fs_dira_a2_usr";
                case "OLAF.A.3": return "olaf_fs_dira_a3_usr";
                case "OLAF.A.4": return "olaf_fs_dira_a4_usr";
                case "OLAF.B.1": return "olaf_fs_dirb_b1_usr";
                case "OLAF.B.2": return "olaf_fs_dirb_b2_usr";
                case "OLAF.B.3": return "olaf_fs_dirb_b3_usr";
                case "OLAF.B.4": return "olaf_fs_dirb_b4_usr";
                case "OLAF.C.1": return "olaf_fs_dirc_c1_usr";
                case "OLAF.C.2": return "olaf_fs_dirc_c2_usr";
                case "OLAF.C.2.001": return "olaf_fs_dirc_c2_usr";
                case "OLAF.C.2.002": return "olaf_fs_dirc_c2_usr";
                case "OLAF.C.2.003": return "olaf_fs_dirc_c2_usr";
                case "OLAF.C.2.004": return "olaf_fs_dirc_c2_usr";
                case "OLAF.C.2.005": return "olaf_fs_dirc_c2_usr";
                case "OLAF.C.3": return "olaf_fs_dirc_c3_usr";
                case "OLAF.C.3.001": return "olaf_fs_dirc_c3_usr";
                case "OLAF.C.3.002": return "olaf_fs_dirc_c3_usr";
                case "OLAF.C.3.003": return "olaf_fs_dirc_c3_usr";
                case "OLAF.C.3.004": return "olaf_fs_dirc_c3_usr";
                case "OLAF.C.3.005": return "olaf_fs_dirc_c3_usr";
                case "OLAF.C.4": return "olaf_fs_dirc_c4_usr";
                case "OLAF.C.5": return "olaf_fs_dirc_c5_usr";
                case "OLAF.D.1": return "olaf_fs_dird_d1_usr";
                case "OLAF.D.2": return "olaf_fs_dird_d2_usr";
                case "OLAF.D.3": return "olaf_fs_dird_d3_usr";
                case "OLAF.D.4": return "olaf_fs_dird_d4_usr";
                case "OLAF.D.5": return "olaf_fs_dird_d5_usr";
                case "OLAF.D.6": return "olaf_fs_dird_d6_usr";
                case "OLAF.D.7": return "olaf_fs_dird_d7_usr";
                case "OLAF.D.8": return "olaf_fs_dird_d8_usr";
                case "OLAF.D.8.001": return "olaf_fs_dird_d8_usr";
                case "OLAF.D.8.002": return "olaf_fs_dird_d8_usr";
                case "OLAF.D.8.003": return "olaf_fs_dird_d8_usr";
                case "OLAF.D.8.004": return "olaf_fs_dird_d8_usr";
                case "OLAF.D.8.005": return "olaf_fs_dird_d8_usr";



                default: return "unknown fs";
            }
        }

        #endregion
    }
}
