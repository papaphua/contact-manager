# Contact Manager

## How to launch
1. Create a migration within the project folder:
```shell
dotnet ef migrations add Initial
```
2. Apply the migration within the project folder:
```shell
dotnet ef database update
```

## Features
1. Filter table rows by any cell value using the search bar.
2. Sort table rows by any column in ascending or descending order (works with filtered rows).
3. Inline row editing and deleting with client-side validation.
4. Import CSV files using the file picker. Example CSVs can be found in the solution folder.

### The CSV import assumes that the first line of the CSV file contains column names and skips it