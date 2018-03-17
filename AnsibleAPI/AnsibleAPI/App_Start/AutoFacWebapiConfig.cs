using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Moq;
using AnsibleAPI.Domain.Abstract;
using AnsibleAPI.Domain.Entities;

namespace AnsibleAPI.App_Start
{
    public class AutoFacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<IEntityRepository>();

            var mockEntityRepository = new Mock<IEntityRepository>();
            var mockEntities = new Entity[]
            {
                new Entity(1, "Test Name 1"),
                new Entity(2, "Test Name 2"),
                new Entity(3, "Test Name 3"),
                new Entity(4, "Test Name 4"),
            };
            mockEntityRepository.Setup(m => m.GetEntities()).Returns(mockEntities);
            mockEntityRepository.Setup(m => m.GetEntiy(It.IsAny<int>()))
                .Returns<int>(id =>
                {
                    return mockEntities.FirstOrDefault(y => y.Id == id);
                });
            mockEntityRepository.Setup(m => m.AddEntity(It.IsAny<Entity>()))
                .Returns<Entity>(e =>
                {
                    return e.Id == null;
                });
            mockEntityRepository.Setup(m => m.AddEntities(It.IsAny<IEnumerable<Entity>>()))
                .Returns<IEnumerable<Entity>>(e =>
                {
                    return e.All(x => x.Id == null);
                });
            builder.RegisterInstance(mockEntityRepository.Object).As<IEntityRepository>();

            Container = builder.Build();

            return Container;
        }
    }
}