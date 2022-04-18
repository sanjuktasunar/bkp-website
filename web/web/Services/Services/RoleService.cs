using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using web.Utility;
using web.Web.Entity.Infrastructure;
using web.Web.Services;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Entity.Infrastructure;
using Web.Services.Mapping;

namespace web.Web.Services.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<RoleDto> GetRoleById(int? id);
        Task<Response> Insert(RoleDto dto);
        Task<Response> Update(RoleDto dto);
        Task<Response> Delete(int? id);
        Task<RoleDto> MenuAccessPermissionAsync(int? RoleId);
        Task<Response> AddMenuAccess(int? RoleId, IEnumerable<MenuAccessPermissionDto> dtos);
    }
    public class RoleService : IRoleService
    {
        private readonly Repository<Role> _repository;
        private readonly Repository<MenuAccessPermission> _menuAccessRepository;
        private SqlConnectionDetails _sql;
        private readonly MessageClass _messageClass;
        public RoleService()
        {
            _repository = new Repository<Role>();
            _menuAccessRepository = new Repository<MenuAccessPermission>();
            _messageClass = new MessageClass();
            _sql = _repository.GetSqlTransactionDetails();
        }
        public async Task<IEnumerable<RoleDto>> GetAllRoles()
        {
            var obj= (await _repository.QueryAsync<RoleDto>("SELECT * FROM Role"));
            return obj;
        }

        public async Task<RoleDto> GetRoleById(int? id)
        {
            var obj = (await _repository.QueryAsync<RoleDto>("SELECT * FROM Role WHERE RoleId=@id", new { id })).FirstOrDefault();
            return obj;
        }

        public async Task<Response> Insert(RoleDto dto)
        {
            var result = new Response();
            try
            {
                var entity = dto.ToEntity();
                int data = await _repository.InsertAsync(entity);
                result = _messageClass.SaveMessage(data);
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
            }
            return result;
        }

        public async Task<Response> Update(RoleDto dto)
        {
            var result = new Response();
            try
            {
                var obj = await GetRoleById(dto.RoleId);
                if (obj == null)
                {
                    result = _messageClass.NotFoundMessage();
                }
                else
                {
                    var entity = dto.ToEntity();
                    int data = await _repository.UpdateAsync(entity);
                    result = _messageClass.SaveMessage(data);
                }
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
            }
            return result;
        }

        public async Task<Response> Delete(int? id)
        {
            var result = new Response();
            try
            {
                var obj = await GetRoleById(id);
                if (obj == null)
                {
                    result = _messageClass.NotFoundMessage();
                }
                else
                {
                    int data = await _repository.DeleteAsync(id);
                    result = _messageClass.DeleteMessage(data);
                }
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
            }
            return result;
        }

        public async Task<RoleDto> MenuAccessPermissionAsync(int? RoleId)
        {
            var role = (await GetRoleById(RoleId));
            if (role is null)
                return null;

            var dto = await _repository.StoredProcedureAsync<MenuAccessPermissionDto>("[dbo].[MenuAccessPermissionForRole]", new { RoleId = RoleId });
            role.MenuAccessPermissions = await _repository.StoredProcedureAsync<MenuAccessPermissionDto>("[dbo].[MenuAccessPermissionForRole]", new { RoleId = RoleId });
            return role;
        }

        public async Task<IEnumerable<MenuAccessPermissionDto>> GetMenuAccessByRoleIdAsync(int? RoleId)
        {
            return (await _repository.QueryAsync<MenuAccessPermissionDto>("SELECT * FROM MenuAccessPermission WHERE RoleId=@RoleId", new { RoleId }));
        }

        public async Task<Response> AddMenuAccess(int ? RoleId,IEnumerable<MenuAccessPermissionDto> dtos)
        {
            var result = new Response();
            try
            {
                int data = 0;
                var role = (await GetRoleById(RoleId));
                if (role.Status != true)
                {
                    result.messageType = "error";
                    result.message = "Menu assign only active roles";
                    return result;
                }
                var list = await GetMenuAccessByRoleIdAsync(RoleId);
                foreach (var d in dtos)
                {
                    var entity = d.ToEntity();
                    var dto = list.Where(a=>a.MenuAccessPermissionId== d.MenuAccessPermissionId).FirstOrDefault();
                    if (dto is null)
                        data = await _menuAccessRepository.InsertAsync(entity, _sql.conn, _sql.trans);
                    else
                    {
                        entity.MenuAccessPermissionId = dto.MenuAccessPermissionId;
                        data = await _menuAccessRepository.UpdateAsync(entity, _sql.conn, _sql.trans);
                    }
                }
                result = _messageClass.SaveMessage(data);
                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return result;
        }

    }
}