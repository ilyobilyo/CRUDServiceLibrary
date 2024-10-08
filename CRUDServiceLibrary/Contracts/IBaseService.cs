using CRUDServiceLibrary.Abstract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDServiceLibrary.Contracts
{
    /// <summary>
    /// Defines the basic CRUD operations for a service that handles a specific data model.
    /// This service manages the lifecycle of the data entity, including creation, retrieval,
    /// update, and deletion. It is designed to be flexible and reusable across different 
    /// entities by leveraging generics.
    /// </summary>
    /// <typeparam name="TDataModel">The type of the data model representing the entity.</typeparam>
    /// <typeparam name="TInputModel">The type of the input model used to create or update the entity.</typeparam>
    /// <typeparam name="TEntityId">The type of the unique identifier for the entity (e.g., int, Guid).</typeparam>
    public interface IBaseService<TDataModel, TInputModel, TEntityId>
        where TDataModel : IBaseEntity<TEntityId>
        where TInputModel : class
    {
        /// <summary>
        /// Retrieves all data entities of the specified type.
        /// </summary>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A collection of data models.</returns>
        public Task<IEnumerable<TDataModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a specific data entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The data model representing the entity.</returns>
        public Task<TDataModel> GetById(TEntityId id, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new data entity based on the provided input model and entity.
        /// </summary>
        /// <param name="dataEntity">The entity to be created.</param>
        /// <param name="inputModel">The input model containing creation data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The created data model.</returns>
        public Task<TDataModel> Create(TDataModel dataEntity, TInputModel inputModel, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing data entity using the provided input model and entity.
        /// </summary>
        /// <param name="dataEntity">The entity to be updated.</param>
        /// <param name="inputModel">The input model containing update data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The updated data model.</returns>
        public Task<TDataModel> Update(TDataModel dataEntity, TInputModel inputModel, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified data entity.
        /// </summary>
        /// <param name="dataEntity">The entity to delete.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The unique identifier of the deleted entity.</returns>
        public Task<TEntityId> Delete(TDataModel dataEntity, CancellationToken cancellationToken);
    }
}
