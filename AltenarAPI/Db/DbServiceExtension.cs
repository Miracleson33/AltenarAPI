using AltenarAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenarAPI
{
    // Регистрация DbContext как службы
    public static class DbServiceExtension
    {
        // Определение статического метода расширения AddDatabaseService, который отвечает за регистрацию DbContext, использующего поставщика базы данных SQL Server в контейнере DI.
        public static void AddDatabaseService(this
        IServiceCollection services, string connectionString)
        => services.
        AddDbContext<AltenarAPIContext>(options => options.
        UseSqlServer(connectionString));
    }
}
