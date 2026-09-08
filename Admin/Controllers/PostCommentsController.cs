using Admin.ViewModels;
using Application.Service.DTOs.PostCommentDto;
using Application.Service.Interface.Services.PostServices;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers;

public class PostCommentsController(IPostCommentService postCommentService) : Controller
{
    public async Task<IActionResult> Index(int postId)
    {
        ViewBag.PostId = postId;
        return View((await postCommentService.GetAllActiveByPostIdAsync(postId)).Select(ToListItem));
    }
    public async Task<IActionResult> Details(int id) => await WithComment(id, comment => View(ToListItem(comment)));
    public IActionResult Create(int postId) => View(new PostCommentFormViewModel { PostId = postId });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PostCommentFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try { var comment = await postCommentService.CreateAsync(new CreatePostCommentDto { PostId = model.PostId!.Value, Text = model.Text, UserProfileId = model.UserProfileId!.Value }); return RedirectToAction(nameof(Details), new { id = comment.Id }); }
        catch (Exception exception) { ModelState.AddModelError(string.Empty, exception.Message); return View(model); }
    }

    public async Task<IActionResult> Edit(int id) => await WithComment(id, comment => View(new PostCommentFormViewModel { PostId = comment.PostId, Text = comment.Text, UserProfileId = comment.UserProfileId }));
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PostCommentFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try { await postCommentService.UpdateAsync(id, new UpdatePostCommentDto { Text = model.Text }); return RedirectToAction(nameof(Details), new { id }); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    public async Task<IActionResult> Delete(int id) => await WithComment(id, comment => View(ToListItem(comment)));
    [HttpPost, ActionName(nameof(Delete)), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try { await postCommentService.DeleteAsync(id); return RedirectToAction(nameof(Index)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    private async Task<IActionResult> WithComment(int id, Func<PostCommentResponseDto, IActionResult> success)
    {
        try { return success(await postCommentService.GetByIdAsync(id)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }
    private static PostCommentListItemViewModel ToListItem(PostCommentResponseDto item) => new() { Id = item.Id, PostId = item.PostId, Text = item.Text, UserProfileId = item.UserProfileId, CreatedAt = item.CreatedAt };
}
