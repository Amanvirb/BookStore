namespace Application.SubCategories;

public class SubCategoryEdit
{
    public class Command : IRequest<Result<Unit>>
    {
        public BookStoreDto SubCategory { get; set; }
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
        
            var subCategory= await _context.SubCategories.FindAsync(new object[] { request.SubCategory.Id }, cancellationToken: cancellationToken);

            if (subCategory == null) return null;

            subCategory.Name = request.SubCategory.Name.ToUpper().Trim();

            _context.Entry(subCategory).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to update category");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
