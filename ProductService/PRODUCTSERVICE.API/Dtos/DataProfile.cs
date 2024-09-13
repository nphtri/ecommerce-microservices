using AutoMapper;
using PRODUCTSERVICE.API.Models;

namespace PRODUCTSERVICE.API.Dtos
{
  public class DataProfile : Profile
  {
    public DataProfile()
    {
      CreateMap<Art, ArtReadDto>()
          .ForMember(dest => dest.Style, opt => opt.MapFrom(src => src.ArtStyle.Value));
      CreateMap<Lookup, ArtStyleDto>()
          .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => _.Id))
          .ForMember(dest => dest.Name, opt => opt.MapFrom(_ => _.Value));
      CreateMap<ArtCreateDto, Art>();

      CreateMap<Artist, ArtistReadDto>();
      CreateMap<Artist, ArtistBriefReadDto>();
      CreateMap<ArtistCreateDto, Artist>();

      CreateMap<ArtCollection, ArtCollectionReadDto>();
      CreateMap<ArtCollection, ArtCollectionBriefReadDto>();

      CreateMap<Banner, BannerReadDto>();
      CreateMap<BannerCreateDto, Banner>();
    }
  }
}