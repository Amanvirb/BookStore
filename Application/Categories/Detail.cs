namespace Application.Categories;

public class Detail
{
    public class Query : IRequest<Result<CategoryDto>>
    {
        public int Id { get; set; }
    }

    public class Handler(DataContext context, IMapper mapper) : IRequestHandler<Query, Result<CategoryDto>>
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CategoryDto>> Handle(Query request, CancellationToken ct)
        {
            var category = await _context.Categories
                 .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (category == null) return null;

            return Result<CategoryDto>.Success(category);


        }

    }
}
