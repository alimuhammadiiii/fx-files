﻿using Functionland.FxFiles.Client.Shared.Enums;
using Functionland.FxFiles.Client.Shared.Services.Implementations.FsFileInfo;
using Functionland.FxFiles.Client.Shared.Services.Implementations.FxFileInfo;
using Functionland.FxFiles.Client.Shared.Utils;

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

using System.Net;
using System.Text.RegularExpressions;

namespace Functionland.FxFiles.Client.Shared.Services.Implementations;

public class FsFileProvider : IFileProvider
{
    private readonly IFileProvider _fileProvider;
    private ILocalDeviceFileService _localDeviceFileService;
    private IFulaFileService _fulaFileService;
    private IArtifactThumbnailService<ILocalDeviceFileService> _localArtifactThumbnailService;
    private IArtifactThumbnailService<IFulaFileService> _fulaArtifactThumbnailService;


    public FsFileProvider(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
        _localDeviceFileService = FsResolver.Resolve<ILocalDeviceFileService>();
        _fulaFileService = FsResolver.Resolve<IFulaFileService>();
        _localArtifactThumbnailService =FsResolver.Resolve<IArtifactThumbnailService<ILocalDeviceFileService>>();
        _fulaArtifactThumbnailService = FsResolver.Resolve<IArtifactThumbnailService<IFulaFileService>>();
    }

    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        return _fileProvider.GetDirectoryContents(subpath);
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        if (PathProtocolUtils.IsMatch(subpath))
        {
            var (_, protocol, address) = PathProtocolUtils.Match(subpath);

            return protocol switch
            {
                PathProtocol.Storage => new PreviewFileInfo<ILocalDeviceFileService>(PreparePath(address), _localDeviceFileService),
                PathProtocol.Fula => new PreviewFileInfo<IFulaFileService>(PreparePath(address), _fulaFileService),
                PathProtocol.ThumbnailStorage => new ThumbFileInfo<ILocalDeviceFileService>(PreparePath(address), _localArtifactThumbnailService, _localDeviceFileService),
                PathProtocol.ThumbnailFula => new ThumbFileInfo<IFulaFileService>(PreparePath(address), _fulaArtifactThumbnailService, _fulaFileService),
                PathProtocol.Wwwroot => _fileProvider.GetFileInfo(PreparePath("_content/Functionland.FxFiles.Client.Shared/" + address)),
                _ => throw new InvalidOperationException($"Protocol not supported: {protocol}")
            };
        }

        return _fileProvider.GetFileInfo(subpath);
    }

    public IChangeToken Watch(string filter)
    {
        return _fileProvider.Watch(filter);
    }

    private string PreparePath(string path)
    {
        path = WebUtility.UrlDecode(path);
        return path;
    }
}




