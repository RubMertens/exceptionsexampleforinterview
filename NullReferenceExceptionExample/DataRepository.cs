using System.Collections;
using System.Collections.Generic;

namespace NullReferenceExceptionExample
{
    public class DataRepository
    {


        public IEnumerable<Person> GetPeople()
        {
            yield return new Person()
            {
                FirstName = "Jef",
                LastName = "Dunham"
            };
            yield return new Person()
            {
                FirstName = "John",
                LastName = "Beton"
            };
            yield return new Person()
            {
                FirstName = "Samantha",
                LastName = null
            };
        }
    }
}