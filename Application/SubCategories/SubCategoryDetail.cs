namespace Application.SubCategories;

public class SubCategoryDetail
{
    public class Query : IRequest<Result<SubCategoryBookListDto>>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<SubCategoryBookListDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<SubCategoryBookListDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var subCategory = await _context.SubCategories
                 .ProjectTo<SubCategoryBookListDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (subCategory is null) return Result<SubCategoryBookListDto>.Failure("Category Does not exist");

            return Result<SubCategoryBookListDto>.Success(subCategory);


        }

    }
}
