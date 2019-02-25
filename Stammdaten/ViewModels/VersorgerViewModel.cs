using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Common.Models.Versorger;
using GalaSoft.MvvmLight;
using Prism.Mvvm;

namespace Stammdaten.ViewModels
{
    public class VersorgerViewModel : StammdatenItemViewModelBase
    {
        private readonly EnumStammdatenTyp stammdatenTyp;

        public VersorgerViewModel(IVersorger versorger)
        {
            Model = versorger;
            stammdatenTyp = versorger.StammdatenTyp;
        }

        public IVersorger Model { get; }

        public override EnumStammdatenTyp StammdatenTyp => stammdatenTyp;

        public override int Id => Model.Id;

        public string Name
        {
            get => Model.Name;
            set
            {
                if (value != Model.Name)
                {
                    Model.Name = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => DisplayString);
                }
            }
        }

        public string Strasse
        {
            get => Model.Strasse;
            set
            {
                if (value != Model.Strasse)
                {
                    Model.Strasse = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => DisplayString);
                }
            }
        }

        public string Hausnummer
        {
            get => Model.Hausnummer;
            set
            {
                if (value != Model.Hausnummer)
                {
                    Model.Hausnummer = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => DisplayString);
                }
            }
        }

        public string Plz
        {
            get => Model.Plz;
            set
            {
                if (value != Model.Plz)
                {
                    Model.Plz = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => DisplayString);
                }
            }
        }

        public string Ort
        {
            get => Model.Ort;
            set
            {
                if (value != Model.Ort)
                {
                    Model.Ort = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => DisplayString);
                }
            }
        }

        public override string DisplayString => $"{Name}, {Ort}";
    }
}
