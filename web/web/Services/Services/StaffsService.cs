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
    public interface IStaffsService
    {
        Task<IEnumerable<StaffsDto>> GetStaffListAsync();
        Task<StaffsDto> GetStaffByIdAsync(int? id);
        Task<Response> Insert(StaffsDto dto);
        Task<Response> Update(StaffsDto dto);
        StaffsDto DropDownMethods(StaffsDto dto);
    }
    public class StaffsService : IStaffsService
    {
        private readonly Repository<Staffs> _repository;
        private readonly Repository<Users> _usersRepository;
        private readonly Repository<PhotoStorages> _photoStorageRepository;
        private readonly MessageClass _messageClass;
        private readonly SqlConnectionDetails _sql;
        private readonly Security security;
        private readonly IDropDownService _dropDownService;
        public StaffsService(IDropDownService dropDownService)
        {
            _repository = new Repository<Staffs>();
            _usersRepository = new Repository<Users>();
            _photoStorageRepository = new Repository<PhotoStorages>();
            _messageClass = new MessageClass();
            _sql = _repository.GetSqlTransactionDetails();
            security = new Security();
            _dropDownService = dropDownService;
        }

        public async Task<IEnumerable<StaffsDto>> GetStaffListAsync()
        {
            var obj= await _repository.QueryAsync<StaffsDto>("SELECT * FROM StaffsView");
            return obj;
        }

        public async Task<StaffsDto> GetStaffByIdAsync(int? id)
        {
            var dto = (await _repository.QueryAsync<StaffsDto>("SELECT * FROM StaffsView WHERE StaffId=@id", new { id })).FirstOrDefault();
            if (dto != null)
            {
                dto.UserName = security.DecryptText(dto.UserName);
            }
            return dto;
        }

        public async Task<Response> Insert(StaffsDto dto)
        {
            var result = new Response();
            string EncryptUsername = "", EncryptPassword = "";
            try
            {
                EncryptUsername = security.EncryptText(dto.UserName);
                EncryptPassword = security.EncryptText(dto.Password);
                var photo = new PhotoStorages();
                int PhotoStorageId = await _photoStorageRepository.InsertAsync(photo, _sql.conn, _sql.trans);

                var user = dto.ToUserEntity();
                user.PhotoStorageId = PhotoStorageId;
                user.UserName = EncryptUsername;
                user.Password = EncryptPassword;
                user.CreatedDate = DateTime.Now;
                user.CreatedBy = _repository.UserIdentity();
                int userId = await _usersRepository.InsertAsync(user, _sql.conn, _sql.trans);

                var staff = dto.ToEntity();
                staff.UserId = userId;
                int staffId = await _repository.InsertAsync(staff, _sql.conn, _sql.trans);
                result = _messageClass.SaveMessage(staffId);
                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
            }
            return result;
        }

        public async Task<Response> Update(StaffsDto dto)
        {
            var result = new Response();
            try
            {
                var staff = await GetStaffByIdAsync(dto.StaffId);
                if (staff == null)
                    return null;
                var user = await GetUserByIdAsync(staff.UserId);
                user.UserName = security.EncryptText(dto.UserName);
                user.EmailAddress = dto.EmailAddress;
                user.ContactNumber = dto.ContactNumber;
                user.UserStatusId = dto.UserStatusId;
                user.UpdatedBy = _repository.UserIdentity();
                user.UpdatedDate = DateTime.Now;
                await _usersRepository.UpdateAsync(user.ToEntity(), _sql.conn, _sql.trans);

                var staffEntity = dto.ToEntity();
                staffEntity.UserId = user.UserId;
                int staffId = await _repository.UpdateAsync(staffEntity, _sql.conn, _sql.trans);
                result = _messageClass.SaveMessage(staffId);
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

        public async Task<UsersDto> GetUserByIdAsync(int id)
        {
            var user = (await _repository.QueryAsync<UsersDto>("SELECT * FROM Users WHERE UserId=@id", new { id })).FirstOrDefault();
            return user;
        }

        public StaffsDto DropDownMethods(StaffsDto dto)
        {
            if (dto is null)
                dto = new StaffsDto();

            dto.Roles =  _dropDownService.GetDropDownRole();
            dto.Designations =  _dropDownService.GetDropDownDesignation();
            dto.Departments = _dropDownService.GetDropDownDepartment();
            dto.Genders = _dropDownService.GetDropDownGender();
            dto.UserStatus = _dropDownService.GetDropDownUserStatus();
            return dto;
        }
    }
}