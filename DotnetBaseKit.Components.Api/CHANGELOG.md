# Changelog

## [4.0.1] - 2025-05-26

### Added

- Support for `net8.0` target framework.
- New `ResponseConflict` and `ResponseUnprocessableEntity` methods in `ApiControllerBase`.
- Dependency injection of `INotificationMessageFormatter` and `_includeKey` flag in `ResponseFactory`.
- Dependency injection of `INotificationMessageFormatter` in `ApiExtensions`.

### Changed

- Updated `Microsoft.AspNetCore.Mvc` to version `2.3.0`.
- Refactored `MessageResolver` in `ResponseFactory` to avoid code duplication by leveraging `NotificationMessageFormatter`.
