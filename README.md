# Interview Questions

## Searcher

An exception is currently being thrown. Look at the stacktrace below:
- What kind of an exception is being thrown? 
- What other information is available in the stacktrace? 
- Where should we look to fix the issue?
- Fix the issue

### The Exception

```cs
System.NullReferenceException : Object reference not set to an instance of an object.
   at Searcher.PersonNameSearcher.<>c__DisplayClass2_0.<Search>b__0(Person p) in D:\InterviewQuestions\Searcher\PersonNameSearcher.cs:line 22
   at System.Linq.Enumerable.WhereEnumerableIterator`1.ToList()
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at SearcherTests.PersonSearcherTests.ShouldReturnPeopleWithNames() in D:\InterviewQuestions\SearcherTests\PersonSearcherTests.cs:line 25
```

## GameReport

### Requirements
Run the tests of the GameReportTests project. We have a couple of failing requirements. Try to locate the problem of the `CreateReportForGame_PlayerWithoutScore_IsIncludedInReport` test and get it green.

### Code review
- The `ScoreRepository` class has a vulnerability. Which one and how can we fix it.
- The `GameBuilder` class uses a `HashSet` and a `List`, why? There is also a `participantMap` variable, how does it work? Should we improve the `Player` class to enforce our design?

### Identical names
A new requirement has been introduced: players can have identical names. If this is the case the score should still be calculated correctly and their technical id needs to be included in the report. The test `CreateReportForGame_PlayersWithIdenticalName_CorrectlyAssignScore` expresses this new requirement.
