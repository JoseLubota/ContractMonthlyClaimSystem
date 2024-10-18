using System;
using System.Data.SqlClient;
using Xunit;
using Moq;
using ContractMonthlyClaimSystem.Models;
namespace TestProject
{
    public class LoginModelTests
    {
        [Fact]
        public void SelectUser_ValidCredentials_ReturnsUserId()
        {
            var email = "jslubota@gmail.com";
            var password = "0000";
            var expectedUser = 1;

            var mockConnection = new Mock<SqlConnection>();
            var mockCommand = new Mock<SqlCommand>();

            mockCommand.Setup(cmd => cmd.ExecuteScalar()).Returns(null);
            mockConnection.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);

            var loginModel = new LoginModel();
            var userID = loginModel.SelectUser(email, password);

            Assert.Equal(expectedUser, userID);
        }

    }

}

