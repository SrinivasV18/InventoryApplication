namespace InventoryUI.Data
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> GetEmployees();
        Task<bool> SaveEmployeeDetails(EmployeeModel employee);
        //Task<SalesDto> GetSalesById(int id);
        //Task<bool> DeleteSales(int id);
    }
}
