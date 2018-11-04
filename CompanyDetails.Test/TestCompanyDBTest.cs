using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using CompanyDetails.Models;
using System.Linq;
using System.Collections.Generic;

namespace CompanyDetails.Test
{
    [TestClass]
    public class TestCompanyDBTest    {        

        [TestMethod]
        public void TestGetCompanies()
        {
            var mockSet = GenerateMock();
            var mockContext = new Mock<CompanyDBEntities>();
            mockContext.Setup(m => m.Companies).Returns(mockSet.Object);
            var companyContext = new CompanyDBContext(mockContext.Object);

            var comp = companyContext.GetAllCompanies().ToList();            

            Assert.AreEqual(3, comp.Count);
            Assert.AreEqual(1, comp[0].CompanyId);
            Assert.AreEqual(2, comp[1].CompanyId);
            Assert.AreEqual(3, comp[2].CompanyId);
        }

        [TestMethod]
        public void TestIfCompanyExistById()
        {
            var mockSet = GenerateMock();
            var mockContext = new Mock<CompanyDBEntities>();
            mockContext.Setup(m => m.Companies).Returns(mockSet.Object);
            var companyContext = new CompanyDBContext(mockContext.Object);

            var comp = companyContext.GetCompanyById(1);
            var findComp = companyContext.FindCompanyById(1);

            Assert.AreEqual(1, findComp.CompanyId);
            Assert.AreEqual(1, comp.Count);
            Assert.AreEqual(1, comp[0].CompanyId);
        }

        [TestMethod]
        public void TestIfCompanyExistOnAdd()
        {
            var mockSet = new Mock<DbSet<Company>>();            
            var mockContext = new Mock<CompanyDBEntities>();
            mockContext.Setup(m => m.Companies).Returns(mockSet.Object);

            var companyContext = new CompanyDBContext(mockContext.Object);
            companyContext.AddCompany(new Company() { CompanyId = 1, CompanyName = "Test",
                Email = "test@test.com", LastName = "Testing", PhoneNumber = 123456 });
            mockSet.Verify(m => m.Add(It.IsAny<Company>()), Times.Once());
        }

        [TestMethod]
        public void TestIfDetailsUpdated()
        {
            var mockSet = GenerateMock();
            var mockContext = new Mock<CompanyDBEntities>();
            mockContext.Setup(m => m.Companies).Returns(mockSet.Object);
            var companyContext = new CompanyDBContext(mockContext.Object);

            //Get a company by Id
            var firstComp = companyContext.FindCompanyById(1);            
            firstComp.CompanyName = "Updated Company";            

            //Get a list of companies
            var comp = companyContext.GetCompanyById(1);
            Assert.AreEqual("Updated Company", comp[0].CompanyName); //Updated data           
        }

        [TestMethod]
        public void TestOnDelete()
        {
            var mockSet = GenerateMock();
            var mockContext = new Mock<CompanyDBEntities>();
            mockContext.Setup(m => m.Companies).Returns(mockSet.Object);
            var companyContext = new CompanyDBContext(mockContext.Object);

            companyContext.DeleteCompany(companyContext.FindCompanyById(1));
            mockSet.Verify(m => m.Remove(It.IsAny<Company>()), Times.Once());
        }

        public Mock<DbSet<Company>> GenerateMock()
        {
            var mockSet = new Mock<DbSet<Company>>();
            var data = new List<Company>
            {
                new Company { CompanyId = 1 },
                new Company { CompanyId = 2 },
                new Company { CompanyId = 3 },
            }.AsQueryable();

            mockSet.As<IQueryable<Company>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Company>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Company>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Company>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;            
        }
    }    
}
