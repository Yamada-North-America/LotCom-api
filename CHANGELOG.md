# `1.0.0` 
### Initial Release
- RESTful endpoints for basic CRUD operations on Print, Scan, Process, and Part databases.
- CI/CD automation.

# `1.0.1`
### Expanded Endpoints
- Part endpoints:
  - `GetAllPrintedBy()`: All Part entities printed by a specific Process.
  - `GetAllScannedBy()`: All Part entities scanned by a specific Process.
  - `Update()`: Updates a Part entity using the provided model.
- Print endpoints:
  - `GetOnDate()`: All Prints created on a specific Date.
  - `GetOnDateByProcess()`: All Prints created on a specific Date by a Process.
  - `Update()`: Updates a Print entity using the provided model.
- Process endpoints:
  - `Update()`: Updates a Process entity using the provided model.
- Scan endpoints:
  - `GetAllWithinRange()`: All Scan entities created within a certain range before the current date.
  - `Update()`: Updates a Scan entity using the provided model.
- CI/CD automation.
- Added a new Serial Feed endpoint that allows universal consumption of JBK and Lot Numbers.

## `1.0.11`
#### `1.0.1` Hotfixes
- Allow access of 0-index Process (dummy Process).
- Remove Stored Procedure access method for Processes (changes are too frequent). 

## `1.0.12`
#### `1.0.1` Hotfixes

# `1.0.2`
### Filter by Serial Number
- Add endpoints to allow retrieval of `Prints` and `Scans` filtered to only entities that contain a specific Serial Number