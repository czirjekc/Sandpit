using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class DocumentBL
    {
        //#region Properties

        //public int Id { get; set; }

        //public string Description { get; set; }
        //public string FileName { get; set; }
        //public byte[]  File { get; set; }
        //public string Path { get; set; }
        //public string SoftwareProductSource { get; set; }

        //public string FileSize
        //{
        //    get
        //    {
        //        if (File != null)
        //        {
        //            return File.Count() + " Bytes";
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //}

        //#endregion  

        //#region Internal Methods

        //internal void MapData(OrderFormDocument item)
        //{
        //    Id = item.Id;
        //    OrderFormId = item.OrderFormId;
        //    OrderFormName = item.OrderForm == null ? null : item.OrderForm.Name;
        //    Name = item.Name;
        //    File = item.File;
        //    Path = item.Path;
        //    SoftwareProductSource = (item.OrderForm == null || item.OrderForm.SoftwareOrderItem == null || item.OrderForm.SoftwareOrderItem.Count == 0 || item.OrderForm.SoftwareOrderItem.First().SoftwareProduct == null) ? null : item.OrderForm.SoftwareOrderItem.First().SoftwareProduct.Source;
        //}

        //internal OrderFormDocument MapData()
        //{
        //    OrderFormDocument item = new OrderFormDocument();
        //    item.Id = Id;
        //    item.OrderFormId = OrderFormId;
        //    item.Name = Name;
        //    item.File = File;
        //    item.Path = Path;
        //    item.EntityKey = new EntityKey("GmsrEntities.OrderFormDocumentSet", "Id", Id);

        //    return item;
        //}

        //#endregion  
    }
}