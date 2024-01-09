namespace Application.Books;

public class DeleteBook
{
    public class Command : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
    }

    public class Handler(DataContext context) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context = context;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken ct)
        {
            var book = await _context.Books
               .Include(x => x.BookCopies).ThenInclude(x => x.BookCopiesHistory)
                .Include(x => x.BookCopies).ThenInclude(x => x.Location)
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (book is null) return null;

            _context.Books.Remove(book);

            var result = await _context.SaveChangesAsync(ct) > 0;

            if (!result) return Result<Unit>.Failure("Failed to Delete Product");
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
