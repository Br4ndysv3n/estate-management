using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Common.Models.Person;
using Common.Models.Versorger;

namespace Common.Services
{
    public class DbService
    {
        private PersonService personService = new PersonService();
        private VersorgerService versorgerService = new VersorgerService();

        public List<IStammdaten> GetStammdaten(EnumStammdatenTyp stammdatenTyp)
        {
            var list = new List<IStammdaten>();

            if (stammdatenTyp == EnumStammdatenTyp.NaN)
            {
                list.AddRange(GetStammdaten(EnumStammdatenTyp.PERSON));
                list.AddRange(GetStammdaten(EnumStammdatenTyp.OBJEKT));
                list.AddRange(GetStammdaten(EnumStammdatenTyp.VERSORGER_TELEKOM));
                list.AddRange(GetStammdaten(EnumStammdatenTyp.VERSORGER_ENERGIE));
                list.AddRange(GetStammdaten(EnumStammdatenTyp.VERSORGER_WASSER));
            }
            else
                switch (stammdatenTyp)
                {
                    case EnumStammdatenTyp.PERSON:
                        list.AddRange(personService.GetPersons());
                        break;
                    case EnumStammdatenTyp.OBJEKT:
                        // ToDo
                        break;
                    case EnumStammdatenTyp.VERSORGER_ENERGIE:
                        list.AddRange(versorgerService.GetAllByVersorgertyp(EnumStammdatenTyp.VERSORGER_ENERGIE));
                        break;
                    case EnumStammdatenTyp.VERSORGER_WASSER:
                        list.AddRange(versorgerService.GetAllByVersorgertyp(EnumStammdatenTyp.VERSORGER_WASSER));
                        break;
                    case EnumStammdatenTyp.VERSORGER_TELEKOM:
                        list.AddRange(versorgerService.GetAllByVersorgertyp(EnumStammdatenTyp.VERSORGER_TELEKOM));
                        break;
                }

            return list;
        }

        public IStammdaten Save(IStammdaten stammdaten)
        {
            IStammdaten value = stammdaten;

            switch (stammdaten.StammdatenTyp)
            {
                case EnumStammdatenTyp.PERSON:
                    value = personService.InsertOrUpdate(stammdaten as IPerson);
                    break;
                case EnumStammdatenTyp.OBJEKT:
                    // ToDo
                    break;
                case EnumStammdatenTyp.VERSORGER_TELEKOM:
                case EnumStammdatenTyp.VERSORGER_ENERGIE:
                case EnumStammdatenTyp.VERSORGER_WASSER:
                    value = versorgerService.InsertOrUpdate(stammdaten as IVersorger);
                    break;
            }

            return value;
        }
    }
}
