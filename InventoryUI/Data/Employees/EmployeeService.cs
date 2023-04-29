using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace InventoryUI.Data
{
    public class EmployeeService : IEmployeeService
    {
        public IConfiguration _configuration { get; }
        public string _connectionString { get; }

        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DevDB");
        }

        //public async Task<bool> DeleteSales(int id)
        //{
        //    var parameters = new DynamicParameters();
        //    parameters.Add("SalesId", id, DbType.Int32);

        //    using (var conn = new SqlConnection(_connectionString))
        //    {

        //        if (conn.State == ConnectionState.Closed)
        //            conn.Open();
        //        try
        //        {
        //            await conn.ExecuteAsync("DemoWorks.DeleteSales", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            if (conn.State == ConnectionState.Open)
        //                conn.Close();
        //        }
        //    }
        //    return true;
        //}

        public async Task<IEnumerable<EmployeeModel>> GetEmployees()
        {
            IEnumerable<EmployeeModel> employeeEntries;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    employeeEntries = await conn.QueryAsync<EmployeeModel>("[dbo].[Get_Employees]", commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return employeeEntries;
        }

        //public async Task<SalesDto> GetSalesById(int id)
        //{
        //    var parameters = new DynamicParameters();
        //    parameters.Add("SalesId", id, DbType.Int32);
        //    SalesDto sales = new SalesDto();

        //    using (var conn = new SqlConnection(_connectionString))
        //    {

        //        if (conn.State == ConnectionState.Closed)
        //            conn.Open();
        //        try
        //        {
        //            sales = await conn.QueryFirstOrDefaultAsync<SalesDto>("DemoWorks.GetSalesById", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            if (conn.State == ConnectionState.Open)
        //                conn.Close();
        //        }
        //    }
        //    return sales;
        //}

        public async Task<bool> SaveEmployeeDetails(EmployeeModel employee)
        {
            var parameters = new DynamicParameters();
            parameters.Add("empid", employee.empid, DbType.String);
            parameters.Add("name", employee.name, DbType.String);
            parameters.Add("sal", employee.sal, DbType.String);
            parameters.Add("deptno", employee.sal, DbType.String);

            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    if (employee.IsUpdate)
                    {
                        parameters.Add("SalesId", employee.empid, DbType.String);
                        await conn.ExecuteAsync("DemoWorks.UpdateSales", parameters, commandType: CommandType.StoredProcedure);
                    }
                    else
                        await conn.ExecuteAsync("[dbo].[Insert_Employee]", parameters, commandType: CommandType.StoredProcedure);

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return true;
        }
    }
}
