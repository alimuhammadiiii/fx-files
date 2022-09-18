﻿using Functionland.FxFiles.Shared.Services;
using Functionland.FxFiles.Shared.Services.Implementations.Db;
using Functionland.FxFiles.Shared.Test.Utils;
using Functionland.FxFiles.Shared.TestInfra.Contracts;
using Functionland.FxFiles.Shared.TestInfra.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionland.FxFiles.Shared.Test.UnitTests
{
    [TestClass]
    public class DbServiceUnitTest : TestBase
    {
        [TestMethod]
        public async Task AddPinDbServiceUnitTest_MustWork()
        {
            var testHost = Host.CreateDefaultBuilder()
               .ConfigureServices((_, services) =>
               {
                   string connectionString = $"DataSource={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "FxDB.db")};";

                   services.AddSingleton<IFxLocalDbService, FxLocalDbService>(_ => new FxLocalDbService(connectionString));
               }
            ).Build();

            var serviceScope = testHost.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var dbService = serviceProvider.GetService<IFxLocalDbService>();

            await dbService.AddPinAsync(new FsArtifact("c:\\txt.txt", "txt",FsArtifactType.File, FsFileProviderType.InternalMemory));
            //Assert.IsNotNull(fileService);

        }

        [TestMethod]
        public async Task RemovePinDbServiceUnitTest_MustWork()
        {
            var testHost = Host.CreateDefaultBuilder()
               .ConfigureServices((_, services) =>
               {
                   string connectionString = $"DataSource={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "FxDB.db")};";

                   services.AddSingleton<IFxLocalDbService, FxLocalDbService>(_ => new FxLocalDbService(connectionString));
               }
            ).Build();

            var serviceScope = testHost.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var dbService = serviceProvider.GetService<IFxLocalDbService>();

            await dbService.RemovePinAsync("c:\\txt.txt");
            //Assert.IsNotNull(fileService);

        }

        [TestMethod]
        public async Task UpdatePinDbServiceUnitTest_MustWork()
        {
            var testHost = Host.CreateDefaultBuilder()
               .ConfigureServices((_, services) =>
               {
                   string connectionString = $"DataSource={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "FxDB.db")};";

                   services.AddSingleton<IFxLocalDbService, FxLocalDbService>(_ => new FxLocalDbService(connectionString));
               }
            ).Build();

            var serviceScope = testHost.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var dbService = serviceProvider.GetService<IFxLocalDbService>();

            await dbService.UpdatePinAsync(new PinnedArtifact
            {
                FullPath = "c:\\txt.txt",
                ContentHash = DateTimeOffset.Now.AddDays(-1).ToString(),
                ThumbnailPath = "c:\\txt.txt"
            }) ;
            //Assert.IsNotNull(fileService);

        }

        [TestMethod]
        public async Task GetPinnedArtifacsDbServiceUnitTest_MustWork()
        {
            var testHost = Host.CreateDefaultBuilder()
               .ConfigureServices((_, services) =>
               {
                   string connectionString = $"DataSource={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "FxDB.db")};";

                   services.AddSingleton<IFxLocalDbService, FxLocalDbService>(_ => new FxLocalDbService(connectionString));
               }
            ).Build();

            var serviceScope = testHost.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var dbService = serviceProvider.GetService<IFxLocalDbService>();

            var pinnedArtifacts = await dbService.GetPinnedArticatInfos();
            Assert.IsNotNull(pinnedArtifacts);

        }

        private void Test_ProgressChanged(object? sender, TestProgressChangedEventArgs e)
        {
            if (e.ProgressType == TestProgressType.Fail)
            {
                Assert.Fail($"{Environment.NewLine}{sender?.GetType().Name}{Environment.NewLine}-> {e.Title}{Environment.NewLine}-> {e.Description}");
            }
        }
    }
}