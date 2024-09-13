using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PRODUCTSERVICE.API.AWS;
using PRODUCTSERVICE.API.Data;
using PRODUCTSERVICE.API.Dtos;
using PRODUCTSERVICE.API.Models;

namespace PRODUCTSERVICE.API.Services
{
  public interface IArtistService
  {
    IEnumerable<ArtistBriefReadDto> GetArtistsBrief();
    ArtistBriefReadDto GetArtistBriefById(int id);
    ArtistReadDto GetArtistById(int id);
    ArtistReadDto GetArtistByEmail(string email);
    ArtistReadDto GetArtistByPhone(string phone);
    ArtistReadDto GetArtistByNickname(string nickname);
    ArtistReadDto CreateArtist(ArtistCreateDto dto);
  }
  public class ArtistService : IArtistService
  {
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IAWSService _awsService;

    public ArtistService(IUnitOfWork uow, IMapper mapper, IAWSService awsService)
    {
      _uow = uow;
      _mapper = mapper;
      _awsService = awsService;
    }
    public ArtistReadDto CreateArtist(ArtistCreateDto dto)
    {
      var artist = _mapper.Map<Artist>(dto);
      artist.CreatedTime = DateTime.UtcNow;
      artist.ModifiedTime = DateTime.UtcNow;
      artist.Avatar = _awsService.UploadFile(dto.Avatar, artist.FirstName).Result;
      _uow.ArtistRepo.Add(artist);
      if (_uow.Complete() > 0)
      {
        return _mapper.Map<ArtistReadDto>(artist);
      }
      return null;
    }

    public ArtistBriefReadDto GetArtistBriefById(int id)
    {
      var artist = _uow.ArtistRepo.GetById(id);
      if (artist != null)
      {
        return _mapper.Map<ArtistBriefReadDto>(artist);
      }
      return null;
    }

    public ArtistReadDto GetArtistById(int id)
    {
      var artist = _uow.ArtistRepo.GetById(id);
      if (artist != null)
      {
        return _mapper.Map<ArtistReadDto>(artist);
      }
      return null;
    }

    public IEnumerable<ArtistBriefReadDto> GetArtistsBrief()
    {
      return _mapper.Map<IEnumerable<ArtistBriefReadDto>>(_uow.ArtistRepo.GetAll().AsEnumerable());
    }

    public ArtistReadDto GetArtistByEmail(string email)
    {
      var artist = _uow.ArtistRepo.Find(_ => _.Email.Equals(email)).FirstOrDefault();
      if (artist != null)
      {
        return _mapper.Map<ArtistReadDto>(artist);
      }
      return null;
    }

    public ArtistReadDto GetArtistByPhone(string phone)
    {
      var artist = _uow.ArtistRepo.Find(_ => _.Phone.Equals(phone)).FirstOrDefault();
      if (artist != null)
      {
        return _mapper.Map<ArtistReadDto>(artist);
      }
      return null;
    }
    public ArtistReadDto GetArtistByNickname(string nickname)
    {
      var artist = _uow.ArtistRepo.Find(_ => _.Nickname.Equals(nickname)).FirstOrDefault();
      if (artist != null)
      {
        return _mapper.Map<ArtistReadDto>(artist);
      }
      return null;
    }
  }
}