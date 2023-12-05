using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{    
    public class DummyBL
    {
        #region Properties

        public int Id { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }
        public string Field7 { get; set; }
        public string Field8 { get; set; }
        public string Field9 { get; set; }
        public string Field10 { get; set; }
        public string Field11 { get; set; }
        public string Field12 { get; set; }
        public string Field13 { get; set; }
        public string Field14 { get; set; }
        public string Field15 { get; set; }
        public string Field16 { get; set; }
        public string Field17 { get; set; }
        public string Field18 { get; set; }
        public string Field19 { get; set; }
        public string Field20 { get; set; }
        public string Field21 { get; set; }
        public string Field22 { get; set; }
        public string Field23 { get; set; }
        public string Field24 { get; set; }
        public string Field25 { get; set; }

        public string Concatenation { get; set; }

        #endregion

        #region Constructors

        public DummyBL()
        {
        }

        public DummyBL(int id, params string[] fields)
        {
            Id = id;
            if (fields.Length > 0)
            {
                Field1 = fields[0];
                Concatenation += fields[0];
            }
            if (fields.Length > 1)
            {
                Field2 = fields[1];
                Concatenation += " " + fields[1];
            }
            if (fields.Length > 2)
            {
                Field3 = fields[2];
                Concatenation += " " + fields[2];
            }
            if (fields.Length > 3)
            {
                Field4 = fields[3];
                Concatenation += " " + fields[3];
            }
            if (fields.Length > 4)
            {
                Field5 = fields[4];
                Concatenation += " " + fields[4];
            }
            if (fields.Length > 5)
            {
                Field6 = fields[5];
                Concatenation += " " + fields[5];
            }
            if (fields.Length > 6)
            {
                Field7 = fields[6];
                Concatenation += " " + fields[6];
            }
            if (fields.Length > 7)
            {
                Field8 = fields[7];
                Concatenation += " " + fields[7];
            }
            if (fields.Length > 8)
            {
                Field9 = fields[8];
                Concatenation += " " + fields[8];
            }
            if (fields.Length > 9)
            {
                Field10 = fields[9];
                Concatenation += " " + fields[9];
            }
            if (fields.Length > 10)
            {
                Field11 = fields[10];
                Concatenation += " " + fields[10];
            }
            if (fields.Length > 11)
            {
                Field12 = fields[11];
                Concatenation += " " + fields[11];
            }
            if (fields.Length > 12)
            {
                Field13 = fields[12];
                Concatenation += " " + fields[12];
            }
            if (fields.Length > 13)
            {
                Field14 = fields[13];
                Concatenation += " " + fields[13];
            }
            if (fields.Length > 14)
            {
                Field15 = fields[14];
                Concatenation += " " + fields[14];
            }
            if (fields.Length > 15)
            {
                Field16 = fields[15];
                Concatenation += " " + fields[15];
            }
            if (fields.Length > 16)
            {
                Field17 = fields[16];
                Concatenation += " " + fields[16];
            }
            if (fields.Length > 17)
            {
                Field18 = fields[17];
                Concatenation += " " + fields[17];
            }
            if (fields.Length > 18)
            {
                Field19 = fields[18];
                Concatenation += " " + fields[18];
            }
            if (fields.Length > 19)
            {
                Field20 = fields[19];
                Concatenation += " " + fields[19];
            }
            if (fields.Length > 20)
            {
                Field21 = fields[20];
                Concatenation += " " + fields[20];
            }
            if (fields.Length > 21)
            {
                Field22 = fields[21];
                Concatenation += " " + fields[21];
            }
            if (fields.Length > 22)
            {
                Field23 = fields[22];
                Concatenation += " " + fields[22];
            }
            if (fields.Length > 23)
            {
                Field24 = fields[23];
                Concatenation += " " + fields[23];
            }
            if (fields.Length > 24)
            {
                Field25 = fields[24];
                Concatenation += " " + fields[24];
            }
        }

        #endregion

        #region Public Methods

        public static List<DummyBL> GetOrderList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager"));
            itemEO_List.Add(new DummyBL(2, "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager"));
            itemEO_List.Add(new DummyBL(3, "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager"));
            itemEO_List.Add(new DummyBL(4, "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager"));
            itemEO_List.Add(new DummyBL(5, "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager"));

            return itemEO_List;
        }

        public static List<DummyBL> GetOrderTypeList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "License"));
            itemEO_List.Add(new DummyBL(2, "License Upgrade"));
            itemEO_List.Add(new DummyBL(3, "License Lifetime Extension"));
            itemEO_List.Add(new DummyBL(4, "License Pack"));
            itemEO_List.Add(new DummyBL(5, "License Pack Upgrade"));
            itemEO_List.Add(new DummyBL(6, "License Pack Lifetime Extension"));

            return itemEO_List;
        }

        public static List<DummyBL> GetProductList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "UPG Partition Manager", "6.02", "Quest", "Other", "Operational"));
            itemEO_List.Add(new DummyBL(2, "UPG Partition Manager", "6.02", "Quest", "Other", "Operational"));
            itemEO_List.Add(new DummyBL(3, "UPG Partition Manager", "6.02", "Quest", "Other", "Operational"));
            itemEO_List.Add(new DummyBL(4, "UPG Partition Manager", "6.02", "Quest", "Other", "Operational"));
            itemEO_List.Add(new DummyBL(5, "UPG Partition Manager", "6.02", "Quest", "Other", "Operational"));

            return itemEO_List;
        }

        public static List<DummyBL> GetMediaItemList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "ACDSee Single user ESD W95", "DVD", "AFIS"));
            itemEO_List.Add(new DummyBL(2, "ACDSee Single user ESD W95", "DVD", "AFIS"));
            itemEO_List.Add(new DummyBL(3, "ACDSee Single user ESD W95", "DVD", "AFIS"));
            itemEO_List.Add(new DummyBL(4, "ACDSee Single user ESD W95", "DVD", "AFIS"));
            itemEO_List.Add(new DummyBL(5, "ACDSee Single user ESD W95", "DVD", "AFIS"));

            return itemEO_List;
        }

        public static List<DummyBL> GetMediaItemTypeList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "CD"));
            itemEO_List.Add(new DummyBL(2, "DVD"));
            itemEO_List.Add(new DummyBL(3, "CD box"));
            itemEO_List.Add(new DummyBL(4, "DVD box"));
            itemEO_List.Add(new DummyBL(5, "CD copy"));
            itemEO_List.Add(new DummyBL(6, "DVD copy"));
            itemEO_List.Add(new DummyBL(7, "Tape"));
            itemEO_List.Add(new DummyBL(8, "Floppy Disk"));

            return itemEO_List;
        }

        public static List<DummyBL> GetMediaItemLocationList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "AFIS"));
            itemEO_List.Add(new DummyBL(2, "Location2"));
            itemEO_List.Add(new DummyBL(3, "Location3"));
            itemEO_List.Add(new DummyBL(4, "Location4"));
            itemEO_List.Add(new DummyBL(5, "Location5"));

            return itemEO_List;
        }

        public static List<DummyBL> GetLicenseList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "1/08/2009", "1/1/2012"));
            itemEO_List.Add(new DummyBL(2, "124", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "1/08/2009", "1/1/2012"));
            itemEO_List.Add(new DummyBL(3, "125", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "1/08/2009", "1/1/2012"));
            itemEO_List.Add(new DummyBL(4, "126", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "1/08/2009", "1/1/2012"));
            itemEO_List.Add(new DummyBL(5, "127", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "1/08/2009", "1/1/2012"));

            return itemEO_List;
        }

        public static List<DummyBL> GetLicenseList2()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User"));
            itemEO_List.Add(new DummyBL(2, "124", "GAE1-07SR-8138-QY4E-1GCM", "Single User"));
            itemEO_List.Add(new DummyBL(3, "125", "GAE1-07SR-8138-QY4E-1GCM", "Single User"));
            itemEO_List.Add(new DummyBL(4, "126", "GAE1-07SR-8138-QY4E-1GCM", "Single User"));
            itemEO_List.Add(new DummyBL(5, "127", "GAE1-07SR-8138-QY4E-1GCM", "Single User"));

            return itemEO_List;
        }

        public static List<DummyBL> GetLicenseList2One()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User"));

            return itemEO_List;
        }

        public static List<DummyBL> GetLicenseUpgradeList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));
            itemEO_List.Add(new DummyBL(2, "124", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));
            itemEO_List.Add(new DummyBL(3, "125", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));
            itemEO_List.Add(new DummyBL(4, "126", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));
            itemEO_List.Add(new DummyBL(5, "127", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));

            return itemEO_List;
        }

        public static List<DummyBL> GetLicenseUpgradeListOne()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));

            return itemEO_List;
        }

        public static List<DummyBL> GetLicenseExtensionList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));
            itemEO_List.Add(new DummyBL(2, "124", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));
            itemEO_List.Add(new DummyBL(3, "125", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));
            itemEO_List.Add(new DummyBL(4, "126", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));
            itemEO_List.Add(new DummyBL(5, "127", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));

            return itemEO_List;
        }

        public static List<DummyBL> GetLicenseExtensionListOne()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "98"));

            return itemEO_List;
        }

        public static List<DummyBL> GetLicenseTypeList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "Single User License"));
            itemEO_List.Add(new DummyBL(2, "Multi User License"));
            itemEO_List.Add(new DummyBL(3, "Enterprise License"));
            itemEO_List.Add(new DummyBL(4, "Volume License"));
            itemEO_List.Add(new DummyBL(5, "Network Dongle License"));
            itemEO_List.Add(new DummyBL(6, "Free License"));

            return itemEO_List;
        }

        public static List<DummyBL> GetLicenseAssignmentList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "125", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(2, "126", "VANHAUWAERT Frederik", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(3, "127", "NAME2", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(4, "128", "NAME3", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(5, "129", "NAME4", "020I 2009 08214 34", "12/07/2010"));

            return itemEO_List;
        }

        public static List<DummyBL> GetProductStatusList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "Operational"));
            itemEO_List.Add(new DummyBL(2, "Operational (Evaluation)"));
            itemEO_List.Add(new DummyBL(3, "Not-Operational (Phase-Out)"));
            itemEO_List.Add(new DummyBL(4, "Not-Operational (Decommissioned)"));

            return itemEO_List;
        }

        //public static List<DummyBL> GetOrderList()
        //{
        //    List<DummyBL> itemEO_List = new List<DummyBL>();

        //    itemEO_List.Add(new DummyBL(1, "ORD/04/97", "DI/5650"));
        //    itemEO_List.Add(new DummyBL(2, "ORD/05/51", "DI/5650"));
        //    itemEO_List.Add(new DummyBL(3, "ORD/05/84", "DI/5650"));
        //    itemEO_List.Add(new DummyBL(4, "ORD/05/95", "DI/5650"));
        //    itemEO_List.Add(new DummyBL(5, "ORD/05/96", "DI/5650"));

        //    return itemEO_List;
        //}

        public static List<DummyBL> GetOlafUserList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "HARDING", "Andrew"));
            itemEO_List.Add(new DummyBL(2, "JANQUART", "Eric"));
            itemEO_List.Add(new DummyBL(3, "VANHAUWAERT", "Frederik"));
            itemEO_List.Add(new DummyBL(4, "DROULEZ", "Yves"));
            itemEO_List.Add(new DummyBL(5, "HANGYA", "Gabor"));

            return itemEO_List;
        }

        public static List<DummyBL> GetSoftwareList()
        {
            List<DummyBL> itemEO_List = new List<DummyBL>();

            itemEO_List.Add(new DummyBL(1, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(2, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(3, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(4, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(5, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(6, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(7, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(8, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(9, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(10, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(11, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(12, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(13, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));
            itemEO_List.Add(new DummyBL(14, "DI/5650", "OR 1675", "09-330", "License Upgrade", "UPG Partition Manager", "6.02", "Quest", "-", "Operational", "31/12/2012", "ACDSee Single user ESD W95", "OR1778", "5.01", "123", "GAE1-07SR-8138-QY4E-1GCM", "Single User", "-", "1/08/2009", "1/1/2012", "52", "HARDING Andrew", "020I 2009 08214 34", "12/07/2010"));             

            return itemEO_List;
        }

        #endregion
    }
}