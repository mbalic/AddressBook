using System;
using System.Threading.Tasks;
using AddressBook.API.Data;
using AddressBook.API.Helpers;
using AddressBook.API.Models;
using AutoMapper;

namespace AddressBook.API.Services
{
    public abstract class ServiceBase
    {
        protected readonly DataContext Context;
        protected readonly IMapper _mapper;

        protected ServiceBase(DataContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }

        public ServiceResult Success()
        {
            return new ServiceResult
            {
                Success = true,
                Message = "Action completed"
            };
        }

        public ServiceResult<T> Success<T>(T data)
        {
            return new ServiceResult<T>
            {
                Success = true,
                Message = "Action completed",
                Data = data
            };
        }

        public ServiceResult<T> Success<T>(T data, string message)
        {
            return new ServiceResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public ServiceResult Failure(string message)
        {
            return new ServiceResult
            {
                Success = false,
                Message = message
            };
        }

        public ServiceResult<T> Failure<T>(string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message
            };
        }

        public ServiceResult<T> Failure<T>(T data, string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                Data = data
            };
        }

        public async Task<ServiceResult> SaveAll()
        {
            try
            {
                // returns changes saved to db
                if (await this.Context.SaveChangesAsync() > 0)
                {
                    return this.Success("Success");
                }

                return this.Failure("Error on saving in db");
            }
            catch (Exception ex)
            {
                return this.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Creates new entity with corresponding session and date created set.
        /// </summary>
        /// <typeparam name="TResult">Entity of type <see cref="EntityBase"/></typeparam>
        /// <returns>Created entity</returns>
        protected TResult CreateNewEntity<TResult>()
            where TResult : EntityBase, new()
        {
            var result = new TResult();
            result.DateCreated = DateTime.UtcNow;

            return result;
        }
    }
}