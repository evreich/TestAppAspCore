﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    interface IOrdersRepository
    {
        IEnumerable<Order> GetNotConfirmedOrders();
        IEnumerable<Order> GetOrdersForUser(string userId);
        Order GetOrder(int id);
        void AddOrder(Order order);
        void ConfirmOrder(int orderID);
        void CancelOrder(int orderID);
    }
}
