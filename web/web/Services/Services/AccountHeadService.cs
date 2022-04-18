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
    public interface IAccountHeadService
    {
        Task<IEnumerable<AccountHeadDto>> GetAllAccountHead();
        Task<AccountHeadDto> GetAccountHeadById(int? id);
        Task<Response> Insert(AccountHeadDto entity);
        Task<Response> Update(AccountHeadDto entity);
        Task<Response> Delete(int? id);
    }

    public class AccountHeadService: IAccountHeadService
    {
        private readonly Repository<AccountHead> _repository;
        private readonly MessageClass _messageClass;
        public AccountHeadService()
        {
            _repository = new Repository<AccountHead>();
            _messageClass = new MessageClass();
        }

        public async Task<IEnumerable<AccountHeadDto>> GetAllAccountHead()
        {
            var obj= (await _repository.QueryAsync<AccountHeadDto>("SELECT * FROM AccountHead"));
            return obj;
        }
        public async Task<AccountHeadDto> GetAccountHeadById(int? id)
        {
            var obj = (await _repository.QueryAsync<AccountHeadDto>("SELECT * FROM AccountHead WHERE AccountHeadId=@id", new { id })).FirstOrDefault();
            return obj;
        }

        public async Task<Response> Insert(AccountHeadDto dto)
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

        public async Task<Response> Update(AccountHeadDto dto)
        {
            var result = new Response();
            try
            {
                var obj = await GetAccountHeadById(dto.AccountHeadId);
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
                var obj = await GetAccountHeadById(id);
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