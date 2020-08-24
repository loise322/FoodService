using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelLine.Food.Domain.Legals
{
    /// <summary>
    /// Причины перевода
    /// </summary>
    public enum TransferReasonsType : byte
    {
        /// <summary>
        /// Без причины
        /// </summary>
        NoReason = 0,
        /// <summary>
        /// Отпуск
        /// </summary>
        Holiday = 1,
        /// <summary>
        /// Больничный
        /// </summary>
        Sick = 2,
        /// <summary>
        /// Командировка
        /// </summary>
        BusinessTrip = 3,
        /// <summary>
        /// Увольнение
        /// </summary>
        Dismissal = 4,
        /// <summary>
        /// Прием
        /// </summary>
        Application = 5
    }
}
