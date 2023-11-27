using Application.Core;
using Domain;

namespace Application.SoldDefectiveBooks;

public class SoldDefectiveBooks
{
    public class Command : IRequest<Result<Unit>>
    {
        public SoldBooksDto SoldBooks { get; set; }
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

            var dbBookCopy = await _context.BookCopies
                .Include(b => b.BookCopiesHistory)
                .Include(b => b.BookDetail)
               .FirstOrDefaultAsync(x => x.BookDetailId == request.SoldBooks.Id,
                cancellationToken: cancellationToken);

            if (dbBookCopy is null) return Result<Unit>.Failure("BookCopy does not exist");

            dbBookCopy.BookCopiesHistory.Add(new BookCopiesHistory
            {
                DateTime = DateTime.Now,
                Price = request.SoldBooks.Price,
                Copies = request.SoldBooks.Copies,
                ActionType = request.SoldBooks.ActionType == "SoldOut" ? ActionTypeEnum.SoldOut : ActionTypeEnum.Defective
            });

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            if (!result) return Result<Unit>.Failure("Cannot add SoldBooks");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
