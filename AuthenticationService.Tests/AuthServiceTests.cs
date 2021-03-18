using Model.CustomException;
using Model.Interface;
using Model.Service;
using NUnit.Framework;
using System.Data;

namespace AuthenticationService.Tests
{
    public class AuthServiceTests
    {
        private readonly IFakeAuthService _authService = new FakeAuthService();

        [Test]
        public void When_wuu_1234_Should_Login_Success()
        {
            _authService.SetDataTable(GetDataTableWithErrorCode(0));
            Assert.True(_authService.Login("wuu", "1234"));
        }

        private DataTable GetDataTableWithErrorCode(int errorCode)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("ErrorCode", typeof(int));
            dataTable.Rows.Add(errorCode);
            return dataTable;
        }

        [Test]
        public void When_wuu_1235_Should_Login_Fail()
        {
            _authService.SetDataTable(GetDataTableWithErrorCode(1));

            Assert.False(_authService.Login("wuu", "1235"));
        }

        [Test]
        public void When_username_is_empty_should_throw_exception()
        {
            Assert.Throws<InvalidParameterException>(() => _authService.Login("", "1234"));
        }

        [Test]
        public void When_password_is_empty_should_throw_exception()
        {
            Assert.Throws<InvalidParameterException>(() => _authService.Login("wuu", ""));
        }
    }

    internal interface IFakeAuthService : IAuthService
    {
        void SetDataTable(DataTable dataTable);
    }

    internal class FakeAuthService : AuthService, IFakeAuthService
    {
        private DataTable _dataTable;

        public void SetDataTable(DataTable dataTable)
        {
            _dataTable = dataTable;
        }

        public override DataTable GetDbTable(string username, string password)
        {
            return _dataTable;
        }
    }
}