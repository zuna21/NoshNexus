namespace API;

public interface IOrderRepository
{
    void Create(Order order);
    void CreateOrderMenuItems(ICollection<OrderMenuItem> orderMenuItems);
    Task<bool> SaveAllAsync();

    Task<ICollection<OrderCardDto>> GetOwnerInProgressOrders(int ownerId);
}
