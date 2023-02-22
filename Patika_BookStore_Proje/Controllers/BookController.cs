using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Patika_BookStore_Proje.BookOperations.CreateBook;
using Patika_BookStore_Proje.BookOperations.GetBookById;
using Patika_BookStore_Proje.BookOperations.GetBooks;
using Patika_BookStore_Proje.BookOperations.UpdateBook;
using Patika_BookStore_Proje.DBOperations;

namespace Patika_BookStore_Proje.AddControllers{
    
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase{

        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context){
            _context = context;
        }
        

        [HttpGet]
        public IActionResult GetBooks(){
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            BookViewModel result;
            try
            {
                GetBookByIdQuery query = new GetBookByIdQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok(result);
        }

        // GetById FromQuery

        // [HttpGet]
        // public Book GetById([FromQuery] int id){
        //     var book = BookList.Where(x => x.Id == id).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){

            CreateBookCommand command = new CreateBookCommand(_context);

            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook){

            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){

            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if(book is null) 
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }

    }
}