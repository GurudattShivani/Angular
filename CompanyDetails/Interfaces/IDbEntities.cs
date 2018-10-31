using System.Data.Entity;
using System.Linq;
using CompanyDetails.Models;


namespace CompanyDetails.Interfaces
{
    public interface IDbEntities
    {
        IQueryable<Company> GetAllCompanies();
        void FlushChangestoDB();
        void SetCompanyState(Company company, EntityState state);
        void AddCompany(Company company);
        Company FindCompanyById(int id);
        bool DeleteCompany(Company company);
        bool IsComapnyExistsById(int companyId);
        void DisposeDb();
    }
}