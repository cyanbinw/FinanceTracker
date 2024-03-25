using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace FinanceTracker.EntityFramework.Autofac
{
    public class DataRegister: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 注册该程序集中的所有公共类型  
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .PublicOnly() // 只注册公共类型  
                   .AsImplementedInterfaces(); // 按照实现的接口注册
        }
    }
}
