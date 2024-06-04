using AutoMapper;
using Plugin.Core.Models;
using Plugin.Repository;
using PluginFramework.Models.Requests;
using PluginFramework.Models.Responses;

namespace PluginFramework.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Image, ImageInfo>()
                .ForMember(dest => dest.Image, src => src.Ignore());

            CreateMap<ImageInfo, Image>()
                .ForMember(dest => dest.Path, src => src.Ignore())
                .ForMember(dest => dest.Name, src => src.Ignore());

            CreateMap<ImageInfo, ImageResponse>();

            CreateMap<ImageRequest, ImageInfo>()
                .ForMember(dest => dest.Path, src => src.Ignore());

        }
    }
}
