using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web.Utility;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Entity.Infrastructure;
using Web.Services.Mapping;

namespace web.Web.Services.Services
{
    public interface IUsersService
    {
        Task<UsersDto> GetLoginUser(UsersDto dto);
        Task<UsersDto> GetUserById(int id);
        Task<IEnumerable<UsersDto>> GetUsersList();
        Task<Response> Insert(UsersDto dto);
        Task<Response> Update(UsersDto dto);
        Task<Response> ChangePassword(UsersDto dto);
    }

    public class UsersService : IUsersService
    {
        private readonly Repository<Users> _repository;
        private readonly Repository<PhotoStorages> _photoStorageRepository;
        private readonly Security _security;
        private readonly MessageClass _messageClass;
        public UsersService()
        {
            _repository = new Repository<Users>();
            _security = new Security();
            _photoStorageRepository = new Repository<PhotoStorages>();
            _messageClass = new MessageClass();
        }
        public async Task<UsersDto> GetLoginUser(UsersDto dto)
        {
            string EncUserName = _security.EncryptText(dto.UserName);
            string EncPassword = _security.EncryptText(dto.Password);

            //string du = _security.DecryptText("XX96H12akoL2okcC7Ur1ow==");
            //string dp = _security.DecryptText("OHzTHRAngCB2vlRNc5g9Ig==");

            string sql = "select * from dbo.[UsersView] where UserName=@UserName and Password=@Password";
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

            user.DescryptUserName = _security.DecryptText(user.UserName);
            return user;
        }

        public async Task<IEnumerable<UsersDto>> GetUsersList()
        {
            string sql = "select * from dbo.[UsersView]";
            var userList = await _repository.QueryAsync<UsersDto>(sql);
            userList.ToList()
                .ForEach(a => a.DescryptUserName = _security.DecryptText(a.UserName));

            return userList;
        }

        public async Task<Response> Insert(UsersDto dto)
        {
            var result = new Response();
            result.messageType = "error";
            var _sql = _repository.GetSqlTransactionDetails();
            try
            {
                var photo = new PhotoStorages();
                int PhotoStorageId = await _photoStorageRepository.InsertAsync(photo, _sql.conn, _sql.trans);

                dto.PhotoStorageId = PhotoStorageId;
                dto.UserName = _security.EncryptText(dto.UserName);
                dto.Password = _security.EncryptText(dto.Password);
                dto.CreatedDate = DateTime.Now;
                dto.CreatedBy = _repository.UserIdentity();

                int userId = await _repository.InsertAsync(dto.ToEntity(), _sql.conn, _sql.trans);

                result = _messageClass.SaveMessage(userId);
                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                result.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return result;
        }

        public async Task<Response> Update(UsersDto dto)
        {
            var result = new Response();
            result.messageType = "error";
            try
            {

                var usersDto = await GetUserById(dto.UserId);
                if (usersDto == null)
                    return _messageClass.NotFoundMessage();

                usersDto.RoleId = dto.RoleId;
                usersDto.UserTypeId = dto.UserTypeId;
                usersDto.FullName = dto.FullName;
                usersDto.UserName = _security.EncryptText(dto.UserName);
                usersDto.ContactNumber = dto.ContactNumber;
                usersDto.EmailAddress = dto.EmailAddress;
                usersDto.UserStatusId = dto.UserStatusId;
                usersDto.UpdatedDate = DateTime.Now;
                usersDto.UpdatedBy = _repository.UserIdentity();

                int resp = await _repository.UpdateAsync(usersDto.ToEntity());
                result = _messageClass.SaveMessage(resp);
            }
            catch (SqlException ex)
            {
                result.message = ex.Message.ToString();
            }
            return result;
        }

        public async Task<Response> ChangePassword(UsersDto dto)
        {
            var result = new Response();
            result.messageType = "error";
            try
            {
                var usersDto = await GetUserById(dto.UserId);
                if (usersDto == null)
                    return _messageClass.NotFoundMessage();

                if (dto.Password != dto.ConfirmPassword)
                {
                    result.message = "Password do not match !!!! ";
                    return result;
                }

                usersDto.Password = _security.EncryptText(dto.Password);
                int resp = await _repository.UpdateAsync(usersDto.ToEntity());
                result = _messageClass.SaveMessage(resp);
            }
            catch (SqlException ex)
            {
                result.message = ex.Message.ToString();
            }
            return result;
        }
    }
}