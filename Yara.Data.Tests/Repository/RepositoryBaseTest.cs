using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NUnit.Framework;
using Yara.Data.Entity.Context;
using Yara.Data.Entity.Repository;
using Yara.Domain.Entities;
using Yara.Domain.Repository;
// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable SuspiciousTypeConversion.Global
#pragma warning disable 618

namespace Yara.Data.Tests.Repository
{
    [TestFixture(typeof(Usuario))]
    public class RepositoryBaseTest<TEntity> where TEntity : class, new()
    {
        private readonly IRepositoryBase<TEntity> _repository;
        readonly IUnitOfWork _unitOfWork;
        private readonly TEntity _entity;

        #region Setup Entities

        const string Nome = "Claudemir";

        public TEntity ObjectBase(bool ativo, string nome)
        {
            var usuario = new Usuario()
            {
                ID = Guid.NewGuid(),
                Ativo = ativo,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.NewGuid(),
                Email = "junior_porfirio@hotmail.com",
                Login = "cpjunior2",
                Nome = nome,
                TipoAcesso = TipoAcesso.AD
            };

            return usuario as TEntity;
        }

        public Expression<Func<TEntity, bool>> Filter(string nome)
        {
            Expression<Func<Usuario, bool>> usuario = usuario1 => usuario1.Nome.Equals(nome);

            return usuario as Expression<Func<TEntity, bool>>;
        }

        #endregion

        public RepositoryBaseTest()
        {
            var dbcontext = new YaraDataContext();
            _repository = new RepositoryBase<TEntity>(dbcontext);

            _entity = ObjectBase(true, Nome);
            _unitOfWork = new UnitOfWork(dbcontext);

        }

        [Test, Order(1)]
        public void Insert()
        {
            //Arrange 
            var entity = _entity;
            //Act
            _repository.Insert(entity);
           var boolInsert =  _unitOfWork.Commit();
           

            //Assert
            Assert.IsTrue(boolInsert);

        }

        [Test, Order(2)]
        public void Update()
        {
            //Arrange
            var entity = _entity;

            //Act
            _repository.Update(entity);
            var boolUpade = _unitOfWork.Commit();
          

            //Assert
            Assert.IsTrue(boolUpade);

        }

        [Test, Order(3)]
        public async Task GetAsync()
        {
            //Arrange 
            var filter = Filter(Nome);
            var entity = _entity as Usuario;
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Act
            var returnOneObj = await _repository.GetAsync(filter) as Usuario;
           

            //Assert
            Assert.IsNotNull(returnOneObj);
            Assert.AreEqual(returnOneObj.Nome, entity.Nome);
        }

        [Test, Order(4)]
        public async Task GetAllAsync()
        {

            //Act
            var returnListObj = await _repository.GetAllAsync();
            var any = returnListObj.Any();
         

            //Assert
            Assert.IsNotNull(returnListObj);
            Assert.IsTrue(any);
        }

        [Test, Order(5)]
        public async Task GetAllFilterAsync()
        {
            //Arrange 
            var filter = Filter(Nome);
            
            //Act
            var returnListObj = await _repository.GetAllFilterAsync(filter);
           
            var any = returnListObj.Any();
           

            //Assert
            Assert.IsNotNull(returnListObj);
            Assert.IsTrue(any);
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}