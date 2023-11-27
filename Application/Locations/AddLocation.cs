namespace Application.Locations;

public class AddLocation
{
    public class Command : IRequest<Result<Unit>>
    {
        public string Name { get; set; }
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
            var location = await _context.Locations.FirstOrDefaultAsync(x => x.Name == request.Name.ToUpper().Trim(),
                cancellationToken: cancellationToken);

            if (location is not null) return Result<Unit>.Failure("Location already exists");

            var newLocation = new Location
            {
                Name = request.Name.ToUpper().Trim(),
            };

            _context.Locations.Add(newLocation);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            if (!result) return Result<Unit>.Failure("Failed to create Location");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
