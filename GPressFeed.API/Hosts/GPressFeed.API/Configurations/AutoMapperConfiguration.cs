using Application.Models;
using AutoMapper;
using Infrastructure.DTOs;

namespace GPressFeed.API.Configurations;

public class AutoMapperConfiguration : Profile
{
	public AutoMapperConfiguration()
	{
		CreateMap<TrendingArticle, Article>()
			.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.title))
			.ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.url));
    }
}
