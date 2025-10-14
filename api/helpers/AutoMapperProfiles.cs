

namespace fotoservice.api.helpers;

    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<fotoservice.data.models.Image, ImageDto>().ReverseMap();
            
            
            CreateMap<AppUser, UserDto>()
             .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            
          
            CreateMap<UserForUpdateDto, AppUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) ; 
            
             CreateMap<AppUser, UserForReturnDto>();
        }

    }
