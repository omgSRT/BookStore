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
        private OrderDAO _dao;
        public OrderRepository(OrderDAO dao)
        {
            _dao = dao;
        }

        public void AddOrder(Order order)
        {
            _dao.AddOrder(order);
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            _dao.AddOrderDetail(orderDetail);
        }

        public void DeleteOrder(Order order)
        {
            _dao.DeleteOrder(order);
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            _dao.DeleteOrderDetail(orderDetail);
        }

        public IEnumerable<OrderDetail> GetAllOrderDetails()
        {
            return _dao.GetAllOrderDetails();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _dao.GetAllOrders();
        }

        public Order? GetOrderById(int id)
        {
            return _dao.GetOrderById(id);
        }

        public OrderDetail? GetOrderDetailById(int id)
        {
            return _dao.GetOrderDetailById(id);
        }

        public void UpdateOrder(Order order)
        {
            _dao.UpdateOrder(order);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _dao.UpdateOrderDetail(orderDetail);
        }
    }
}
