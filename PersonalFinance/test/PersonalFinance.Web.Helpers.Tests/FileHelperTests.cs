using Moq;
using NUnit.Framework;
using PersonalFinance.Web.Models;
using Raptorious.SharpMt940Lib;
using Raptorious.SharpMt940Lib.Mt940Format;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace PersonalFinance.Web.Helpers.Tests
{
    [TestFixture]
    public class FileHelperTests
    {
        public string PathBase = @"C:\CODE\Projects\PersonalFinance\src\PersonalFinance.Web\Resources\BankStatements\";

        [Test]
        public void GetTransactionsTests_GetBankStatement_ReturnTransactions()
        {
            try
            {
                var fileName =$@"{Directory.GetFiles(PathBase)[0]}";

                var header = new Separator("STARTUMSE");
                var trailer = new Separator("-");
                var genericFormat = new GenericFormat(header, trailer);
                var parsed = Mt940Parser.Parse(genericFormat, fileName, CultureInfo.CurrentCulture);

                var transactions = parsed.Select(x => x.Transactions).FirstOrDefault();
                var transaction = transactions.FirstOrDefault();

                Assert.That(transactions.Count, Is.Not.Null);
                Assert.That(transactions.Contains(transaction), Is.True);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [Test]
        public void GetExpensesTests_TakeTransactions_ReturnExpenses()
        {
            try
            {
                var fileName = $@"{Directory.GetFiles(PathBase)[0]}";

                var header = new Separator("STARTUMSE");
                var trailer = new Separator("-");
                var genericFormat = new GenericFormat(header, trailer);
                var parsed = Mt940Parser.Parse(genericFormat, fileName, CultureInfo.CurrentCulture);

                var transactions = parsed.Select(x => x.Transactions).FirstOrDefault();

                var expenses = new List<Expense>();

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
                Assert.That(expenses.Count, Is.GreaterThan(0));
                Assert.That(expenses[0], Is.Not.Null);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [Test]
        public void GetIncomeTests_TakeTransactions_ReturnRevenue()
        {
            try
            {
                var fileName = $@"{Directory.GetFiles(PathBase)[0]}";

                var header = new Separator("STARTUMSE");
                var trailer = new Separator("-");
                var genericFormat = new GenericFormat(header, trailer);
                var parsed = Mt940Parser.Parse(genericFormat, fileName, CultureInfo.CurrentCulture);

                var transactions = parsed.Select(x => x.Transactions).FirstOrDefault();

                var income = new List<Revenue>();

                foreach (var transaction in transactions)
                {
                    if (transaction.DebitCredit.ToString() == "Credit")
                    {
                        income.Add(new Revenue
                        {
                            Date = DateTime.Parse(transaction.ValueDate.ToShortDateString()),
                            Amount = decimal.Parse(transaction.Amount.Value.ToString()),
                            Description = transaction.Description,
                            Payer = transaction.Details.Name,
                            Notes = "",
                        });
                    }
                }
                Assert.That(income.Count, Is.GreaterThan(0));
                Assert.That(income[0], Is.Not.Null);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
