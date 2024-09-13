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
  public interface IBannerService
  {
    IEnumerable<BannerReadDto> GetBanners();
    BannerReadDto GetBannerById(int id);
    BannerReadDto CreateBanner(BannerCreateDto dto);
    void ActiveBanner(int id);
    void DeactiveBanner(int id);
  }

  public class BannerService : IBannerService
  {
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IAWSService _awsService;

    public BannerService(IUnitOfWork uow, IMapper mapper, IAWSService awsService)
    {
      _uow = uow;
      _mapper = mapper;
      _awsService = awsService;
    }

    public void ActiveBanner(int id)
    {
      throw new NotImplementedException();
    }

    public BannerReadDto CreateBanner(BannerCreateDto dto)
    {
      var banner = _mapper.Map<Banner>(dto);
      banner.ModifiedTime = DateTime.UtcNow;
      banner.CreatedTime = DateTime.UtcNow;
      banner.Image = _awsService.UploadFile(dto.Image, "banner").Result;
      var bannersToUpdate = _uow.BannerRepo.Find(_ => _.IsActive && _.OrderIndex >= dto.OrderIndex).OrderBy(_ => _.OrderIndex).ToList();

      // update index
      var currentIndex = banner.OrderIndex;
      foreach (var b in bannersToUpdate)
      {
        if (b.OrderIndex > currentIndex) break;
      }

      _uow.BannerRepo.Add(banner);
      if (_uow.Complete() > 0)
      {
        return _mapper.Map<BannerReadDto>(banner);
      }
      return null;
    }

    public void DeactiveBanner(int id)
    {
      throw new NotImplementedException();
    }

    public BannerReadDto GetBannerById(int id)
    {
      var result = _uow.BannerRepo.GetById(id);
      if (result != null)
      {
        return _mapper.Map<BannerReadDto>(result);
      }
      return null;
    }

    public IEnumerable<BannerReadDto> GetBanners()
    {
      return _mapper.Map<IEnumerable<BannerReadDto>>(_uow.BannerRepo.Find(_ => _.IsActive).OrderBy(_ => _.OrderIndex).AsEnumerable());
    }

  }
}