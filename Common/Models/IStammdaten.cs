using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public interface IStammdaten
    {
        EnumStammdatenTyp StammdatenTyp { get; }

        /// <summary>
        /// Liefert die Id des Stammdatums oder setzt diese.
        /// </summary>
        int Id { get; set; }
    }
}
