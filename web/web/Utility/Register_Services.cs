using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
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
            //container.RegisterType<IImageService, ImageService>();
            container.RegisterType<IMenusService, MenusService>();
            container.RegisterType<IStaffsService, StaffsService>();
            container.RegisterType<IDropDownService, DropDownService>();
            //container.RegisterType<IAdministrationService, AdministrationService>();
            //container.RegisterType<IUnitService, UnitService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IDesignationService, DesignationService>();
            container.RegisterType<IDepartmentService, DepartmentService>();
            //container.RegisterType<IOrganizationInfoService, OrganizationInfoService>();
            //container.RegisterType<ICustomerQueryService, CustomerQueryService>();
            container.RegisterType<IAccountHeadService, AccountHeadService>();
            //container.RegisterType<ISupplierService, SupplierService>();
            container.RegisterType<IFiscalYearService, FiscalYearService>();
            //container.RegisterType<IDateService, DateService>();
            //container.RegisterType<IDataService, DataService>();
            container.RegisterType<IMemberService, MemberService>();
            container.RegisterType<IMemberRegisterService, MemberRegisterService>();
            //container.RegisterType<IEmailService, EmailService>();
            //container.RegisterType<IEmailTemplateService, EmailTemplateService>();
            //container.RegisterType<ICountryService, CountryService>();
            //container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IShareTypesService, ShareTypesService>();
            container.RegisterType<IAgentService, AgentService>();
            //container.RegisterType<IReportService, ReportService>();
            //container.RegisterType<IPratigyaPatraService, PratigyaPatraService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}