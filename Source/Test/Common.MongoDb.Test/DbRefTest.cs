using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using Zhoubin.Infrastructure.Common.MongoDb.Entity;

namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    /// <summary>
    /// DbRefTest 的摘要说明
    /// </summary>
    [TestClass]
    public class DbRefTest:TestBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            var objectStorage = CreateObjectStorage();
            var enity = new Agc {Name = "四川日报招标比选网"};
            objectStorage.Insert(enity);

            var notice = new Notice {Name = "测试公告一", Owner = new RefAgc {Agc = enity.CreateDbRef(), Role = "业主"}};
          var oid =   objectStorage.Insert(notice);
            Assert.AreNotEqual(null,oid,"对象插入失败");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var objectStorage = CreateObjectStorage();
            var enity = new Agc { Name = "四川日报招标比选网1" ,MyData = new Data(){Name="testName"}};
            objectStorage.Insert(enity);

            var notice = new Notice { Name = "测试公告一1", Owner = new RefAgc { Agc = enity.CreateDbRef(), Role = "业主1" } };
            var oid = objectStorage.Insert(notice);
            Assert.AreNotEqual(null, oid, "对象插入失败");
            var ob = objectStorage.FindByQuery<Notice,string>(p=>p.Id == new ObjectId(oid),p=>p.Name,false,p=>p.Select(w=>new {w.Id,w.Owner.Role,AgcId= objectStorage.LoadRef<Agc>(w.Owner.Agc) }).ToList<object>());
            
            Assert.AreEqual(1,ob.Count);
            //Assert.AreEqual(oid,ob[0].Id.ToString());
        }
    }

    public class Notice : DocumentEntityBase
    {
        public override string CollectionName
        {
            get { return "Notice"; }
        }

        public string Name { get; set; }

        public RefAgc Owner { get; set; }
    }

    public class Agc : DocumentEntityBase
    {
        public override string CollectionName
        {
            get { return "Agc"; }
        }
        public string Name { get; set; }
        public Data MyData { get; set; }
    }

    public class Data
    {
        public string Name { get; set; }
    }
    public class RefAgc
    {
        public string Role { get; set; }

        public MongoDBRef Agc { get; set; }
    }
}
