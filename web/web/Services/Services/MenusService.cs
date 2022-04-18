using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using web.Utility;
using web.Web.Services;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Entity.Infrastructure;
using Web.Services.Mapping;

namespace Web.Services.Services
{
    public interface IMenusService
    {
        Task<IEnumerable<MenusDto>> GetMenusAsync();
        Task<MenusDto> GetMenusByIdAsync(int? id);
        Task<Response> Insert(MenusDto dto);
        Task<Response> Update(MenusDto dto);
        Task<Response> Delete(int? id);
    }
    public class MenusService : IMenusService
    {
        private readonly Repository<Menus> _repository;
        private readonly MessageClass _messageClass;
        public MenusService()
        {
            _repository = new Repository<Menus>();
            _messageClass = new MessageClass();
        }

        public async Task<IEnumerable<MenusDto>> GetMenusAsync()
        {
            var result = await _repository.QueryAsync<MenusDto>("SELECT * from Menus");
            return result;
        }
        
        public async Task<MenusDto> GetMenusByIdAsync(int? id)
        {
            var menus = await _repository.QueryAsync<MenusDto>("SELECT * FROM Menus WHERE MenuId=@id",
                new { id });
            if (menus.Count() > 0)
                return menus.FirstOrDefault();
            return null;
        }
       
        public async Task<Response> Insert(MenusDto dto)
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

        public async Task<Response> Update(MenusDto dto)
        {
            var result = new Response();
            try
            {
                var obj = await GetMenusByIdAsync(dto.MenuId);
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
                var obj = await GetMenusByIdAsync(id);
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
