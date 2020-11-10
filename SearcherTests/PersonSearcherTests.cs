using System;
using System.Linq;
using NullReferenceExceptionExample;
using NUnit.Framework;

namespace SearcherTests
{
    public class PersonSearcherTests
    {

        private PersonNameSearcher _searcher;

        [SetUp]
        public void Setup()
        {
            _searcher = new PersonNameSearcher(new DataRepository());
        }


        [Test]
        public void ShouldReturnPeopleWithNames()
        {
            var fragment = "Bet";

            var results = _searcher.Search(fragment).ToList();
            Assert.That(results, Has.Count.EqualTo(1));
            Assert.That(results.Single().FirstName, Is.EqualTo("John"));
            Assert.That(results.Single().LastName, Is.EqualTo("Beton"));


        }
    }
}