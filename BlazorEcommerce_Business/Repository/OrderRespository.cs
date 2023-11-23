﻿using AutoMapper;
using BlazorEcommerce_Business.Repository.IRepository;
using BlazorEcommerce_Common;
using BlazorEcommerce_DataAccessLayer;
using BlazorEcommerce_DataAccessLayer.Data;
using BlazorEcommerce_DataAccessLayer.ViewModel;
using EcommerceModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce_Business.Repository
{
    public class OrderRespository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public OrderRespository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<OrderDTO> Create(OrderDTO orderDTO)
        {
            try
            {
                var obj = _mapper.Map<OrderDTO, Order>(orderDTO);
                _db.OrderHeaders.Add(obj.OrderHeader);
                await _db.SaveChangesAsync();
                foreach (var details in obj.OrderDetails)
                {
                    details.OrderHeaderId = obj.OrderHeader.Id;

                }
                _db.OrderDetails.AddRange(obj.OrderDetails);
                await _db.SaveChangesAsync();
                return new OrderDTO()
                {
                    OrderHeaderDTO = _mapper.Map<OrderHeader, OrderHeaderDTO>(obj.OrderHeader),
                    OrderDetailsDTO = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDTO>>(obj.OrderDetails).ToList()
                };


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orderDTO;

        }

        public async Task<int> DeleteById(int id)
        {
            var objHeader = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.Id == id);
            if (objHeader != null)
            {
                IEnumerable<OrderDetail> objDetail = _db.OrderDetails.Where(u => u.OrderHeaderId == id);
                _db.OrderDetails.RemoveRange(objDetail);
                _db.OrderHeaders.Remove(objHeader);
                return _db.SaveChanges();
            }
            return 0;
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null)
        {
            List<Order> OrderFromDb = new List<Order>();
            IEnumerable<OrderHeader> orderHeaderList = _db.OrderHeaders;
            IEnumerable<OrderDetail> orderDetailList = _db.OrderDetails;
            foreach (OrderHeader header in orderHeaderList)
            {
                Order order = new()
                {
                    OrderHeader = header,
                    OrderDetails = orderDetailList.Where(u => u.OrderHeaderId == header.Id).ToList(),
                };
                OrderFromDb.Add(order);
            }

            //do some Filtering #TODO
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(OrderFromDb);
        }

        public async Task<OrderDTO> GetById(int id)
        {
            Order order = new()
            {
                OrderHeader = _db.OrderHeaders.FirstOrDefault(u => u.Id == id),
                OrderDetails = _db.OrderDetails.Where(u => u.OrderHeaderId == id).ToList(),
            };
            if (order != null)
            {
                return _mapper.Map<Order, OrderDTO>(order);
            }
            return new OrderDTO();

        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccessful(int id)
        {
            var data = await _db.OrderHeaders.FindAsync(id);
            if (data == null)
            {
                return new OrderHeaderDTO();
            }
            if (data.Status == StaticDetails.Status_Pending)
            {
                data.Status = StaticDetails.Status_Confirmed;
                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(data);
            }
            return new OrderHeaderDTO();
        }

        public async Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO orderHeaderDTO)
        {
            if (orderHeaderDTO != null)
            {
                var OrderHeader = _mapper.Map<OrderHeaderDTO, OrderHeader>(orderHeaderDTO);
                _db.OrderHeaders.Update(OrderHeader);
                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(OrderHeader);
            }
            return new OrderHeaderDTO();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var data = await _db.OrderHeaders.FindAsync(orderId);
            if (data == null)
            {
                return false;
            }
            data.Status = status;
            if (status == StaticDetails.Status_Shipped)
            {
                data.ShippingDate = DateTime.Now;
            }

            await _db.SaveChangesAsync();
            return true;

        }
    }
}