# Changelog

## [Unreleased]

### Added
- Registered `IBaseReadRepository<>` in `MongoExtensions`.
- Registered `IMongoClient` as a singleton for reuse and better testability.
- Added `ConfigureMongoSerialization` method to explicitly configure `GuidSerializer` for BSON.
- Added helper methods in `MongoExtensions` to separate responsibilities: `ConfigureMongoSettings`, `AddMongoClient`, `AddMongoRepositories`.

### Changed
- `BaseReadRepository` and `BaseWriteRepository` now receive `IMongoClient` via dependency injection (instead of creating a new instance directly using `new MongoClient(...)`).
- Refactored `AddMongoDb` method in `MongoExtensions` for better clarity and maintainability.

### Fixed
- Prevents multiple `MongoClient` instantiations by ensuring a single shared instance.
- Fixed missing registration of `IBaseReadRepository<>`, which could impact dependency resolution.

### Deprecated
- No items in this version.

### Removed
- No items in this version.