using Microsoft.AspNetCore.Mvc;
using TZ_Atlasitsolutions_SimpleCreditCalculator.Models;
using TZ_Atlasitsolutions_SimpleCreditCalculator.Shared;

namespace TZ_Atlasitsolutions_SimpleCreditCalculator.Controllers
{
    public class ComplexLoanCalculatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ComplexCalcCreditInput inputCalcCreditData)
        {
            if (!ModelState.IsValid)
                return BadRequest("Request param is no valid");

            var resultCalc = OtherMethodCalculator.CalcComplexCredit(inputCalcCreditData);

            ViewBag.SummaryProcent = OtherMethodCalculator.CalcSummaryProcent(resultCalc);

            return View("Result", resultCalc);
        }
    }
}
