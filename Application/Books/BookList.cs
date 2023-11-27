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

        public async Task<Result<PagedList<BookDetailDto>>> Handle(Query request, CancellationToken cancellationToken)
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

           

            //if (request.SearchTerm is null)
            //{
            //var searchTerm = request.Params.SearchTerm.ToLower();

            //query = request.Params.OrderBy switch
            //    {
            //        "price" => query.OrderBy(p => p.Price).Where(p => p.Author.Contains(searchTerm) ||  p.Title.Contains(request.Params.SearchTerm)),
            //        "priceDesc" => query.OrderByDescending(p => p.Price),
            //        _ => query.OrderBy(p => p.Author).Where(p => p.Author.Contains(searchTerm) || p.Title.Contains(request.Params.SearchTerm)),
            //    };

            //query.Where(p => p.Title.Contains(request.Title));

            //var dbBooks = await query.ToListAsync(cancellationToken: cancellationToken);

            //var dbBooks = await PagedList<BookDetailDto>.ToPagedList(query, request.Params.PageNumber, request.Params.PageSize);

            //Response.Headers.Add("Pagination", JsonSerializer.SerializeObject(dbBooks.MetaData));



            //}
            //else
            //{
            //    var searchTerm = request.SearchTerm.ToLower();

            //    var searchedQuery = query.Where(p => p.Author.Contains(searchTerm));

            //    var dbBooks = await searchedQuery.ToListAsync(cancellationToken: cancellationToken);
            //    return Result<List<BookDetailDto>>.Success(dbBooks);
            //}
            //var searchTerm = request.SearchTerm.ToUpper().Trim();
        }
    }

}
    