using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.MongoDb.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;

namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    /// <summary>
    /// ÎÄµµ·½Ê½¶É
    /// </summary>
    [TestClass]
    public class FileDocTest : TestBase
    {
        [ExpectedException(typeof(InstrumentationException))]
        [TestMethod]
        public void TestFindByQueryPage_1()
        {
            for (int i = 0; i < 100; i++)
            {
                var content = i % 2 == 0 ? Resource1.File1 : System.Text.Encoding.UTF8.GetBytes(Resource1.File2);
                CreateFile(new MemoryStream(content), i.ToString());
            }
            var service = CreateFileStorage();
            var list = service.FindByQuery<FileEntity, string>(2, 10, p => p.Metadata["CreateName"] == "CreateName",
                p => p.Name, false);
        }

        [ExpectedException(typeof(InstrumentationException))]

        [TestMethod]
        public void TestFindByQuery_1()
        {
            for (int i = 0; i < 100; i++)
            {
                var content = i % 2 == 0 ? Resource1.File1 : System.Text.Encoding.UTF8.GetBytes(Resource1.File2);
                CreateFile(new MemoryStream(content), i.ToString());
            }
            var service = CreateFileStorage();
            var list = service.FindByQuery<FileEntity, string>(p => p.Metadata["CreateName"] == "CreateName",
                p => p.Name, false);

        }


        protected override IFileStorage CreateFileStorage()
        {
            return Factory.CreateFileStorage("FileDoc");
        }

        [TestInitialize]
        public void Init()
        {
            ClearAllFile();
        }
        [TestCleanup]
        public void Clear() { Init(); }

        [TestMethod]
        public void TestInsert()
        {
            var service = CreateFileStorage();
            var id = service.Insert(new MemoryStream(Resource1.File1), new FileEntity { FileName = "test" });
            Assert.AreEqual(false, id == ObjectId.Empty);
        }

        [TestMethod]
        public void TestGet()
        {
            var service = CreateFileStorage();
            var id = service.Insert(new MemoryStream(Resource1.File1), new FileEntity { FileName = "test" });
            var entity = service.FindById<FileEntity>(id);
            Assert.AreEqual(Resource1.File1.Length, entity.FileSize);
            Assert.AreEqual("test", entity.FileName);
        }
        [TestMethod]
        public void TestFindOneByCondition()
        {
            var service = CreateFileStorage();
            var id = service.Insert(new MemoryStream(Resource1.File1), new FileEntity { FileName = "test" });
            var entity = service.FindOneByCondition<FileEntity>(new Dictionary<string, object> { { "FileName", "test" } });
            Assert.AreEqual(Resource1.File1.Length, entity.FileSize);
            Assert.AreEqual("test", entity.FileName);
        }
        [TestMethod]
        public void TestUpdate()
        {
            var service = CreateFileStorage();
            var id = service.Insert(new MemoryStream(Resource1.File1), new FileEntity { FileName = "test" });
            var entity = service.FindById<FileEntity>(id);
            entity.Name = "name";
            entity.ModifyName = "createName";
            var dt = DateTime.Now;
            entity.ModifyTime = dt;
            service.Update(entity);

            var entity1 = service.FindById<FileEntity>(entity.Id);
            Assert.AreEqual(entity.Name, entity1.Name);
            Assert.AreEqual(entity.ModifyName, entity1.ModifyName);
            Assert.AreEqual(true, System.String.Compare(entity.ModifyTime.ToString("yyyyMMddHHmmss"), entity1.ModifyTime.ToString("yyyyMMddHHmmss"), System.StringComparison.Ordinal) == 0);
        }


        [TestMethod]
        public void TestFindByCondition()
        {
            for (int i = 0; i < 10; i++)
            {
                var content = i % 2 == 0 ? Resource1.File1 : System.Text.Encoding.UTF8.GetBytes(Resource1.File2);
                CreateFile(new MemoryStream(content), i.ToString());
            }
            var service = CreateFileStorage();
            var list = service.FindByCondition<FileEntity>(new Dictionary<string, object>() { { "CreateName", "CreateName" } }, new Dictionary<string, bool>() { { "Index", false } });

            Assert.AreEqual(10, list.Count);
            Assert.AreEqual(9, list[0].Index);
        }

        [TestMethod]
        public void TestFindByQuery()
        {
            for (int i = 0; i < 100; i++)
            {
                var content = i % 2 == 0 ? Resource1.File1 : System.Text.Encoding.UTF8.GetBytes(Resource1.File2);
                CreateFile(new MemoryStream(content), i.ToString());
            }
            var service = CreateFileStorage();
            var list = service.FindByQuery<FileEntity, string>(p => p.CreateName == "CreateName",
                p => p.Name, false);

            Assert.AreEqual(100, list.Count);
            Assert.AreEqual(99, list[0].Index);
        }



        [TestMethod]
        public void TestFindByQueryPage()
        {
            for (int i = 0; i < 100; i++)
            {
                var content = i % 2 == 0 ? Resource1.File1 : System.Text.Encoding.UTF8.GetBytes(Resource1.File2);
                CreateFile(new MemoryStream(content), i.ToString());
            }
            var service = CreateFileStorage();
            var list = service.FindByQuery<FileEntity, string>(2, 10, p => p.CreateName == "CreateName",
                p => p.Name, false);

            Assert.AreEqual(100, list.Count);

            Assert.AreEqual(10, list.DataList.Count);
            Assert.AreEqual(81, list.DataList[9].Index);
        }
        private ObjectId CreateFile(MemoryStream file, string tag)
        {
            var entity = new FileEntity { FileName = "test" + tag, Name = "Name" + tag, CreateName = "CreateName", CreateTime = DateTime.Now, Index = int.Parse(tag) };
            var service = CreateFileStorage();
            service.Insert(file, entity);
            return entity.Id;
        }



        [TestMethod]
        public void TestFindByQueryPage_2()
        {
            for (int i = 0; i < 100; i++)
            {
                var content = i % 2 == 0 ? Resource1.File1 : System.Text.Encoding.UTF8.GetBytes(Resource1.File2);
                CreateFile(new MemoryStream(content), i.ToString());
            }
            var service = CreateFileStorage();
            var list = service.AdvanceQuery<FileEntity>(2, 10, p1 => p1.Where(p => p.CreateName == "CreateName"),
                                                               p => p.OrderByDescending(p1 => p1.Name));

            Assert.AreEqual(100, list.Count);

            Assert.AreEqual(10, list.DataList.Count);
            Assert.AreEqual(81, list.DataList[9].Index);

        }

        [TestMethod]
        public void TestFindByQueryPage_3()
        {
            for (int i = 0; i < 100; i++)
            {
                var content = i % 2 == 0 ? Resource1.File1 : System.Text.Encoding.UTF8.GetBytes(Resource1.File2);
                CreateFile(new MemoryStream(content), i.ToString());
            }
            var service = CreateFileStorage();
            var list = service.AdvanceQuery<FileEntity>(p1 => p1.Where(p => p.CreateName == "CreateName"),
                                                               p => p.OrderBy(p1 => p1.Name));

            Assert.AreEqual(100, list.Count);
        }
        [ExpectedException(typeof(InstrumentationException))]
        [TestMethod]
        public void TestFindByQueryPage_5()
        {
            for (int i = 0; i < 100; i++)
            {
                var content = i % 2 == 0 ? Resource1.File1 : System.Text.Encoding.UTF8.GetBytes(Resource1.File2);
                CreateFile(new MemoryStream(content), i.ToString());
            }
            var service = CreateFileStorage();
            var list = service.AdvanceQuery<FileEntity>(2, 10, p1 => p1.Where(p => p.Metadata["CreateName"] == "CreateName"),
                                                               p => p.OrderByDescending(p1 => p1.Metadata["Name"]));

            Assert.AreEqual(100, list.Count);

            Assert.AreEqual(10, list.DataList.Count);
            Assert.AreEqual(81, list.DataList[9].Index);

        }
        [ExpectedException(typeof(InstrumentationException))]
        [TestMethod]
        public void TestFindByQueryPage_4()
        {
            for (int i = 0; i < 100; i++)
            {
                var content = i % 2 == 0 ? Resource1.File1 : System.Text.Encoding.UTF8.GetBytes(Resource1.File2);
                CreateFile(new MemoryStream(content), i.ToString());
            }
            var service = CreateFileStorage();
            var list = service.AdvanceQuery<FileEntity>(p1 => p1.Where(p => p.Metadata["CreateName"] == "CreateName"),
                                                               p => p.OrderBy(p1 => p1.Metadata["Name"]));

            Assert.AreEqual(100, list.Count);
        }

        [TestMethod]
        public void TestDownloadFindOneByCondition()
        {
            var service = CreateFileStorage();
            var id = service.Insert(new MemoryStream(Resource1.File1), new FileEntity { FileName = "test" });

            var entity = service.DownLoad<FileEntity>(new Dictionary<string, object>() { { "FileName", "test" }, { "Id", id } });
            Assert.IsNotNull(entity);
        }

        [TestMethod]
        public void TestDownload()
        {
            var service = CreateFileStorage();
            var id = service.Insert(new MemoryStream(Resource1.File1), new FileEntity { FileName = "test" });
            Assert.AreEqual(false, id == ObjectId.Empty);
            var stream = service.DownLoad<FileEntity>(id);
            Assert.AreEqual(Resource1.File1.LongLength, stream.Length);
        }
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestDownload_1()
        {
            var service = CreateFileStorage();
            var stream = service.DownLoad<FileEntity>(ObjectId.GenerateNewId());
        }
    }
}