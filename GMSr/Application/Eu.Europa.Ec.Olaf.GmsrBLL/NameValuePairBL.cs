using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;
using System.Data;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class NameValuePairBL : BaseBL
    {
        #region Properties

        public string Name { get; set; }
        public string Value { get; set; }        

        #endregion
    }
}