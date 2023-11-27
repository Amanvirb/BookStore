namespace Application.Locations;

public class DeleteLocation
{
    public class Command : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
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
            var location = await _context.Locations.FindAsync(new object[] { request.Id },
                cancellationToken: cancellationToken);

            if (location is null) return null;

            _context.Locations.Remove(location);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Location can not be deleted");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
