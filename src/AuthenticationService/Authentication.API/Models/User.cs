using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.API.Models;

[Table("Users")] // ← Add this attribute
public class User
{
    public int id { get; set; }
    public string username { get; set; }
    public string password_hash { get; set; }
    public string email { get; set; }
    public string firstname { get; set; }
    public string lastname { get; set; }
    public string last_ip { get; set; }
    public DateTime? last_login { get; set; }
    public DateTime created_at { get; set; }
    public string device_type { get; set; }
    public bool is_admin { get; set; }
    public bool is_employee { get; set; }
}