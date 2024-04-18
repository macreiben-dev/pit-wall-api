namespace PitWallDataGatheringApi.Integration.Tests.Frameworks;

public readonly struct RelativeUri(string relativeUri)
{
    public Uri Uri { get; } = new(relativeUri, UriKind.Relative);
}