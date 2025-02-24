using Xunit;
using Moq;
using FluentAssertions;
using PhotoSharingApplication.Client.Core.Models;
using PhotoSharingApplication.Client.Core.Interfaces;
using PhotoSharingApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApplication.Core.Interfaces;

namespace PhotoSharingApplication.BUnitTests.Controllers;

public class CommentsControllerTests
{
    private readonly Mock<ICommentsService> mockService;
    private readonly CommentsController sut;
    public CommentsControllerTests()
    {
        mockService = new Mock<ICommentsService>();
        sut = new CommentsController(mockService.Object);
    }
    [Fact]
    public async Task GetComment_ReturnsAComment()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        mockService.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync(comment);
        
        // Act
        var result = await sut.GetComment(1);

        // Assert
        result.Value.Should().BeEquivalentTo(comment);
    }

    [Fact]
    public async Task GetComment_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        mockService.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync((Comment)null);

        // Act
        var result = await sut.GetComment(1);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetCommentsByPhotoId_ReturnsComments()
    {
        // Arrange
        var comments = new List<Comment>
        {
            new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" },
            new Comment { Id = 2, PhotoId = 1, Subject = "Test", Body = "Test" }
        };
        mockService.Setup(repo => repo.GetCommentsForPhotoAsync(1)).ReturnsAsync(comments);

        // Act
        var result = await sut.GetCommentsByPhotoId(1);

        // Assert
        result.Should().BeEquivalentTo(comments);
    }

    [Fact]
    public async Task PutComment_WithNonMatchingId_ReturnsBadRequest()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        
        // Act
        var result = await sut.PutComment(2, comment);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task PutComment_WithMatchingId_ReturnsNoContent()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        mockService.Setup(repo => repo.UpdateCommentAsync(comment));
        
        // Act
        var result = await sut.PutComment(1, comment);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task PostComment_ReturnsCreatedAtAction()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        mockService.Setup(repo => repo.AddCommentAsync(comment));
        
        // Act
        var result = await sut.PostComment(comment);

        // Assert
        var createdAtActionResult = result.Result as CreatedAtActionResult;
        createdAtActionResult.ActionName.Should().Be("GetComment");
        createdAtActionResult.RouteValues["id"].Should().Be(comment.Id);
        createdAtActionResult.Value.Should().BeEquivalentTo(comment);
    }

    [Fact]
    public async Task DeleteComment_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        mockService.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync((Comment)null);
        
        // Act
        var result = await sut.DeleteComment(1);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteComment_WithExistentId_ReturnsComment()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        mockService.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync(comment);
        
        // Act
        var result = await sut.DeleteComment(1);

        // Assert
        result.Value.Should().BeEquivalentTo(comment);
    }

    [Fact]
    public async Task DeleteComment_WithExistentId_DeletesComment()
    {
        // Arrange
        var comment = new Comment { Id = 1, PhotoId = 1, Subject = "Test", Body = "Test" };
        mockService.Setup(repo => repo.GetCommentByIdAsync(1)).ReturnsAsync(comment);
        
        // Act
        var result = await sut.DeleteComment(1);

        // Assert
        mockService.Verify(repo => repo.DeleteCommentAsync(1), Times.Once);
    }
}
