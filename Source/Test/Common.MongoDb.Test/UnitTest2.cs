using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    [TestClass]
    public class UnitTest2 : TestBase
    {
        [TestInitialize]
        public void Init()
        {
            ClearAllObject();
        }
        [TestCleanup]
        public void Clear() { Init(); }

        [TestMethod]
        public void TestInsert()
        {
            var id = CreateNewEntity("1");
            Assert.AreNotEqual(true, string.IsNullOrEmpty(id));
        }

        private string CreateNewEntity(string tag)
        {
            var objectService = CreateObjectStorage();
            return objectService.Insert(CreateEntity(tag));
        }
        [TestMethod]
        public void TestRemove()
        {
            var id = CreateNewEntity("2");
            Assert.AreNotEqual(true, string.IsNullOrEmpty(id));
            var objectService = CreateObjectStorage();
            objectService.Delete<DocumentSample>(id);
        }

        [TestMethod]
        public void TestRemoveTarget()
        {
            for (var i = 0; i < 10; i++)
            {
                CreateNewEntity(i.ToString(CultureInfo.InvariantCulture));
            }
            var objectService = CreateObjectStorage();
            bool result = objectService.Delete<DocumentSample>(new Dictionary<string, object>() { { "Name", "测试5" } });
            Assert.AreEqual(true, result);
            var count = objectService.Count<DocumentSample>(default(IDictionary<string, object>));
            Assert.AreEqual(9, count);
        }
        [TestMethod]
        public void TestRemoveTargetForLinq()
        {
            for (var i = 0; i < 10; i++)
            {
                CreateNewEntity(i.ToString(CultureInfo.InvariantCulture));
            }
            var objectService = CreateObjectStorage();
            
            var result = objectService.Delete<DocumentSample>(p => p.Name == "测试5");
            Assert.AreEqual(true, result);
            var count = objectService.Count<DocumentSample>(default(IDictionary<string, object>));
            Assert.AreEqual(9, count);
        }
        [TestMethod]
        public void TestUpdate()
        {
            var id = CreateNewEntity("test");
            var objectService = CreateObjectStorage();
            var entity = objectService.FindById<DocumentSample>(id);
            Assert.IsNotNull(entity);

            entity.Name = "测试名称修改";
            entity.CreateName = "测试创建者";
            entity.ModifyName = "w测试修改者";

            var result = objectService.Update(entity);
            Assert.AreEqual(true, result);

            var entity1 = objectService.FindOneByCondition<DocumentSample>(new Dictionary<string, object> { { "Name", entity.Name } });
            Assert.AreEqual(entity.Name, entity1.Name);
            Assert.AreEqual(entity.CreateName, entity1.CreateName);
            Assert.AreEqual(entity.ModifyName, entity1.ModifyName);
        }

        [TestMethod]
        public void TestAny()
        {
            for (var i = 0; i < 10; i++)
            {
                CreateNewEntity(i.ToString(CultureInfo.InvariantCulture));
            }
            var objectService = CreateObjectStorage();
            var result = objectService.Any<DocumentSample>(new Dictionary<string, object>() { { "Name", "测试5" } });
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestAnyLanmda()
        {
            for (var i = 0; i < 10; i++)
            {
                CreateNewEntity(i.ToString(CultureInfo.InvariantCulture));
            }
            var objectService = CreateObjectStorage();
            var result = objectService.Any<DocumentSample>(p => p.Name == "测试5");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestFindByCondition()
        {
            for (var i = 0; i < 10; i++)
            {
                CreateNewEntity(i.ToString(CultureInfo.InvariantCulture));
                CreateNewEntity("2");
            }
            var objectService = CreateObjectStorage();
            var result = objectService.FindByCondition<DocumentSample>(new Dictionary<string, object> { { "CreateName", "CreateName" } }, new Dictionary<string, bool> { { "Name", false } });
            Assert.AreEqual(20, result.Count);
            Assert.AreEqual("测试9", result[0].Name);
        }

        [TestMethod]
        public void TestFindByQuery()
        {
            for (var i = 0; i < 19; i++)
            {
                CreateNewEntity(i.ToString(CultureInfo.InvariantCulture));
            }

            Thread.Sleep(1000);

            CreateNewEntity("2");
            var objectService = CreateObjectStorage();
            var result = objectService.FindByQuery<DocumentSample, DateTime>(p => p.CreateName == "CreateName", p => p.ModifyTime, false);
            Assert.AreEqual(20, result.Count);
            Assert.AreEqual("测试2", result[0].Name);
        }

        [TestMethod]
        public void TestFindByQueryPage1()
        {
            for (var i = 0; i < 100; i++)
            {
                CreateNewEntity(i.ToString(CultureInfo.InvariantCulture));
                CreateNewEntity("2");
            }
            var objectService = CreateObjectStorage();
            var result = objectService.FindByQuery<DocumentSample, string>(3, 5, p => p.CreateName == "CreateName" && p.Name != "测试2", p => p.Name, false);

            Assert.AreEqual(99, result.Count);
            Assert.AreEqual(5, result.DataList.Count);
            Assert.AreEqual("测试9", result.DataList[0].Name);
            Assert.AreEqual("测试86", result.DataList[4].Name);
        }

        [TestMethod]
        public void TestFindByQueryPage2()
        {
            for (var i = 0; i < 100; i++)
            {
                CreateNewEntity(i.ToString(CultureInfo.InvariantCulture));
                CreateNewEntity("2");
            }
            var objectService = CreateObjectStorage();
            var result = objectService.FindByQuery<DocumentSample, string,string>(3, 5, p => p.CreateName == "CreateName" && p.Name != "测试2", p => p.Name, false,p=>p.Select(w=>w.Name).ToList());

            Assert.AreEqual(99, result.Count);
            Assert.AreEqual(5, result.DataList.Count);
            Assert.AreEqual("测试9", result.DataList[0]);
            Assert.AreEqual("测试86", result.DataList[4]);
        }
        private static DocumentSample CreateEntity(string tag)
        {
            var entity = new DocumentSample
                {
                    Name = "测试" + tag,
                    CreateTime = DateTime.Now,
                    ModifyTime = DateTime.Now.AddDays(1),
                    CreateName = "CreateName",
                    ModifyName = "修改人:" + tag
                };

            return entity;
        }

        private static BitDocument createBitDocument(int bitNumber, List<int> rolesList = null)
        {
            var entity = new BitDocument
            {
                Role = bitNumber,
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now.AddDays(1),
                CreateName = "CreateName",
                ModifyName = "修改人:" + bitNumber,

            };

            entity.Roles = rolesList ?? new List<int> { 1, 2, 3, 4, bitNumber };

            return entity;
        }


        [TestMethod]
        public void TestBitDocumentQuery()
        {
            using (var objectService = CreateObjectStorage())
            {
                objectService.Insert(createBitDocument(7));
                Thread.Sleep(1000);
                objectService.Insert(createBitDocument(4));
                var result = objectService.FindByQuery<BitDocument, DateTime>(p => p.Role == 7 || p.Role ==4,
                    p => p.ModifyTime, false);
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(4, result[0].Role);
            }
        }

        [TestMethod]
        public void TestBitDocumentQuery1()
        {
            using (var objectService = CreateObjectStorage())
            {
                objectService.Insert(createBitDocument(7));
                Thread.Sleep(1000);
                objectService.Insert(createBitDocument(4));
                var result = objectService.FindByQuery<BitDocument, DateTime>(p => p.ModifyName.Contains("7") || p.ModifyName.Contains("4"),
                    p => p.ModifyTime, false);
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(4, result[0].Role);
            }
        }

        [TestMethod]
        public void TestBitDocumentQuery2()
        {
            using (var objectService = CreateObjectStorage())
            {
                objectService.Insert(createBitDocument(7));
                objectService.Insert(createBitDocument(9, new List<int>() { 4, 8, 9, 10, 11, 12 }));
                objectService.Insert(createBitDocument(13));
                Thread.Sleep(1000);
                objectService.Insert(createBitDocument(15));

                objectService.Insert(createBitDocument(4, new List<int>() { 18, 5, 9, 10, 11, 12 }));
                var result = objectService.FindByQuery<BitDocument, DateTime>(p => p.Roles.Contains(7) || p.Roles.Contains(4),
                    p => p.ModifyTime, false);
                Assert.AreEqual(4, result.Count);
                Assert.AreEqual(15, result[0].Role);
            }
        }
    }
}
