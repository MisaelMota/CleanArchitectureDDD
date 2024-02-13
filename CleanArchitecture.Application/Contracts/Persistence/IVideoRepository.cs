using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IVideoRepository: IAsyncRepository<Video>
    {
        Task<IEnumerable<Video>> GetVideoByName(string videoName);
        Task<IEnumerable<Video>> GetVideoByUsername(string userName);
    }
}
