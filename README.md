# Interview Questions

## Searcher

Run the tests of the SearcherTests project. An exception is currently being thrown. Look at the stacktrace below:
- What kind of an exception is being thrown? 
- What other information is available in the stacktrace? 
- Where should we look to fix the issue?

### The Exception

```cs
System.NullReferenceException : Object reference not set to an instance of an object.
   at Searcher.PersonNameSearcher.<>c__DisplayClass2_0.<Search>b__0(Person p) in D:\InterviewQuestions\Searcher\PersonNameSearcher.cs:line 22
   at System.Linq.Enumerable.WhereEnumerableIterator`1.ToList()
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SearcherTests.PersonSearcherTests.ShouldReturnPeopleWithNames() in D:\InterviewQuestions\SearcherTests\PersonSearcherTests.cs:line 25
```
