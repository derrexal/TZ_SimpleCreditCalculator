using TZ_Atlasitsolutions_SimpleCreditCalculator.Models;

namespace TZ_Atlasitsolutions_SimpleCreditCalculator.Shared
{
    /// <summary>
    /// Вспомогательный класс содержащий различные методы вычисления (По хорошему для таких должен быть отдельный проект-библиотека классов и не статическим. В данном случае уместно упрощение кода)
    /// </summary>
    public static class OtherMethodCalculator
    {
        /// <summary>
        /// Метод вычисления аннуитетного займа с указанием ставки за год. 
        /// !По хорошему - нужно выносить такие методы в отдельную библиотеку
        /// </summary>
        /// <param name="inputCalcCreditData"></param>
        /// <returns></returns>
        public static List<Result> CalcSimpleCredit(SimpleCalcCreditInput inputCalcCreditData)
        {
            var resultCalc = new List<Result>();
            // Дата оформления займа
            var creditStartDate = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            // Месячная процентная ставка (округляем до 2 знаков после запятой)
            double rateMonth = Math.Round(inputCalcCreditData.RateYear / 12 / 100, 2);
            // Коэффициент аннуитета
            double annuityRatio = rateMonth * Math.Pow((1 + rateMonth), inputCalcCreditData.LoanTermMonth)
                                    / (Math.Pow((1 + rateMonth), inputCalcCreditData.LoanTermMonth) - 1);

            // Ежемесячный аннуитетный платеж
            double monthlyPayment = annuityRatio * inputCalcCreditData.LoanAmount;

            for (int month = 1; month <= inputCalcCreditData.LoanTermMonth; month++)
            {
                // Платеж по проценту
                double procent = inputCalcCreditData.LoanAmount * rateMonth;
                // Платеж по телу
                double baseDebt = monthlyPayment - procent;
                // Уменьшаем сумму общего долг
                inputCalcCreditData.LoanAmount -= baseDebt;

                resultCalc.Add(new Result
                {
                    Id = month,
                    PaymentDate = creditStartDate.AddMonths(month),
                    TotalPaymentAmount = Math.Round(monthlyPayment, 2),
                    PaymentAmountByBody = Math.Round(baseDebt, 2),
                    PaymentAmountByPercent = Math.Round(procent, 2),
                    PrincipalBalance = Math.Round(inputCalcCreditData.LoanAmount, 2)
                });
            }
            return resultCalc;

        }

        /// <summary>
        /// Метод вычисления аннуитетного займа с указанием ставки день и шага платежей. 
        /// !По хорошему - нужно выносить такие методы в отдельную библиотеку
        /// </summary>
        /// <param name="inputCalcCreditData"></param>
        /// <returns></returns>
        public static List<Result> CalcComplexCredit(ComplexCalcCreditInput inputCalcCreditData)
        {
            var resultCalc = new List<Result>();
            // Дата оформления займа
            var creditStartDate = DateOnly.FromDateTime(DateTime.Today);

            // Месячная процентная ставка (округляем до 2 знаков после запятой)
            double rateMonth = Math.Round(inputCalcCreditData.RateDays * 365 / 12 / 100, 2);
            // Количество периодов, в течение которых выплачивается кредит
            int countPeriod = Math.Max(inputCalcCreditData.LoanTermDays / inputCalcCreditData.PaymentStepDays,1); 
            // Коэффициент аннуитета
            double annuityRatio = rateMonth * Math.Pow((1 + rateMonth), countPeriod)
                                    / (Math.Pow((1 + rateMonth), countPeriod) - 1);

            // Ежемесячный аннуитетный платеж
            double monthlyPayment = annuityRatio * inputCalcCreditData.LoanAmount;

            for (int numberPeriod = 1; numberPeriod <= countPeriod; numberPeriod++)
            {
                // Платеж по проценту
                double procent = inputCalcCreditData.LoanAmount * rateMonth;
                // Платеж по телу
                double baseDebt = monthlyPayment - procent;
                // Уменьшаем сумму общего долг
                inputCalcCreditData.LoanAmount -= baseDebt;

                creditStartDate = creditStartDate.AddDays(inputCalcCreditData.PaymentStepDays);
                resultCalc.Add(new Result
                {
                    Id = numberPeriod,
                    PaymentDate = creditStartDate,
                    TotalPaymentAmount = Math.Round(monthlyPayment, 2),
                    PaymentAmountByBody = Math.Round(baseDebt, 2),
                    PaymentAmountByPercent = Math.Round(procent, 2),
                    PrincipalBalance = Math.Round(inputCalcCreditData.LoanAmount, 2)
                });
            }
            return resultCalc;

        }

        /// <summary>
        /// Вычисление итоговой переплаты за весь период
        /// </summary>
        /// <param name="resultCalc"></param>
        /// <returns></returns>
        public static double CalcSummaryProcent(List<Result> resultCalc)
        {
            double sum = 0;

            foreach (var item in resultCalc)
            {
                sum += item.PaymentAmountByPercent;
            }
            sum = Math.Round(sum, 2);

            return sum;
        }
    }
}
