namespace Application.SubCategories;

public class DeleteSubCategory
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
            var subCategory = await _context.SubCategories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken);

            if (subCategory == null) return Result<Unit>.Failure("Can not delete because, SubCategory does not exist");

            _context.SubCategories.Remove(subCategory);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to delete category");

            return Result<Unit>.Success(Unit.Value);

        }
    }
}
