using Admin.ViewModels;
using Application.Service.DTOs.UserProfileDto;
using Application.Service.Interface.Services.UserProfileServices;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers;

public class UserProfilesController(IUserProfileService userProfileService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var result = await userProfileService.GetAllAsync();
        if (result.IsError) return Problem("Unable to load user profiles.");
        return View(result.Result!.Select(profile => new UserProfileListItemViewModel { Id = profile.Id, FullName = $"{profile.FirstName} {profile.LastName}", EmailAddress = profile.EmailAddress, CurrentCity = profile.CurrentCity, CreatedAt = profile.CreatedAt }));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var result = await userProfileService.GetByIdAsync(id);
        return result.IsError ? NotFound() : View(ToDetails(result.Result!));
    }

    public IActionResult Create() => View(new UserProfileFormViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserProfileFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await userProfileService.CreateAsync(ToCreateDto(model));
        if (result.IsError) return ValidationError(result.Errors.Select(error => error.Message), model);
        return RedirectToAction(nameof(Details), new { id = result.Result!.Id });
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await userProfileService.GetByIdAsync(id);
        return result.IsError ? NotFound() : View(ToForm(result.Result!));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UserProfileFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await userProfileService.UpdateAsync(id, ToUpdateDto(model));
        if (result.IsError) return ValidationError(result.Errors.Select(error => error.Message), model);
        return RedirectToAction(nameof(Details), new { id });
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await userProfileService.GetByIdAsync(id);
        return result.IsError ? NotFound() : View(ToDetails(result.Result!));
    }

    [HttpPost, ActionName(nameof(Delete)), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var result = await userProfileService.DeleteAsync(id);
        return result.IsError ? NotFound() : RedirectToAction(nameof(Index));
    }

    private IActionResult ValidationError(IEnumerable<string> errors, UserProfileFormViewModel model)
    {
        foreach (var error in errors) ModelState.AddModelError(string.Empty, error);
        return View(model);
    }

    private static UserProfileDetailsViewModel ToDetails(UserProfileResponseDto profile) => new() { Id = profile.Id, IdentityUserId = profile.IdentityUserId, FullName = $"{profile.FirstName} {profile.LastName}", DateOfBirth = profile.DateOfBirth, Bio = profile.Bio, Phone = profile.Phone, EmailAddress = profile.EmailAddress, CurrentCity = profile.CurrentCity, CreatedAt = profile.CreatedAt };
    private static UserProfileFormViewModel ToForm(UserProfileResponseDto profile) => new() { IdentityUserId = profile.IdentityUserId, FirstName = profile.FirstName, LastName = profile.LastName, DateOfBirth = profile.DateOfBirth, Bio = profile.Bio, Phone = profile.Phone, EmailAddress = profile.EmailAddress, CurrentCity = profile.CurrentCity };
    private static CreateUserProfileDto ToCreateDto(UserProfileFormViewModel model) => new() { IdentityUserId = model.IdentityUserId, FirstName = model.FirstName, LastName = model.LastName, DateOfBirth = model.DateOfBirth!.Value, Bio = model.Bio, Phone = model.Phone, EmailAddress = model.EmailAddress, CurrentCity = model.CurrentCity };
    private static UpdateUserProfileDto ToUpdateDto(UserProfileFormViewModel model) => new() { FirstName = model.FirstName, LastName = model.LastName, DateOfBirth = model.DateOfBirth!.Value, Bio = model.Bio, Phone = model.Phone, EmailAddress = model.EmailAddress, CurrentCity = model.CurrentCity };
}
