using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PRODUCTSERVICE.API.AWS;
using PRODUCTSERVICE.API.Data;
using PRODUCTSERVICE.API.Dtos;
using PRODUCTSERVICE.API.Models;

namespace PRODUCTSERVICE.API.Services
{
  public interface IArtService
  {
    IEnumerable<ArtReadDto> GetAllArts();
    ArtReadDto GetArtById(int id);
    ArtReadDto CreateArt(ArtCreateDto dto, int artistId);
    IEnumerable<ArtStyleDto> GetArtStyles();
  }

  public class ArtService : IArtService
  {
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IAWSService _awsService;

    public ArtService(IUnitOfWork uow, IMapper mapper, IAWSService aWSService)
    {
      _uow = uow;
      _mapper = mapper;
      _awsService = aWSService;
    }

    public ArtReadDto CreateArt(ArtCreateDto dto, int artistId)
    {
      var art = _mapper.Map<Art>(dto);
      art.ArtistId = artistId;
      art.ModifiedTime = DateTime.UtcNow;
      art.CreatedTime = DateTime.UtcNow;
      art.Image = _awsService.UploadFile(dto.Image, art.ArtistId.ToString()).Result;
      _uow.ArtRepo.Add(art);
      if (_uow.Complete() > 0)
      {
        art = _uow.ArtRepo.GetById(art.Id);
        return _mapper.Map<ArtReadDto>(art);
      }
      return null;
    }

    public IEnumerable<ArtReadDto> GetAllArts()
    {
      return _mapper.Map<IEnumerable<ArtReadDto>>(_uow.ArtRepo.GetAll().Include(_ => _.ArtStyle).Include(_ => _.Artist).AsEnumerable());
    }

    public ArtReadDto GetArtById(int id)
    {
      var art = _uow.ArtRepo.Find(_ => _.Id == id).Include(_ => _.ArtStyle).Include(_ => _.Artist).FirstOrDefault();
      if (art != null)
      {
        return _mapper.Map<ArtReadDto>(art);
      }
      return null;
    }

    public IEnumerable<ArtStyleDto> GetArtStyles()
    {
      return _mapper.Map<IEnumerable<ArtStyleDto>>(_uow.LookupRepo.GetArtStyles());
    }
  }
}