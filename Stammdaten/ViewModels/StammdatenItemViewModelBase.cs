using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using GalaSoft.MvvmLight;

namespace Stammdaten.ViewModels
{
    public abstract class StammdatenItemViewModelBase : ViewModelBase, IStammdatenItemViewModel
    {
        private bool isEditMode;

        public bool IsEditMode
        {
            get => isEditMode;
            set
            {
                if (value != isEditMode)
                {
                    isEditMode = value;
                    RaisePropertyChanged();
                }
            }
        }

        public abstract int Id { get; }

        public abstract EnumStammdatenTyp StammdatenTyp { get; }

        public abstract string DisplayString { get; }        
    }
}
