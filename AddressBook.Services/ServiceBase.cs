using System;
using System.Threading.Tasks;
using AddressBook.Contracts.Shared;
using AddressBook.Models;

namespace AddressBook.Services
{
    public abstract class ServiceBase
    {
        protected readonly DataContext Context;
        protected ServiceBase(DataContext context)
        {
            Context = context;
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
            // returns changes saved to db
            if (await this.Context.SaveChangesAsync() > 0)
            {
                return this.Success("Success");
            }

            return this.Failure("Error on saving in db");

        }

        // Creates new entity with date created set.
        protected TResult CreateNewEntity<TResult>()
            where TResult : EntityBase, new()
        {
            var result = new TResult();
            result.DateCreated = DateTime.UtcNow;

            return result;
        }
    }
}