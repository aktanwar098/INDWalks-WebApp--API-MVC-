using INDWalks.API.Models.Domain;
using System.Net;

namespace INDWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
