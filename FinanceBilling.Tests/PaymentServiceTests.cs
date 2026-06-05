using FinanceBilling.Data.Entities;
using FinanceBilling.Data.Interfaces;
using FinanceBilling.Data.Services;
using Moq;
using Xunit;

public class PaymentServiceTests
{
    [Fact]
    public async Task AddAsync_Should_Call_Repository()
    {
        // Arrange
        var payment = new Payment
        {
            PaymentId = 1
        };

        var repoMock =
            new Mock<IPaymentRepository>();

        repoMock.Setup(x => x.AddAsync(payment))
                .Returns(Task.CompletedTask);

        var service =
            new PaymentService(repoMock.Object);

        // Act
        await service.AddAsync(payment);

        // Assert
        repoMock.Verify(
            x => x.AddAsync(payment),
            Times.Once);
    }
}