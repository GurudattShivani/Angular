using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CompanyDetails.Models;
using CompanyDetails.Interfaces;


namespace CompanyDetails.Controllers
{    
    public class CompaniesController : ApiController
    {
        private readonly IDbEntities _dbEntities;
        public CompaniesController() : this(new CompanyDBContext())
        {

        }

        /// <summary>
        /// Instantiation
        /// </summary>
        /// <param name="dbEntities"></param>
        public CompaniesController(IDbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        /// <summary>
        /// GET: api/Companies - Get Company Details
        /// </summary>
        /// <returns></returns>
        public IQueryable<Company> GetCompanies()
        {
            return _dbEntities.GetAllCompanies();
        }
        

        /// <summary>
        /// PUT: api/Companies/5 - Modify Company Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompany(int id, Company company)
        {
            if (id != company.CompanyId)
            {
                return BadRequest();
            }

            _dbEntities.SetCompanyState(company, EntityState.Modified);

            try
            {
                _dbEntities.FlushChangestoDB();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// POST: api/Companies - Add New Company Details
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompany(Company company)
        {
            if (!ValidateEmailPhoneAlreadyExists(company))
            {
                _dbEntities.AddCompany(company);
                _dbEntities.FlushChangestoDB();
                return CreatedAtRoute("DefaultApi", new { id = company.CompanyId }, company);
            }
            return CreatedAtRoute("DefaultApi", new { id = company.CompanyId }, new Company());
        }

        /// <summary>
        /// DELETE: api/Companies/5 - Delete Company Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
            Company company = _dbEntities.FindCompanyById(id);
            if (company == null)
            {
                return NotFound();
            }

            _dbEntities.DeleteCompany(company);
            _dbEntities.FlushChangestoDB();

            return Ok(company);
        }

        /// <summary>
        /// Clear the references
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbEntities.DisposeDb();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Validates and returns false if an email and phone number already exists in database
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        private bool ValidateEmailPhoneAlreadyExists(Company company)
        {
            var companies = _dbEntities.GetAllCompanies().ToList();
            if (companies.Any())
            {
                return companies.Exists(x => (string.Compare(x.Email, company.Email, true) == 0 || x.PhoneNumber == company.PhoneNumber));
            }
            return false;
        }
        
        /// <summary>
         /// Check if company exists
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        private bool CompanyExists(int id)
        {
            return _dbEntities.IsComapnyExistsById(id);
        }
    }
}