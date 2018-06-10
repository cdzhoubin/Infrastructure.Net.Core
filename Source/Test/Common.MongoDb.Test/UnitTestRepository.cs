using System;
using System.CodeDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Ninject;
using Zhoubin.Infrastructure.Common.Ioc;
using Zhoubin.Infrastructure.Common.Ioc.Ninject;
using Zhoubin.Infrastructure.Common.Repositories;
using Zhoubin.Infrastructure.Common.Repositories.Mongo;

namespace Zhoubin.Infrastructure.Common.MongoDb.Test
{
    [TestClass]
    public class UnitTestRepository: TestBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            UnitOfWork.RegisterResolver(ResolverBase.GetResolver<IocFactory>());
            using (var workFactory = UnitOfWork.Current)
            {
                using (var rep = workFactory.CreateRepository<DocumentRepositorySample>())
                {
                   var entity =  rep.Insert(new DocumentRepositorySample() {Name="123456"});
                    Assert.IsNotNull(entity.Id);
                    Assert.AreNotEqual(ObjectId.Empty,entity.Id);
                    entity.CreateName = "4321";
                    entity = rep.GetByKey(entity.Id);
                    Assert.AreEqual("123456", entity.Name);
                }
            }
        }
    }

    public class IocFactory: NinjectResolverBase
    {
        protected override void Init(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<MongoUnitOfWork>();
            kernel.Bind<IUnitOfWorkFactory>().To<MongoUnitOfWorkFactory>();
            kernel.Bind<IRepository<DocumentRepositorySample>>().To<MongoRepository<DocumentRepositorySample>>();
        }
    }
}
