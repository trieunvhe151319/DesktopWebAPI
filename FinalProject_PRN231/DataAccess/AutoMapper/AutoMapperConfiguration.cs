using AutoMapper;
using AutoMapper.Execution;
using BusinessObject.Common;
using BusinessObject.RequestModel.Account;
using BusinessObject.RequestModel.Order;
using BusinessObject.RequestModel.Product;
using BusinessObject.ResponseModel.Account;
using BusinessObject.ResponseModel.Order;
using BusinessObject.ResponseModel.Product;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<RegisterCreationModel, Account> ()
                .ReverseMap();
            CreateMap<RegisterCreationModel, Customer>()
                //.ForMember(des => des.CustomerId, opt => opt.MapFrom($"ACC{DateTime.Now.Ticks}"))
                .ReverseMap();
            CreateMap<Account, LoginResponseModel>()
                .ForMember(des => des.Role, opt => opt.MapFrom(src => src.Role))
                .ReverseMap();
            CreateMap<Account, AccountModel>()
                .ForMember(des => des.Role, opt => opt.MapFrom(src => src.Role))
                .ReverseMap();
            CreateMap<Product, ProductModel>()
                .ReverseMap();
            CreateMap<Product, ProductCreationModel>()
                .ReverseMap();
            CreateMap<Product, ProductDetailModel>()
                .ReverseMap();
            CreateMap<Order, OrderCreationModel>()
                .ReverseMap();
            CreateMap<Order, OrderModel>()
                .ReverseMap();
            CreateMap<OrderDetail, OrderDetailModel>()
                .ForMember(des => des.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ReverseMap();
            CreateMap<OrderDetail, OrderDetailCreationModel>()
                .ReverseMap();
            CreateMap<ProductModel, ProductCreationModel>()
                .ReverseMap();
            CreateMap<ProductDetailModel, ProductCreationModel>()
                .ReverseMap();
            CreateMap<Category, CategoryDisplayModel>()
                .ReverseMap();


        }
    }
}
