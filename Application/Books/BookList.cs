using Application.Extensions;
using System.Collections.Immutable;

namespace Application.Books;

public class BookList
{
    public class Query : IRequest<Result<PagedList<BookDetailDto>>>
    {
        public ProductParams Params { get; set; }
    }
    public class Handler : IRequestHandler<Query, Result<PagedList<BookDetailDto>>>
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<BookDetailDto>>> Handle(Query request, CancellationToken ct)
        {
            var query = _context.Books
                .Include(x => x.SubCategory)
                .Include(x => x.BookCopies).ThenInclude(x => x.BookCopiesHistory)
                .Include(x => x.BookCopies).ThenInclude(x => x.Location)
                .ProjectTo<BookDetailDto>(_mapper.ConfigurationProvider)
                .Sort(request.Params.OrderBy)
                .Search(request.Params.SearchTerm)
                //.Filter()
                .AsQueryable();

            return Result<PagedList<BookDetailDto>>.Success(
                await PagedList<BookDetailDto>.CreateAsync(query, request.Params.PageNumber,
                request.Params.PageSize)
            );
        }
    }

}
