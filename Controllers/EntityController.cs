using Microsoft.AspNetCore.Mvc;
using GrapheneCore.Database;
using GrapheneCore.Entities;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace GrapheneCore.Controllers;

[ApiController]
[Route("api")]
public class EntityController : ControllerBase
{
    private readonly GrapheneCoreDbContext DbContext;

    public EntityController(GrapheneCoreDbContext dbContext)
    {
        DbContext = dbContext;
    }

    [HttpGet(Name = "Entity")]
    [Route("{entity}")]
    public IResult Get(string entity)
    {
        var query = HttpContext.Request.Query;
        var queryable = DbContext.GetSet<dynamic>(entity);
        if (queryable == null)
        {
            return Results.Ok(entity);
        }
        var queryableFiltered = ApplyFilters(queryable, query);
        return Results.Ok(queryableFiltered.ToList());
    }

    private IQueryable<dynamic> ApplyFilters(IQueryable<dynamic> queryable, IQueryCollection query)
    {
        foreach (var param in query)
        {
            var key = param.Key;
            var value = param.Value.ToString();
            // Remove all non letters and numbers chars from the value
            var cleanedValue = Regex.Replace(value ?? string.Empty, @"[^a-zA-Z0-9]", "");
            var fortmattedValue = key.Contains(".")
                ? $"(\"{cleanedValue}\")"
                : $"\"{cleanedValue}\"";
            System.Console.WriteLine(key + fortmattedValue);
            queryable = queryable.Where(key + fortmattedValue);
            System.Console.WriteLine(queryable.ToQueryString());
        }
        return queryable;
    }
}
