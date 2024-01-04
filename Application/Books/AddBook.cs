namespace Application.Books;

public class AddBook
{
    public class Command : IRequest<Result<Unit>>
    {
        public AddBooksDto BookDetail { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {

            var dbBook = await _context.Books
                .Include(b => b.BookCopies).ThenInclude(c => c.Location)
                .Include(b => b.BookCopies).ThenInclude(c => c.BookCopiesHistory)
                .Include(b => b.Author)
                .Include(b => b.Series)
                .FirstOrDefaultAsync(x => x.ISBN == request.BookDetail.ISBN,
                cancellationToken: cancellationToken);

            bool result;

            if (dbBook is null)
            {
                var subCategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.Name == request.BookDetail.SubCategoryName.ToUpper().Trim(),
                cancellationToken: cancellationToken);

                if (subCategory is null) return Result<Unit>.Failure("SubCategory does not exists, Please create sub category First");

                var location = await _context.Locations.FirstOrDefaultAsync(x => x.Name == request.BookDetail.Location.ToUpper().Trim(),
                    cancellationToken: cancellationToken);

                if (location is null) return Result<Unit>.Failure("Location does not exists, Please create location first");

                var author = await _context.Authors.FirstOrDefaultAsync(x => x.Name == request.BookDetail.Author,
                    cancellationToken: cancellationToken);

                if (author is null)
                {
                    author = new Author
                    {
                        Name = request.BookDetail.Author,
                        //DOB = request.BookDetail.Dob,
                        //Country= request.BookDetail.Country,
                    };

                    _context.Authors.Add(author);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                var series = await _context.Series.FirstOrDefaultAsync(x => x.Name == request.BookDetail.Series,
                    cancellationToken: cancellationToken);

                if (series is null)
                {
                    series = new Series
                    {
                        Name = request.BookDetail.Series,
                    };

                    _context.Series.Add(series);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                var newBook = new BookDetail
                {
                    Title = request.BookDetail.Title,
                    Author = author,
                    Series = series,
                    Price = request.BookDetail.Price,
                    ISBN = request.BookDetail.ISBN,
                    SubCategory = subCategory,
                };

                _context.Books.Add(newBook);
                await _context.SaveChangesAsync(cancellationToken);

                List<BookCopiesHistory> bookCopiesHistories = new()
                {
                    new BookCopiesHistory
                    {
                        DateTime = DateTime.Now,
                        Price= request.BookDetail.Price,
                        Copies = request.BookDetail.NumberOfCopies,
                        ActionType = ActionTypeEnum.Entered
                    }
                };

                var bookNewCopies = new BookCopies
                {
                    BookDetail = newBook,
                    Location = location,
                    BookCopiesHistory = bookCopiesHistories
                };

                _context.BookCopies.Add(bookNewCopies);
                result = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Cannot add new Book");

            }
            else
            {
                var oldBookCopy = dbBook.BookCopies
                    .FirstOrDefault(x => x.Location.Name == request.BookDetail.Location.ToUpper().Trim());

                if (oldBookCopy is not null)
                {

                    oldBookCopy.BookCopiesHistory.Add(new BookCopiesHistory
                    {
                        DateTime = DateTime.Now,
                        Price = request.BookDetail.Price,
                        Copies = request.BookDetail.NumberOfCopies,
                        ActionType = ActionTypeEnum.Entered
                    });

                    result = await _context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Cannot Modify copy");

                }
                else
                {
                    List<BookCopiesHistory> bookCopiesHistories =
                    [
                        new BookCopiesHistory
                        {
                            DateTime = DateTime.Now,
                            Price = request.BookDetail.Price,
                            Copies = request.BookDetail.NumberOfCopies,
                            ActionType = ActionTypeEnum.Entered
                        }
                    ];

                    var bookNewCopies = new BookCopies
                    {
                        BookDetail = dbBook,
                        Location = new Location { Name = request.BookDetail.Location.ToUpper().Trim() },
                        BookCopiesHistory = bookCopiesHistories
                    };
                    _context.BookCopies.Add(bookNewCopies);

                    result = await _context.SaveChangesAsync(cancellationToken) > 0;

                    if (!result) return Result<Unit>.Failure("Cannot add new Book on new location");
                }
            }
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
