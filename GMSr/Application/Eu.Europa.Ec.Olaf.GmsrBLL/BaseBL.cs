using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public abstract class BaseBL
    {
        #region Properties

        private static RepositoryFactory repositoryFactory = new EntityFrameworkRepositoryFactory(); // Default repository
        public static RepositoryFactory RepositoryFactory
        {
            get
            {
                return repositoryFactory;
            }

            set
            {
                repositoryFactory = value;
            }
        }

        #endregion
    }
}
