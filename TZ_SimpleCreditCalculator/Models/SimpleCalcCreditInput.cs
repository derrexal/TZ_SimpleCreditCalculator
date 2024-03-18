using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TZ_Atlasitsolutions_SimpleCreditCalculator.Models
{
    /// <summary>
    /// Модель входных данных для простого расчета аннуитетных займов
    /// </summary>
    public class SimpleCalcCreditInput
    {
        [Description("Сумма займа")]
        [Required]
        [Range(1, 1000000000000)] //В данном случае ограничение условное. В проде - изменить. Аналогичное ограничение установлено на клиенте
        public double LoanAmount { get; set; }

        [Description("Срок займа (в месяцах)")]
        [Required]
        [Range(1, 12)]
        public int LoanTermMonth { get; set; }
       
        [Description("Ставка (в год)")]
        [Required]
        [Range(0, 100)]
        public float RateYear { get; set; }
    }
}
