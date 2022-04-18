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
    public interface IShareTypesService
    {
        Task<IEnumerable<ShareTypesDto>> GetShareTypesAsync();
        Task<ShareTypesDto> GetShareTypesByIdAsyc(int? id);
        Task<Response> Insert(ShareTypesDto dto);
        Task<Response> Update(ShareTypesDto dto);
        Task<Response> Delete(int? id);
    }

    public class ShareTypesService : IShareTypesService
    {
        private readonly Repository<ShareTypes> _repository;
        private readonly MessageClass _messageClass;
        public ShareTypesService()
        {
            _repository = new Repository<ShareTypes>();
            _messageClass = new MessageClass();
        }

        public async Task<IEnumerable<ShareTypesDto>> GetShareTypesAsync()
        {
            var obj = (await _repository.QueryAsync<ShareTypesDto>("SELECT * FROM [dbo].[ShareTypeView] WHERE Status=1"));
            return obj;
        }

        public async Task<ShareTypesDto> GetShareTypesByIdAsyc()
        {
            var obj = (await _repository.QueryAsync<ShareTypesDto>("SELECT * FROM [dbo].[ShareTypeView]"));
            if (obj?.Count() > 0)
            {
                return obj.FirstOrDefault();
            }
            return null;
        }

        public async Task<ShareTypesDto> GetShareTypesByIdAsyc(int? id)
        {
            var obj = (await _repository.QueryAsync<ShareTypesDto>("SELECT * FROM [dbo].[ShareTypes] WHERE ShareTypeId=@id", new { id }));
            if (obj?.Count() > 0)
            {
                return obj.FirstOrDefault();
            }
            return null;
        }


        public async Task<Response> Insert(ShareTypesDto dto)
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

        public async Task<Response> Update(ShareTypesDto dto)
        {
            var result = new Response();
            try
            {
                var obj = await GetShareTypesByIdAsyc(dto.ShareTypeId);
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
                var obj = await GetShareTypesByIdAsyc(id);
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