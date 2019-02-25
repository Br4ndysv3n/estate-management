namespace Common.Models.Versorger
{
    public interface IVersorger : IStammdaten
    {        
        int Id { get; set; }
        string Name { get; set; }
        string Strasse { get; set; }
        string Hausnummer { get; set; }
        string Plz { get; set; }
        string Ort { get; set; }
    }
}