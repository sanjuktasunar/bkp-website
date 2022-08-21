using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using web.Services;
using web.Services.Services;
using web.Web.Services.Services;
using Web.Services.Services;

namespace web.Utility
{
    public static class RegisterServices
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IUsersService, UsersService>();

            //services
            container.RegisterType<IMenusService, MenusService>();
            container.RegisterType<IStaffsService, StaffsService>();
            container.RegisterType<IDropDownService, DropDownService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IDesignationService, DesignationService>();
            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<IAccountHeadService, AccountHeadService>();
            container.RegisterType<IFiscalYearService, FiscalYearService>();
            container.RegisterType<IMemberService, MemberService>();
            //container.RegisterType<IMemberRegisterService, MemberRegisterService>();
            container.RegisterType<IShareTypesService, ShareTypesService>();
            container.RegisterType<IAgentService, AgentService>();

            //Repositories
            container.RegisterType<IMemberRepository, MemberRepository>();
            container.RegisterType<IAgentRepository, AgentRepository>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}