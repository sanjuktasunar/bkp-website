using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web.Web.Entity.Infrastructure;

namespace web.Web.Services
{
    public interface IRepository<TModel> where TModel : class, new()
    {

    }

    public class Repository<TModel> : IRepository<TModel> where TModel : class, new()
    {
        string con;
        int WaitingTime;
        public Repository()
        {
            con = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            WaitingTime = 30;
            //WaitingTime = Convert.ToInt32(ConfigurationManager.AppSettings["Waiting.Time"].ToString());
        }

        public SqlConnectionDetails GetSqlTransactionDetails()
        {
            var connectionDetails = new SqlConnectionDetails();
            connectionDetails.conn = new SqlConnection(con);
            if (connectionDetails.conn.State == ConnectionState.Closed)
                connectionDetails.conn.Open();

            connectionDetails.trans = connectionDetails.conn.BeginTransaction();
            return connectionDetails;
        }

        public IEnumerable<TEntity> StoredProcedure<TEntity>(string sql, object param = null)
        {
            using (var db = new SqlConnection(con))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Query<TEntity>(sql, param, commandType: CommandType.StoredProcedure);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public IEnumerable<TEntity> Query<TEntity>(string sql, object param = null)
        {
            using (var db = new SqlConnection(con))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Query<TEntity>(sql, param, commandType: CommandType.Text);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public async Task<IEnumerable<TEntity>> StoredProcedureAsync<TEntity>(string sql, object param = null)
        {
            using (var db = new SqlConnection(con))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    var obj = await db.QueryAsync<TEntity>(sql, param, commandType: CommandType.StoredProcedure);
                    return obj;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object param = null)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return await db.QueryAsync<TEntity>(sql, param);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public async Task<int> InsertAsync(TModel obj, SqlConnection conn = null, IDbTransaction transaction = null)
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(con);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return await conn.InsertAsync(obj, transaction);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int Insert(TModel obj, SqlConnection conn = null, IDbTransaction transaction = null)
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(con);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return  conn.Insert(obj, transaction);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateAsync(TModel obj, SqlConnection conn = null, IDbTransaction transaction = null)
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(con);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                bool result = await conn.UpdateAsync(obj, transaction);
                if (result == true)
                    return 0;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(TModel obj, SqlConnection conn = null, IDbTransaction transaction = null)
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(con);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                bool result = conn.Update(obj, transaction);
                if (result == true)
                    return 0;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteAsync(object id, SqlConnection conn = null, IDbTransaction transaction = null)
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(con);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                bool result = await conn.DeleteAsync(Get(id), transaction, WaitingTime);
                if (result == true)
                    return 0;
                else
                    return -1;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<int> ExecuteQueryAsync(string sql, object param = null, SqlConnection conn = null, IDbTransaction transaction = null)
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(con);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var result = await conn.ExecuteAsync(sql, param, transaction, WaitingTime, commandType: CommandType.Text);
                return result;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<int> ExecuteStoreProcedureAsync(string sql, object param = null, SqlConnection conn = null, IDbTransaction transaction = null)
        {
            using (var db = new SqlConnection(con))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    var result = await db.ExecuteAsync(sql, param, transaction, WaitingTime, CommandType.StoredProcedure);
                    return result;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public TModel Get(object id)
        {
            using (var db = new SqlConnection(con))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    return db.Get<TModel>(id);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public int UserIdentity()
        {
            int UserId = 0;
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                UserId = Convert.ToInt32(HttpContext.Current.User.Identity.Name.ToString());
            }
            return UserId;
        }
    }
}