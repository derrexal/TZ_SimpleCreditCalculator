using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TZ_Atlasitsolutions_SimpleCreditCalculator.Models
{
    /// <summary>
    /// Модель для таблицы результатов расчета аннуитетных займов
    /// </summary>
    public class Result
    {
        [Description("Номер платежа")]
        public long Id { get; set; }

        [Description("Дата платежа")]
        public DateOnly PaymentDate { get; set; }

        [Description("Сумма платежа")]
        public double TotalPaymentAmount { get; set; }

        [Description("Размер платежа по телу")]
        public double PaymentAmountByBody { get; set; }

        [Description("Размер платежа по проценту")]
        public double PaymentAmountByPercent { get; set; }

        [Description("Остаток основного долга")]
        public double PrincipalBalance { get; set; }
    }
}
