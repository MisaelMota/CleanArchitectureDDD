using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideoListQuery : IRequest<List<VideosVm>>
    {
        //Se necesita esta clase para hacer el request de la peticion
        //Se agrega el parametro de entrada
        public string _UserName { get; set; } = string.Empty;
        public GetVideoListQuery(string username)
        {
            _UserName = username ?? throw new ArgumentNullException(nameof(username));
        }
        
    }
}
