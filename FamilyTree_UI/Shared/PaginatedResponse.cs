using FamilyTreeUI.Pages.Shared;

namespace UraCareUI.Shared.UtilityHelpers.Wrapper;

public class PaginatedResponse<T> : Response
{
    public PaginatedResponse(List<T> data)
    {
        Data = data;
    }

    public List<T> Data { get; set; }

    internal PaginatedResponse(bool succeeded, List<T> data = default!, List<string> messages = null!, int count = 0, int page = 1, int pageSize = 10)
    {
        Data = data;
        CurrentPage = page;
        Succeeded = succeeded;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
    }

    public static PaginatedResponse<T> Failure(List<string> messages)
    {
        return new PaginatedResponse<T>(false, default!, messages);
    }

    public static PaginatedResponse<T> Success(List<T> data, int count, int page, int pageSize)
    {
        return new PaginatedResponse<T>(true, data, null!, count, page, pageSize);
    }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int TotalCount { get; set; }
    public int PageSize { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;

    public bool HasNextPage => CurrentPage < TotalPages;
}