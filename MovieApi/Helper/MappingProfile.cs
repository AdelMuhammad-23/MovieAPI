namespace MovieApi.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie,MovieDetailsDto>();
            CreateMap<MoviesDto, Movie>()
                .ForMember(dist => dist.Poster, opt => opt.Ignore());
              
        }
    }
}
