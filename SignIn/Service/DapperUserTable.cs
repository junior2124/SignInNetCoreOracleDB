using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using SignIn.Data;

namespace SignIn.Service
{
    public class DapperUserTable
    {
        private readonly ApplictionDBContext _connection;
        public DapperUserTable(ApplictionDBContext connection)
        {
            _connection = connection;
        }
        
        #region createuser
        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            string sql = "INSERT INTO dbo.CustomUser " +
                "VALUES (@id, @Email, @EmailConfirmed, @PasswordHash, @UserName)";

            var CURSOR = new OracleParameter("P_RECORDSET", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            var P_DEPTID = new OracleParameter("P_DEPTID", OracleDbType.Int32).Value = "0";
            var P_NUM_RESULTS = new OracleParameter("P_NUMRESULTS", OracleDbType.Int32).Value = "10";
            int rows = await _connection.Database.ExecuteSqlCommandAsync("BEGIN S_API_GETEMPLOYEES({0}, {1}, :P_RECORDSET); END;", new object[] { P_DEPTID, P_NUM_RESULTS, CURSOR });

            var P_CUR = new OracleParameter("curParam", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            var refCur = _connection.CompanyAddress.FromSql("BEGIN GET_COMPADDR_PROC(:curParam); END;", new object[] { P_CUR }).ToList();

            if (rows > 0)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." });            
        }
        #endregion

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            return IdentityResult.Success; ;
            //    string sql = "DELETE FROM dbo.CustomUser WHERE Id = @Id";
            //    int rows = await _connection.ExecuteAsync(sql, new { user.Id });

            //    if (rows > 0)
            //    {
            //        return IdentityResult.Success;
            //    }
            //    return IdentityResult.Failed(new IdentityError { Description = $"Could not delete user {user.Email}." });
        }


        public async Task<ApplicationUser> FindByIdAsync(Guid userId)
        {
            //    string sql = "SELECT * " +
            //                "FROM dbo.CustomUsers " +
            //                "WHERE Id = @Id;";

            //    return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new
            //    {
            //        Id = userId
            //    });
            return null;
        }


        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            //    string sql = "SELECT * " +
            //                "FROM dbo.CustomUser " +
            //                "WHERE UserName = @UserName;";

            //    return await _connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new
            //    {
            //        UserName = userName
            //    });
            return null;
        }
    }
}
