namespace Application.Locations;

public class LocationDetail
{
    public class Query : IRequest<Result<LocationDto>>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<LocationDto>>
        {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<LocationDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            //if (request == null) return null;

            var location = await _context.Locations
                .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken: cancellationToken);
               

            if (location == null) return null;

            return Result<LocationDto>.Success(location);

        }
    }
}
