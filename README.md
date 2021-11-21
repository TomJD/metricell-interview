# Metricell Interview (Stack: React + .NET Core + SQLite)

## The problem
Create a list, presented in the browser, which will read the contents of the Employees table existing in
the SQLite database included in the base project.

Add functionality so that you can add, remove and modify the items of this list.

Additionally, write the following SQL queries and present their results in the front end, ensuring the
front end is updated every time the data in the database is modified:
* Increment the field Value by 1 where the field Name begins with ‘E’ and by 10 where Name
begins with ‘G’ and all others by 100
* List the sum of all Values for all Names that begin with A, B or C but only present the data where
the summed values are greater than or equal to 11171

## Built With
* [React JS](https://reactjs.org/)
* [ASP.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1)
* [SQLite](https://www.sqlite.org/index.html)
* [EF Core 3.1](https://docs.microsoft.com/en-us/ef/core/)

## Additional Features
1. Filter Employees List
2. EF Core 3.1 Implementation
3. Add Service Layer
4. Dependency Injection

## Installation
1. Clone the repo
   ```sh
   git clone https://github.com/TomJD/metricell-interview.git
   ```
2. Run the project

