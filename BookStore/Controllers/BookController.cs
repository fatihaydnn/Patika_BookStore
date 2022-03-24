﻿using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UptadeBook;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.UptadeBook.UptadeBookCommand;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        //https://localhost:44325/Books/3
        public IActionResult GetById(int id)
        {
            GetByIdQuery query = new GetByIdQuery(_context);
            var result = query.Handle(id);

            return Ok(result);

        }

        //[HttpGet]
        ////"https://localhost:44325/Books?id=3
        //public Book Get([FromQuery] string id)
        //{
        //    var book = _context.Books.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // CreateBookModelin içinde yazmış olduğumuz exception'ımızı dönememize yarar!!
            }

            return Ok();


        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
            UptadeBookCommand command = new UptadeBookCommand(_context);

            try
            {
                command.Model = updatedBook;
                command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if (book == null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
