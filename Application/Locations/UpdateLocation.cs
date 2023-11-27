namespace Application.Locations;

public class UpdateLocation
{
    public class Command : IRequest<Result<Unit>>
    {
        public BookStoreDto Location { get; set; }
    }
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {

            var location = await _context.Locations.FindAsync(new object[] { request.Location.Id }, 
                cancellationToken: cancellationToken);

            if (location == null) return null;
            
            location.Name=request.Location.Name.ToUpper().Trim();

            _context.Entry(location).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to update Location");

            return Result<Unit>.Success(Unit.Value);

        }
    }
}
