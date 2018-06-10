using Zhoubin.Infrastructure.Common.MongoDb.Entity;

namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    public class FileEntity : FileEntityBase
    {
        public int Index { get; set; }
        public override string CollectionName
        {
            get { return "FileCollection"; }
        }
    }
}