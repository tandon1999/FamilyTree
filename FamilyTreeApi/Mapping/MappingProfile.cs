using AutoMapper;
using FamilyTreeApi.Param;
using FamilyTreeApi.Param.Auth;
using FamilyTreeApi.RequestModel;
using FamilyTreeApi.RequestModel.Auth;
using FamilyTreeApi.Shared.Model;

namespace FamilyTreeApi.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<FamilyTreeParam, FamilyTreeMemberRequestModel>().ReverseMap();
            CreateMap<DropDownModelApi, DropDownModel>().ReverseMap();
            CreateMap<GallerySetupParam, GallerySetupRequestModel>().ReverseMap();
            CreateMap<HistorySetupParam, HistorySetupRequestModel>().ReverseMap();
            CreateMap<LoginParam, LoginRequestModel>().ReverseMap();
            CreateMap<BlogsParam, BlogsRequestModel>().ReverseMap();
        }
    }
}
