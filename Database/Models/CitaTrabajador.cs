namespace Database.Models;

public partial class CitaTrabajador
{
    
    public int PlacaAuto { get; set; }
    public DateTime FechaCita { get; set; }
    public string Sucursal { get; set; }
    public int Cedula { get; set; }
    
    //public virtual Citum Citum { get; set; } = null!;
    
    //public virtual Trabajador CedulaNavigation { get; set; }
    
    
}