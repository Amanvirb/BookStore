namespace Application.Categories;

public class List
{
    public class Query : IRequest<Result<List<CategoryDto>>>
    {
        public class Handler : IRequestHandler<Query, Result<List<CategoryDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task<Result<List<CategoryDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories
                    .Include(x => x.SubCategories)
                  .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                  .ToListAsync(cancellationToken: cancellationToken);

                if (category.Count < 0) return null;

                return Result<List<CategoryDto>>.Success(category);
            }
        }
    }
}
