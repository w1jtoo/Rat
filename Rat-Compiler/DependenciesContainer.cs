using System;
using Autofac;
using log4net;
using Rat_Compiler.Infrastructure;

namespace Rat_Compiler
{
    public static class DependenciesContainer
    {
        public static ContainerBuilder Create(IProjectData data)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(DependenciesContainer).Assembly).AsImplementedInterfaces();
            builder.RegisterInstance(data).As<IProjectData>();
            
            builder.RegisterInstance(LogManager.GetLogger(typeof(EntryPoint))).As<ILog>();
            builder.RegisterType<Rat>().As<IRat>();
            
            return builder;
        }
    }
}