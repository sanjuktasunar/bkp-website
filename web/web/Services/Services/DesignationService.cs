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
    public interface IDesignationService
    {
        Task<IEnumerable<DesignationDto>> GetAllDesignation();
        Task<DesignationDto> GetDesignationById(int? id);
        Task<Response> Insert(DesignationDto dto);
        Task<Response> Update(DesignationDto dto);
        Task<Response> Delete(int? id);
    }

    public class DesignationService : IDesignationService
    {
        private readonly Repository<Designation> _repository;
        private readonly MessageClass _messageClass;
        public DesignationService()
        {
            _repository = new Repository<Designation>();
            _messageClass = new MessageClass();
        }

        public async Task<IEnumerable<DesignationDto>> GetAllDesignation()
        {
            var obj = (await _repository.QueryAsync<DesignationDto>("SELECT * FROM Designation"));
            return obj;
        }

        public async Task<DesignationDto> GetDesignationById(int? id)
        {
            var obj = (await _repository.QueryAsync<DesignationDto>("SELECT * FROM Designation WHERE DesignationId=@id", new { id })).FirstOrDefault();
            return obj;
        }

        public async Task<Response> Insert(DesignationDto dto)
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

        public async Task<Response> Update(DesignationDto dto)
        {
            var result = new Response();
            try
            {
                var obj = await GetDesignationById(dto.DesignationId);
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
                var obj = await GetDesignationById(id);
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