using BookStore.Common;
using BookStore.DBOperations;
using BookStore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<GetBooksQueryDTO> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<GetBooksQueryDTO> vm = new List<GetBooksQueryDTO>();
            foreach (var book in bookList)
            {
                vm.Add(new GetBooksQueryDTO
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                    PageCount = book.PageCount
                });
            }
            return vm;
        }

    }


    //public class BookViewModel
    //{
    //    public string Title { get; set; }
    //    public int PageCount { get; set; }
    //    public string PublishDate { get; set; }
    //    public string Genre { get; set; }
    //}

}
