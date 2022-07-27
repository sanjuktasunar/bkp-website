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
    public interface IFiscalYearService
    {
        Task<IEnumerable<FiscalYearDto>> GetAllFiscalYearAsync();
        Task<FiscalYearDto> GetFiscalYearByIdAsync(int? id);
        Task<Response> InsertAsync(FiscalYearDto dto);
        Task<Response> UpdateAsync(FiscalYearDto dto);
        Task<Response> DeleteAsync(int? id);
    }

    public class FiscalYearService : IFiscalYearService
    {
        private readonly Repository<FiscalYear> _repository;
        private readonly MessageClass _messageClass;
        private SqlConnectionDetails _sql;
        private DateSettings dateSettings;
        public FiscalYearService()
        {
            _repository = new Repository<FiscalYear>();
            _messageClass = new MessageClass();
            _sql = _repository.GetSqlTransactionDetails();
            dateSettings = new DateSettings();
        }

        public async Task<IEnumerable<FiscalYearDto>> GetAllFiscalYearAsync()
        {
            var obj= (await _repository.QueryAsync<FiscalYearDto>("SELECT * FROM dbo.[FiscalYear]"));
            return obj;
        }

        public async Task<FiscalYearDto> GetFiscalYearByIdAsync(int? id)
        {
            var obj = (await _repository.QueryAsync<FiscalYearDto>("SELECT * FROM FiscalYear WHERE FiscalYearId=@id", new { id })).FirstOrDefault();
            return obj;
        }

        public async Task<Response> InsertAsync(FiscalYearDto dto)
        {
            var result = new Response();
            try
            {
                dto.StartDateAD = Convert.ToDateTime(dateSettings.ConvertToEnglishDate(dto.StartDateBS));
                dto.EndDateAD = Convert.ToDateTime(dateSettings.ConvertToEnglishDate(dto.EndDateBS));
                var entity = dto.ToEntity();
                if (entity.IsCurrent == true)
                    await MakeIsCurrentFalse();

                int FiscalYearId = await _repository.InsertAsync(entity,_sql.conn,_sql.trans);
                result = _messageClass.SaveMessage(FiscalYearId);
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

        public async Task<Response> UpdateAsync(FiscalYearDto dto)
        {
            var result = new Response();
            try
            {
                var obj = await GetFiscalYearByIdAsync(dto.FiscalYearId);
                if (obj == null)
                {
                    result = _messageClass.NotFoundMessage();
                }
                else
                {
                    var entity = dto.ToEntity();
                    if (dto.IsCurrent == true)
                        await MakeIsCurrentFalse();

                    int data = await _repository.UpdateAsync(entity);
                    result = _messageClass.SaveMessage(data);
                    _sql.trans.Commit();
                }
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return result;
        }

        public async Task<Response> DeleteAsync(int? id)
        {
            var result = new Response();
            try
            {
                var obj = await GetFiscalYearByIdAsync(id);
                if (obj == null)
                {
                    result = _messageClass.NotFoundMessage();
                }
                else
                {
                    if (obj.IsCurrent == true)
                    {
                        await _repository.ExecuteQueryAsync("update dbo.[FiscalYear] " +
                            "set IsCurrent=1 " +
                            "where FiscalYearId=" +
                            "(select top 1 FiscalYearId from " +
                            "dbo.FiscalYear order by EndDateAD desc)");
                    }
                    int data = await _repository.DeleteAsync(id);
                    result = _messageClass.DeleteMessage(data);
                    _sql.trans.Commit();
                }
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return result;
        }

        public async Task MakeIsCurrentFalse()
        {
            await _repository.ExecuteQueryAsync("update dbo.[FiscalYear] set " +
                       "IsCurrent=0 where IsCurrent=1", null, _sql.conn, _sql.trans);
        }
    }
}