using System;
using System.Data.Entity;
using System.Linq;
using CompanyDetails.Interfaces;
using System.Collections.Generic;

namespace CompanyDetails.Models
{    
    public class CompanyDBContext : IDbEntities
    {
        private readonly CompanyDBEntities companyDbEntities;

        public CompanyDBContext() : this(new CompanyDBEntities())
        {

        }
        public CompanyDBContext(CompanyDBEntities cDBEntities)
        {
            companyDbEntities = cDBEntities;
        }
        public IQueryable<Company> GetAllCompanies()
        {
            return companyDbEntities.Companies;
        }
        public void AddCompany(Company company)
        {
            companyDbEntities.Companies.Add(company);
        }
        public Company FindCompanyById(int id)
        {
            return companyDbEntities.Companies.SingleOrDefault(x => x.CompanyId == id);
        }

        public List<Company> GetCompanyById(int id)
        {
            return companyDbEntities.Companies.Where(x => x.CompanyId == id).ToList();
        }
        public bool DeleteCompany(Company company)
        {
            var successFlag = false;
            try
            {
                companyDbEntities.Companies.Remove(company);
                successFlag = true;
            }
            catch (Exception ex)
            {
                //do some logging
                return false;
            }
            return successFlag;
        }
        public bool IsCompanyExistsById(int companyId)
        {
            return companyDbEntities.Companies.Count(e => e.CompanyId == companyId) > 0;
        }
        public void FlushChangestoDB()
        {
            companyDbEntities.SaveChanges();
        }
        public void SetCompanyState(Company company, EntityState state)
        {
            companyDbEntities.Entry(company).State = state;
        }
        public void DisposeDb()
        {
            companyDbEntities.Dispose();
        }
    }
}