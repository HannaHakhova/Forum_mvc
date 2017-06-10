using FORUM.WEB.Util;
using Ninject;
using Owin;
using FORUM.BLL.Infrastructure;
using FORUM.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;


[assembly: OwinStartup(typeof(FORUM.WEB.Startup))]


namespace FORUM.WEB
{
    public class Startup
    {
        NinjectDependencyResolver resolver = new NinjectDependencyResolver(new StandardKernel(new ServiceModule()));

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            var serviceBag = (IServiceBag)resolver.GetService(typeof(IServiceBag));
            return serviceBag.UserService;
        }
    }
}