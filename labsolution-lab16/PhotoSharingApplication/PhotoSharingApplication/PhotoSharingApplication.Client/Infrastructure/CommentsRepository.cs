using PhotoSharingApplication.Client.Core.Interfaces;
using PhotoSharingApplication.Client.Core.Models;
using System.Net.Http.Json;

namespace PhotoSharingApplication.Client.Infrastructure;

public class CommentsRepository(HttpClient httpClient) : ICommentsRepository
{
    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("/api/comments", comment);
        if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new Exception("Unauthorized");
        }
        if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            //we should read the ProblemDetails from the response and throw a more specific exception
            throw new Exception("Bad Request");
        }
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to add comment");
        }
        return await response.Content.ReadFromJsonAsync<Comment>();
    }

    public async Task DeleteCommentAsync(int id)
    {
        HttpResponseMessage response = await httpClient.DeleteAsync($"/api/comments/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new Exception("Unauthorized");
        }
        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            //we should read the ProblemDetails from the response and throw a more specific exception
            throw new Exception("Bad Request");
        }
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to delete comment");
        }
    }

    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"/api/comments/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null; // Comment with the specified ID was not found
        }

        response.EnsureSuccessStatusCode(); // Ensure the response is successful

        return await response.Content.ReadFromJsonAsync<Comment>();
    }

    public async Task<List<Comment>> GetCommentsForPhotoAsync(int photoId)
    {
        return await httpClient.GetFromJsonAsync<List<Comment>>($"/api/photos/{photoId}/comments");
    }

    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        HttpResponseMessage response = await httpClient.PutAsJsonAsync($"/api/comments/{comment.Id}", comment);
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new Exception("Unauthorized");
        }
        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            //we should read the ProblemDetails from the response and throw a more specific exception
            throw new Exception("Bad Request");
        }
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to update comment");
        }
        return comment;
    }
}
