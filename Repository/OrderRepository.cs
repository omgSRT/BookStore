using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void AddOrder(Order order)
        {
            OrderDAO.SingletonInstance.AddOrder(order);
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            OrderDAO.SingletonInstance.AddOrderDetail(orderDetail);
        }

        public void DeleteOrder(Order order)
        {
            OrderDAO.SingletonInstance.DeleteOrder(order);
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            OrderDAO.SingletonInstance.DeleteOrderDetail(orderDetail);
        }

        public IList<OrderDetail> GetAllOrderDetails()
        {
            return OrderDAO.SingletonInstance.GetAllOrderDetails();
        }

        public IList<Order> GetAllOrders()
        {
            return OrderDAO.SingletonInstance.GetAllOrders();
        }

        public Order? GetOrderById(int id)
        {
            return OrderDAO.SingletonInstance.GetOrderById(id);
        }

        public OrderDetail? GetOrderDetailById(int id)
        {
            return OrderDAO.SingletonInstance.GetOrderDetailById(id);
        }

        public void UpdateOrder(Order order)
        {
            OrderDAO.SingletonInstance.UpdateOrder(order);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            OrderDAO.SingletonInstance.UpdateOrderDetail(orderDetail);
        }
        public IList<Order> GetAllOrdersWithIncludeCustomerAndStaff()
        {
            return OrderDAO.SingletonInstance.GetAllOrdersWithIncludeCustomerAndStaff();
        }
        public IList<OrderDetail> GetAllOrderDetailsWithBookAndBiS()
        {
            return OrderDAO.SingletonInstance.GetAllOrderDetailsWithBookAndBiS();
        }

        public IList<Order> GetOrderByCustomerID(int id)
        {
            return OrderDAO.SingletonInstance.GetOrderByCustomerID(id);
        }

        public IList<OrderDetail> GetAllOrderDetailByOrderId(int id)
        {
            return OrderDAO.SingletonInstance.GetAllOrderDetailByOrderId(id);
        }
    }
}
