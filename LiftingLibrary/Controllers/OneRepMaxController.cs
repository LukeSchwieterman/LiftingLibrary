using LiftingLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LiftingLibrary.Controllers
{
    public class OneRepMaxController : Controller
    {
        private GlobalUser _globalUser;

        public OneRepMaxController(GlobalUser globalUser) 
        {
            _globalUser = globalUser;
        }

        public IActionResult CalculateOneRepMax(int weightNumber, int repNumber)
        {
            OneRepMax oneRepMax = new OneRepMax()
            {
                Reps = new List<int>(),
                Weights = new List<int>()
            };
            double[] percentages = { 1.00, 0.95, 0.93, 0.90, 0.87, 0.85, 0.83, 0.80, 0.77, 0.75 };
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    double oneRepMaxValue = weightNumber * (1.0 + repNumber / 30.0) * percentages[i-1];
                    oneRepMax.Reps.Add(i);
                    if (i == repNumber)
                    {
                        oneRepMax.Weights.Add(Convert.ToInt32(weightNumber));
                    }
                    else
                    {
                        oneRepMax.Weights.Add(Convert.ToInt32(oneRepMaxValue));
                    }
                }
            }
            catch (Exception)
            {

            }
            return PartialView("~/Views/Library/OneRepMaxDetails.cshtml", oneRepMax);
        }
    }
}
