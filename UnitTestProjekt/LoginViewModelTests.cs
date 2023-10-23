using SpilPlatform.Mvvm.ViewModels;
using Xunit;

namespace UnitTestProjekt
{
    public class LoginViewModelTests
    {
        [Fact]
        public void CanLogin_WhenUsernameAndPasswordAreNotEmpty_ReturnsTrue()
        {
            // Arrange: Forbered testdata og objekter

            // Opret en instans af LoginViewModel
            var viewModel = new LoginViewModel();

            // Indstil brugernavn og adgangskode med v�rdier
            viewModel.Username = "Admin";
            viewModel.Password = "Admin123";

            // Act: Udf�r handlinger p� objekterne

            // Kald CanLogin-metoden for at afg�re, om brugeren kan logge ind
            bool canLogin = viewModel.CanLogin();

            // Assert: Bekr�ft forventede resultater

            // Kontroller, om CanLogin returnerer true, n�r b�de brugernavn og adgangskode er udfyldt
            Assert.True(canLogin);
        }

        [Fact]
        public void CanLogin_WhenUsernameOrPasswordIsEmpty_ReturnsFalse()
        {
            // Arrange: Forbered testdata og objekter

            // Opret en instans af LoginViewModel
            var viewModel = new LoginViewModel();

            // Indstil brugernavn som "Admin" og adgangskode som tom streng
            viewModel.Username = "Admin";
            viewModel.Password = string.Empty;

            // Act: Udf�r handlinger p� objekterne

            // Kald CanLogin-metoden for at afg�re, om brugeren kan logge ind
            bool canLogin = viewModel.CanLogin();

            // Assert: Bekr�ft forventede resultater

            // Kontroller, om CanLogin returnerer false, n�r enten brugernavn eller adgangskode mangler
            Assert.False(canLogin);
        }

        [Fact]
        public void Authenticate_WithCorrectCredentials_SetsIsAuthenticatedToTrue()
        {
            // Arrange: Forbered testdata og objekter

            // Opret en instans af LoginViewModel
            var viewModel = new LoginViewModel();

            // Indstil brugernavn og adgangskode med korrekte v�rdier
            viewModel.Username = "Admin";
            viewModel.Password = "Admin123";

            // Act: Udf�r handlinger p� objekterne

            // Kald Authenticate-metoden for at bekr�fte brugerens legitimationsoplysninger
            viewModel.Authenticate();

            // Assert: Bekr�ft forventede resultater

            // Kontroller, om IsAuthenticated er blevet indstillet til true, n�r de korrekte legitimationsoplysninger bruges
            Assert.True(viewModel.IsAuthenticated);
        }

        [Fact]
        public void Authenticate_WithIncorrectCredentials_SetsIsAuthenticatedToFalse()
        {
            // Arrange: Forbered testdata og objekter

            // Opret en instans af LoginViewModel
            var viewModel = new LoginViewModel();

            // Indstil brugernavn som "Admin" og adgangskode som en forkert v�rdi
            viewModel.Username = "Admin";
            viewModel.Password = "WrongPassword";

            // Act: Udf�r handlinger p� objekterne

            // Kald Authenticate-metoden for at bekr�fte brugerens legitimationsoplysninger
            viewModel.Authenticate();

            // Assert: Bekr�ft forventede resultater

            // Kontroller, om IsAuthenticated er blevet indstillet til false, n�r de forkerte legitimationsoplysninger bruges
            Assert.False(viewModel.IsAuthenticated);
        }
    }
}
