using Common.Models;

namespace Stammdaten.ViewModels
{
    public interface IStammdatenItemViewModel
    {
        EnumStammdatenTyp StammdatenTyp { get; }

        string DisplayString { get; }

        bool IsEditMode { get; set; }

        int Id { get; }
    }
}