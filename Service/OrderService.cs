using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public void AddOrder(Order order)
        {
            _repository.AddOrder(order);
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            _repository.AddOrderDetail(orderDetail);
        }

        public void DeleteOrder(Order order)
        {
            _repository.DeleteOrder(order);
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            _repository.DeleteOrderDetail(orderDetail);
        }

        public IList<OrderDetail> GetAllOrderDetails()
        {
            return _repository.GetAllOrderDetails();
        }

        public IList<Order> GetAllOrders()
        {
            return _repository.GetAllOrders();
        }

        public Order? GetOrderById(int id)
        {
            return _repository.GetOrderById(id);
        }

        public OrderDetail? GetOrderDetailById(int id)
        {
            return _repository.GetOrderDetailById(id);
        }

        public void UpdateOrder(Order order)
        {
            _repository.UpdateOrder(order);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _repository.UpdateOrderDetail(orderDetail);
        }
        public IList<Order> GetAllOrdersWithIncludeCustomerAndStaff()
        {
            return _repository.GetAllOrdersWithIncludeCustomerAndStaff();
        }
        public IList<OrderDetail> GetAllOrderDetailsWithBookAndBiS()
        {
            return _repository.GetAllOrderDetailsWithBookAndBiS();
        }

        public IList<Order> GetOrderByCustomerID(int id)
        {
            return _repository.GetOrderByCustomerID(id);
        }

        public IList<OrderDetail> GetAllOrderDetailByOrderId(int id)
        {
            return _repository.GetAllOrderDetailByOrderId(id);
        }
    }
}
