using BookStore.Common;
using BookStore.DBOperations;
using BookStore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.GetBooks
{
    public class GetByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public GetByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GetByIdQueryDTO Handle(int id)
        {
            var book = _dbContext.Books.Where(x => x.Id == id).SingleOrDefault();

            GetByIdQueryDTO vm = new GetByIdQueryDTO();
            vm.Title = book.Title;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM//yyyy");
            vm.PageCount = book.PageCount;
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            return vm;
        }
    }

    //public class GetByIdViewModel
    //{
    //    public string Title { get; set; }
    //    public int PageCount { get; set; }
    //    public string PublishDate { get; set; }
    //    public string Genre { get; set; }
    //}

}
