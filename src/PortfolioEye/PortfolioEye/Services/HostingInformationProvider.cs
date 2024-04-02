namespace PortfolioEye.Services;

public class HostingInformationProvider : IHostingInformationProvider
{
    private readonly IWebHostEnvironment _env;

    public HostingInformationProvider(IWebHostEnvironment env)
    {
        _env = env;
        ProfilePhotosDirectory = new DirectoryInfo(Path.Combine(env.ContentRootPath, "Data/ProfilePhotos"));
        if(!ProfilePhotosDirectory.Exists)
            ProfilePhotosDirectory.Create();
    }

    public DirectoryInfo ProfilePhotosDirectory { get; set; }
}