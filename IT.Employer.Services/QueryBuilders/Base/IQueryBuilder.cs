using System.Linq;

namespace IT.Employer.Services.QueryBuilders.Base
{
    public interface IQueryBuilder<TEnitity>
    {
        IQueryable<TEnitity> Build();
    }
}
