using System.ComponentModel.DataAnnotations;

namespace InventoryUI.Data
{
    public class EmployeeModel
    {
        public string  empid { get; set; }
        public string name { get; set; }
        public string sal { get; set; }
        public string deptno { get; set; }
        public bool IsUpdate { get; set; }
    }
}
