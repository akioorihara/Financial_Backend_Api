using Microsoft.AspNetCore.Mvc;

namespace FinancialApi.Controllers
{
    public class FinancialServiceController : Controller
    {
        // GET: FinancialServiceController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FinancialServiceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FinancialServiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialServiceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FinancialServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialServiceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FinancialServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
