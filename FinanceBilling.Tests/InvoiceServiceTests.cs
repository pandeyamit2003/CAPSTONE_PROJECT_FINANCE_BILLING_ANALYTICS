using FinanceBilling.Data.Entities;
using FinanceBilling.Data.Interfaces;
using FinanceBilling.Data.Services;
using Moq;
using Xunit;

public class InvoiceServiceTests
{
    [Fact]
    public async Task GetAllAsync_Should_Return_Invoices()
    {
        // Arrange
        var invoices = new List<Invoice>
        {
            new Invoice
            {
                InvoiceId = 1,
                InvoiceNumber = "INV001"
            }
        };

        var repoMock = new Mock<IInvoiceRepository>();

        repoMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(invoices);

        var service =
            new InvoiceService(repoMock.Object);

        // Act
        var result =
            await service.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("INV001",
                     result.First().InvoiceNumber);
    }
}