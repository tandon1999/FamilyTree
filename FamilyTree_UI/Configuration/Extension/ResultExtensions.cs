using System.Text.Json.Serialization;
using System.Text.Json;
using UraCareUI.Shared.UtilityHelpers.Wrapper;
using FamilyTreeUI.Pages.Shared;

namespace FamilyTree_UI.Configuration.Extension
{
    internal static class ResultExtensions
    {
        internal static async Task<IResponse<T>> ToResult<T>(this HttpResponseMessage response)
        {
            try
            {
                var responseAsString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonSerializer.Deserialize<Response<T>>(responseAsString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    });
                    return responseObject!;
                }
                else if ((int)response.StatusCode == 401)
                    return await Response<T>.FailAsync("Unauthorized Access. Please Login and Try again");
                else if ((int)response.StatusCode == 403)
                    return await Response<T>.FailAsync("You are not authorized for this request");
                else
                {
                    var Error = JsonSerializer.Deserialize<Response>(responseAsString);
                    if ((int)response.StatusCode == 500)
                        return await Response<T>.FailAsync(Error!.Messages);
                    else if ((int)response.StatusCode == 400)
                        return await Response<T>.FailAsync(Error!.Messages);
                    else
                        return await Response<T>.FailAsync("Error occured. Please contact administrator.");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        internal static async Task<IResponse> ToResult(this HttpResponseMessage response)
        {
            try
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonSerializer.Deserialize<Response>(responseAsString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    });
                    return responseObject!;
                }
                else if ((int)response.StatusCode == 401)
                    return await Response.FailAsync("Unauthorized Access. Please login and try again.");
                else if ((int)response.StatusCode == 403)
                    return await Response.FailAsync("You are not authorized for this request");
                else
                {
                    var Error = JsonSerializer.Deserialize<Response>(responseAsString);
                    if ((int)response.StatusCode == 500)
                        return await Response.FailAsync(Error!.Messages);
                    else if ((int)response.StatusCode == 400)
                        return await Response.FailAsync(Error!.Messages);
                    else
                        return await Response.FailAsync("Error occured. Please contact administrator.");
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        internal static async Task<PaginatedResponse<T>> ToPaginatedResult<T>(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<PaginatedResponse<T>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return responseObject!;
        }
    }
}
