using Domain;

namespace Application.Books;

public class UpdateBook
{
    public class Command : IRequest<Result<Unit>>
    {
        public UpdateBookDto BookDetail { get; set; }
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
                .Include(x => x.SubCategory)
                .FirstOrDefaultAsync(x => x.Id == request.BookDetail.Id,
                cancellationToken: cancellationToken);

            if (dbBook is null) return null;

            dbBook.Title = request.BookDetail.Title;
            //dbBook.Author = request.BookDetail.Author;
            dbBook.Price = request.BookDetail.Price;
            dbBook.ISBN = request.BookDetail.ISBN;

            if (dbBook.SubCategory.Name != request.BookDetail.SubCategoryName.ToUpper().Trim())
            {
                var subCategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.Name == request.BookDetail.SubCategoryName.ToUpper().Trim(),
               cancellationToken: cancellationToken);

                if (subCategory is null) return Result<Unit>.Failure("SubCategory does not exists, Please create sub category First");

                dbBook.SubCategory = subCategory;
            }
            _context.Entry(dbBook).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to update");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
