using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.API.Models;

[Table("Employees")]
public class Employee
{
    public int employee_id { get; set; }    
    public string first_name { get; set; }
    public string last_name { get; set; }   
    public string employee_email { get; set; }
    public bool is_active { get; set; }
    public bool  is_deleted { get; set; }   
    
}