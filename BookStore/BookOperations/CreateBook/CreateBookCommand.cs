using BookStore.DBOperations;
using BookStore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public CreateBookDTO Model { get; set; }

        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book != null)
                throw new InvalidOperationException("Kitap zaten mevcut");  // Return BadRrequest() yerine exception fırlatarak açıklamasını yazıyoruz!!

            book = new Book();  // Once bir entity oluşturup içerisine setledikten sonra o entityyi _dbContextin içerisine vermemiz için 
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        //public class CreateBookModel
        //{
        //    public string Title { get; set; }
        //    public int GenreId { get; set; }
        //    public int PageCount { get; set; }
        //    public DateTime PublishDate { get; set; }
        //}

    }
}
