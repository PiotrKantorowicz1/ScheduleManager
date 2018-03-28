using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Repositories;
using Manager.Struct.EF;
using Manager.Struct.Services;
using Moq;
using Xunit;

namespace Passenger.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task when_calling_get_async_and_user_does_not_exist_it_should_invoke_user_repository_get_async()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var scheduleRepositoryMock = new Mock<IScheduleRepository>();
            var attendeeRepositoryMock = new Mock<IAttendeeRepository>();
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var UnitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object, scheduleRepositoryMock.Object, activityRepositoryMock.Object, 
                attendeeRepositoryMock.Object, UnitOfWorkMock.Object, mapperMock.Object);
                
            await userService.GetByEmailAsync("piotr2@gmail.com");
            
            userRepositoryMock.Setup(x => x.GetByEmailAsync("piotr2@gmail.com"))
                              .ReturnsAsync(() => null);

            userRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once);
        }
    }
}