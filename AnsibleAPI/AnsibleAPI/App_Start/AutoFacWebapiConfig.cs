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
            mockEntityRepository.Setup(m => m.Entities).Returns(new List<Entity>
            {
                new Entity(1, "Test Name 1"),
                new Entity(2, "Test Name 2"),
                new Entity(3, "Test Name 3"),
                new Entity(4, "Test Name 4"),
            });
            builder.RegisterInstance(mockEntityRepository.Object).As<IEntityRepository>();

            Container = builder.Build();

            return Container;
        }
    }
}