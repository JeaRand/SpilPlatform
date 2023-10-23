using SpilPlatform.Mvvm.Models;
using SpilPlatform.Mvvm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProjekt
{
    public class SpilViewModelTests
    {
        [Fact]
        public void SpilViewModel_SetAndGetSpilProperty()
        {
            // Arrange: Forbered testdata og objekter

            // Opret en instans af SpilViewModel
            var viewModel = new SpilViewModel();

            // Opret det forventede Spil-objekt
            var expectedSpil = new Spil
            {
                Titel = "Test Spil",
                Beskrivelse = "Dette er en testbeskrivelse.",
                Link = "https://example.com/testgame",
            };

            // Act: Udfør handlinger på objekterne

            // Indstil Spil-egenskaben i viewModel med det forventede Spil-objekt
            viewModel.Spil = expectedSpil;

            // Hent det faktiske Spil-objekt fra viewModel
            var actualSpil = viewModel.Spil;

            // Assert: Bekræft forventede resultater

            // Sammenlign det forventede Spil-objekt med det faktiske Spil-objekt for lighed
            Assert.Equal(expectedSpil, actualSpil);
        }
    }
}



