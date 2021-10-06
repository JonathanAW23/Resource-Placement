using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Client.Repository.Interface
{
    //T adalah Entity dan x adalah key dari setiap model
    public interface IRepository<T, X>
        where T : class
    {
        Task<List<T>> Get();
        Task<T> Get(X key);
        HttpStatusCode Post(T entity);
        HttpStatusCode Put(X key, T entity);
        HttpStatusCode Delete(X key);
    }
}
