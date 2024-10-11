using CRUDServiceLibrary.Abstract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDServiceLibrary.Contracts
{
    /// <summary>
    /// Defines the higher-level CRUD operations for managing data entities and transforming 
    /// them into response models. This service provides a generic way to handle different 
    /// entities by leveraging the service layer for core CRUD functionality and mapping 
    /// results to response models.
    /// </summary>
    /// <typeparam name="TService">The service that implements IBaseService for performing CRUD operations.</typeparam>
    /// <typeparam name="TDataModel">The data model representing the entity type.</typeparam>
    /// <typeparam name="TInputModel">The input model used for creating or updating entities.</typeparam>
    /// <typeparam name="TResponseModel">The response model returned to the client after CRUD operations.</typeparam>
    /// <typeparam name="TEntityId">The type of the unique identifier for the entity (e.g., int, Guid).</typeparam>
    public interface ICRUDService<TService, TDataModel, TInputModel, TResponseModel, TEntityId>
        where TInputModel : class
        where TDataModel : IBaseEntity<TEntityId>
        where TResponseModel : class
        where TService : IBaseService<TDataModel, TInputModel, TEntityId>
    {
        /// <summary>
        /// Retrieves all entities, transforms them into response models, and returns them.
        /// </summary>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <param name="filters">A dictionary of filters to apply to the query (optional).</param>
        /// <param name="take">The number of records (pageSize) to retrieve from the result set (optional).</param>
        /// <param name="pageNumber">The page number for pagination (optional).</param>
        /// <returns>A collection of response models representing the entities.</returns>
        public Task<IEnumerable<TResponseModel>> GetAll(CancellationToken cancellationToken, Dictionary<string, string> filters = null, int? take = null, int? pageNumber = null);

        /// <summary>
        /// Retrieves a specific entity by its unique identifier and returns it as a response model.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The response model representing the entity.</returns>
        public Task<TResponseModel> GetById(TEntityId id, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new entity from the input model, processes it via the service layer,
        /// and returns the created entity as a response model.
        /// </summary>
        /// <param name="model">The input model containing the data for the new entity.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The response model representing the created entity.</returns>
        public Task<TResponseModel> Create(TInputModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing entity using the input model and returns the updated entity
        /// as a response model.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be updated.</param>
        /// <param name="model">The input model containing the update data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The response model representing the updated entity.</returns>
        public Task<TResponseModel> Update(TEntityId id, TInputModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes an entity by its unique identifier and returns the ID of the deleted entity.
        /// </summary>
        /// <param name="model">The unique identifier of the entity to delete.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The unique identifier of the deleted entity.</returns>
        public Task<TEntityId> Delete(TEntityId model, CancellationToken cancellationToken);
    }
}
