using Admin.ViewModels;
using Application.DTOs.PostDto;
using Application.Service.Interface.Services.PostServices;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers;

public class PostsController(IPostService postService) : Controller
{
    public async Task<IActionResult> Index() => View((await postService.GetAllActiveAsync()).Select(ToListItem));
    public async Task<IActionResult> Details(string id) => await WithPost(id, post => View(ToDetails(post)));
    public IActionResult Create() => View(new PostFormViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PostFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try 
        {
            var post = await postService.CreateAsync(ToCreateDto(model));
            return RedirectToAction(nameof(Details), new { id = post.Result.Id }); 
        }
        catch (Exception exception)
        {
            ModelState.AddModelError(string.Empty, exception.Message); 
            return View(model); 
        }
    }

    public async Task<IActionResult> Edit(string id) => await WithPost(id, post => View(ToForm(post)));
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, PostFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try { await postService.UpdateAsync(id, ToUpdateDto(model)); return RedirectToAction(nameof(Details), new { id }); }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (Exception exception) { ModelState.AddModelError(string.Empty, exception.Message); return View(model); }
    }

    public async Task<IActionResult> Delete(string id) => await WithPost(id, post => View(ToDetails(post)));
    [HttpPost, ActionName(nameof(Delete)), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        try { await postService.DeleteAsync(id); return RedirectToAction(nameof(Index)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    private async Task<IActionResult> WithPost(string id, Func<PostResponseDto, IActionResult> success)
    {
        try { return success(await postService.GetByIdAsync(id)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }
    private static PostListItemViewModel ToListItem(PostResponseDto post) 
        => new() { Id = post.Id, Content = post.Content, CreatedById = post.CreatedById, CreatedAt = post.CreatedAt };
  
    
    private static PostDetailsViewModel ToDetails(PostResponseDto post) 
        => new() { Id = post.Id, Content = post.Content, CreatedById = post.CreatedById, UpdatedById = post.UpdatedById, DeletedById = post.DeletedById, CreatedAt = post.CreatedAt, UpdatedAt = post.UpdatedAt };
   
    private static PostFormViewModel ToForm(PostResponseDto post) 
        => new() { Content = post.Content, CreatedById = post.CreatedById, UpdatedById = post.UpdatedById };
    
    private static CreatePostDto ToCreateDto(PostFormViewModel model)
        => new() { Content = model.Content, MediaUrl = model.MediaUrl, PostType = model.PostType, PrivacySetting = model.PrivacySetting, CreatedById = model.CreatedById, UpdatedById = model.UpdatedById};
    private static UpdatePostDto ToUpdateDto(PostFormViewModel model)
        => new() { Content = model.Content, MediaUrl = model.MediaUrl, PostType = model.PostType, PrivacySetting = model.PrivacySetting };
}
