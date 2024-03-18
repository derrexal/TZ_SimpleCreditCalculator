using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TZ_Atlasitsolutions_SimpleCreditCalculator.Models
{
    /// <summary>
    /// Модель входных данных для сложного расчета аннуитетных займов
    /// </summary>
    public class ComplexCalcCreditInput
    {
        [Description("Сумма займа")]
        [Required]
        [Range(1, 1000000000000)] // Максимальная сумма займа ~ триллион. В данном случае ограничение условное. Аналогичное ограничение установлено на клиенте
        public double LoanAmount { get; set; }

        [Description("Срок займа (в днях)")]
        [Required]
        [Range(1, 36000)] // Максимальный срок ~ 100 лет. В данном случае ограничение условное. Аналогичное ограничение установлено на клиенте
        public int LoanTermDays { get; set; }

        [Description("Ставка (в день)")]
        [Required]
        [Range(0, 100)]
        public float RateDays { get; set; }

        [Description("Шаг платежа (в днях)")]
        [Required]
        [Range(1, 3600)] // Максимальный шаг ~ 10 лет. В данном случае ограничение условное. Аналогичное ограничение установлено на клиенте
        public int PaymentStepDays { get; set; }
    }
}
