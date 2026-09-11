# Implementation Plan - Create Handlers for Post, PostInteraction, and PostComment

Implement MediatR command and query handlers for **Post**, **PostInteraction**, and **PostComment** under `Application/Features/PostsFeatures/`, following the CQRS architecture established in `UserProfiles`.

## User Review Required

> [!IMPORTANT]
> **Key Architectural Question: `int` vs `Guid` ID types**
>
> In Domain (`Post`, `PostComment`, `PostInterAction`) and Database (`Data/Migrations`), all three entities inherit from `BaseEntity<int>` where primary keys (`Id`) and foreign keys (`PostId`) are integers (`int`).
> Likewise, `IPostService`, `IPostCommentService`, `IPostInterActionService`, DTOs (`PostResponseDto`, etc.), and `Admin` MVC controllers all use `int`.
>
> However, the newly added Command/Query classes in `Application/Features/PostsFeatures/` and their corresponding API controllers/contracts in `API` were modeled with `Guid` (copied from `UserProfiles`, where `UserProfile` is `BaseEntity<Guid>`).
>
> **Proposed Fix**:
> We recommend aligning the Command/Query properties and API controllers/contracts for Post, PostComment, and PostInteraction to use `int` for `Id` and `PostId` (while keeping `UserProfileId` as `Guid`).
> This ensures type safety and end-to-end compatibility between the database, service layer, MediatR handlers, and API endpoints.

---

## Proposed Handlers & Structure

All handlers will return `OperationResult<T>` and catch potential exceptions (such as `KeyNotFoundException` or validation exceptions) from the underlying services to return consistent error results to the API controllers.

### 1. Post Handlers (`Application/Features/PostsFeatures/Posts/`)
- **[NEW] `CommandHandlers/CreatePostCommandHandler.cs`**:
  - Handles `CreatePostCommand` -> returns `OperationResult<PostResponseDto>`
  - Invokes `IPostService.CreateAsync(dto)`
- **[NEW] `CommandHandlers/UpdatePostCommandHandler.cs`**:
  - Handles `UpdatePostCommand` -> returns `OperationResult<PostResponseDto>`
  - Invokes `IPostService.UpdateAsync(id, dto)`
- **[NEW] `CommandHandlers/DeletePostCommandHandler.cs`**:
  - Handles `DeletePostCommand` -> returns `OperationResult<PostResponseDto>`
  - Invokes `IPostService.DeleteAsync(id)`
- **[NEW] `QueryHandlers/GetAllPostsQueryHandler.cs`**:
  - Handles `GetAllPostsQuery` -> returns `OperationResult<IEnumerable<PostResponseDto>>`
  - Invokes `IPostService.GetAllActiveAsync()`
- **[NEW] `QueryHandlers/GetPostByIdQueryHandler.cs`**:
  - Handles `GetPostByIdQuery` -> returns `OperationResult<PostResponseDto>`
  - Invokes `IPostService.GetByIdAsync(id)`

### 2. PostComment Handlers (`Application/Features/PostsFeatures/PostComments/`)
- **[NEW] `CommandHandler/CreatePostCommentCommandHandler.cs`**:
  - Handles `CreatePostCommentCommand` -> returns `OperationResult<PostCommentResponseDto>`
  - Invokes `IPostCommentService.CreateAsync(dto)`
- **[NEW] `CommandHandler/UpdatePostCommentCommandHandler.cs`**:
  - Handles `UpdatePostCommentCommand` -> returns `OperationResult<PostCommentResponseDto>`
  - Invokes `IPostCommentService.UpdateAsync(id, dto)`
- **[NEW] `CommandHandler/DeletePostCommentCommandHandler.cs`**:
  - Handles `DeletePostCommentCommand` -> returns `OperationResult<PostCommentResponseDto>`
  - Invokes `IPostCommentService.DeleteAsync(id)`
- **[NEW] `QueriesHandlers/GetPostCommentsByPostIdQueryHandler.cs`**:
  - Handles `GetPostCommentsByPostIdQuery` -> returns `OperationResult<IEnumerable<PostCommentResponseDto>>`
  - Invokes `IPostCommentService.GetAllActiveByPostIdAsync(postId)`

### 3. PostInteraction Handlers (`Application/Features/PostsFeatures/PostInteractions/`)
- **[NEW] `CommandHandler/CreatePostInteractionCommandHandler.cs`**:
  - Handles `CreatePostInteractionCommand` -> returns `OperationResult<PostInterActionResponseDto>`
  - Invokes `IPostInterActionService.CreateAsync(dto)`
- **[NEW] `CommandHandler/UpdatePostInteractionCommandHandler.cs`**:
  - Handles `UpdatePostInteractionCommand` -> returns `OperationResult<PostInterActionResponseDto>`
  - Invokes `IPostInterActionService.UpdateAsync(id, dto)`
- **[NEW] `CommandHandler/DeletePostInteractionCommandHandler.cs`**:
  - Handles `DeletePostInteractionCommand` -> returns `OperationResult<PostInterActionResponseDto>`
  - Invokes `IPostInterActionService.DeleteAsync(id)`
- **[NEW] `QueriesHandlers/GetPostInteractionsByPostIdQueryHandler.cs`**:
  - Handles `GetPostInteractionsByPostIdQuery` -> returns `OperationResult<IEnumerable<PostInterActionResponseDto>>`
  - Invokes `IPostInterActionService.GetAllActiveByPostIdAsync(postId)`

---

## Dependency Injection & Support Adjustments

### [MODIFY] [API/Registers/RepositoryRegister.cs](file:///c:/Users/hamouda/source/repos/CWKSocial/API/Registers/RepositoryRegister.cs)
Register the post-related services in the API container (matching `Admin/Registers/RepositoryRegister.cs`):
```csharp
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostInterActionService, PostInterActionService>();
builder.Services.AddScoped<IPostCommentService, PostCommentService>();
```

### [MODIFY] Commands, Queries, and API Contracts for `int` ID alignment:
- Update `Id` / `PostId` from `Guid` to `int` in:
  - `UpdatePostCommand.cs`, `DeletePostCommand.cs`, `GetPostByIdQuery.cs`
  - `CreatePostCommentCommand.cs`, `UpdatePostCommentCommand.cs`, `DeletePostCommentCommand.cs`, `GetPostCommentsByPostIdQuery.cs`
  - `CreatePostInteractionCommand.cs`, `UpdatePostInteractionCommand.cs`, `DeletePostInteractionCommand.cs`, `GetPostInteractionsByPostIdQuery.cs`
  - Associated API request/response contracts and controllers (`PostController.cs`, `PostCommentController.cs`, `PostInteractionController.cs`).

---

## Verification Plan

### Automated Tests / Builds
- Build `Application.csproj`:
  ```pwsh
  dotnet build Application/Application.csproj
  ```
- Verify all MediatR handlers compile without warnings or errors.
- Build the entire solution once Visual Studio / running API process locks are cleared:
  ```pwsh
  dotnet build CWKSocial.slnx
  ```
