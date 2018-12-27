namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using Contexts;
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<CmsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Cms";
        }

        protected override void Seed(CmsDbContext context)
        {
            context.CmsUsers.AddOrUpdate(
                x => x.Login,
                new CmsUser()
                {
                    Id = 1,
                    Login = "admin",
                    Name = "�����������",
                    Email = "bissoft.dev@gmail.com",
                    Salt = "xPrtU78lKp",
                    Password = GetPasswordHash("bissoft.dev@gmail.com", "xPe18cm_Ad", "xPrtU78lKp"),
                    RegisterDate = DateTime.Now,
                    Roles = new List<CmsRole>() { new CmsRole() { Id = 1, Name = "Admin", TitleUk = "�����������", Premissions = "*" } }
                });

            context.ArticleCategoryTemplates.AddOrUpdate(x => x.Name, new ArticleCategoryTemplate()
            {
                Id = 1,
                Name = "simple",
                TitleUk = "������� ������",
                TitleRu = "������� ������",
                TitleEn = "Simple list",
                Position = 1,
                Deleted = false,
            });

            context.PersonCategoryTemplates.AddOrUpdate(x => x.Name, new PersonCategoryTemplate()
            {
                Id = 1,
                Name = "simple",
                TitleUk = "������� ������",
                TitleRu = "������� ������",
                TitleEn = "Simple list",
                Position = 1,
                Deleted = false,
            });

            context.EnterpriseCategoryTemplates.AddOrUpdate(x => x.Name, new EnterpriseCategoryTemplate()
            {
                Id = 1,
                Name = "simple",
                TitleUk = "������� ������",
                TitleRu = "������� ������",
                TitleEn = "Simple list",
                Position = 1,
                Deleted = false,
            });

            context.DocCategoryTemplates.AddOrUpdate(x => x.Name, new DocCategoryTemplate()
            {
                Id = 1,
                Name = "simple",
                TitleUk = "������� ������",
                TitleRu = "������� ������",
                TitleEn = "Simple list",
                Position = 1,
                Deleted = false,
            });

            context.PageTemplates.AddOrUpdate(x => x.Name, new PageTemplate()
            {
                Id = 1,
                Name = "fullwidth",
                TitleUk = "�� ��� ������",
                TitleRu = "�� ��� ������",
                TitleEn = "FullWidth",
                Position = 1,
                Deleted = false,
            });        

            context.CmsPremissions.AddOrUpdate(x => x.Name,

                new CmsPremission() { TitleUk = "�������� ������� �������", TitleRu = "", TitleEn = "", Position = 1, Name = "cr_pages" },
                new CmsPremission() { TitleUk = "���������� ����-�� ������� �������", TitleRu = "", TitleEn = "", Position = 2, Name = "ed_pages_full" },
                new CmsPremission() { TitleUk = "���������� �������� ������� �������", TitleRu = "", TitleEn = "", Position = 3, Name = "ed_pages_allowed" },
                new CmsPremission() { TitleUk = "���������� ������� �������", TitleRu = "", TitleEn = "", Position = 4, Name = "pb_pages" },
                new CmsPremission() { TitleUk = "�������� ������� �������", TitleRu = "", TitleEn = "", Position = 5, Name = "dl_pages" },

                new CmsPremission() { TitleUk = "��������/���������� ����-�� �������", TitleRu = "", TitleEn = "", Position = 6, Name = "cr_cat" },
                new CmsPremission() { TitleUk = "���������� �������� �������", TitleRu = "", TitleEn = "", Position = 7, Name = "ed_cat" },
                new CmsPremission() { TitleUk = "�������� �������", TitleRu = "", TitleEn = "", Position = 8, Name = "dl_cat" },

                new CmsPremission() { TitleUk = "��������/���������� �������� � ����-���� ���������", TitleRu = "", TitleEn = "", Position = 9, Name = "cr_articles_full" },
                new CmsPremission() { TitleUk = "��������/���������� �������� � ���������� ���������", TitleRu = "", TitleEn = "", Position = 10, Name = "cr_articles_allowed" },
                new CmsPremission() { TitleUk = "���������� ��������", TitleRu = "", TitleEn = "", Position = 11, Name = "pb_articles" },
                new CmsPremission() { TitleUk = "�������� ��������", TitleRu = "", TitleEn = "", Position = 12, Name = "dl_articles" },

                new CmsPremission() { TitleUk = "��������/���������� ��������� � �������� ���������", TitleRu = "", TitleEn = "", Position = 13, Name = "cr_docs_full" },
                new CmsPremission() { TitleUk = "��������/���������� ��������� � ���������� ���������", TitleRu = "", TitleEn = "", Position = 14, Name = "cr_docs_allowed" },
                new CmsPremission() { TitleUk = "���������� ���������", TitleRu = "", TitleEn = "", Position = 15, Name = "pb_docs" },
                new CmsPremission() { TitleUk = "�������� ���������", TitleRu = "", TitleEn = "", Position = 16, Name = "dl_docs" },

                new CmsPremission() { TitleUk = "��������/���������� ������� � ����-���� ���������", TitleRu = "", TitleEn = "", Position = 17, Name = "cr_persons_full" },
                new CmsPremission() { TitleUk = "��������/���������� ������� � ���������� ���������", TitleRu = "", TitleEn = "", Position = 18, Name = "cr_persons_allowed" },
                new CmsPremission() { TitleUk = "���������� �������� �������", TitleRu = "", TitleEn = "", Position = 19, Name = "ed_persons_allowed" },                
                new CmsPremission() { TitleUk = "���������� �������", TitleRu = "", TitleEn = "", Position = 20, Name = "pb_persons" },
                new CmsPremission() { TitleUk = "�������� �������", TitleRu = "", TitleEn = "", Position = 21, Name = "dl_persons" },
               
                new CmsPremission() { TitleUk = "��������/���������� ���������� � ����-���� ���������", TitleRu = "", TitleEn = "", Position = 22, Name = "cr_enterprises_full" },
                new CmsPremission() { TitleUk = "��������/���������� ���������� � ���������� ���������", TitleRu = "", TitleEn = "", Position = 23, Name = "cr_enterprises_allowed" },
                new CmsPremission() { TitleUk = "���������� �������� ����������", TitleRu = "", TitleEn = "", Position = 24, Name = "ed_enterprises_allowed" },
                new CmsPremission() { TitleUk = "���������� ����������", TitleRu = "", TitleEn = "", Position = 25, Name = "pb_enterprises" },
                new CmsPremission() { TitleUk = "�������� ����������", TitleRu = "", TitleEn = "", Position = 26, Name = "dl_enterprises" },

                new CmsPremission() { TitleUk = "������ �� ������ ���", TitleRu = "", TitleEn = "", Position = 27, Name = "sessions" },
                new CmsPremission() { TitleUk = "������ �� ������ ������", TitleRu = "", TitleEn = "", Position = 27, Name = "banners" },
                new CmsPremission() { TitleUk = "��������/���������� ����", TitleRu = "", TitleEn = "", Position = 28, Name = "menu_full" },
                new CmsPremission() { TitleUk = "��������/���������� �������� ������ ����", TitleRu = "", TitleEn = "", Position = 29, Name = "menu_allowed" },

                new CmsPremission() { TitleUk = "��������/���������� ��� ������������", TitleRu = "", TitleEn = "", Position = 30, Name = "cr_roles" },
                new CmsPremission() { TitleUk = "��������/���������� ������������", TitleRu = "", TitleEn = "", Position = 31, Name = "cr_users" },
                new CmsPremission() { TitleUk = "�������� ������ ������������", TitleRu = "", TitleEn = "", Position = 32, Name = "al_users" },
                new CmsPremission() { TitleUk = "��������� ������������", TitleRu = "", TitleEn = "", Position = 33, Name = "bl_users" },
                new CmsPremission() { TitleUk = "�������� ������������", TitleRu = "", TitleEn = "", Position = 34, Name = "dl_users" },
                new CmsPremission() { TitleUk = "�������� ������ ������������", TitleRu = "", TitleEn = "", Position = 36, Name = "ch_password" },

                new CmsPremission() { TitleUk = "���������� ������������ �������", TitleRu = "", TitleEn = "", Position = 37, Name = "settings" });

            context.SaveChanges();
        }

        private String CreateHash(String source, SHA256 encrypter)
        {
            var hash = encrypter.ComputeHash(Encoding.Default.GetBytes(source));

            return BitConverter.ToString(hash).Replace("-", String.Empty);
        }

        public string GetPasswordHash(string email, string password, string salt)
        {
            var result = String.Empty;
            var sha = SHA256.Create();
            var hash = CreateHash(email + password + salt, sha);

            for (var i = 0; i < 500; i++)
            {
                hash = CreateHash(hash, sha);
            }

            return hash;
        }
    }
}
