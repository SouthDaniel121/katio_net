namespace katio.Data.Models
{
    public class User : BaseEntity<int>
    {
     public string Name { get; set; } = string.Empty;
     public string LastName { get; set; } = string.Empty;
     public string Email { get; set; } = string.Empty;        
     public string Telefono { get; set; } = string.Empty;

     public string Identificacion { get; set; } = string.Empty;
        
    public string Password { get; set; } = string.Empty;

    public string img {get; set; } = string.Empty;
    
    }
}
