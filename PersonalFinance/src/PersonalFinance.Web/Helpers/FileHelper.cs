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
        public ICollection<Transaction> GetTransactions()
        {
            var pathBase = @"C:\CODE\Projects\PersonalFinance\src\PersonalFinance.Web\Resources\BankStatements\";

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

        public List<ITransaction> GetExpenses(ICollection<Transaction> transactions)
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
                            Id = Guid.NewGuid(),
                            Date = DateTime.Parse(transaction.ValueDate.ToShortDateString()),
                            Amount = decimal.Parse(transaction.Amount.Value.ToString()),
                            Description = transaction.Description,
                            Payee = transaction.Details.Name,
                            Notes = "",
                            Type = ExpenseType.Card
                        });
                    }
                }
                return expenses.ToList<ITransaction>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ITransaction> GetIncome(ICollection<Transaction> transactions)
        {
            var income = new List<Income>();

            try
            {
                foreach (var transaction in transactions)
                {
                    if (transaction.DebitCredit.ToString() == "Credit")
                    {
                        income.Add(new Income
                        {
                            Id = Guid.NewGuid(),
                            Date = DateTime.Parse(transaction.ValueDate.ToShortDateString()),
                            Amount = decimal.Parse(transaction.Amount.Value.ToString()),
                            Description = transaction.Description,
                            Payor = transaction.Details.Name,
                            Notes = "",
                        });
                    }
                }
                return income.ToList<ITransaction>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
