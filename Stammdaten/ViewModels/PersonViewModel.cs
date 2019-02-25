using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Common.Models;
using Common.Models.Person;
using GalaSoft.MvvmLight;
using Prism.Mvvm;

namespace Stammdaten.ViewModels
{
    public class PersonViewModel : StammdatenItemViewModelBase
    {
        public PersonViewModel(IPerson person)
        {
            Model = person;
        }

        public IPerson Model { get; }

        public override EnumStammdatenTyp StammdatenTyp => EnumStammdatenTyp.PERSON;

        public override int Id => Model.Id;

        public string Titel
        {
            get => Model.Titel;
            set
            {
                Model.Titel = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => DisplayString);
            }
        }

        public EnumAnrede Anrede
        {
            get => Model.Anrede;
            set
            {
                Model.Anrede = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => DisplayString);
            }
        }

        public string Vorname
        {
            get => Model.Vorname;
            set
            {
                Model.Vorname = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => DisplayString);
            }
        }

        public string Nachname
        {
            get => Model.Nachname;
            set
            {
                Model.Nachname = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => DisplayString);
            }
        }

        public DateTime Geburtsdatum
        {
            get => Model.Geburtsdatum;
            set
            {
                Model.Geburtsdatum = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => DisplayString);
            }
        }

        public string Geburtsort
        {
            get => Model.Geburtsort;
            set
            {
                Model.Geburtsort = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => DisplayString);
            }
        }

        public override string DisplayString
        {
            get
            {
                var displayString = new StringBuilder();
                AppendNachname(displayString);
                AppendTitel(displayString);
                AppendVorname(displayString);

                return displayString.Length > 0 ? displayString.ToString() : "Neue Person";
            }
        }

        private void AppendNachname(StringBuilder displayString)
        {
            if (!string.IsNullOrEmpty(Nachname))
            {
                displayString.Append(Nachname);
            }
        }

        private void AppendTitel(StringBuilder displayString)
        {
            if (!string.IsNullOrEmpty(Titel))
            {
                if (displayString.Length > 0)
                {
                    displayString.Append(" ");
                }
                displayString.Append(Titel);
            }
        }

        private void AppendVorname(StringBuilder displayString)
        {
            if (!string.IsNullOrEmpty(Vorname))
            {
                if (displayString.Length > 0)
                {
                    displayString.Append(", ");
                }
                displayString.Append(Vorname);
            }
        }
    }
}
