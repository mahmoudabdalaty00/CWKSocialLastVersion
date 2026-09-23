using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Responses;

public class APIResponse
{
    public string Result { get; set; }
    public string? Msg { get; set; }
    public object? Data { get; set; }
}

public class APIResponse<T>
{
    public string Result { get; set; }
    public string? Msg { get; set; }
    public T? Data { get; set; }
}

public enum APIStatus
{
    [Display(Name = "Approved")] Approved = 1,
    [Display(Name = "Pending")] Pending = 2,
    [Display(Name = "Canceled")] Canceled = 3,
    [Display(Name = "Reject")] Reject = 4,
    [Display(Name = "Failed")] Failed = 5,
    [Display(Name = "Success")] Success = 6,
    [Display(Name = "Already Approved")] AlreadyApproved = 8,
    [Display(Name = "Expired")] Expired = 9,
    [Display(Name = "Not Exist")] NotExist = 10,
}

public class PostResponse
{
    public string Result { get; set; }
    public bool hasNotification { get; set; }
    public bool HasStories { get; set; }
    public bool ExistsAnyStories { get; set; }
    public bool AssignToMe { get; set; }
    public bool HasNextPage { get; set; }
    public int PageSize { get; set; }
    public string? Msg { get; set; }
    public object? Data { get; set; }
   

}

public class ActionResponse
{
    public string Result { get; set; }
    public bool HasNextPage { get; set; }
    public string? Msg { get; set; }
    public object? Data { get; set; }
}

public class ActionResponseLMSCourses
{
    public bool HasNextPage { get; set; }
     public string? CategoryName { get; set; }
    public object? Data { get; set; }
}

public class EventResponse<T>
{
    public string Result { get; set; }
    public bool SeeMore { get; set; }
    public string? Msg { get; set; }
    public T? Data { get; set; }
}

#region

public class NewApiResponse<T>
{
    public string Result { get; set; }
    public bool HasNextPage { get; set; }
    public string? Msg { get; set; }
    public T? Data { get; set; }
}

public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public bool HasNextPage { get; set; }
}

public class ServiceResponse<T>
{
    public string Result { get; set; }
    public string? Msg { get; set; }
    public bool HasNextPage { get; set; }
    public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
}

#endregion