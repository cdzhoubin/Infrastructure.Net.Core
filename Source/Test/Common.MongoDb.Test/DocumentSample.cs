using System.Collections.Generic;
using Zhoubin.Infrastructure.Common.MongoDb.Entity;

namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    public class DocumentSample : DocumentEntityBase
    {
        public override string CollectionName
        {
            get { return "DocumentSamples"; }
        }

        public string Name { get; set; }
        public override void Fill(IEntity entity)
        {
            if (entity is DocumentSample)
            {
                Name = ((DocumentSample)entity).Name;
            }
            base.Fill(entity);
        }
    }

    public class BitDocument : DocumentEntityBase
    {

        public override string CollectionName
        {
            get { return "BitDocumnet"; }
        }

        public int Role { get; set; }

        public List<int> Roles { get; set; }
    }
}