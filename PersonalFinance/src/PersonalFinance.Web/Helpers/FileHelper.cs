using PersonalFinance.Web.Interfaces;
using PersonalFinance.Web.Models;
using Raptorious.SharpMt940Lib;
using Raptorious.SharpMt940Lib.Mt940Format;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Helpers
{
    public class FileHelper
    {
        public string pathBase = @"C:\CODE\Projects\PersonalFinance\src\PersonalFinance.Web\Resources\BankStatements\";

        public ICollection<Transaction> GetTransactions()
        {

            try
            {
                var fileName = $@"{Directory.GetFiles(pathBase)[0]}";

                var header = new Separator("STARTUMSE");
                var trailer = new Separator("-");
                var genericFormat = new GenericFormat(header, trailer);
                var parsed = Mt940Parser.Parse(genericFormat, fileName, CultureInfo.CurrentCulture);

                var transactions = parsed.Select(x => x.Transactions).FirstOrDefault();
                return transactions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Expense> GetExpenses(ICollection<Transaction> transactions)
        {
            var expenses = new List<Expense>();

            try
            {
                foreach (var transaction in transactions)
                {
                    if (transaction.DebitCredit.ToString() == "Debit")
                    {
                        expenses.Add(new Expense
                        {
                            Date = DateTime.Parse(transaction.ValueDate.ToShortDateString()),
                            Amount = decimal.Parse(transaction.Amount.Value.ToString()),
                            Description = transaction.Description,
                            Payee = transaction.Details.Name,
                            Notes = "",
                            Type = ExpenseType.Card
                        });
                    }
                }
                return expenses.ToList<Expense>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Revenue> GetRevenues(ICollection<Transaction> transactions)
        {
            var revenues = new List<Revenue>();

            try
            {
                foreach (var transaction in transactions)
                {
                    if (transaction.DebitCredit.ToString() == "Credit")
                    {
                        revenues.Add(new Revenue
                        {
                            Date = DateTime.Parse(transaction.ValueDate.ToShortDateString()),
                            Amount = decimal.Parse(transaction.Amount.Value.ToString()),
                            Description = transaction.Description,
                            Payer = transaction.Details.Name,
                            Notes = "",
                        });
                    }
                }
                return revenues.ToList<Revenue>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
