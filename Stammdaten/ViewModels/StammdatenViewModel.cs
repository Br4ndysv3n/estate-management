using Common.Models;
using Common.Models.Person;
using Common.Models.Versorger;
using Common.Services;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Prism;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Stammdaten.ViewModels
{
    public class StammdatenViewModel : ViewModelBase
    {
        private ObservableCollection<IStammdatenItemViewModel> stammdatenListe;
        private List<IStammdatenItemViewModel> stammdatenListeInternal;
        private IStammdatenItemViewModel selectedItemViewModel;
        private IStammdatenItemViewModel previouslySelectedItemViewModel;
        private bool isAlleContext;
        private bool isPersonContext;
        private bool isObjektContext;
        private bool isWasserContext;
        private bool isEnergieContext;
        private bool isTelekomContext;
        private DbService dbService = new DbService();

        public StammdatenViewModel()
        {
            stammdatenListeInternal = new List<IStammdatenItemViewModel>();
            stammdatenListe = new ObservableCollection<IStammdatenItemViewModel>();

            RefreshStammdaten();
            /*
            stammdatenListeInternal = new List<IStammdatenItemViewModel>
            {
                new PersonViewModel(PersonFactory.CreatePerson("Dr.", EnumAnrede.Herr, "Max", "Mustermann", DateTime.Now, "Erfurt")),
                new PersonViewModel(PersonFactory.CreatePerson("", EnumAnrede.Frau, "Erika", "Mustermann", DateTime.Now, "Erfurt")),
                new VersorgerViewModel(VersorgerFactory.Create(EnumStammdatenTyp.VERSORGER_ENERGIE, 1,
                "E.ON Thüringer Energie", "Energiestr.", "1", "99099", "Erfurt")),
                new VersorgerViewModel(VersorgerFactory.Create(EnumStammdatenTyp.VERSORGER_WASSER, 2,
                "ThüringenWasser", "Wasserstr.", "11", "99099", "Erfurt")),
                new VersorgerViewModel(VersorgerFactory.Create(EnumStammdatenTyp.VERSORGER_TELEKOM, 3,
                "1&1 Internet AG", "Telestr.", "11", "12345", "Montabaur"))
            };            
            */

            cmdNew = new DelegateCommand(OnNeu, CanNeu);
            cmdEdit = new DelegateCommand(OnEdit, CanEdit);
            cmdSave = new DelegateCommand(OnSave, CanSave);
            cmdCancel = new DelegateCommand(OnCancel, CanCancel);

            IsAlleContext = true;
        }

        private void RefreshStammdaten()
        {
            stammdatenListeInternal.Clear();
            stammdatenListe.Clear();

            var context = SelectedItemViewModel == null ? EnumStammdatenTyp.NaN : SelectedItemViewModel.StammdatenTyp;
            foreach (var person in dbService.GetStammdaten(EnumStammdatenTyp.PERSON))
            {
                stammdatenListeInternal.Add(new PersonViewModel(person as IPerson));
            }
            foreach (var versorger in dbService.GetStammdaten(EnumStammdatenTyp.VERSORGER_TELEKOM))
            {
                stammdatenListeInternal.Add(new VersorgerViewModel(versorger as IVersorger));
            }
            foreach (var versorger in dbService.GetStammdaten(EnumStammdatenTyp.VERSORGER_ENERGIE))
            {
                stammdatenListeInternal.Add(new VersorgerViewModel(versorger as IVersorger));
            }
            foreach (var versorger in dbService.GetStammdaten(EnumStammdatenTyp.VERSORGER_WASSER))
            {
                stammdatenListeInternal.Add(new VersorgerViewModel(versorger as IVersorger));
            }

            FilterList(context);
        }

        #region Neu

        private readonly DelegateCommand cmdNew;
        public DelegateCommand CmdNew => cmdNew;

        private void OnNeu()
        {
            SetNewItem();
            SetEditMode(true);
        }

        private bool CanNeu()
        {
            var canNeu = !IsAlleContext;
            canNeu = !canNeu || SelectedItemViewModel == null || !SelectedItemViewModel.IsEditMode;

            return canNeu;
        }

        #endregion

        #region Bearbeiten

        private readonly DelegateCommand cmdEdit;
        public DelegateCommand CmdEdit => cmdEdit;

        private void OnEdit()
        {
            StoreSelectedItem();
            SetEditMode(true);
        }

        private bool CanEdit()
        {
            return SelectedItemViewModel != null && !SelectedItemViewModel.IsEditMode;
        }

        #endregion

        #region Speichern

        private readonly DelegateCommand cmdSave;
        public DelegateCommand CmdSave => cmdSave;

        private void OnSave()
        {
            // Neues Objekt mit den eingegebenen Daten anlegen oder dieses Objekt speichern
            SetEditMode(false);

            IStammdaten model = null;

            if (SelectedItemViewModel.StammdatenTyp == EnumStammdatenTyp.PERSON)
            {
                model = dbService.Save(((PersonViewModel)SelectedItemViewModel).Model);
            }
            if (SelectedItemViewModel.StammdatenTyp == EnumStammdatenTyp.VERSORGER_TELEKOM 
                || SelectedItemViewModel.StammdatenTyp == EnumStammdatenTyp.VERSORGER_WASSER 
                || SelectedItemViewModel.StammdatenTyp == EnumStammdatenTyp.VERSORGER_ENERGIE)
            {
                model = dbService.Save(((VersorgerViewModel)SelectedItemViewModel).Model);
            }

            RefreshStammdaten();

            if (model != null)
            {
                SelectedItemViewModel = stammdatenListeInternal.FirstOrDefault(s => s.Id == model.Id);
            }
        }

        private bool CanSave()
        {
            return SelectedItemViewModel != null && SelectedItemViewModel.IsEditMode;
        }

        #endregion

        #region Abbrechen

        private readonly DelegateCommand cmdCancel;
        public DelegateCommand CmdCancel => cmdCancel;

        private void OnCancel()
        {
            // Bearbeitung abbrechen
            SetEditMode(false);

            RefreshStammdaten();
            RestoreSelectedItem();
        }

        private bool CanCancel()
        {
            return SelectedItemViewModel != null && SelectedItemViewModel.IsEditMode;
        }

        #endregion Abbrechen

        private void SetEditMode(bool editMode)
        {
            SelectedItemViewModel.IsEditMode = editMode;

            CmdEdit.RaiseCanExecuteChanged();
            CmdSave.RaiseCanExecuteChanged();
            CmdCancel.RaiseCanExecuteChanged();
        }

        public ObservableCollection<IStammdatenItemViewModel> StammdatenListe
        {
            get { return stammdatenListe; }
        }

        public IStammdatenItemViewModel SelectedItemViewModel
        {
            get { return selectedItemViewModel; }
            set
            {
                if (value != selectedItemViewModel)
                {
                    selectedItemViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsAlleContext
        {
            get => isAlleContext;
            set
            {
                isAlleContext = value;
                if (value)
                {
                    DeselectContextsButThisOne(EnumStammdatenTyp.NaN);
                    FilterList(EnumStammdatenTyp.NaN);
                    UpdateCommands();
                }
                RaisePropertyChanged();
            }
        }

        public bool IsPersonContext
        {
            get => isPersonContext;
            set
            {
                isPersonContext = value;
                if (value)
                {
                    DeselectContextsButThisOne(EnumStammdatenTyp.PERSON);
                    FilterList(EnumStammdatenTyp.PERSON);
                    UpdateCommands();
                }
                RaisePropertyChanged();
            }
        }

        public bool IsObjektContext
        {
            get => isObjektContext;
            set
            {
                isObjektContext = value;
                if (value)
                {
                    DeselectContextsButThisOne(EnumStammdatenTyp.OBJEKT);
                    FilterList(EnumStammdatenTyp.OBJEKT);
                    UpdateCommands();
                }
                RaisePropertyChanged();
            }
        }

        public bool IsWasserContext
        {
            get => isWasserContext;
            set
            {
                isWasserContext = value;
                if (value)
                {
                    DeselectContextsButThisOne(EnumStammdatenTyp.VERSORGER_WASSER);
                    FilterList(EnumStammdatenTyp.VERSORGER_WASSER);
                    UpdateCommands();
                }
                RaisePropertyChanged();
            }
        }

        public bool IsEnergieContext
        {
            get => isEnergieContext;
            set
            {
                isEnergieContext = value;
                if (value)
                {
                    DeselectContextsButThisOne(EnumStammdatenTyp.VERSORGER_ENERGIE);
                    FilterList(EnumStammdatenTyp.VERSORGER_ENERGIE);
                    UpdateCommands();
                }
                RaisePropertyChanged();
            }
        }

        public bool IsTelekomContext
        {
            get => isTelekomContext;
            set
            {
                isTelekomContext = value;
                if (value)
                {
                    DeselectContextsButThisOne(EnumStammdatenTyp.VERSORGER_TELEKOM);
                    FilterList(EnumStammdatenTyp.VERSORGER_TELEKOM);
                    UpdateCommands();
                }
                RaisePropertyChanged();
            }
        }

        private void DeselectContextsButThisOne(EnumStammdatenTyp thisOne)
        {
            switch (thisOne)
            {
                case EnumStammdatenTyp.NaN:
                    IsPersonContext = false;
                    IsObjektContext = false;
                    IsEnergieContext = false;
                    IsWasserContext = false;
                    IsTelekomContext = false;
                    break;
                case EnumStammdatenTyp.PERSON:
                    IsAlleContext = false;
                    IsObjektContext = false;
                    IsWasserContext = false;
                    IsEnergieContext = false;
                    IsTelekomContext = false;
                    break;
                case EnumStammdatenTyp.OBJEKT:
                    IsAlleContext = false;
                    IsPersonContext = false;
                    IsWasserContext = false;
                    IsEnergieContext = false;
                    IsTelekomContext = false;
                    break;
                case EnumStammdatenTyp.VERSORGER_WASSER:
                    IsAlleContext = false;
                    IsPersonContext = false;
                    IsObjektContext = false;
                    IsEnergieContext = false;
                    IsTelekomContext = false;
                    break;
                case EnumStammdatenTyp.VERSORGER_ENERGIE:
                    IsAlleContext = false;
                    IsPersonContext = false;
                    IsObjektContext = false;
                    IsWasserContext = false;
                    IsTelekomContext = false;
                    break;
                case EnumStammdatenTyp.VERSORGER_TELEKOM:
                    IsAlleContext = false;
                    IsPersonContext = false;
                    IsObjektContext = false;
                    IsEnergieContext = false;
                    IsWasserContext = false;
                    break;
                default:
                    IsAlleContext = false;
                    IsPersonContext = false;
                    IsObjektContext = false;
                    IsObjektContext = false;
                    IsEnergieContext = false;
                    IsWasserContext = false;
                    break;
            }
        }

        private void FilterList(EnumStammdatenTyp filter)
        {
            stammdatenListe.Clear();
            if (filter == EnumStammdatenTyp.NaN)
            {
                stammdatenListe.AddRange(stammdatenListeInternal);
            }
            else
            {
                stammdatenListe.AddRange(stammdatenListeInternal.Where(item => item.StammdatenTyp == filter));
            }

            if (StammdatenListe.Any())
            {
                SelectedItemViewModel = StammdatenListe.FirstOrDefault();
            }
        }

        private void UpdateCommands()
        {
            CmdNew.RaiseCanExecuteChanged();
        }

        private void SetNewItem()
        {
            IStammdatenItemViewModel stammdatenNeu = null;
            if (isPersonContext)
            {
                stammdatenNeu = new PersonViewModel(PersonFactory.CreateNew());
            }
            else if (IsObjektContext)
            {

            }
            else if (IsEnergieContext)
            {
                stammdatenNeu = new VersorgerViewModel(VersorgerFactory.CreateNew(EnumStammdatenTyp.VERSORGER_ENERGIE));
            }
            else if (IsWasserContext)
            {
                stammdatenNeu = new VersorgerViewModel(VersorgerFactory.CreateNew(EnumStammdatenTyp.VERSORGER_WASSER));
            }
            else if (IsTelekomContext)
            {
                stammdatenNeu = new VersorgerViewModel(VersorgerFactory.CreateNew(EnumStammdatenTyp.VERSORGER_TELEKOM));
            }

            StoreSelectedItem();
            SelectedItemViewModel = stammdatenNeu;
            StammdatenListe.Add(SelectedItemViewModel);

            RaisePropertyChanged(() => SelectedItemViewModel.DisplayString);
        }

        private void StoreSelectedItem()
        {
            previouslySelectedItemViewModel = selectedItemViewModel;
        }

        private void RestoreSelectedItem()
        {
            if (previouslySelectedItemViewModel != null)
            {
                SelectedItemViewModel = stammdatenListeInternal.FirstOrDefault(s => s.Id == previouslySelectedItemViewModel.Id);
                previouslySelectedItemViewModel = null;
            }
        }
    }
}
