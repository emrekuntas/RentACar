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

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<CreatedBrandDto>
    {
        public string Name { get; set; }
        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
        {
            private readonly BrandBusinessRules _brandBusinessRules;
            private readonly IBrandRespository _brandRespository;
            private readonly IMapper _mapper;

            public CreateBrandCommandHandler(BrandBusinessRules brandBusinessRules, IBrandRespository brandRespository, IMapper mapper)
            {
                _brandBusinessRules = brandBusinessRules;
                _brandRespository = brandRespository;
                _mapper = mapper;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);

                Brand mappedBrand = _mapper.Map<Brand>(request);
                Brand createdBrand = await _brandRespository.AddAsync(mappedBrand);
                CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand);

                return createdBrandDto;
            }
        }
    }
}
