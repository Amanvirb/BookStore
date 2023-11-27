using Domain;

namespace Application.Locations;

public class LocationList
{
    public class Query : IRequest<Result<List<BookStoreDto>>>
    {

        public class Handler : IRequestHandler<Query, Result<List<BookStoreDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task<Result<List<BookStoreDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var location = await _context.Locations
                .ProjectTo<BookStoreDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);

                if (location == null) return null;

                return Result<List<BookStoreDto>>.Success(location);
            }

        }
    }
}
