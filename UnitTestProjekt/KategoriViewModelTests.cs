using SpilPlatform.Mvvm.ViewModels;
using Xunit;

namespace UnitTestProjekt
{
    public class KategoriViewModelTests
    {
        [Fact]
        public void KategoriViewModel_KategorierContainsExpectedCategories()
        {
            // Arrange: Forbered testdata og objekter

            // Opret en instans af KategoriViewModel
            var viewModel = new KategoriViewModel();

            // Forventede kategorier
            var expectedKategorier = new List<KategoriModel>
            {
                new KategoriModel("0-1 Klasse"),
                new KategoriModel("2-3 Klasse"),
                new KategoriModel("4-5 Klasse"),
                new KategoriModel("6-7 Klasse")
            };

            // Act: Udfør handlinger på objekterne

            // Hent den faktiske liste af kategorier fra viewModel og konverter den til en liste
            var actualKategorier = viewModel.Kategorier.ToList();

            // Assert: Bekræft forventede resultater

            // Kontroller, om antallet af forventede kategorier svarer til antallet af faktiske kategorier
            Assert.Equal(expectedKategorier.Count, actualKategorier.Count);

            // Sammenlign hver forventet kategori med den tilsvarende faktiske kategori
            for (int i = 0; i < expectedKategorier.Count; i++)
            {
                Assert.Equal(expectedKategorier[i].Navn, actualKategorier[i].Navn);
            }
        }
    }
}
