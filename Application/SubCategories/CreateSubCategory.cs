namespace Application.SubCategories;

public class CreateSubCategory
{
    public class Command : IRequest<Result<Unit>>
    {
        public SubCategoryDto Category { get; set; }
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
                 .Include(x => x.SubCategories)
                .FirstOrDefaultAsync(x => x.Name == request.Category.Category.ToUpper().Trim(),
            cancellationToken: cancellationToken);

            if (existingCategory is null) return Result<Unit>.Failure("Category does not exist, First create category and then create it's subcategory");

            var existingSubCategory = await _context.SubCategories.FirstOrDefaultAsync(x =>x.Name==request.Category.SubCategory,
                cancellationToken: cancellationToken);

            if (existingSubCategory is not null) return Result<Unit>.Failure("SubCategory already exists");

            var newSubCategory = new SubCategory
            {
                Category = existingCategory,
                Name = request.Category.SubCategory.ToUpper().Trim()
            };

            _context.SubCategories.Add(newSubCategory);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to create category");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}

