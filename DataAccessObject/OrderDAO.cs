using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class OrderDAO
    {
        private BookStoreDBContext _context;
        public OrderDAO()
        {
            _context = new BookStoreDBContext();
        }
        public void AddOrder(Order order)
        {
            try
            {
                var checkExist = _context.Orders.Find(order.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Add(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void DeleteOrder(Order order)
        {
            try
            {
                var checkExist = _context.Orders.Find(order.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Remove(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public IList<Order> GetAllOrders()
        {
            try
            {
                return _context.Set<Order>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Order>();
            }
        }

        public Order? GetOrderById(int id)
        {
            try
            {
                return _context.Set<Order>().Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                var checkExist = _context.Orders.Find(order.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Update(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                var checkExist = _context.OrderDetails.Find(orderDetail.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Add(orderDetail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                var checkExist = _context.OrderDetails.Find(orderDetail.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Remove(orderDetail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public IList<OrderDetail> GetAllOrderDetails()
        {
            try
            {
                return _context.Set<OrderDetail>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<OrderDetail>();
            }
        }

        public OrderDetail? GetOrderDetailById(int id)
        {
            try
            {
                return _context.Set<OrderDetail>().Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                var checkExist = _context.OrderDetails.Find(orderDetail.Id);
                if (checkExist != null)
                {
                    _context.Entry(checkExist).State = EntityState.Detached;
                }
                _context.Update(orderDetail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public IList<Order> GetAllOrdersWithIncludeCustomerAndStaff()
        {
            try
            {
                return _context.Set<Order>()
                    .Include("Customer")
                    .Include("Staff")
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Order>();
            }
        }
        public IList<OrderDetail> GetAllOrderDetailsWithBookAndBiS()
        {
            try
            {
                return _context.Set<OrderDetail>()
                    .Include("Book")
                    .Include("BookInStore")
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<OrderDetail>();
            }
        }
    }
}
