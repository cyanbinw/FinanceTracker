using Autofac;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.Loader;
using Module = Autofac.Module;

namespace FinanceTracker.Service.Autofac
{
    public class AutoRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

        }
    }
}
