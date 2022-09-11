using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Respositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandQuery : IRequest<BrandGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandGetByIdDto>
        {
            private readonly IBrandRespository _brandRespository;
            private readonly BrandBusinessRules _brandBusinessRules;
            private readonly IMapper _mapper;

            public GetByIdBrandQueryHandler(IBrandRespository brandRespository, BrandBusinessRules brandBusinessRules, IMapper mapper)
            {
                _brandRespository = brandRespository;
                _brandBusinessRules = brandBusinessRules;
                _mapper = mapper;
            }

            public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
            {
                Brand? brand = await _brandRespository.GetAsync(b => b.Id == request.Id);
                _brandBusinessRules.BrandShouldExistWhenRequested(brand);
                BrandGetByIdDto brandGetByIdDto = _mapper.Map<BrandGetByIdDto>(brand);
                return brandGetByIdDto;
            }
        }

    }
}
