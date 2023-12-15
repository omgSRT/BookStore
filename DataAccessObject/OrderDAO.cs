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
        private static OrderDAO _instance = null;
        private static readonly object _instanceLock = new object();
        public OrderDAO() { }

        public static OrderDAO SingletonInstance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new OrderDAO();
                    }
                    return _instance;
                }
            }
        }
        public void AddOrder(Order order)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Orders.Find(order.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Add(order);
                    _context.SaveChanges();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Orders.Find(order.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Remove(order);
                    _context.SaveChanges();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Order>().ToList();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Order>().Find(id);
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.Orders.Find(order.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Update(order);
                    _context.SaveChanges();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.OrderDetails.Find(orderDetail.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Add(orderDetail);
                    _context.SaveChanges();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist = _context.OrderDetails.Find(orderDetail.Id);
                    if (checkExist != null)
                    {
                        _context.Entry(checkExist).State = EntityState.Detached;
                    }
                    _context.Remove(orderDetail);
                    _context.SaveChanges();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<OrderDetail>().ToList();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<OrderDetail>().Find(id);
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    var checkExist_orderdetail = _context.OrderDetails.Find(orderDetail.Id);
                    if (checkExist_orderdetail != null)
                    {
                        _context.Entry(checkExist_orderdetail).State = EntityState.Detached;
                    }

                    var checkExit_order = _context.Orders.Find(orderDetail.OrderId);
                    if (checkExit_order != null)
                    {
                        checkExit_order.TotalPrice = UpdateTotalPrice(checkExit_order.Id);
                        _context.Entry(checkExit_order).State = EntityState.Detached;
                    }

                    _context.Update(orderDetail);
                    _context.SaveChanges();
                }
                    

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        private decimal? UpdateTotalPrice(int id)
        {
            IList<OrderDetail> orderDetails = GetAllOrderDetailByOrderId(id);
            decimal? totalPrice = 0;
            foreach (var orderDetail in orderDetails) {
                decimal? price = orderDetail.Book.Price;
                totalPrice += price * orderDetail.Quantity;
            }return totalPrice;
        }
        public IList<Order> GetAllOrdersWithIncludeCustomerAndStaff()
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Order>()
                    .Include("Customer")
                    .Include("Staff")
                    .ToList();
                }
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
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<OrderDetail>()
                    .Include("Book")
                    .Include("BookInStore")
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<OrderDetail>();
            }
        }

        public IList<Order> GetOrderByCustomerID(int id)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<Order>()
                    .Where(c => c.CustomerId == id)
                    .Include("Customer")
                    .Include("Staff")
                    .ToList();
                }
                    
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Order>();
            }
        }
        public IList<OrderDetail> GetAllOrderDetailByOrderId(int id)
        {
            try
            {
                using (var _context = new BookStoreDBContext())
                {
                    return _context.Set<OrderDetail>()
                    .Where(c => c.OrderId == id)
                    .Include("Book")
                    .Include("Order")
                    .ToList();
                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<OrderDetail>();
            }
        }
        
    }
}
