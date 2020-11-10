using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NullReferenceExceptionExample
{
    public class PersonNameSearcher
    {

        private DataRepository _repository;

        public PersonNameSearcher(DataRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<Person> Search(string searchFragment)
        {
            return _repository
                .GetPeople()
                .Where(p => p.FirstName.Contains(searchFragment) || p.LastName.Contains(searchFragment));
        }
    }
}