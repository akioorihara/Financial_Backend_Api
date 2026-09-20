using FinancialBackendApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialController : ControllerBase
    {
        private static readonly string[] Categories =
            [
                "Debit", "Credit"
            ];

        private static readonly string[] Descriptions =
            [
                "Payment for services", "Refund", "Purchase", "Salary", "Transfer"
            ];


        /// <summary>
        /// Get financial transactions with random types (Debit or Credit).
        /// </summary>
        /// <returns>
        /// A list of financial transactions with random types.
        /// </returns>
        [HttpGet("types", Name = "GetFinancialTypes")]
        public IEnumerable<FinancialTransactions> Get()
        {
            return Categories.Select(category => new FinancialTransactions
            {
                Type = category
            }).ToArray();

        }

        /// <summary>
        /// Get a financial transaction by its ID.
        /// </summary>
        /// <param name="id">TransactionId</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetFinancialTransactionById")]
        public ActionResult<FinancialTransactions> Get(int id)
        {
            var transaction = Enumerable.Range(1, 2).Select(index => new FinancialTransactions
            {
                Type = Categories[Random.Shared.Next(Categories.Length)]
            }).ToArray()[id - 1];

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

    }
}
