using Autofac;
using FinanceTracker.BillDomain;
using FinanceTracker.EntityFramework.Entity;
using Module = Autofac.Module;

namespace FinanceTracker.Service.Autofac
{
    public class AutoRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //// 注册该程序集中的所有公共类型  
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //       .PublicOnly() // 只注册公共类型  
            //       .AsImplementedInterfaces(); // 按照实现的接口注册

            LoadBill(builder);
        }

        private void LoadBill(ContainerBuilder builder)
        {
            builder.RegisterType<BillRepository>().As<IBillRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BillWorker>().As<IBillWorker>().InstancePerDependency();
        }
    }
}
