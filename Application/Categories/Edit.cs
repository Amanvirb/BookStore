namespace Application.Categories;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public BookStoreDto Category { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken ct)
        {
        
            var category= await _context.Categories.FindAsync(new object[] { request.Category.Id }, ct);

            if (category == null) return null;

            category.Name = request.Category.Name.ToUpper().Trim();

            _context.Entry(category).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync(ct) > 0;

            if (!result) return Result<Unit>.Failure("Failed to update category");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
