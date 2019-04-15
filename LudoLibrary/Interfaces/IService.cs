using System.Collections.Generic;

namespace LudoLibrary.Interfaces
{
    public interface IService<T> where T : class
    {
        void Add(T entry);
        void Update(int id, T data);
        void Delete(int id);
        void Clear();

        T Get(int id);

        IList<T> GetAll();
    }
}