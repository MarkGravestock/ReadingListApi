# ReadingListApi
TDD ASP.NET5 Web Api Solution

Initially try to test drive API implementation in .NET, but then extended to try SPA technologies. Needs a bit more work to get 
the end-to-end tests to work out of the box.

- Using 
- ASP.NET 5 beta 8
- Xunit for DNX to test drive MVC JSON API (Integration and Unit)
- Raven DB for persistance (in memory for API integration tests)
- Angular/Bootstrap for Javascript SPA UI
- Jasmine/Karma to unit test UI
- Protractor for End-to-End tests.
  - Protractor Gulp task/plugin seems to have problems with Windows paths with spaces
  - Requires web driver to be running already
  - Needs work to get Raven DB configured with end-to-end test data

