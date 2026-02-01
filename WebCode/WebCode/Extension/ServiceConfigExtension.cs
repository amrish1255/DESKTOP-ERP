using Microsoft.AspNetCore.Cors.Infrastructure;
using web.Interface.Repository;
using web.Repository;

namespace WebCode.Extension
{
    public static class ServiceConfigExtension
    {
        public static void addDapperContext(this IServiceCollection services)
        {
            services.AddSingleton<IDapperContext, DapperContext>();
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            //services.AddScoped<IAccountRepository, AccountRepository>();
            //services.AddScoped<IMemberRepository, MemberRepository>();
            //services.AddScoped<IBackOfficeRepository, BackOfficeRepository>();
            //services.AddScoped<IBackOfficeAccountRepository, BackOfficeAccountRepository>();
            //services.AddScoped<IHomeRepository, HomeRepository>();
            //services.AddScoped<IBackOfficeECommerceRepository, BackOfficeECommerceRepository>();


        }
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            //services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<IUserClaimService, UserClaimService>();
            //services.AddScoped<IUploadImage, UploadImage>();
            //services.AddScoped<IFileHelper, FileHelper>();
            //services.AddScoped<IMemberService, MemberService>();
            //services.AddScoped<IBackOfficeService, BackOfficeService>();
            //services.AddScoped<IBackOfficeAccountService, BackOfficeAccountService>();
            //services.AddScoped<IHttpClientHelper, HttpClientHelper>();
            //services.AddScoped<IHomeService, HomeService>();
            //services.AddScoped<IBackOfficeECommerceService, BackOfficeECommerceService>();
            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<IAesEncryptionService, AesEncryptionService>();
            //services.AddScoped<ICartService, CartService>();
            services.AddHttpContextAccessor();

        }
    }
}

