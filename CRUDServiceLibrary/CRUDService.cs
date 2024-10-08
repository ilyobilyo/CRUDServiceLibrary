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
        where TDataModel : IBaseEntity<TDataModel>
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

        public async Task<IEnumerable<TResponseModel>> GetAll(CancellationToken cancellationToken)
        {
            var allDataEntities = await _service.GetAll(cancellationToken);

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

            dataEntity.CreatedAt = DateTime.Now;
            dataEntity.UpdatedAt = DateTime.Now;

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

            existingDataEntity.UpdatedAt = DateTime.Now;

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
