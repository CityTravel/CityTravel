using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CityTravel.Domain.Abstract;
using CityTravel.Domain.Entities;
using Moq;

namespace CityTravel.Tests.Domain.Repository
{
    /// <summary>
    /// The fake repository.
    /// </summary>
    /// <typeparam name="T">
    /// type of entity in repository
    /// </typeparam>
    public static class FakeRepository<T>
        where T : BaseEntity, new()
    {
        #region Public Methods and Operators

        /// <summary>
        /// The mock.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        /// <returns>
        /// Mocked repository interface
        /// </returns>
        public static IProvider<T> Mock(IDbSet<T> entities)
        {
            var mockRepository = new Mock<IProvider<T>>();
            mockRepository.Setup(x => x.All()).Returns(entities.AsQueryable());

            mockRepository.Setup(x => x.Contains(It.IsAny<Expression<Func<T, bool>>>())).Returns(
                (Expression<Func<T, bool>> expr) => entities.AsQueryable().FirstOrDefault(expr) != null);

            mockRepository.Setup(x => x.Count).Returns(entities.Count());

            mockRepository.Setup(x => x.Add(It.IsAny<T>())).Callback((T entity) => entities.Add(entity));

            mockRepository.Setup(x => x.Delete(It.IsAny<T>())).Callback((T entity) => entities.Remove(entity));

            mockRepository.Setup(x => x.Filter(It.IsAny<Expression<Func<T, bool>>>())).Returns(
                (Expression<Func<T, bool>> expr) => entities.AsQueryable().Where(expr).AsEnumerable());

            mockRepository.Setup(x => x.Find(It.IsAny<Expression<Func<T, bool>>>())).Returns(
                (Expression<Func<T, bool>> expr) => entities.AsQueryable().FirstOrDefault(expr));

            mockRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => entities.Single(e => e.Id == id));

            mockRepository.Setup(x => x.Update(It.IsAny<T>())).Callback((T entity) => entities.Attach(entity));

            return mockRepository.Object;
        }

        #endregion
    }
}