using Moq;
using Xunit;
using UserService.DTO.User;
using UserService.Models; 
using Microsoft.Extensions.Logging;
using UserService.Services.UserService;

namespace UserService.UnitTests; 

public class ProfileServiceTests
{
    [Fact]
    public async Task UpdateProfileAsync_WithValidDto_UpdatesUserAndReturnsTrue()
    {
        var user = new User { Id = 1, FirstName = "Old", LastName = "Name" };
        var repoMock = new Mock<IUserRepository>();
        repoMock.Setup(r => r.GetUserByIdAsync(1)).ReturnsAsync(user);
        repoMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

        var logger = Mock.Of<ILogger<ProfileService>>();
        var service = new ProfileService(repoMock.Object, logger);

        var dto = new UpdateProfileDto { FirstName = "New", LastName = "User" };
        
        var result = await service.UpdateProfileAsync(1, dto);
        
        Assert.True(result);
        Assert.Equal("New", user.FirstName);
        Assert.Equal("User", user.LastName);
    }
}