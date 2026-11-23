using Factory.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace Factory.Application.Queries.Organization;

public class OrganizationQuery : DataBaseQuery<OrganizationDto, EntityDto>
{
    protected override string _queryString => @"
        SELECT
            Id,
            Name,
            Address,
            ContactNumber
        FROM Organizations";

    public OrganizationQuery(IConfiguration config) : base(config) { }
}