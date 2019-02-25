namespace Common.Models.Versorger
{
    public class VersorgerFactory
    {
        public static IVersorger Create(int id, EnumStammdatenTyp versorgerTyp, string name, string strasse, string hausnummer, string plz, string ort)
        {
            VersorgerBase versorger = (VersorgerBase)CreateNew(versorgerTyp);

            versorger.Name = name;
            versorger.Strasse = strasse;
            versorger.Hausnummer = hausnummer;
            versorger.Plz = plz;
            versorger.Ort = ort;

            return versorger;
        }

        public static IVersorger CreateNew(EnumStammdatenTyp stammdatenTyp)
        {
            VersorgerBase versorger = null;

            switch (stammdatenTyp)
            {
                case EnumStammdatenTyp.VERSORGER_ENERGIE:
                    versorger = new Stromversorger();
                    break;
                case EnumStammdatenTyp.VERSORGER_WASSER:
                    versorger = new Wasserversorger();
                    break;
                case EnumStammdatenTyp.VERSORGER_TELEKOM:
                    versorger = new Telekomversorger();
                    break;
            }

            return versorger;
        }
    }
}
