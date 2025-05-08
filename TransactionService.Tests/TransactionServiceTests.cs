using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransactionService.Controllers;
using TransactionService.Data;
using TransactionService.Models;
using Xunit;

namespace TransactionService.Tests
{
    public class TransactionServiceTests
    {
        private TransactionDbContext GetInMemoryDbContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<TransactionDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            return new TransactionDbContext(options);
        }

        [Fact]
        public async Task PostTransaction_ShouldAddTransactionAndReturnCreated()
        {
            // Arrange
            var context = GetInMemoryDbContext("PostTransaction_ShouldAddTransaction");
            var controller = new TransactionsController(context);
            var transaction = new Transaction
            {
                Date = DateTime.Now,
                Amount = 100,
                Description = "Credit Test"
            };

            // Act
            var result = await controller.PostTransaction(transaction);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Transaction>>(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Single(context.Transactions);
            var savedTransaction = context.Transactions.First();
            Assert.Equal(transaction.Amount, savedTransaction.Amount);
            Assert.Equal(transaction.Description, savedTransaction.Description);
        }

        [Fact]
        public async Task GetTransactions_ShouldReturnAllTransactions()
        {
            // Arrange
            var context = GetInMemoryDbContext("GetTransactions_ShouldReturnAllTransactions");
            context.Transactions.AddRange(
                new Transaction { Date = DateTime.Now, Amount = 100, Description = "Credit" },
                new Transaction { Date = DateTime.Now, Amount = -50, Description = "Debit" }
            );
            await context.SaveChangesAsync();
            var controller = new TransactionsController(context);

            // Act
            var result = await controller.GetTransactions();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Transaction>>>(result);
            var transactions = Assert.IsAssignableFrom<IEnumerable<Transaction>>(okResult.Value);
            Assert.Equal(2, transactions.Count());
        }
    }
}