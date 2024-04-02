namespace PortfolioEye.Services;

public interface IHostingInformationProvider
{
    DirectoryInfo ProfilePhotosDirectory { get; set; }
}