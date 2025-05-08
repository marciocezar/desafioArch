using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ConsolidationService.Controllers;
using ConsolidationService.Data;
using ConsolidationService.Models;
using Xunit;

namespace ConsolidationService.Tests
{
    public class ConsolidationServiceTests
    {
        private TransactionDbContext GetInMemoryDbContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<TransactionDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            return new TransactionDbContext(options);
        }

        [Fact]
        public async Task GetDailyReport_ShouldCalculateCorrectBalance()
        {
            // Arrange
            var context = GetInMemoryDbContext("GetDailyReport_ShouldCalculateCorrectBalance");
            var testDate = DateTime.Today;
            context.Transactions.AddRange(
                new Transaction { Date = testDate, Amount = 100, Description = "Credit" },
                new Transaction { Date = testDate, Amount = -50, Description = "Debit" },
                new Transaction { Date = testDate.AddDays(-1), Amount = 200, Description = "Old Credit" }
            );
            await context.SaveChangesAsync();
            var controller = new ConsolidationController(context);

            // Act
            var result = await controller.GetDailyReport(testDate);

            // Assert
            var okResult = Assert.IsType<ActionResult<decimal>>(result);
            Assert.Equal(250, okResult.Value); // 100 - 50 + 200 = 250
        }

        [Fact]
        public async Task GetDailyReport_WithNoTransactions_ShouldReturnZero()
        {
            // Arrange
            var context = GetInMemoryDbContext("GetDailyReport_WithNoTransactions");
            var controller = new ConsolidationController(context);

            // Act
            var result = await controller.GetDailyReport(DateTime.Today);

            // Assert
            var okResult = Assert.IsType<ActionResult<decimal>>(result);
            Assert.Equal(0, okResult.Value);
        }
    }
}