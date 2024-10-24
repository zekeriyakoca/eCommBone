using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Queries;

public record PaginationQuery([Range(1,100)]int PageSize = 10, [Range(1,Int32.MaxValue)]int PageIndex = 0);