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

            // Indstil brugernavn og adgangskode med værdier
            viewModel.Username = "Admin";
            viewModel.Password = "Admin123";

            // Act: Udfør handlinger på objekterne

            // Kald CanLogin-metoden for at afgøre, om brugeren kan logge ind
            bool canLogin = viewModel.CanLogin();

            // Assert: Bekræft forventede resultater

            // Kontroller, om CanLogin returnerer true, når både brugernavn og adgangskode er udfyldt
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

            // Act: Udfør handlinger på objekterne

            // Kald CanLogin-metoden for at afgøre, om brugeren kan logge ind
            bool canLogin = viewModel.CanLogin();

            // Assert: Bekræft forventede resultater

            // Kontroller, om CanLogin returnerer false, når enten brugernavn eller adgangskode mangler
            Assert.False(canLogin);
        }

        [Fact]
        public void Authenticate_WithCorrectCredentials_SetsIsAuthenticatedToTrue()
        {
            // Arrange: Forbered testdata og objekter

            // Opret en instans af LoginViewModel
            var viewModel = new LoginViewModel();

            // Indstil brugernavn og adgangskode med korrekte værdier
            viewModel.Username = "Admin";
            viewModel.Password = "Admin123";

            // Act: Udfør handlinger på objekterne

            // Kald Authenticate-metoden for at bekræfte brugerens legitimationsoplysninger
            viewModel.Authenticate();

            // Assert: Bekræft forventede resultater

            // Kontroller, om IsAuthenticated er blevet indstillet til true, når de korrekte legitimationsoplysninger bruges
            Assert.True(viewModel.IsAuthenticated);
        }

        [Fact]
        public void Authenticate_WithIncorrectCredentials_SetsIsAuthenticatedToFalse()
        {
            // Arrange: Forbered testdata og objekter

            // Opret en instans af LoginViewModel
            var viewModel = new LoginViewModel();

            // Indstil brugernavn som "Admin" og adgangskode som en forkert værdi
            viewModel.Username = "Admin";
            viewModel.Password = "WrongPassword";

            // Act: Udfør handlinger på objekterne

            // Kald Authenticate-metoden for at bekræfte brugerens legitimationsoplysninger
            viewModel.Authenticate();

            // Assert: Bekræft forventede resultater

            // Kontroller, om IsAuthenticated er blevet indstillet til false, når de forkerte legitimationsoplysninger bruges
            Assert.False(viewModel.IsAuthenticated);
        }
    }
}
