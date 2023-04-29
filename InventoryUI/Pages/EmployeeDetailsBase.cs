using Microsoft.AspNetCore.Components;
using InventoryUI.Data;
namespace InventoryUI.Pages
{
    public class EmployeeDetailsBase :ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeServices{ get; set; }
        public IEnumerable<EmployeeModel> employeeList;
        public EmployeeModel employee = new EmployeeModel();
        protected override async Task OnInitializedAsync()
        {
            await this.GetSalesDetails();
        }

        protected async Task GetSalesDetails()
        {
            employeeList = await EmployeeServices.GetEmployees();
        }
        protected async Task InsertEmployee()
        {
            await EmployeeServices.SaveEmployeeDetails(employee);
            await this.GetSalesDetails();
            this.ClearAll();
        }
        public void ClearAll()
        {
            employee.empid = string.Empty;
            employee.name = string.Empty;
            employee.sal = string.Empty;
            employee.deptno = string.Empty;
        }
    }
}
