using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web.Utility;
using Web.Entity.Dto;

namespace web.Web.Services.Services
{
    public interface IUsersService
    {
        Task<UsersDto> GetLoginUser(UsersDto dto);
        Task<UsersDto> GetUserById(int id);
    }

    public class UsersService : IUsersService
    {
        private readonly Repository<UsersDto> _repository;
        private readonly Security _security;
        public UsersService()
        {
            _repository = new Repository<UsersDto>();
            _security = new Security();
        }
        public async Task<UsersDto> GetLoginUser(UsersDto dto)
        {
            string EncUserName = _security.EncryptText(dto.UserName);
            string EncPassword = _security.EncryptText(dto.Password);

            string dusername = _security.DecryptText("XX96H12akoL2okcC7Ur1ow==");
            string dpw = _security.DecryptText("OHzTHRAngCB2vlRNc5g9Ig==");
            string sql = "select * from dbo.[Users] where UserName=@UserName and Password=@Password";
            var user = (await _repository.QueryAsync<UsersDto>(sql,new { UserName = EncUserName , Password=EncPassword })).FirstOrDefault();
            if (user == null)
                return null;
            return user;
        }

        public async Task<UsersDto> GetUserById(int id)
        {
          
            string sql = "select * from dbo.[Users] where UserId=@id";
            var user = (await _repository.QueryAsync<UsersDto>(sql, new { id })).FirstOrDefault();
            if (user == null)
                return null;
            return user;
        }
    }
}