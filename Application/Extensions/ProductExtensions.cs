namespace Application.Extensions;
public static class ProductExtensions
{
    public static IQueryable<BookDetailDto> Sort(this IQueryable<BookDetailDto> query, string orderBy)
    {
        if (orderBy == null) return query.OrderBy(p => p.Title);
        query = orderBy switch
        {
            "price" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Author),
        };
        return query;
    }
    public static IQueryable<BookDetailDto> Search(this IQueryable<BookDetailDto> query, string searchTerm)
    {
        if (searchTerm == null) return query;

        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

        return query.Where(p => p.Author.ToLower().Contains(lowerCaseSearchTerm));

    }

    //public static IQueryable<BookDetailDto> Filter(this IQueryable<BookDetailDto> query, string brands, string types)
    //{
    //    var brandList = new List<string>();
    //    var typeList = new List<string>();

    //    if (!string.IsNullOrEmpty(brands))
    //    {
    //        brandList.AddRange(brands.ToLower().Split(',').ToList());
    //    }

    //    if (!string.IsNullOrEmpty(types))
    //    {
    //        typeList.AddRange(types.ToLower().Split(',').ToList());
    //    }

    //    query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.Brand.ToLower()));
    //    query = query.Where(p => typeList.Count == 0 || brandList.Contains(p.Type.ToLower()));

    //    return query;
    //}

}
