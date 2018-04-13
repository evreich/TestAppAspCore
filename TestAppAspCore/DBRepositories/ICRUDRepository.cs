using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.DBRepositories
{
    public interface ICRUDRepository<T> where T:DBModel
    {
        T GetElem(int id);
        void AddElem(T elem);
        void EditElem(T elem);
        void DeleteElem(T elem);
    }
}
