namespace katio.Data.Models
{
    public class User : BaseEntity<int>
    {
     public string Nombre { get; set; } = string.Empty;
     public string Apellido { get; set; } = string.Empty;
     public string Email { get; set; } = string.Empty;        
     public string Telefono { get; set; } = string.Empty;

     public string Identificacion { get; set; } = string.Empty;
        
        public string Password { get; set; } = string.Empty;
        
     public string Username { get; set; } = string.Empty;
    }
}
