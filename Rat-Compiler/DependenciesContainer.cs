using Autofac;
using log4net;

namespace Rat_Compiler
{
    internal static class DependenciesContainer
    {
        public static ContainerBuilder Create()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(ContainerBuilder).Assembly).AsImplementedInterfaces();
            builder.RegisterInstance(LogManager.GetLogger(typeof(EntryPoint))).As<ILog>();
            
            return builder;
        }
    }
}