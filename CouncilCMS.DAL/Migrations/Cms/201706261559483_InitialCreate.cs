namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlName = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        DescriptionEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Image = c.String(unicode: false),
                        DefaultNoImage = c.String(unicode: false),
                        ShowSearchForm = c.Boolean(nullable: false),
                        AllowOpenDataExport = c.Boolean(nullable: false),
                        SidebarMenuId = c.Int(),
                        RelatedCategotyId = c.Int(),
                        CurrentMenuItemId = c.Int(),
                        TemplateId = c.Int(),
                        ParentArticleCategoryId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                        RelatedCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.CurrentMenuItemId)
                .ForeignKey("dbo.ArticleCategories", t => t.ParentArticleCategoryId)
                .ForeignKey("dbo.ArticleCategories", t => t.RelatedCategory_Id)
                .ForeignKey("dbo.Menus", t => t.SidebarMenuId)
                .ForeignKey("dbo.ArticleCategoryTemplates", t => t.TemplateId)
                .Index(t => t.SidebarMenuId)
                .Index(t => t.CurrentMenuItemId)
                .Index(t => t.TemplateId)
                .Index(t => t.ParentArticleCategoryId)
                .Index(t => t.RelatedCategory_Id);
            
            CreateTable(
                "dbo.CmsUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Phone = c.Int(),
                        Password = c.String(unicode: false),
                        Name = c.String(unicode: false),
                        Avatar = c.String(unicode: false),
                        Gender = c.Int(),
                        RegisterDate = c.DateTime(nullable: false, precision: 0),
                        Blocked = c.Boolean(nullable: false),
                        ChangePasswordOnLogin = c.Boolean(nullable: false),
                        WrongLoginTries = c.Int(nullable: false),
                        RestorePasswordToken = c.String(unicode: false),
                        RestorePasswordExpire = c.DateTime(precision: 0),
                        LastLoginDate = c.DateTime(precision: 0),
                        Salt = c.String(unicode: false),
                        CID = c.String(unicode: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlName = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        DescriptionEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Image = c.String(unicode: false),
                        ShowSearchForm = c.Boolean(nullable: false),
                        CurrentMenuItemId = c.Int(),
                        TemplateId = c.Int(),
                        ParentCategoryId = c.Int(),
                        RelatedCategoryId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.CurrentMenuItemId)
                .ForeignKey("dbo.DocCategories", t => t.ParentCategoryId)
                .ForeignKey("dbo.DocCategories", t => t.RelatedCategoryId)
                .ForeignKey("dbo.DocCategoryTemplates", t => t.TemplateId)
                .Index(t => t.CurrentMenuItemId)
                .Index(t => t.TemplateId)
                .Index(t => t.ParentCategoryId)
                .Index(t => t.RelatedCategoryId);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameRu = c.String(unicode: false),
                        NameUk = c.String(unicode: false),
                        NameEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Url = c.String(unicode: false),
                        Image = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionEn = c.String(unicode: false),
                        Type = c.Int(nullable: false),
                        Value = c.String(unicode: false),
                        ItemId = c.Int(nullable: false),
                        ItemName = c.Int(nullable: false),
                        ShowItem = c.Boolean(nullable: false),
                        ShowItemUk = c.Boolean(nullable: false),
                        ShowItemRu = c.Boolean(nullable: false),
                        ShowItemEn = c.Boolean(nullable: false),
                        MenuId = c.Int(nullable: false),
                        ParentMenuItemId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.MenuItems", t => t.ParentMenuItemId, cascadeDelete: true)
                .Index(t => t.MenuId)
                .Index(t => t.ParentMenuItemId);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        NameRu = c.String(unicode: false),
                        NameUk = c.String(unicode: false),
                        NameEn = c.String(unicode: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Docs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        Text = c.String(unicode: false),
                        CleanText = c.String(unicode: false),
                        KeywordsUk = c.String(unicode: false),
                        KeywordsRu = c.String(unicode: false),
                        KeywordsEn = c.String(unicode: false),
                        ShowPublihDate = c.Boolean(nullable: false),
                        ShowEditDate = c.Boolean(nullable: false),
                        AcceptDate = c.DateTime(precision: 0),
                        PostDate = c.DateTime(precision: 0),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        PublishDate = c.DateTime(precision: 0),
                        LastEditDate = c.DateTime(precision: 0),
                        EditedDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(),
                        LastEditUserId = c.Int(),
                        Published = c.Boolean(nullable: false),
                        Saved = c.Boolean(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        DocTypeId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DocTypes", t => t.DocTypeId, cascadeDelete: true)
                .Index(t => t.DocTypeId);
            
            CreateTable(
                "dbo.DocTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Enterprises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionEn = c.String(unicode: false),
                        KeywordsUk = c.String(unicode: false),
                        KeywordsRu = c.String(unicode: false),
                        KeywordsEn = c.String(unicode: false),
                        ContactAddressRu = c.String(unicode: false),
                        ContactAddressUk = c.String(unicode: false),
                        ContactAddressEn = c.String(unicode: false),
                        ContactDatesRu = c.String(unicode: false),
                        ContactDatesUk = c.String(unicode: false),
                        ContactDatesEn = c.String(unicode: false),
                        ContactTimeRu = c.String(unicode: false),
                        ContactTimeUk = c.String(unicode: false),
                        ContactTimeEn = c.String(unicode: false),
                        ContactEmailsUk = c.String(unicode: false),
                        ContactEmailsRu = c.String(unicode: false),
                        ContactEmailsEn = c.String(unicode: false),
                        ContactPhonesUk = c.String(unicode: false),
                        ContactPhonesRu = c.String(unicode: false),
                        ContactPhonesEn = c.String(unicode: false),
                        ContactWebsiteUk = c.String(unicode: false),
                        ContactWebsiteRu = c.String(unicode: false),
                        ContactWebsiteEn = c.String(unicode: false),
                        ShowSecondPage = c.Boolean(nullable: false),
                        SecondPageTitleUk = c.String(unicode: false),
                        SecondPageTitleRu = c.String(unicode: false),
                        SecondPageTitleEn = c.String(unicode: false),
                        SecondPageTextUk = c.String(unicode: false),
                        SecondPageTextRu = c.String(unicode: false),
                        SecondPageTextEn = c.String(unicode: false),
                        NotificationEmail = c.String(unicode: false),
                        NotificationPhone = c.String(unicode: false),
                        UrlNameRu = c.String(unicode: false),
                        UrlNameUk = c.String(unicode: false),
                        UrlNameEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        PersonsTitleUk = c.String(unicode: false),
                        PersonsTitleRu = c.String(unicode: false),
                        PersonsTitleEn = c.String(unicode: false),
                        FbLink = c.String(unicode: false),
                        TwLink = c.String(unicode: false),
                        GpLink = c.String(unicode: false),
                        YtLink = c.String(unicode: false),
                        IgLink = c.String(unicode: false),
                        Image = c.String(unicode: false),
                        HasContactForm = c.Boolean(nullable: false),
                        ShowPublihDate = c.Boolean(nullable: false),
                        ShowEditDate = c.Boolean(nullable: false),
                        Facebook = c.String(unicode: false),
                        Twitter = c.String(unicode: false),
                        Published = c.Boolean(nullable: false),
                        Saved = c.Boolean(nullable: false),
                        PublishDate = c.DateTime(precision: 0),
                        EditedDate = c.DateTime(precision: 0),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        LastEditDate = c.DateTime(nullable: false, precision: 0),
                        ViewCount = c.Int(nullable: false),
                        TypeId = c.Int(),
                        ParentEnterpriseId = c.Int(),
                        SidebarMenuId = c.Int(),
                        CreateUserId = c.Int(),
                        LastEditUserId = c.Int(),
                        CurrentMenuItemId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                        EnterpriseCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EnterpriseCategories", t => t.EnterpriseCategory_Id)
                .ForeignKey("dbo.Enterprises", t => t.ParentEnterpriseId)
                .ForeignKey("dbo.MenuItems", t => t.CurrentMenuItemId)
                .ForeignKey("dbo.Menus", t => t.SidebarMenuId)
                .ForeignKey("dbo.EnterpriseTypes", t => t.TypeId)
                .Index(t => t.TypeId)
                .Index(t => t.ParentEnterpriseId)
                .Index(t => t.SidebarMenuId)
                .Index(t => t.CurrentMenuItemId)
                .Index(t => t.EnterpriseCategory_Id);
            
            CreateTable(
                "dbo.EnterpriseEnterpriseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnterpriseId = c.Int(nullable: false),
                        EnterpriseCategoryId = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enterprises", t => t.EnterpriseId, cascadeDelete: true)
                .ForeignKey("dbo.EnterpriseCategories", t => t.EnterpriseCategoryId, cascadeDelete: true)
                .Index(t => t.EnterpriseId)
                .Index(t => t.EnterpriseCategoryId);
            
            CreateTable(
                "dbo.EnterpriseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlName = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        DescriptionEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Image = c.String(unicode: false),
                        ShowSearchForm = c.Boolean(nullable: false),
                        CurrentMenuItemId = c.Int(),
                        TemplateId = c.Int(),
                        ParentCategoryId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.CurrentMenuItemId)
                .ForeignKey("dbo.EnterpriseCategories", t => t.ParentCategoryId)
                .ForeignKey("dbo.EnterpriseCategoryTemplates", t => t.TemplateId)
                .Index(t => t.CurrentMenuItemId)
                .Index(t => t.TemplateId)
                .Index(t => t.ParentCategoryId);
            
            CreateTable(
                "dbo.EnterpriseCategoryTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Preview = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContentRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        BackgroundColor = c.String(unicode: false),
                        BackgroundImage = c.String(unicode: false),
                        BackgroundPosition = c.String(unicode: false),
                        BackgroundRepeat = c.String(unicode: false),
                        BackgroundSize = c.String(unicode: false),
                        CssClasses = c.String(unicode: false),
                        CssStyles = c.String(unicode: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContentColumns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        LgWidth = c.Int(nullable: false),
                        MdWidth = c.Int(nullable: false),
                        SmWidth = c.Int(nullable: false),
                        XsWidth = c.Int(nullable: false),
                        LgOffset = c.Int(nullable: false),
                        MdOffset = c.Int(nullable: false),
                        SmOffset = c.Int(nullable: false),
                        XsOffset = c.Int(nullable: false),
                        BackgroundColor = c.String(unicode: false),
                        BackgroundImage = c.String(unicode: false),
                        BackgroundPosition = c.String(unicode: false),
                        BackgroundRepeat = c.String(unicode: false),
                        BackgroundSize = c.String(unicode: false),
                        CssClasses = c.String(unicode: false),
                        CssStyles = c.String(unicode: false),
                        Type = c.String(unicode: false),
                        ContentRowId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentRows", t => t.ContentRowId, cascadeDelete: true)
                .Index(t => t.ContentRowId);
            
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        BodyRu = c.String(unicode: false),
                        BodyUk = c.String(unicode: false),
                        BodyEn = c.String(unicode: false),
                        ContentColumnId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentColumns", t => t.ContentColumnId, cascadeDelete: true)
                .Index(t => t.ContentColumnId);
            
            CreateTable(
                "dbo.EnterprisePersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnterpriseId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                        PostUk = c.String(unicode: false),
                        PostRu = c.String(unicode: false),
                        PostEn = c.String(unicode: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enterprises", t => t.EnterpriseId, cascadeDelete: true)
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.EnterpriseId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        PostRu = c.String(unicode: false),
                        PostUk = c.String(unicode: false),
                        PostEn = c.String(unicode: false),
                        ProfessionRu = c.String(unicode: false),
                        ProfessionUk = c.String(unicode: false),
                        ProfessionEn = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionEn = c.String(unicode: false),
                        KeywordsUk = c.String(unicode: false),
                        KeywordsRu = c.String(unicode: false),
                        KeywordsEn = c.String(unicode: false),
                        ContactAddressRu = c.String(unicode: false),
                        ContactAddressUk = c.String(unicode: false),
                        ContactAddressEn = c.String(unicode: false),
                        ContactEmailsUk = c.String(unicode: false),
                        ContactEmailsRu = c.String(unicode: false),
                        ContactEmailsEn = c.String(unicode: false),
                        ContactPhonesUk = c.String(unicode: false),
                        ContactPhonesRu = c.String(unicode: false),
                        ContactPhonesEn = c.String(unicode: false),
                        ReceptionAddressRu = c.String(unicode: false),
                        ReceptionAddressUk = c.String(unicode: false),
                        ReceptionAddressEn = c.String(unicode: false),
                        ReceptionDatesRu = c.String(unicode: false),
                        ReceptionDatesUk = c.String(unicode: false),
                        ReceptionDatesEn = c.String(unicode: false),
                        ReceptionTimeRu = c.String(unicode: false),
                        ReceptionTimeUk = c.String(unicode: false),
                        ReceptionTimeEn = c.String(unicode: false),
                        NotificationEmail = c.String(unicode: false),
                        NotificationPhone = c.String(unicode: false),
                        UrlNameRu = c.String(unicode: false),
                        UrlNameUk = c.String(unicode: false),
                        UrlNameEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        DeputyFractionId = c.Int(),
                        FbLink = c.String(unicode: false),
                        TwLink = c.String(unicode: false),
                        GpLink = c.String(unicode: false),
                        YtLink = c.String(unicode: false),
                        IgLink = c.String(unicode: false),
                        Image = c.String(unicode: false),
                        Birthday = c.DateTime(precision: 0),
                        HasContactForm = c.Boolean(nullable: false),
                        HasReceptionForm = c.Boolean(nullable: false),
                        ShowPublihDate = c.Boolean(nullable: false),
                        ShowEditDate = c.Boolean(nullable: false),
                        Facebook = c.String(unicode: false),
                        Twitter = c.String(unicode: false),
                        Published = c.Boolean(nullable: false),
                        Saved = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        LastEditDate = c.DateTime(precision: 0),
                        PublishDate = c.DateTime(precision: 0),
                        EditedDate = c.DateTime(precision: 0),
                        ViewCount = c.Int(),
                        SidebarMenuId = c.Int(),
                        CreateUserId = c.Int(),
                        LastEditUserId = c.Int(),
                        CurrentMenuItemId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.CurrentMenuItemId)
                .ForeignKey("dbo.Enterprises", t => t.DeputyFractionId)
                .ForeignKey("dbo.Menus", t => t.SidebarMenuId)
                .Index(t => t.DeputyFractionId)
                .Index(t => t.SidebarMenuId)
                .Index(t => t.CurrentMenuItemId);
            
            CreateTable(
                "dbo.PersonPersonCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        PersonCategoryId = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                        ShowOnPersonPage = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.PersonCategories", t => t.PersonCategoryId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.PersonCategoryId);
            
            CreateTable(
                "dbo.PersonCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlName = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        DescriptionEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Image = c.String(unicode: false),
                        ShowSearchForm = c.Boolean(nullable: false),
                        SidebarMenuId = c.Int(),
                        ParentPersonCategoryId = c.Int(),
                        CurrentMenuItemId = c.Int(),
                        TemplateId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.CurrentMenuItemId)
                .ForeignKey("dbo.PersonCategories", t => t.ParentPersonCategoryId)
                .ForeignKey("dbo.Menus", t => t.SidebarMenuId)
                .ForeignKey("dbo.PersonCategoryTemplates", t => t.TemplateId)
                .Index(t => t.SidebarMenuId)
                .Index(t => t.ParentPersonCategoryId)
                .Index(t => t.CurrentMenuItemId)
                .Index(t => t.TemplateId);
            
            CreateTable(
                "dbo.PersonCategoryTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Preview = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EnterpriseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        File = c.String(unicode: false),
                        Size = c.Double(nullable: false),
                        Extension = c.String(unicode: false),
                        DocId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Docs", t => t.DocId, cascadeDelete: true)
                .Index(t => t.DocId);
            
            CreateTable(
                "dbo.DocCategoryTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Preview = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlName = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        KeywordsUk = c.String(unicode: false),
                        KeywordsRu = c.String(unicode: false),
                        KeywordsEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        Type = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionEn = c.String(unicode: false),
                        CleanTextRu = c.String(unicode: false),
                        CleanTextUk = c.String(unicode: false),
                        CleanTextEn = c.String(unicode: false),
                        ViewCount = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        LastEditDate = c.DateTime(precision: 0),
                        PublishDate = c.DateTime(precision: 0),
                        EditedDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(),
                        LastEditUserId = c.Int(),
                        PageTemplateId = c.Int(),
                        SidebarMenuId = c.Int(),
                        CurrentMenuItemId = c.Int(),
                        Saved = c.Boolean(nullable: false),
                        Published = c.Boolean(nullable: false),
                        ShowPublihDate = c.Boolean(nullable: false),
                        ShowEditDate = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.CurrentMenuItemId)
                .ForeignKey("dbo.Menus", t => t.SidebarMenuId)
                .ForeignKey("dbo.PageTemplates", t => t.PageTemplateId)
                .Index(t => t.PageTemplateId)
                .Index(t => t.SidebarMenuId)
                .Index(t => t.CurrentMenuItemId);
            
            CreateTable(
                "dbo.PageTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CmsRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(unicode: false),
                        Name = c.String(unicode: false),
                        Premissions = c.String(unicode: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        UrlNameRu = c.String(unicode: false),
                        UrlNameUk = c.String(unicode: false),
                        UrlNameEn = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        DescriptionEn = c.String(unicode: false),
                        KeywordsUk = c.String(unicode: false),
                        KeywordsRu = c.String(unicode: false),
                        KeywordsEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        ShowPublihDate = c.Boolean(nullable: false),
                        ShowEditDate = c.Boolean(nullable: false),
                        AllowComments = c.Boolean(nullable: false),
                        ExportToOpenData = c.Boolean(nullable: false),
                        OpenDataId = c.String(unicode: false),
                        OpenDataTitle = c.String(unicode: false),
                        OpenDataDescription = c.String(unicode: false),
                        OpenDataKeywords = c.String(unicode: false),
                        OpenDataRefresh = c.String(unicode: false),
                        OpenDataCategory = c.String(unicode: false),
                        OpenDataName = c.String(unicode: false),
                        OpenDataEmail = c.String(unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        PublishDate = c.DateTime(precision: 0),
                        LastEditDate = c.DateTime(precision: 0),
                        EventDate = c.DateTime(precision: 0),
                        EditedDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(),
                        LastEditUserId = c.Int(),
                        Published = c.Boolean(nullable: false),
                        Saved = c.Boolean(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        Image = c.String(unicode: false),
                        ImageSource = c.String(unicode: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsletterArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsletterId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Newsletters", t => t.NewsletterId, cascadeDelete: true)
                .Index(t => t.NewsletterId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Newsletters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        UrlNameRu = c.String(unicode: false),
                        UrlNameUk = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        PublishDate = c.DateTime(nullable: false, precision: 0),
                        LastEditDate = c.DateTime(nullable: false, precision: 0),
                        LastBulkDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(),
                        LastEditUserId = c.Int(),
                        FileRu = c.String(unicode: false),
                        FileUk = c.String(unicode: false),
                        Published = c.Boolean(nullable: false),
                        Saved = c.Boolean(nullable: false),
                        Sended = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleCategoryTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Preview = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TextRu = c.String(unicode: false),
                        TextUk = c.String(unicode: false),
                        TextEn = c.String(unicode: false),
                        Url = c.String(unicode: false),
                        Image = c.String(unicode: false),
                        Link = c.String(unicode: false),
                        Published = c.Boolean(nullable: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CmsSearches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        CleanTextRu = c.String(unicode: false),
                        CleanTextUk = c.String(unicode: false),
                        CleanTextEn = c.String(unicode: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        Published = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CmsSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleUk = c.String(unicode: false),
                        TitlePrefixUk = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitlePrefixRu = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        TitlePrefixREn = c.String(unicode: false),
                        LogoUk = c.String(unicode: false),
                        LogoRu = c.String(unicode: false),
                        LogoEn = c.String(unicode: false),
                        BaseDomain = c.String(unicode: false),
                        AdminDomain = c.String(unicode: false),
                        CdnDomain = c.String(unicode: false),
                        UploadDomain = c.String(unicode: false),
                        DefaultColorSchema = c.String(unicode: false),
                        DefaultCulture = c.String(unicode: false),
                        Cultures = c.String(unicode: false),
                        ReleaseVersion = c.String(unicode: false),
                        ProductVersion = c.String(unicode: false),
                        DesignVersion = c.String(unicode: false),
                        DefaultLat = c.Double(nullable: false),
                        DefaultLng = c.Double(nullable: false),
                        DefaultMapZoom = c.Int(nullable: false),
                        DefaultMetaKeywordsUk = c.String(unicode: false),
                        DefaultMetaDescriptionUk = c.String(unicode: false),
                        DefaultMetaKeywordsRu = c.String(unicode: false),
                        DefaultMetaDescriptionRu = c.String(unicode: false),
                        DefaultMetaKeywordsEn = c.String(unicode: false),
                        DefaultMetaDescriptionEn = c.String(unicode: false),
                        GoogleMapApiKey = c.String(unicode: false),
                        GoogleCaptchaApiKey = c.String(unicode: false),
                        OpenDataApiKey = c.String(unicode: false),
                        DisqusName = c.String(unicode: false),
                        GoogleAnaliticsCode = c.String(unicode: false),
                        YandexMetricsCode = c.String(unicode: false),
                        HeadSection = c.String(unicode: false),
                        BodySection = c.String(unicode: false),
                        DefaultEmail = c.String(unicode: false),
                        DefaultEmailName = c.String(unicode: false),
                        DefaultEmailHost = c.String(unicode: false),
                        DefaultEmailPort = c.Int(nullable: false),
                        DefaultEmailUser = c.String(unicode: false),
                        DefaultEmailPassword = c.String(unicode: false),
                        DefaultEmailSsl = c.Boolean(nullable: false),
                        Favicon = c.String(unicode: false),
                        ShowMenuItemMode = c.Int(nullable: false),
                        DateLastEdit = c.DateTime(precision: 0),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContentWidgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CmsUserArticleCategories",
                c => new
                    {
                        CmsUser_Id = c.Int(nullable: false),
                        ArticleCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CmsUser_Id, t.ArticleCategory_Id })
                .ForeignKey("dbo.CmsUsers", t => t.CmsUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.ArticleCategories", t => t.ArticleCategory_Id, cascadeDelete: true)
                .Index(t => t.CmsUser_Id)
                .Index(t => t.ArticleCategory_Id);
            
            CreateTable(
                "dbo.DocCategoryCmsUsers",
                c => new
                    {
                        DocCategory_Id = c.Int(nullable: false),
                        CmsUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DocCategory_Id, t.CmsUser_Id })
                .ForeignKey("dbo.DocCategories", t => t.DocCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.CmsUsers", t => t.CmsUser_Id, cascadeDelete: true)
                .Index(t => t.DocCategory_Id)
                .Index(t => t.CmsUser_Id);
            
            CreateTable(
                "dbo.DocDocCategories",
                c => new
                    {
                        Doc_Id = c.Int(nullable: false),
                        DocCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Doc_Id, t.DocCategory_Id })
                .ForeignKey("dbo.Docs", t => t.Doc_Id, cascadeDelete: true)
                .ForeignKey("dbo.DocCategories", t => t.DocCategory_Id, cascadeDelete: true)
                .Index(t => t.Doc_Id)
                .Index(t => t.DocCategory_Id);
            
            CreateTable(
                "dbo.EnterpriseCmsUsers",
                c => new
                    {
                        Enterprise_Id = c.Int(nullable: false),
                        CmsUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Enterprise_Id, t.CmsUser_Id })
                .ForeignKey("dbo.Enterprises", t => t.Enterprise_Id, cascadeDelete: true)
                .ForeignKey("dbo.CmsUsers", t => t.CmsUser_Id, cascadeDelete: true)
                .Index(t => t.Enterprise_Id)
                .Index(t => t.CmsUser_Id);
            
            CreateTable(
                "dbo.EnterpriseCategoryCmsUsers",
                c => new
                    {
                        EnterpriseCategory_Id = c.Int(nullable: false),
                        CmsUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EnterpriseCategory_Id, t.CmsUser_Id })
                .ForeignKey("dbo.EnterpriseCategories", t => t.EnterpriseCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.CmsUsers", t => t.CmsUser_Id, cascadeDelete: true)
                .Index(t => t.EnterpriseCategory_Id)
                .Index(t => t.CmsUser_Id);
            
            CreateTable(
                "dbo.EnterpriseContentRow",
                c => new
                    {
                        EnterpriseId = c.Int(nullable: false),
                        ContenRowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EnterpriseId, t.ContenRowId })
                .ForeignKey("dbo.Enterprises", t => t.EnterpriseId, cascadeDelete: true)
                .ForeignKey("dbo.ContentRows", t => t.ContenRowId, cascadeDelete: true)
                .Index(t => t.EnterpriseId)
                .Index(t => t.ContenRowId);
            
            CreateTable(
                "dbo.EnterpriseDocs",
                c => new
                    {
                        Enterprise_Id = c.Int(nullable: false),
                        Doc_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Enterprise_Id, t.Doc_Id })
                .ForeignKey("dbo.Enterprises", t => t.Enterprise_Id, cascadeDelete: true)
                .ForeignKey("dbo.Docs", t => t.Doc_Id, cascadeDelete: true)
                .Index(t => t.Enterprise_Id)
                .Index(t => t.Doc_Id);
            
            CreateTable(
                "dbo.PersonCmsUsers",
                c => new
                    {
                        Person_Id = c.Int(nullable: false),
                        CmsUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_Id, t.CmsUser_Id })
                .ForeignKey("dbo.Persons", t => t.Person_Id, cascadeDelete: true)
                .ForeignKey("dbo.CmsUsers", t => t.CmsUser_Id, cascadeDelete: true)
                .Index(t => t.Person_Id)
                .Index(t => t.CmsUser_Id);
            
            CreateTable(
                "dbo.PersonCategoryCmsUsers",
                c => new
                    {
                        PersonCategory_Id = c.Int(nullable: false),
                        CmsUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonCategory_Id, t.CmsUser_Id })
                .ForeignKey("dbo.PersonCategories", t => t.PersonCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.CmsUsers", t => t.CmsUser_Id, cascadeDelete: true)
                .Index(t => t.PersonCategory_Id)
                .Index(t => t.CmsUser_Id);
            
            CreateTable(
                "dbo.PersonContentRow",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        ContenRowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonId, t.ContenRowId })
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.ContentRows", t => t.ContenRowId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.ContenRowId);
            
            CreateTable(
                "dbo.PersonDocs",
                c => new
                    {
                        Person_Id = c.Int(nullable: false),
                        Doc_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_Id, t.Doc_Id })
                .ForeignKey("dbo.Persons", t => t.Person_Id, cascadeDelete: true)
                .ForeignKey("dbo.Docs", t => t.Doc_Id, cascadeDelete: true)
                .Index(t => t.Person_Id)
                .Index(t => t.Doc_Id);
            
            CreateTable(
                "dbo.PageCmsUsers",
                c => new
                    {
                        Page_Id = c.Int(nullable: false),
                        CmsUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Page_Id, t.CmsUser_Id })
                .ForeignKey("dbo.Pages", t => t.Page_Id, cascadeDelete: true)
                .ForeignKey("dbo.CmsUsers", t => t.CmsUser_Id, cascadeDelete: true)
                .Index(t => t.Page_Id)
                .Index(t => t.CmsUser_Id);
            
            CreateTable(
                "dbo.PageContentRow",
                c => new
                    {
                        PageId = c.Int(nullable: false),
                        ContentRowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PageId, t.ContentRowId })
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .ForeignKey("dbo.ContentRows", t => t.ContentRowId, cascadeDelete: true)
                .Index(t => t.PageId)
                .Index(t => t.ContentRowId);
            
            CreateTable(
                "dbo.CmsRoleCmsUsers",
                c => new
                    {
                        CmsRole_Id = c.Int(nullable: false),
                        CmsUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CmsRole_Id, t.CmsUser_Id })
                .ForeignKey("dbo.CmsRoles", t => t.CmsRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.CmsUsers", t => t.CmsUser_Id, cascadeDelete: true)
                .Index(t => t.CmsRole_Id)
                .Index(t => t.CmsUser_Id);
            
            CreateTable(
                "dbo.ArticleArticleCategories",
                c => new
                    {
                        Article_Id = c.Int(nullable: false),
                        ArticleCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_Id, t.ArticleCategory_Id })
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .ForeignKey("dbo.ArticleCategories", t => t.ArticleCategory_Id, cascadeDelete: true)
                .Index(t => t.Article_Id)
                .Index(t => t.ArticleCategory_Id);
            
            CreateTable(
                "dbo.ArticleContentRow",
                c => new
                    {
                        ArticleId = c.Int(nullable: false),
                        ContenRowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArticleId, t.ContenRowId })
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.ContentRows", t => t.ContenRowId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.ContenRowId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleCategories", "TemplateId", "dbo.ArticleCategoryTemplates");
            DropForeignKey("dbo.ArticleCategories", "SidebarMenuId", "dbo.Menus");
            DropForeignKey("dbo.ArticleCategories", "RelatedCategory_Id", "dbo.ArticleCategories");
            DropForeignKey("dbo.ArticleCategories", "ParentArticleCategoryId", "dbo.ArticleCategories");
            DropForeignKey("dbo.ArticleCategories", "CurrentMenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.NewsletterArticles", "NewsletterId", "dbo.Newsletters");
            DropForeignKey("dbo.NewsletterArticles", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleContentRow", "ContenRowId", "dbo.ContentRows");
            DropForeignKey("dbo.ArticleContentRow", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleArticleCategories", "ArticleCategory_Id", "dbo.ArticleCategories");
            DropForeignKey("dbo.ArticleArticleCategories", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.CmsRoleCmsUsers", "CmsUser_Id", "dbo.CmsUsers");
            DropForeignKey("dbo.CmsRoleCmsUsers", "CmsRole_Id", "dbo.CmsRoles");
            DropForeignKey("dbo.Pages", "PageTemplateId", "dbo.PageTemplates");
            DropForeignKey("dbo.Pages", "SidebarMenuId", "dbo.Menus");
            DropForeignKey("dbo.Pages", "CurrentMenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.PageContentRow", "ContentRowId", "dbo.ContentRows");
            DropForeignKey("dbo.PageContentRow", "PageId", "dbo.Pages");
            DropForeignKey("dbo.PageCmsUsers", "CmsUser_Id", "dbo.CmsUsers");
            DropForeignKey("dbo.PageCmsUsers", "Page_Id", "dbo.Pages");
            DropForeignKey("dbo.DocCategories", "TemplateId", "dbo.DocCategoryTemplates");
            DropForeignKey("dbo.DocCategories", "RelatedCategoryId", "dbo.DocCategories");
            DropForeignKey("dbo.DocCategories", "ParentCategoryId", "dbo.DocCategories");
            DropForeignKey("dbo.DocFiles", "DocId", "dbo.Docs");
            DropForeignKey("dbo.Enterprises", "TypeId", "dbo.EnterpriseTypes");
            DropForeignKey("dbo.Enterprises", "SidebarMenuId", "dbo.Menus");
            DropForeignKey("dbo.Persons", "SidebarMenuId", "dbo.Menus");
            DropForeignKey("dbo.EnterprisePersons", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.PersonDocs", "Doc_Id", "dbo.Docs");
            DropForeignKey("dbo.PersonDocs", "Person_Id", "dbo.Persons");
            DropForeignKey("dbo.Persons", "DeputyFractionId", "dbo.Enterprises");
            DropForeignKey("dbo.Persons", "CurrentMenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.PersonContentRow", "ContenRowId", "dbo.ContentRows");
            DropForeignKey("dbo.PersonContentRow", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.PersonCategories", "TemplateId", "dbo.PersonCategoryTemplates");
            DropForeignKey("dbo.PersonCategories", "SidebarMenuId", "dbo.Menus");
            DropForeignKey("dbo.PersonPersonCategories", "PersonCategoryId", "dbo.PersonCategories");
            DropForeignKey("dbo.PersonCategories", "ParentPersonCategoryId", "dbo.PersonCategories");
            DropForeignKey("dbo.PersonCategories", "CurrentMenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.PersonCategoryCmsUsers", "CmsUser_Id", "dbo.CmsUsers");
            DropForeignKey("dbo.PersonCategoryCmsUsers", "PersonCategory_Id", "dbo.PersonCategories");
            DropForeignKey("dbo.PersonPersonCategories", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.PersonCmsUsers", "CmsUser_Id", "dbo.CmsUsers");
            DropForeignKey("dbo.PersonCmsUsers", "Person_Id", "dbo.Persons");
            DropForeignKey("dbo.EnterprisePersons", "EnterpriseId", "dbo.Enterprises");
            DropForeignKey("dbo.EnterpriseDocs", "Doc_Id", "dbo.Docs");
            DropForeignKey("dbo.EnterpriseDocs", "Enterprise_Id", "dbo.Enterprises");
            DropForeignKey("dbo.Enterprises", "CurrentMenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.EnterpriseContentRow", "ContenRowId", "dbo.ContentRows");
            DropForeignKey("dbo.EnterpriseContentRow", "EnterpriseId", "dbo.Enterprises");
            DropForeignKey("dbo.Contents", "ContentColumnId", "dbo.ContentColumns");
            DropForeignKey("dbo.ContentColumns", "ContentRowId", "dbo.ContentRows");
            DropForeignKey("dbo.Enterprises", "ParentEnterpriseId", "dbo.Enterprises");
            DropForeignKey("dbo.EnterpriseEnterpriseCategories", "EnterpriseCategoryId", "dbo.EnterpriseCategories");
            DropForeignKey("dbo.EnterpriseCategories", "TemplateId", "dbo.EnterpriseCategoryTemplates");
            DropForeignKey("dbo.EnterpriseCategories", "ParentCategoryId", "dbo.EnterpriseCategories");
            DropForeignKey("dbo.Enterprises", "EnterpriseCategory_Id", "dbo.EnterpriseCategories");
            DropForeignKey("dbo.EnterpriseCategories", "CurrentMenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.EnterpriseCategoryCmsUsers", "CmsUser_Id", "dbo.CmsUsers");
            DropForeignKey("dbo.EnterpriseCategoryCmsUsers", "EnterpriseCategory_Id", "dbo.EnterpriseCategories");
            DropForeignKey("dbo.EnterpriseEnterpriseCategories", "EnterpriseId", "dbo.Enterprises");
            DropForeignKey("dbo.EnterpriseCmsUsers", "CmsUser_Id", "dbo.CmsUsers");
            DropForeignKey("dbo.EnterpriseCmsUsers", "Enterprise_Id", "dbo.Enterprises");
            DropForeignKey("dbo.Docs", "DocTypeId", "dbo.DocTypes");
            DropForeignKey("dbo.DocDocCategories", "DocCategory_Id", "dbo.DocCategories");
            DropForeignKey("dbo.DocDocCategories", "Doc_Id", "dbo.Docs");
            DropForeignKey("dbo.DocCategories", "CurrentMenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItems", "ParentMenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItems", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.DocCategoryCmsUsers", "CmsUser_Id", "dbo.CmsUsers");
            DropForeignKey("dbo.DocCategoryCmsUsers", "DocCategory_Id", "dbo.DocCategories");
            DropForeignKey("dbo.CmsUserArticleCategories", "ArticleCategory_Id", "dbo.ArticleCategories");
            DropForeignKey("dbo.CmsUserArticleCategories", "CmsUser_Id", "dbo.CmsUsers");
            DropIndex("dbo.ArticleContentRow", new[] { "ContenRowId" });
            DropIndex("dbo.ArticleContentRow", new[] { "ArticleId" });
            DropIndex("dbo.ArticleArticleCategories", new[] { "ArticleCategory_Id" });
            DropIndex("dbo.ArticleArticleCategories", new[] { "Article_Id" });
            DropIndex("dbo.CmsRoleCmsUsers", new[] { "CmsUser_Id" });
            DropIndex("dbo.CmsRoleCmsUsers", new[] { "CmsRole_Id" });
            DropIndex("dbo.PageContentRow", new[] { "ContentRowId" });
            DropIndex("dbo.PageContentRow", new[] { "PageId" });
            DropIndex("dbo.PageCmsUsers", new[] { "CmsUser_Id" });
            DropIndex("dbo.PageCmsUsers", new[] { "Page_Id" });
            DropIndex("dbo.PersonDocs", new[] { "Doc_Id" });
            DropIndex("dbo.PersonDocs", new[] { "Person_Id" });
            DropIndex("dbo.PersonContentRow", new[] { "ContenRowId" });
            DropIndex("dbo.PersonContentRow", new[] { "PersonId" });
            DropIndex("dbo.PersonCategoryCmsUsers", new[] { "CmsUser_Id" });
            DropIndex("dbo.PersonCategoryCmsUsers", new[] { "PersonCategory_Id" });
            DropIndex("dbo.PersonCmsUsers", new[] { "CmsUser_Id" });
            DropIndex("dbo.PersonCmsUsers", new[] { "Person_Id" });
            DropIndex("dbo.EnterpriseDocs", new[] { "Doc_Id" });
            DropIndex("dbo.EnterpriseDocs", new[] { "Enterprise_Id" });
            DropIndex("dbo.EnterpriseContentRow", new[] { "ContenRowId" });
            DropIndex("dbo.EnterpriseContentRow", new[] { "EnterpriseId" });
            DropIndex("dbo.EnterpriseCategoryCmsUsers", new[] { "CmsUser_Id" });
            DropIndex("dbo.EnterpriseCategoryCmsUsers", new[] { "EnterpriseCategory_Id" });
            DropIndex("dbo.EnterpriseCmsUsers", new[] { "CmsUser_Id" });
            DropIndex("dbo.EnterpriseCmsUsers", new[] { "Enterprise_Id" });
            DropIndex("dbo.DocDocCategories", new[] { "DocCategory_Id" });
            DropIndex("dbo.DocDocCategories", new[] { "Doc_Id" });
            DropIndex("dbo.DocCategoryCmsUsers", new[] { "CmsUser_Id" });
            DropIndex("dbo.DocCategoryCmsUsers", new[] { "DocCategory_Id" });
            DropIndex("dbo.CmsUserArticleCategories", new[] { "ArticleCategory_Id" });
            DropIndex("dbo.CmsUserArticleCategories", new[] { "CmsUser_Id" });
            DropIndex("dbo.NewsletterArticles", new[] { "ArticleId" });
            DropIndex("dbo.NewsletterArticles", new[] { "NewsletterId" });
            DropIndex("dbo.Pages", new[] { "CurrentMenuItemId" });
            DropIndex("dbo.Pages", new[] { "SidebarMenuId" });
            DropIndex("dbo.Pages", new[] { "PageTemplateId" });
            DropIndex("dbo.DocFiles", new[] { "DocId" });
            DropIndex("dbo.PersonCategories", new[] { "TemplateId" });
            DropIndex("dbo.PersonCategories", new[] { "CurrentMenuItemId" });
            DropIndex("dbo.PersonCategories", new[] { "ParentPersonCategoryId" });
            DropIndex("dbo.PersonCategories", new[] { "SidebarMenuId" });
            DropIndex("dbo.PersonPersonCategories", new[] { "PersonCategoryId" });
            DropIndex("dbo.PersonPersonCategories", new[] { "PersonId" });
            DropIndex("dbo.Persons", new[] { "CurrentMenuItemId" });
            DropIndex("dbo.Persons", new[] { "SidebarMenuId" });
            DropIndex("dbo.Persons", new[] { "DeputyFractionId" });
            DropIndex("dbo.EnterprisePersons", new[] { "PersonId" });
            DropIndex("dbo.EnterprisePersons", new[] { "EnterpriseId" });
            DropIndex("dbo.Contents", new[] { "ContentColumnId" });
            DropIndex("dbo.ContentColumns", new[] { "ContentRowId" });
            DropIndex("dbo.EnterpriseCategories", new[] { "ParentCategoryId" });
            DropIndex("dbo.EnterpriseCategories", new[] { "TemplateId" });
            DropIndex("dbo.EnterpriseCategories", new[] { "CurrentMenuItemId" });
            DropIndex("dbo.EnterpriseEnterpriseCategories", new[] { "EnterpriseCategoryId" });
            DropIndex("dbo.EnterpriseEnterpriseCategories", new[] { "EnterpriseId" });
            DropIndex("dbo.Enterprises", new[] { "EnterpriseCategory_Id" });
            DropIndex("dbo.Enterprises", new[] { "CurrentMenuItemId" });
            DropIndex("dbo.Enterprises", new[] { "SidebarMenuId" });
            DropIndex("dbo.Enterprises", new[] { "ParentEnterpriseId" });
            DropIndex("dbo.Enterprises", new[] { "TypeId" });
            DropIndex("dbo.Docs", new[] { "DocTypeId" });
            DropIndex("dbo.MenuItems", new[] { "ParentMenuItemId" });
            DropIndex("dbo.MenuItems", new[] { "MenuId" });
            DropIndex("dbo.DocCategories", new[] { "RelatedCategoryId" });
            DropIndex("dbo.DocCategories", new[] { "ParentCategoryId" });
            DropIndex("dbo.DocCategories", new[] { "TemplateId" });
            DropIndex("dbo.DocCategories", new[] { "CurrentMenuItemId" });
            DropIndex("dbo.ArticleCategories", new[] { "RelatedCategory_Id" });
            DropIndex("dbo.ArticleCategories", new[] { "ParentArticleCategoryId" });
            DropIndex("dbo.ArticleCategories", new[] { "TemplateId" });
            DropIndex("dbo.ArticleCategories", new[] { "CurrentMenuItemId" });
            DropIndex("dbo.ArticleCategories", new[] { "SidebarMenuId" });
            DropTable("dbo.ArticleContentRow");
            DropTable("dbo.ArticleArticleCategories");
            DropTable("dbo.CmsRoleCmsUsers");
            DropTable("dbo.PageContentRow");
            DropTable("dbo.PageCmsUsers");
            DropTable("dbo.PersonDocs");
            DropTable("dbo.PersonContentRow");
            DropTable("dbo.PersonCategoryCmsUsers");
            DropTable("dbo.PersonCmsUsers");
            DropTable("dbo.EnterpriseDocs");
            DropTable("dbo.EnterpriseContentRow");
            DropTable("dbo.EnterpriseCategoryCmsUsers");
            DropTable("dbo.EnterpriseCmsUsers");
            DropTable("dbo.DocDocCategories");
            DropTable("dbo.DocCategoryCmsUsers");
            DropTable("dbo.CmsUserArticleCategories");
            DropTable("dbo.ContentWidgets");
            DropTable("dbo.CmsSettings");
            DropTable("dbo.CmsSearches");
            DropTable("dbo.Banners");
            DropTable("dbo.ArticleCategoryTemplates");
            DropTable("dbo.Newsletters");
            DropTable("dbo.NewsletterArticles");
            DropTable("dbo.Articles");
            DropTable("dbo.CmsRoles");
            DropTable("dbo.PageTemplates");
            DropTable("dbo.Pages");
            DropTable("dbo.DocCategoryTemplates");
            DropTable("dbo.DocFiles");
            DropTable("dbo.EnterpriseTypes");
            DropTable("dbo.PersonCategoryTemplates");
            DropTable("dbo.PersonCategories");
            DropTable("dbo.PersonPersonCategories");
            DropTable("dbo.Persons");
            DropTable("dbo.EnterprisePersons");
            DropTable("dbo.Contents");
            DropTable("dbo.ContentColumns");
            DropTable("dbo.ContentRows");
            DropTable("dbo.EnterpriseCategoryTemplates");
            DropTable("dbo.EnterpriseCategories");
            DropTable("dbo.EnterpriseEnterpriseCategories");
            DropTable("dbo.Enterprises");
            DropTable("dbo.DocTypes");
            DropTable("dbo.Docs");
            DropTable("dbo.Menus");
            DropTable("dbo.MenuItems");
            DropTable("dbo.DocCategories");
            DropTable("dbo.CmsUsers");
            DropTable("dbo.ArticleCategories");
        }
    }
}
