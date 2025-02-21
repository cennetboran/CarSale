﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using CarSale.Data.Entities;
using CarSale.Infrastructure;
using CarSale.Models;

namespace CarSale.Controllers
{
    
    public class CarController : BaseController<Car>
    {
        public CarController(
            IRepository<Car> repo,
            IMapper mapper,
            ICurrentUserService currentUserService) : base(repo, mapper, currentUserService)
        { }
        [Authorize]
        [HttpGet("my")]
        public async Task<ApiResponse<List<Car>>> GetMyCars([FromQuery] CarPagingRequestModel req)
        {
            var paging = new Paging
            {
                Column = req.Column,
                Page = req.Page,
                Size = req.Size,
                SortBy = req.SortBy,
            };
            Expression<Func<Car, bool>> filterExpression = entity => true;
            if (req.Price > 0)
            {
                Expression<Func<Car, bool>> priceExp = x => x.Price == req.Price;

                filterExpression = Expression.Lambda<Func<Car, bool>>(
                    Expression.AndAlso(filterExpression.Body, priceExp.Body),
                    filterExpression.Parameters
                );
            }

            if (!string.IsNullOrEmpty(req.Brand))
            {
                Expression<Func<Car, bool>> brandExp = x => x.Brand.Contains(req.Brand);
                filterExpression = Expression.Lambda<Func<Car, bool>>(
                 Expression.AndAlso(filterExpression.Body, brandExp.Body),
                 filterExpression.Parameters
             );
            }

            if (!string.IsNullOrEmpty(req.Color))
            {
                Expression<Func<Car, bool>> colorExp = x => x.Color.Contains(req.Color);
                filterExpression = Expression.Lambda<Func<Car, bool>>(
                 Expression.AndAlso(filterExpression.Body, colorExp.Body),
                 filterExpression.Parameters
             );
            }
            if (!string.IsNullOrEmpty(req.Model))
            {
                Expression<Func<Car, bool>> modelEx = x => x.Model.Contains(req.Model);
                filterExpression = Expression.Lambda<Func<Car, bool>>(
                 Expression.AndAlso(filterExpression.Body, modelEx.Body),
                 filterExpression.Parameters
             );
            }
            Expression<Func<Car,bool>> userExp = x => x.UserId == _currentUserService.Id;
            return await _repository.GetAsync(paging, filterExpression, x => x.CarFeature);

        }

        [HttpGet]
        public async Task<ApiResponse<List<Car>>> GetxAll([FromQuery] CarPagingRequestModel req)
        {
            var paging = new Paging
            {
                Column = req.Column,
                Page = req.Page,
                Size = req.Size,
                SortBy = req.SortBy,
            };

            Expression<Func<Car, bool>> filterExpression = entity => true;
            if (req.Price > 0)
            {
                Expression<Func<Car, bool>> priceExp = x => x.Price == req.Price;

                filterExpression = Expression.Lambda<Func<Car, bool>>(
                    Expression.AndAlso(filterExpression.Body, priceExp.Body),
                    filterExpression.Parameters
                );
            }

            if (!string.IsNullOrEmpty(req.Brand))
            {
                Expression<Func<Car, bool>> brandExp = x => x.Brand.Contains(req.Brand);
                filterExpression = Expression.Lambda<Func<Car, bool>>(
                 Expression.AndAlso(filterExpression.Body, brandExp.Body),
                 filterExpression.Parameters
             );
            }

            if (!string.IsNullOrEmpty(req.Color))
            {
                Expression<Func<Car, bool>> colorExp = x => x.Color.Contains(req.Color);
                filterExpression = Expression.Lambda<Func<Car, bool>>(
                 Expression.AndAlso(filterExpression.Body, colorExp.Body),
                 filterExpression.Parameters
             );
            }
            if (!string.IsNullOrEmpty(req.Model))
            {
                Expression<Func<Car, bool>> modelEx = x => x.Model.Contains(req.Model);
                filterExpression = Expression.Lambda<Func<Car, bool>>(
                 Expression.AndAlso(filterExpression.Body, modelEx.Body),
                 filterExpression.Parameters
             );
            }

            return await _repository.GetAsync(paging, filterExpression, x => x.CarFeature);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<Car>> GetCarById([FromQuery] int id)
        {
            return new ApiResponse<Car>
            {
                Result = await _repository.GetByIdAsync(id),
                Message = "Car get"
            };
        }

        [Authorize]
        [HttpPost]
        public async Task<ApiResponse<Car>> AddCar(AddCarRequestModel req)
        {
            var carToAdd = _mapper.Map<Car>(req);
            carToAdd.UserId = _currentUserService.Id;
            var car = await _repository.AddAsync(carToAdd);

            return new ApiResponse<Car> { Message = "Car added", Result = car };
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteCar([FromQuery] int id)
        {
            var car = await _repository.GetByIdAsync(id);
            if(car.UserId != _currentUserService.Id){
                return StatusCode(400, new ApiResponse<bool>{Result= false, Message= "You can't delete a car that doesn't belongs to you" });
            }
            var delete = await _repository.DeleteAsync(id);
            return StatusCode(200,new ApiResponse<bool> { Result = delete, Message = "Car deleted" });
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ApiResponse<Car>>> UpdateCar(AddCarRequestModel req, [FromQuery] int id)
        {
            var car = await _repository.GetByIdAsync(id);
            if(car.UserId != _currentUserService.Id){
                return StatusCode(400, new ApiResponse<Car>{Result= car, Message= "You can't update a car that doesn't belongs to you" });
            }
            var updatedCar = await _repository.UpdateAsync(_mapper.Map<Car>(req), id);
            return StatusCode(200,new ApiResponse<Car> { Result = updatedCar, Message = "Car updated" });
        }
    }
}