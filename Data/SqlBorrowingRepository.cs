﻿using Dapper;
using LibrarySystemManagement.Data.Connection;
using LibrarySystemManagement.Models;

namespace LibrarySystemManagement.Data
{
    public class SqlBorrowingRepository : IBorrowingRepository
    {
        private IDatabaseConnection _databaseConnection;
        public SqlBorrowingRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void Add(BorrowingBook model)
        {
            var queryAddBorrowingBookById = @"INSERT INTO Borrowing (BookID, ReaderID, BorrowDate, ReturnDate) 
                                            VALUES (@BookID, @ReaderID, @BorrowDate, @ReturnDate)";

            _databaseConnection.Connection.Execute(queryAddBorrowingBookById, model);
        }

        public void Delete(int bookId)
        {
            var deleteBorrowingBookById = "DELETE FROM Borrowing WHERE BookID = @bookIdToDelete";

            _databaseConnection.Connection.Execute(deleteBorrowingBookById, new { bookIdToDelete = bookId });
        }

        public BorrowingBook Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<BookBorrowedByReader> GetAllBorrowingBooks()
        {
            string getAllBorrowedBooks = @"SELECT Book.ID, Book.Title, Reader.Name, Reader.Email, Borrowing.BorrowDate, Borrowing.ReturnDate
                                        FROM Borrowing
                                        JOIN Book ON Borrowing.BookID = Book.ID
                                        JOIN Reader ON Borrowing.ReaderID = Reader.ID";

            return _databaseConnection.Connection.Query<BookBorrowedByReader>(getAllBorrowedBooks).ToList();
        }

        public IEnumerable<BorrowedBookDetailsViewModel> GetAllBorrowingBooksByReaderId(int readerId)
        {
            string booksForReaderIdQuery = @"SELECT Book.Id, Book.Title, Book.Year, Book.Author, Category.Name AS Category, Borrowing.BorrowDate, Borrowing.ReturnDate
                                           FROM Borrowing
                                           INNER JOIN 
                                           Book ON Borrowing.BookID = Book.ID
                                           INNER JOIN 
                                           Category ON Book.CategoryID = Category.Id 
                                           WHERE Borrowing.ReaderId = @readerId;";

            return _databaseConnection.Connection.Query<BorrowedBookDetailsViewModel>(booksForReaderIdQuery, new { readerId }).ToList();
        }

        public void Update(BorrowingBook model)
        {
            throw new NotImplementedException();
        }
    }
}
