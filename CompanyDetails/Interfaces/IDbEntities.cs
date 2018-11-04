using System.Data.Entity;
using System.Linq;
using CompanyDetails.Models;
using System.Collections.Generic;

namespace CompanyDetails.Interfaces
{
    public interface IDbEntities
    {
        IQueryable<Company> GetAllCompanies();
        void FlushChangestoDB();
        void SetCompanyState(Company company, EntityState state);
        void AddCompany(Company company);
        Company FindCompanyById(int id);
        List<Company> GetCompanyById(int id);

        bool DeleteCompany(Company company);
        bool IsCompanyExistsById(int companyId);
        void DisposeDb();
    }
}