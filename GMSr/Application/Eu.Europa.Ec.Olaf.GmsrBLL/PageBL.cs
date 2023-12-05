using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class PageBL : BaseBL
    {
        #region Properties

        private string urlField;

        public string Url
        {
            get { return urlField; }
            set { urlField = value; }
        }

        private string sitePathField;

        public string SitePath
        {
            get { return sitePathField; }
            set { sitePathField = value; }
        }

        #endregion

        #region Constructors

        public PageBL()
        {
        }

        public PageBL(string url, string sitePath)
        {
            urlField = url;
            sitePathField = sitePath;
        }

        #endregion
    }
}