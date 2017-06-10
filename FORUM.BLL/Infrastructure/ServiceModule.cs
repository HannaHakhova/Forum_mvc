using Ninject.Modules;
using FORUM.DAL.Interfaces;
using FORUM.DAL.Repositories.General;

namespace FORUM.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
    public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
