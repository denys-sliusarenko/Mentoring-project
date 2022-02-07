using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.Domain.Core.Entities;
using MentoringProject.Domain.Core.Exceptions;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OwnerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OwnerDTO> CreateAsync(OwnerDTO ownerDto)
        {
            var newOwner = _mapper.Map<Owner>(ownerDto);
            await _unitOfWork.OwnerRepository.Create(newOwner);
            await _unitOfWork.SaveAsync();
            var createdOwner = _mapper.Map<OwnerDTO>(newOwner);
            return createdOwner;
        }

        public async Task DeleteAsync(int id)
        {
            var owner = await _unitOfWork.OwnerRepository.GetAsync(id);
            if (owner == null)
            {
                throw new NotFoundException($"Owner with {id} not found");
            }

            await _unitOfWork.OwnerRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<OwnerDTO> GetAll()
        {
            var owners = _unitOfWork.OwnerRepository.GetAll();
            var ownersDto = _mapper.Map<IEnumerable<OwnerDTO>>(owners);
            return ownersDto;
        }

        public FileStreamResult GetFile()
        {
            var owners = _unitOfWork.OwnerRepository.GetAll();
            MemoryStream ms = new ();
            StreamWriter sw = new (ms);
            foreach (var t in owners)
            {
                sw.WriteLine($"{t.Id} {t.FirstName} {t.LastName}");
            }

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            FileStreamResult result = new (ms, MediaTypeNames.Text.Plain)
            {
                FileDownloadName = "test.txt",
            };

            return result;
        }

        public async Task<OwnerDTO> GetAsync(int id)
        {
            var owner = await _unitOfWork.OwnerRepository.GetAsync(id);
            if (owner == null)
            {
                throw new NotFoundException($"Owner with {id} not found");
            }

            var ownerDto = _mapper.Map<OwnerDTO>(owner);

            return ownerDto;
        }

        public async Task<OwnerDTO> UpdateAsync(OwnerDTO ownerDto)
        {
            if (!await _unitOfWork.OwnerRepository.Exist(ownerDto.Id))
            {
                throw new NotFoundException();
            }

            var owner = _mapper.Map<Owner>(ownerDto);
            _unitOfWork.OwnerRepository.Update(owner);
            await _unitOfWork.SaveAsync();
            var updatedOwner = _mapper.Map<OwnerDTO>(owner);
            return updatedOwner;
        }
    }
}