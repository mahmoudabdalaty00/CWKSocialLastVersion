using Admin.ViewModels;
using Application.Service.DTOs.PostInterActionDto;
using Application.Service.Interface.Services.PostServices;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers;

public class PostInteractionsController(IPostInterActionService postInteractionService) : Controller
{
    public async Task<IActionResult> Index(int postId)
    {
        ViewBag.PostId = postId;
        return View((await postInteractionService.GetAllActiveByPostIdAsync(postId)).Select(ToListItem));
    }
    public async Task<IActionResult> Details(int id) => await WithInteraction(id, interaction => View(ToListItem(interaction)));
    public IActionResult Create(int postId) => View(new PostInteractionFormViewModel { PostId = postId });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PostInteractionFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try { var interaction = await postInteractionService.CreateAsync(new CreatePostInterActionDto { PostId = model.PostId!.Value, ReactionType = model.ReactionType!.Value }); return RedirectToAction(nameof(Details), new { id = interaction.Id }); }
        catch (Exception exception) { ModelState.AddModelError(string.Empty, exception.Message); return View(model); }
    }

    public async Task<IActionResult> Edit(int id) => await WithInteraction(id, interaction => View(new PostInteractionFormViewModel { PostId = interaction.PostId, ReactionType = interaction.ReactionType }));
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PostInteractionFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try { await postInteractionService.UpdateAsync(id, new UpdatePostInterActionDto { ReactionType = model.ReactionType!.Value }); return RedirectToAction(nameof(Details), new { id }); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    public async Task<IActionResult> Delete(int id) => await WithInteraction(id, interaction => View(ToListItem(interaction)));
    [HttpPost, ActionName(nameof(Delete)), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try { await postInteractionService.DeleteAsync(id); return RedirectToAction(nameof(Index)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    private async Task<IActionResult> WithInteraction(int id, Func<PostInterActionResponseDto, IActionResult> success)
    {
        try { return success(await postInteractionService.GetByIdAsync(id)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }
    private static PostInteractionListItemViewModel ToListItem(PostInterActionResponseDto item) => new() { Id = item.Id, PostId = item.PostId, ReactionType = item.ReactionType, CreatedAt = item.CreatedAt };
}
