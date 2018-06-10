using Zhoubin.Infrastructure.Common.MongoDb.Entity;

namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    public class DocumentRepositorySample : DocumentEntityBase
    {
        public override string CollectionName
        {
            get { return "DocumentRepositorySample"; }
        }

        public string Name { get; set; }
        public override void Fill(IEntity entity)
        {
            if (entity is DocumentRepositorySample)
            {
                Name = ((DocumentRepositorySample)entity).Name;
            }
            base.Fill(entity);
        }
    }
}