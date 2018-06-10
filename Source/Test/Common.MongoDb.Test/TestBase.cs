namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    public class TestBase
    {
        protected bool ClearAllFile()
        {
            var fileService = CreateFileStorage();
            return fileService.DeleteAll<FileEntity>();
        }

        protected bool ClearAllObject()
        {
            var objectService = CreateObjectStorage();
            objectService.DeleteAll<BitDocument>();
            return objectService.DeleteAll<DocumentSample>();
        }
        protected virtual IFileStorage CreateFileStorage()
        {
            return Factory.CreateFileStorage();
        }

        protected IObjectStorage CreateObjectStorage()
        {
            return Factory.CreateObjectStorage();
        }
    }
}