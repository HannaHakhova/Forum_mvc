using System.Threading.Tasks;
using FORUM.BLL.DTO;
using FORUM.BLL.Infrastructure;
using FORUM.BLL.Services;
using FORUM.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace FORUM.BLL.UnitTests
{
    
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public async Task CreateUser_EmailNull_ShouldReturnFalseInOperationDetails()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            UserService userService = new UserService(mock.Object);
            UserDTO userDto = new UserDTO() { Email = null, Id = "id", Password = "123Qwerty", UserName = "User", Role = "User"};
            var expectedOpDet = new OperationDetails(false, "", "");

            //Act
            var actualOpDet = await userService.Create(userDto);

            //Assert
            Assert.AreEqual(expectedOpDet.Succeeded, actualOpDet.Succeeded, "True OperationDetails.Succeded result occurred");
        }
    }
}
