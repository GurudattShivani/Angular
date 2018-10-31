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
    public class TestCompanyDBTest
    {
        [TestMethod]
        public void TestIfCompanyExistOnAdd()
        {
            var mockSet = new Mock<DbSet<Company>>();
            
            var mockContext = new Mock<CompanyDBEntities>();
            mockContext.Setup(m => m.Companies).Returns(mockSet.Object);

            var commpanyContext = new CompanyDBContext(mockContext.Object);
            commpanyContext.AddCompany(new Company() { CompanyId = 1, CompanyName = "Test",
                Email = "test@test.com", LastName = "Testing", PhoneNumber = 123456 });

            mockSet.Verify(m => m.Add(It.IsAny<Company>()), Times.Once());
        }

        [TestMethod]
        public void TestIfCompanyExistOnDelete()
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

            var mockContext = new Mock<CompanyDBEntities>();
            mockContext.Setup(m => m.Companies).Returns(mockSet.Object);
            


            var commpanyContext = new CompanyDBContext(mockContext.Object);
            var comp = commpanyContext.GetAllCompanies().ToList();

            Assert.AreEqual(3, comp.Count);
            Assert.AreEqual(1, comp[0].CompanyId);
            Assert.AreEqual(2, comp[1].CompanyId);
            Assert.AreEqual(3, comp[2].CompanyId);
        }
    }
}
