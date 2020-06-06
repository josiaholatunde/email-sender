using System.Collections.Generic;

namespace EmailSender.Repositories
{
    public interface IDapperRepository<T>
    {
        T GetEntity(int id);

        IEnumerable<T> GetEntityList();


        T InsertEntityIntoDb(T parameters);

        T GetEntityByPropertyName(string propertyName, string searchValue);


        T UpdateEntity(T parameters);


    }
}