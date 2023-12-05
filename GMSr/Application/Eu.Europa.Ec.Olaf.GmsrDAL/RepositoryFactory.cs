using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Eu.Europa.Ec.Olaf.GmsrDAL
{
    public abstract class RepositoryFactory
    {
        public abstract IRepository CreateWithGmsrEntities();
        
        public abstract IRepository CreateWithGetSmcEntities();
    }
}
