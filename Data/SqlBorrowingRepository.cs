using Dapper;
using LibrarySystemManagement.Data.Connection;
using LibrarySystemManagement.Models.Borrowers;

namespace LibrarySystemManagement.Data
{
    public class SqlBorrowingRepository : IBorrowingRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public SqlBorrowingRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void Add(BorrowingBook model)
        {
            var queryAddBorrowingBookById = @"
                INSERT INTO Borrowing 
                (BookInstanceId, ReaderID, BorrowDate, ReturnDate) 
                VALUES 
                (@BookInstanceId, @ReaderID, @BorrowDate, @ReturnDate)";

            _databaseConnection.Connection.Execute(queryAddBorrowingBookById, model);
        }

        public void Delete(int bookId)
        {
            var deleteBorrowingBookById = @"
                DELETE FROM Borrowing 
                WHERE BookInstanceId = @bookIdToDelete";

            _databaseConnection.Connection.Execute(deleteBorrowingBookById, new { bookIdToDelete = bookId });
        }

        public BorrowingBook? Get(int id)
        {
            var getBorrowingBookForIdQuery = @"
                SELECT * FROM Borrowing 
                WHERE BookInstanceId = @id";

            return _databaseConnection.Connection
                .QueryFirstOrDefault<BorrowingBook>(getBorrowingBookForIdQuery, new { id });
        }

        public List<BookBorrowedByReader> GetAllBorrowingBooks()
        {
            var getAllBorrowedBooks = @"
                SELECT 
                    Book.Title, 
                    Book.Author,
                    BookInstance.Year,
                    BookInstance.SerialNumber, 
                    Reader.Name, 
                    Reader.Email, 
                    Borrowing.BorrowDate, 
                    Borrowing.ReturnDate,
                    DATEDIFF(day, GETDATE(), Borrowing.ReturnDate) AS ReturnStatus 
                FROM Borrowing
                JOIN BookInstance ON Borrowing.BookInstanceId = BookInstance.Id
                JOIN Book ON Book.Id = BookInstance.BookId
                JOIN Reader ON Borrowing.ReaderID = Reader.ID
                WHERE Borrowing.ActualReturnDate IS NULL";

            return _databaseConnection.Connection
                .Query<BookBorrowedByReader>(getAllBorrowedBooks)
                .ToList();
        }

        public List<BorrowedBookDetailsViewModel> GetBorrowedBooksByReaderId(int readerId)
        {
            var booksForReaderIdQuery = @"
                SELECT 
                    BookInstance.Id, 
                    Book.Title, 
                    BookInstance.Year, 
                    BookInstance.SerialNumber, 
                    Book.Author, 
                    Category.Name AS Category, 
                    Borrowing.BorrowDate, 
                    Borrowing.ReturnDate
                FROM Borrowing
                INNER JOIN BookInstance ON Borrowing.BookInstanceId = BookInstance.Id
                INNER JOIN Book ON Book.Id = BookInstance.BookId
                INNER JOIN Category ON Book.CategoryId = Category.Id 
                WHERE Borrowing.ReaderId = @readerId 
                  AND Borrowing.ActualReturnDate IS NULL";

            return _databaseConnection.Connection
                .Query<BorrowedBookDetailsViewModel>(booksForReaderIdQuery, new { readerId })
                .ToList();
        }

        public void Update(BorrowingBook model)
        {
            var updateBorrowBookQuery = @"
                UPDATE Borrowing 
                SET ActualReturnDate = @ActualReturnDate
                WHERE BookInstanceId = @BookInstanceId";

            _databaseConnection.Connection.Execute(updateBorrowBookQuery, model);
        }
    }
}
