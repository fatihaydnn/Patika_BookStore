using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.UptadeBook
{
    public class UptadeBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookModel Model { get; set; }
        public UptadeBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(int id)
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == id);

            if (book == null)
                throw new NullReferenceException("Düzenlenilecek kitap bulunamadı!!!");

            // GenreId nin boş olup olmadığına bakıyoruz. Eğer boş değilse kendi değerini kullan(book.GenreId) demek.
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
