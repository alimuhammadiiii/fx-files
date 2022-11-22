﻿
namespace Functionland.FxFiles.Client.Shared.Models;

public class DeepSearchFilter
{
    public List<ArtifactCategorySearchType?> ArtifactCategorySearchType { get; set; }
    public ArtifactDateSearchType? ArtifactDateSearchType { get; set; }
    public string? SearchText { get; set; }
}
