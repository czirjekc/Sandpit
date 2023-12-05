using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;


namespace Eu.Europa.Ec.Olaf.GmsrDAL
{
    public class EntityFrameworkRepositoryFactory : RepositoryFactory
    {
        public override IRepository CreateWithGmsrEntities()
        {
            return new EntityFrameworkRepository(new GmsrEntities());
        }

        public override IRepository CreateWithGetSmcEntities()
        {
            return new EntityFrameworkRepository(new GetSmcEntities());
        }
    }
}