namespace Common.Models.Versorger
{
    public abstract class VersorgerBase : IVersorger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Strasse { get; set; }
        public string Hausnummer { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public abstract EnumStammdatenTyp StammdatenTyp { get; }
    }
}
