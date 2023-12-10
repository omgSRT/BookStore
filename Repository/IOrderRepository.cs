using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        void DeleteOrder(Order order);
        void UpdateOrder(Order order);
        IEnumerable<Order> GetAllOrders();
        Order? GetOrderById(int id);
        void AddOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(OrderDetail orderDetail);
        IEnumerable<OrderDetail> GetAllOrderDetails();
        OrderDetail? GetOrderDetailById(int id);
    }
}
