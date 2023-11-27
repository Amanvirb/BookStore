namespace Application.Categories;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public BookStoreNameDto Category { get; set; }
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
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(x => x.Name == request.Category.Name.ToUpper().Trim(),
            cancellationToken: cancellationToken);

            if (existingCategory is not null) return Result<Unit>.Failure("Category already exists");

            var newCategory = new Category
            {
                Name = request.Category.Name.ToUpper().Trim()
            };

            _context.Categories.Add(newCategory);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to create category");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}

