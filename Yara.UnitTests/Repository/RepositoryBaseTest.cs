using System;
using Moq;
using NUnit.Framework;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.UnitTests.Repository
{
    [TestFixture(typeof(Usuario))]
    public class RepositoryBaseTest<T> where T:class
    {
        private Moq.Mock<IRepositoryBase<T>> _repositorybase;

        public RepositoryBaseTest()
        {
            _repositorybase = new Mock<IRepositoryBase<T>>();
        }

     [Test]
        public void SaveRepositoryBase(T Object)
        {
            _repositorybase.Object.Insert(Object);
        }
        
    }
}