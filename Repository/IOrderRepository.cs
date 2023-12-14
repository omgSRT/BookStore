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
        IList<Order> GetAllOrders();
        Order? GetOrderById(int id);
        void AddOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(OrderDetail orderDetail);
        IList<OrderDetail> GetAllOrderDetails();
        OrderDetail? GetOrderDetailById(int id);
        IList<Order> GetAllOrdersWithIncludeCustomerAndStaff();
        IList<OrderDetail> GetAllOrderDetailsWithBookAndBiS();
        IList<Order> GetOrderByCustomerID(int id);
        IList<OrderDetail> GetAllOrderDetailByOrderId(int id);
    }
}
