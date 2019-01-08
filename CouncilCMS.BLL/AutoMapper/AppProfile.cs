using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.Helpers;
using xTab.Tools.Helpers;
using Bissoft.CouncilCMS.Core.Enums;
using xTab.Tools.Extensions;
using Bissoft.CouncilCMS.Core;

using Bissoft.CouncilCMS.Entities.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels.Public;

namespace Bissoft.CouncilCMS.BLL.AutoMapper
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {            
            CreateMap<CmsSettings, CmsAppSettings>().ReverseMap();
            CreateMap<CmsSettings, CmsSettingsEdit>().ReverseMap();            
            CreateMap<ContentRow, ContentRowsEdit>().ReverseMap();
            CreateMap<ContentColumn, ContentColumnEdit>().ReverseMap();
            CreateMap<ContentRow, ContentRowItem>().ReverseMap();
            CreateMap<ContentColumn, ContentColumnItem>().ReverseMap();
           

            CreateMap<Content, ContentItem>()
                .ForMember(x => x.Type, opt => opt.MapFrom(p => ((ContentType)p.Type).GetLocalDescription()));

            CreateMap<Content, CmsContent>()
                .ForMember(x => x.Body, opt => opt.MapFrom(p => p.GetLocalValue("Body", String.Empty)));

            CreateMap<ContentColumn, CmsContentColumn>()
                .ForMember(x => x.CssClass, opt => opt.MapFrom(p => ViewHelper.GetContentColumnClass(p)))
                .ForMember(x => x.CssStyle, opt => opt.MapFrom(p => ViewHelper.GetContentColumnStyle(p)))
                .ForMember(x => x.Contents, opt => opt.UseValue(new List<CmsContent>()));

            CreateMap<ContentRow, CmsContentRow>()
                .ForMember(x => x.CssStyle, opt => opt.MapFrom(p => ViewHelper.GetContentRowStyle(p)))
                .ForMember(x => x.Columns, opt => opt.UseValue(new List<CmsContentColumn>()));

            CreateMap<ArticleCategory, ArticleCategoryEdit>()
                .ForMember(x => x.AllowedRoles, opt => opt.Ignore())
                .ForMember(x => x.AllowedUsers, opt => opt.Ignore())
                .ForMember(x => x.RelatedCategories, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Position, opt => opt.Ignore())
                .ForMember(x => x.AllowedRoles, opt => opt.Ignore())
                .ForMember(x => x.AllowedUsers, opt => opt.Ignore())
                .ForMember(x => x.RelatedCategories, opt => opt.Ignore());

			CreateMap<DamagedHousingCategory, DamagedHousingCategoryEdit>()
			   .ForMember(x => x.AllowedRoles, opt => opt.Ignore())
			   .ForMember(x => x.AllowedUsers, opt => opt.Ignore())
			   .ForMember(x => x.RelatedCategories, opt => opt.Ignore())
			   .ReverseMap()
			   .ForMember(x => x.Position, opt => opt.Ignore())
			   .ForMember(x => x.AllowedRoles, opt => opt.Ignore())
			   .ForMember(x => x.AllowedUsers, opt => opt.Ignore())
			   .ForMember(x => x.RelatedCategories, opt => opt.Ignore());

			CreateMap<DocCategory, DocCategoryEdit>()
               .ForMember(x => x.AllowedRoles, opt => opt.Ignore())
               .ForMember(x => x.AllowedUsers, opt => opt.Ignore())
               .ForMember(x => x.RelatedCategories, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x.Position, opt => opt.Ignore())
               .ForMember(x => x.AllowedRoles, opt => opt.Ignore())
               .ForMember(x => x.AllowedUsers, opt => opt.Ignore())
               .ForMember(x => x.RelatedCategories, opt => opt.Ignore());


            CreateMap<PersonCategory, PersonCategoryEdit>()
               .ForMember(x => x.AllowedRoles, opt => opt.Ignore())
               .ForMember(x => x.AllowedUsers, opt => opt.Ignore())
               .ForMember(x => x.RelatedCategories, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x.Position, opt => opt.Ignore())
               .ForMember(x => x.AllowedRoles, opt => opt.Ignore())
               .ForMember(x => x.AllowedUsers, opt => opt.Ignore())
               .ForMember(x => x.RelatedCategories, opt => opt.Ignore());

            CreateMap<EnterpriseCategory, EnterpriseCategoryEdit>()
               .ForMember(x => x.AllowedRoles, opt => opt.Ignore())
               .ForMember(x => x.AllowedUsers, opt => opt.Ignore())
               .ForMember(x => x.RelatedCategories, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x.Position, opt => opt.Ignore())
               .ForMember(x => x.AllowedRoles, opt => opt.Ignore())
               .ForMember(x => x.AllowedUsers, opt => opt.Ignore())
               .ForMember(x => x.RelatedCategories, opt => opt.Ignore());
        }
            
    }    
}
