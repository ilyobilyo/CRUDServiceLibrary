using CRUDServiceLibrary.Abstract;
using CRUDServiceLibrary.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDServiceLibrary
{
    /// <summary>
    /// Abstract base controller that provides generic implementations for common CRUD operations. 
    /// It handles Create, Read, Update, and Delete operations for any entity type by utilizing 
    /// a service that implements the ICRUDService interface.
    /// </summary>
    /// <typeparam name="TService">The service type that handles the core CRUD operations.</typeparam>
    /// <typeparam name="TDataModel">The data model type representing the entity.</typeparam>
    /// <typeparam name="TInputModel">The input model type used for create and update operations.</typeparam>
    /// <typeparam name="TResponseModel">The response model type returned to the client.</typeparam>
    /// <typeparam name="TEntityId">The type of the unique identifier for the entity (e.g., int, Guid).</typeparam>
    [ApiController]
    public abstract class CRUDController<TService, TDataModel, TInputModel, TResponseModel, TEntityId> : ControllerBase
        where TInputModel : class
        where TDataModel : IBaseEntity<TDataModel>
        where TResponseModel : class
        where TService : IBaseService<TDataModel, TInputModel, TEntityId>
    {
        private readonly ICRUDService<TService, TDataModel, TInputModel, TResponseModel, TEntityId> crudService;

        protected CRUDController(ICRUDService<TService, TDataModel, TInputModel, TResponseModel, TEntityId> crudService)
        {
            this.crudService = crudService;
        }

        /// <summary>
        /// Retrieves all entities and returns them as a list of response models.
        /// </summary>p
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A list of all entities wrapped in response models.</returns>
        [HttpGet]
        public virtual async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var allEntities = await crudService.GetAll(cancellationToken);

                return Ok(allEntities);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponseModel { Status = StatusCodes.Status400BadRequest, Message = e.Message });
            }
        }

        /// <summary>
        /// Retrieves a specific entity by its ID and returns it as a response model.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The entity wrapped in a response model.</returns>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(TEntityId id, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await crudService.GetById(id, cancellationToken);

                return Ok(entity);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponseModel { Status = StatusCodes.Status400BadRequest, Message = e.Message });
            }
        }

        /// <summary>
        /// Creates a new entity from the provided input model and returns the created entity as a response model.
        /// </summary>
        /// <param name="model">The input model containing the data for the new entity.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The newly created entity wrapped in a response model.</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Create(TInputModel model, CancellationToken cancellationToken)
        {
            try
            {
                var createdEntity = await crudService.Create(model, cancellationToken);

                return Ok(createdEntity);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponseModel { Status = StatusCodes.Status400BadRequest, Message = e.Message });
            }
        }

        /// <summary>
        /// Updates an existing entity with the provided input model and returns the updated entity as a response model.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to update.</param>
        /// <param name="model">The input model containing the update data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The updated entity wrapped in a response model.</returns>
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(TEntityId id, TInputModel model, CancellationToken cancellationToken)
        {
            try
            {
                var updatedEntity = await crudService.Update(id, model, cancellationToken);

                return Ok(updatedEntity);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponseModel { Status = StatusCodes.Status400BadRequest, Message = e.Message });
            }
        }

        /// <summary>
        /// Deletes an entity by its ID and returns the ID of the deleted entity.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The unique identifier of the deleted entity.</returns>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TEntityId id, CancellationToken cancellationToken)
        {
            try
            {
                var deletedEntityId = await crudService.Delete(id, cancellationToken);

                return Ok(deletedEntityId);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponseModel { Status = StatusCodes.Status400BadRequest, Message = e.Message });
            }
        }
    }
}
