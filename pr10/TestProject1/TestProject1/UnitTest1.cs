using Xunit;
using Moq;

public class OrderServiceTests
{
    [Fact]
    public void PlaceOrder_PaymentSuccess_ShouldSaveOrder()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var mockPayment = new Mock<IPaymentGateway>();

        var order = new Order { Total = 100m };

        // Настраиваем мок платёжного сервиса
        mockPayment.Setup(p => p.Charge(100m)).Returns(true);

        var service = new OrderService(mockRepo.Object, mockPayment.Object);

        // Act
        var result = service.PlaceOrder(order);

        // Assert
        Assert.True(result);
        mockRepo.Verify(r => r.Save(order), Times.Once);
        mockPayment.Verify(p => p.Charge(100m), Times.Once);
    }

    [Fact]
    public void PlaceOrder_PaymentFails_DoesNotSaveOrder()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var mockPayment = new Mock<IPaymentGateway>();

        var order = new Order { Total = 50m };

        // Настраиваем возвращение false (оплата не прошла)
        mockPayment.Setup(p => p.Charge(50m)).Returns(false);

        var service = new OrderService(mockRepo.Object, mockPayment.Object);

        // Act
        var result = service.PlaceOrder(order);

        // Assert
        Assert.False(result);
        mockRepo.Verify(r => r.Save(It.IsAny<Order>()), Times.Never);
        mockPayment.Verify(p => p.Charge(50m), Times.Once);
    }

    [Fact]
    public void PlaceOrder_ShouldCharge_CorrectAmount()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var mockPayment = new Mock<IPaymentGateway>();
        var order = new Order { Total = 75m };

        mockPayment.Setup(p => p.Charge(It.IsAny<decimal>())).Returns(true);

        var service = new OrderService(mockRepo.Object, mockPayment.Object);

        // Act
        service.PlaceOrder(order);

        // Assert
        mockPayment.Verify(p => p.Charge(It.Is<decimal>(amt => amt == 75m)), Times.Once);
    }
}