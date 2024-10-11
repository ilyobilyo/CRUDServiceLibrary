using AutoMapper;
using CRUDServiceLibrary.Abstract;
using CRUDServiceLibrary.Contracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CRUDServiceLibrary
{
    public class CRUDService<TService, TDataModel, TInputModel, TResponseModel, TEntityId> : ICRUDService<TService, TDataModel, TInputModel, TResponseModel, TEntityId>
        where TInputModel : class
        where TDataModel : IBaseEntity<TEntityId>
        where TResponseModel : class
        where TService : IBaseService<TDataModel, TInputModel, TEntityId>
    {
        private readonly TService _service;
        private readonly IMapper _mapper;

        public CRUDService(TService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TResponseModel>> GetAll(CancellationToken cancellationToken, Dictionary<string, string> filters = null, int? take = null, int? pageNumber = null)
        {
            if (pageNumber is not null 
                && (take is null || take == 0))
            {
                throw new ArgumentException("If pageNumber is provided, 'take' must be a non-null, non-zero value to determine the page size.");
            }

            var allDataEntities = await _service.GetAll(cancellationToken, filters, take, pageNumber);

            var allResponseEntities = _mapper.Map<IEnumerable<TResponseModel>>(allDataEntities);

            return allResponseEntities;
        }

        public async Task<TResponseModel> GetById(TEntityId id, CancellationToken cancellationToken)
        {
            var dataEntity = await _service.GetById(id, cancellationToken);

            var responseEntity = _mapper.Map<TResponseModel>(dataEntity);

            return responseEntity;
        }

        public async Task<TResponseModel> Create(TInputModel model, CancellationToken cancellationToken)
        {
            TDataModel dataEntity = _mapper.Map<TDataModel>(model);

            dataEntity.CreatedAt = DateTime.UtcNow;
            dataEntity.UpdatedAt = DateTime.UtcNow;

            await _service.Create(dataEntity, model, cancellationToken);

            var responseEntity = _mapper.Map<TResponseModel>(dataEntity);

            return responseEntity;
        }

        public async Task<TResponseModel> Update(TEntityId id, TInputModel model, CancellationToken cancellationToken)
        {
            var existingDataEntity = await _service.GetById(id, cancellationToken);

            if (existingDataEntity is null)
            {
                throw new ArgumentException("Entity not found");
            }

            existingDataEntity.UpdatedAt = DateTime.UtcNow;

            await _service.Update(existingDataEntity, model, cancellationToken);

            var responseEntity = _mapper.Map<TResponseModel>(existingDataEntity);

            return responseEntity;
        }

        public async Task<TEntityId> Delete(TEntityId id, CancellationToken cancellationToken)
        {
            var dataEntity = await _service.GetById(id, cancellationToken);

            return await _service.Delete(dataEntity, cancellationToken);
        }
    }
}
