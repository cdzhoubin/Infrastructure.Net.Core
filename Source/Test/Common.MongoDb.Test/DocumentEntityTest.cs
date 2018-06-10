using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhoubin.Infrastructure.Common.MongoDb.Entity;

namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    [TestClass]
    public  class DocumentEntityTest: TestBase
    {
        [TestInitialize]
        public void Init()
        {
            ClearAllObject();
        }
        [TestCleanup]
        public void Clear() { Init(); }

        [TestMethod]
        public void TestAssertCollectionName()
        {
            var id = new DocumentEntity<DocumentEntity>();
            Assert.AreEqual("DocumentEntities", id.CollectionName);
        }
        [TestMethod]
        public void TestUpdate()
        {
            var objectService = CreateObjectStorage();
            var entity = new DocumentEntitySample(1921,3,8);
            entity.Name = "Name";
            entity.Phone = "Phone";
            entity.Age = 10;
            var entity1 = new DocumentEntitySample(1991,2,10);
            entity1.Name = "NameUpdate";
            entity1.Phone = "PhoneUpdate";
            entity1.Age = 20;
            entity1.Id = ObjectId.GenerateNewId();
            entity.Fill(entity1);
            Assert.AreEqual(entity.Name, entity1.Name);
            Assert.AreEqual(entity.Phone, entity1.Phone);
            Assert.AreEqual(entity.Age, entity1.Age);
            Assert.AreEqual(8, entity.GetDay());
            Assert.AreEqual(3, entity.Month);
            Assert.AreEqual(1921, entity.GetYear());
            Assert.AreEqual(entity.Id, ObjectId.Empty);
        }
    }

    public class DocumentEntitySample : DocumentEntity<DocumentEntitySample>
    {
        public DocumentEntitySample() { }
        public DocumentEntitySample(int year,int month,int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        private int Year { get; set; }
        protected int Day { get; set; }
        internal int Month { get; set; }

        public int GetDay()
        {
            return Day;
        }
        public int GetYear()
        {
            return Year;
        }

    }
}
