namespace Application.Categories;

public class DeleteCategory
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
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, ct);

            if (category == null) return Result<Unit>.Failure("Can not delete because, Category does not exist");

            _context.Categories.Remove(category);

            var result = await _context.SaveChangesAsync(ct) > 0;

            if (!result) return Result<Unit>.Failure("Failed to delete category");

            return Result<Unit>.Success(Unit.Value);

        }
    }
}
