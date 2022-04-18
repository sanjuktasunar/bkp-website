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
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartment();
        Task<DepartmentDto> GetDepartmentById(int? id);
        Task<Response> Insert(DepartmentDto dto);
        Task<Response> Update(DepartmentDto dto);
        Task<Response> Delete(int? id);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly Repository<Department> _repository;
        private readonly MessageClass _messageClass;
        public DepartmentService()
        {
            _repository = new Repository<Department>();
            _messageClass = new MessageClass();
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartment()
        {
            var obj= (await _repository.QueryAsync<DepartmentDto>("SELECT * FROM Department"));
            return obj;
        }

        public async Task<DepartmentDto> GetDepartmentById(int? id)
        {
            var obj= (await _repository.QueryAsync<DepartmentDto>("SELECT * FROM Department WHERE DepartmentId=@id", new { id })).FirstOrDefault();
            return obj;
        }

        public async Task<Response> Insert(DepartmentDto dto)
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

        public async Task<Response> Update(DepartmentDto dto)
        {
            var result = new Response();
            try
            {
                var obj = await GetDepartmentById(dto.DepartmentId);
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
                var obj = await GetDepartmentById(id);
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
    }
}