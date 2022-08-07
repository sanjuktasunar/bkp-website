using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web.Utility;
using web.Web.Entity.Dto;
using Web.Entity.Dto;

namespace web.Web.Services.Services
{
    public interface IDropDownService
    {
        IEnumerable<DropdownDto> GetDropDownRole();
        IEnumerable<DropdownDto> GetDropDownDepartment();
        IEnumerable<DropdownDto> GetDropDownDesignation();
        IEnumerable<DropdownDto> GetDropDownGender();
        IEnumerable<DropdownDto> GetDropDownUserStatus();
        IEnumerable<DropdownDto> GetDropDownUserType();
        IEnumerable<DropdownDto> GetDropDownUserTypeForUserList();
        IEnumerable<DropdownDto> GetDropDownFiscalYear();
        IEnumerable<DropdownDto> GetDropDownProvince();
        IEnumerable<DropdownDto> GetDropDownDistrict(int? ProvinceId = null);
        IEnumerable<DropdownDto> GetDropDownOccupation();
        IEnumerable<DropdownDto> GetDropDownShareTypes();
        IEnumerable<DropdownDto> GetDropDownAgentStatus();
        IEnumerable<DropdownDto> GetDropDownAccountHead();
        IEnumerable<DropdownDto> GetDropDownMaritalStatus();
        IEnumerable<DropdownDto> GetOutsideCountry();
        IEnumerable<DropdownDto> GetReferenceAgentList();
        IEnumerable<DropdownDto> GetReferenceMemberList();
    }

    public class DropDownService:IDropDownService
    {
        private readonly Repository<DropdownDto> _repository;
        public DropDownService()
        {
            _repository = new Repository<DropdownDto>();
        }
        public IEnumerable<DropdownDto> GetDropDownRole()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.ROLE_LIST] == null)
            {
                data = _repository.Query<DropdownDto>("select RoleId as [Key],RoleName as [Value]" +
                    " from dbo.[Role] where [Status]=1").ToList();
                HttpRuntime.Cache[CacheCodes.ROLE_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.ROLE_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownDepartment()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.DEPARTMENT_LIST] == null)
            {
                data = _repository.Query<DropdownDto>("select DepartmentId as [Key],DepartmentName as [Value]" +
                    " from dbo.[Department] where [Status]=1").ToList();
                HttpRuntime.Cache[CacheCodes.DEPARTMENT_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.DEPARTMENT_LIST] as List<DropdownDto>;
            }
            return data;
        }
        public IEnumerable<DropdownDto> GetDropDownDesignation()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.DESIGNATION_LIST] == null)
            {
                data = _repository.Query<DropdownDto>("select DesignationId as [Key],DesignationName as [Value]" +
                    " from dbo.[Designation] where [Status]=1").ToList();
                HttpRuntime.Cache[CacheCodes.DESIGNATION_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.DESIGNATION_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownGender()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.GENDER_LIST] == null)
            {
                data = _repository.Query<DropdownDto>("select GenderId as [Key],GenderName as [Value]" +
                    " from dbo.[Gender] where [Status]=1").ToList();
                HttpRuntime.Cache[CacheCodes.GENDER_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.GENDER_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownUserStatus()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.USER_STATUS_LIST] == null)
            {
                data = _repository.Query<DropdownDto>
                    (
                        "select StatusId as [Key]," +
                        "StatusName as [Value] " +
                        "from dbo.[UserStatus] " +
                        "where [Status]=1"
                    ).ToList();
                HttpRuntime.Cache[CacheCodes.USER_STATUS_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.USER_STATUS_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownUserType()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.USER_TYPE_LIST] == null)
            {
                data = _repository.Query<DropdownDto>
                    (
                        "select UserTypeId as [Key]," +
                        "UserTypeTitle as [Value] " +
                        "from dbo.[UserType] " +
                        "where [Status]=1"
                    ).ToList();
                HttpRuntime.Cache[CacheCodes.USER_TYPE_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.USER_TYPE_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownUserTypeForUserList()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.USER_TYPE_LIST_FOR_USER_LIST] == null)
            {
                data = _repository.Query<DropdownDto>
                    (
                        "select UserTypeId as [Key]," +
                        "UserTypeTitle as [Value] " +
                        "from dbo.[UserType] " +
                        "where [Status]=1 and lower(UserTypeTitle)<>'staff'"
                    ).ToList();
                HttpRuntime.Cache[CacheCodes.USER_TYPE_LIST_FOR_USER_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.USER_TYPE_LIST_FOR_USER_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownFiscalYear()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.FISCALYEAR_LIST] == null)
            {
                data = _repository.Query<DropdownDto>
                        (
                            "select FiscalYearId as [Key]," +
                            "FiscalYearName as [Value] " +
                            "from dbo.[FiscalYear] " +
                            "where Status=1"
                        ).ToList();
                HttpRuntime.Cache[CacheCodes.FISCALYEAR_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.FISCALYEAR_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownProvince()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.PROVINCE_LIST] == null)
            {
                data = _repository.Query<DropdownDto>
                        (
                            "select ProvinceId as [Key]," +
                            "ProvinceName as [Value] " +
                            "from dbo.[Province] " +
                            "where [Status]=1"
                        ).ToList();
                HttpRuntime.Cache[CacheCodes.PROVINCE_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.PROVINCE_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownDistrict(int ? ProvinceId=null)
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.DISTRICT_LIST] == null)
            {
                data = _repository.Query<DropdownDto>
                        (
                            "select DistrictId as [Key]," +
                            "DistrictName as [Value] " +
                            "from dbo.[District] " +
                            "where [Status]=1 and " +
                            "(ProvinceId=@ProvinceId or isnull(@ProvinceId,0)=0)",
                            new { ProvinceId }
                        ).ToList();
                HttpRuntime.Cache[CacheCodes.DISTRICT_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.DISTRICT_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownOccupation()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.OCCUPATION_LIST] == null)
            {
                data = _repository.Query<DropdownDto>
                        (
                            "select Id as [Key]," +
                            "[Name] as [Value] " +
                            "from dbo.[Occupation] " +
                            "where [Status]=1"
                        ).ToList();
                data.Add(new DropdownDto{ Key=-1,Value="Others"});
                HttpRuntime.Cache[CacheCodes.OCCUPATION_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.OCCUPATION_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownShareTypes()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.SHARE_TYPE_LIST] == null)
            {
                data = _repository.Query<DropdownDto>
                        (
                            "select ShareTypeId as [Key]," +
                            "ShareTypeName as [Value] " +
                            "from dbo.[ShareTypes] " +
                            "where [Status]=1 order by IsPrimary desc"
                        ).ToList();
                //HttpRuntime.Cache[CacheCodes.SHARE_TYPE_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.SHARE_TYPE_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownAgentStatus()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.AGENT_STATUS] == null)
            {
                data = _repository.Query<DropdownDto>
                        (
                            "select Id as [Key]," +
                            "StatusName as [Value] " +
                            "from dbo.[AgentStatus] " +
                            "where [IsActive]=1"
                        ).ToList();
                HttpRuntime.Cache[CacheCodes.AGENT_STATUS] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.AGENT_STATUS] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownAccountHead()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.ACCOUNT_HEAD_LIST] == null)
            {
                data = _repository.Query<DropdownDto>
                        (
                            "select AccountHeadId as [Key]," +
                            "AccountHeadName+'('+AccountNumber+')' as [Value] " +
                            "from dbo.[AccountHead] " +
                            "where [Status]=1"
                        ).ToList();
                HttpRuntime.Cache[CacheCodes.ACCOUNT_HEAD_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.ACCOUNT_HEAD_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetDropDownMaritalStatus()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.MARITAL_STATUS_LIST] == null)
            {
                data = _repository.Query<DropdownDto>
                        (
                            "select Id as [Key]," +
                            "MaritalStatusName as [Value] " +
                            "from dbo.[MaritalStatus] " +
                            "where [IsActive]=1"
                        ).ToList();
                HttpRuntime.Cache[CacheCodes.MARITAL_STATUS_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.MARITAL_STATUS_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetOutsideCountry()
        {
            var data = new List<DropdownDto>();
            if (HttpRuntime.Cache[CacheCodes.OUTSIDE_COUNTRY_LIST] == null)
            {
                data = _repository.Query<DropdownDto>
                        (
                            "select Id as [Key]," +
                            "Name as [Value] " +
                            "from dbo.[Country] " +
                            "where [Status]=1 and IsOutsideNepal=1"
                        ).ToList();
                HttpRuntime.Cache[CacheCodes.OUTSIDE_COUNTRY_LIST] = data;
            }
            else
            {
                data = HttpRuntime.Cache[CacheCodes.OUTSIDE_COUNTRY_LIST] as List<DropdownDto>;
            }
            return data;
        }

        public IEnumerable<DropdownDto> GetReferenceAgentList()
        {
            var data = _repository.StoredProcedure<DropdownDto>
                        (
                           "[dbo].[Sp_GetReferenceAgents]"
                        ).ToList();
            return data;
        }
        public IEnumerable<DropdownDto> GetReferenceMemberList()
        {
            var data = _repository.StoredProcedure<DropdownDto>
                        (
                           "[dbo].[Sp_GetReferenceMembers]"
                        ).ToList();
            return data;
        }
    }
}