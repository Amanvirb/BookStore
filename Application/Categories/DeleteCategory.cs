namespace Application.Categories;

public class DeleteCategory
{
    public class Command : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
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
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken);

            if (category == null) return Result<Unit>.Failure("Can not delete because, Category does not exist");

            _context.Categories.Remove(category);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to delete category");

            return Result<Unit>.Success(Unit.Value);

        }
    }
}
