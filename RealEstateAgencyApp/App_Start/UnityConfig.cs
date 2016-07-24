using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Assemblies.DataContext;
using Assemblies;

namespace RealEstateAgencyApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ApplicationDbContext>();
            container.RegisterType<IAgentBL, AgentBL>();
            container.RegisterType<IAgencyBL, AgencyBL>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}