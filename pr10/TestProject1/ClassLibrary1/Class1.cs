

public interface IOrderRepository
{
    void Save(Order order);
}

public interface IPaymentGateway
{
    bool Charge(decimal amount);
}
public class Order
{
    public decimal Total { get; set; }
    public bool IsPaid { get; set; }
}
public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly IPaymentGateway _paymentGateway;

    public OrderService(IOrderRepository repository, IPaymentGateway paymentGateway)
    {
        _repository = repository;
        _paymentGateway = paymentGateway;
    }

    public bool PlaceOrder(Order order)
    {
        var success = _paymentGateway.Charge(order.Total);
        if (!success)
            return false;

        order.IsPaid = true;
        _repository.Save(order);
        return true;
    }
}