namespace Application.Books;

public class ShowBookDetail
{
    public class Query : IRequest<Result<BookDetailDto>>
    {
        public int Id { get; set; }
    }
    public class Handler(DataContext context, IMapper mapper) : IRequestHandler<Query, Result<BookDetailDto>>
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<BookDetailDto>> Handle(Query request, CancellationToken ct)
        {
            var bookDetail = await _context.Books
                .Include(x=>x.BookCopies).ThenInclude(x=>x.BookCopiesHistory)
                .Include(x => x.BookCopies).ThenInclude(x => x.Location)
                .ProjectTo<BookDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x =>x.Id==request.Id, ct);

            if (bookDetail is null) return null;

            return Result<BookDetailDto>.Success(bookDetail);
        }
    }
}
