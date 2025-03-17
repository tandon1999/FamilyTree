using AutoMapper;
using FamilyTree_UI.Models;
using FamilyTree_UI.ViewModels;

namespace FamilyTreeUI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogsPostVModel,BlogsPostModel>().ReverseMap();

        }
    }
}
