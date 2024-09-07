using AutoMapper;
using ConwayGameOfLife.API.Dtos;
using Domain.Entities;

namespace ConwayGameOfLife.API.Profiles
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
            CreateMap<Board, BoardDto>();
            CreateMap<int, BoardDto>()
                .ForMember(dest => dest.BoardState, opt => opt.MapFrom(src => src));

            CreateMap<int, BoardCreatedDto>()
                .ForMember(dest => dest.BoardId, opt => opt.MapFrom(src => src));
        }
    }
}
