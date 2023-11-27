namespace Application.Categories;

public class Detail
{
    public class Query : IRequest<Result<CategoryDto>>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<CategoryDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                 .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (category == null) return null;

            return Result<CategoryDto>.Success(category);


        }

    }
}
