namespace Application.Categories;

public class List
{
    public class Query : IRequest<Result<List<CategoryDto>>>
    {
        public class Handler(DataContext context, IMapper mapper) : IRequestHandler<Query, Result<List<CategoryDto>>>
        {
            private readonly DataContext _context = context;
            private readonly IMapper _mapper = mapper;

            public async Task<Result<List<CategoryDto>>> Handle(Query request, CancellationToken ct)
            {
                var category = await _context.Categories
                    .Include(x => x.SubCategories)
                  .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                  .ToListAsync(ct);

                if (category.Count < 0) return null;

                return Result<List<CategoryDto>>.Success(category);
            }
        }
    }
}
