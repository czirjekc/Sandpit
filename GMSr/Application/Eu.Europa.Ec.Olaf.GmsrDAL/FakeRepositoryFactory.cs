using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;


namespace Eu.Europa.Ec.Olaf.GmsrDAL
{
    public class FakeRepositoryFactory : RepositoryFactory
    {        
        public override IRepository CreateWithGmsrEntities()
        {
            return new FakeRepository();
        }

        public override IRepository CreateWithGetSmcEntities()
        {
            return new FakeRepository();
        }
    }
}