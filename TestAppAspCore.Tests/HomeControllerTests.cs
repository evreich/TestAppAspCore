using Moq;
using System;
using System.Collections.Generic;
using TestAppAspCore.Controllers;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Models;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.ViewModels;
using System.Linq;

namespace TestAppAspCore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task CheckCorrectBooksForPage2Async()
        {
            Mock<IBooksRepository> mockBooksRepository = new Mock<IBooksRepository>();
            Mock<IGenresRepository> mockGenresRepository = new Mock<IGenresRepository>();

            #region testGenres and testBooks
            var testGenres = new List<Genre>
            {
                new Genre
                {
                    Id = 1,
                    Title = "Genre1",
                    Books = new List<Book>()
                },
                new Genre
                {
                    Id = 2,
                    Title = "Genre2",
                    Books = new List<Book>()
                },
                new Genre
                {
                    Id = 3,
                    Title = "Genre3",
                    Books = new List<Book>()
                }
            };

            var testBooks = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Book1",
                    Author = "Author1",
                    Genre = testGenres[0],
                    DateCreating = DateTime.Now
                },
                new Book
                {
                    Id = 2,
                    Title = "Book2",
                    Author = "Author2",
                    Genre = testGenres[1],
                    DateCreating = DateTime.Now
                },
                new Book
                {
                    Id = 3,
                    Title = "Book3",
                    Author = "Author1",
                    Genre = testGenres[2],
                    DateCreating = DateTime.Now
                },
                new Book
                {
                    Id = 4,
                    Title = "Book4",
                    Author = "Author0",
                    Genre = testGenres[0],
                    DateCreating = DateTime.Now
                },
                new Book
                {
                    Id = 5,
                    Title = "Book5",
                    Author = "Author5",
                    Genre = testGenres[2],
                    DateCreating = DateTime.Now
                },
                new Book
                {
                    Id = 6,
                    Title = "Book6",
                    Author = "Author1",
                    Genre = testGenres[1],
                    DateCreating = DateTime.Now
                }
            };
            #endregion 

            mockBooksRepository.Setup(m => m.GetAllBooks()).ReturnsAsync(testBooks);
            mockGenresRepository.Setup(m => m.GetAllGenres()).Returns(testGenres);

            HomeController controller = new HomeController(mockBooksRepository.Object, mockGenresRepository.Object, 5);
            var controllerResult = (ViewResult) await controller.Index(null, 2);
            var resBooksOnPage = controllerResult.ViewData.Model as IndexBookViewModel;
            
            //test PageViewModel
            Assert.NotNull(resBooksOnPage);
            Assert.Equal(2, resBooksOnPage.PageViewModel.PageNumber);
            Assert.False(resBooksOnPage.PageViewModel.HasNextPage);
            Assert.True(resBooksOnPage.PageViewModel.HasPreviousPage);
            Assert.Equal(2,resBooksOnPage.PageViewModel.TotalPages);
            Assert.Empty(resBooksOnPage.SearchExpr);

            //test returning books
            Assert.NotNull(resBooksOnPage.Books);
            Assert.True(resBooksOnPage.Books.Count == 1);
            Assert.Equal(testBooks.ElementAt(testBooks.Count-1).Id, resBooksOnPage.Books[0].Id);
        }
    }
}
