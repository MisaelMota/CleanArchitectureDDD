using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    //Este es el manejador de las llamadas
    public class GetVideoListQueryHandler : IRequestHandler<GetVideoListQuery, List<VideosVm>>
    {
        //Acceso a la interfaz que contiene el metodo
        private readonly IVideoRepository _videoRepository;
        //Se necesita mapear los datos 
        private readonly IMapper _mapper;

        // el constructor que inicialice las variables con inyeccion de dependencias
        public GetVideoListQueryHandler(IVideoRepository videoRepository, IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        //Metodo que maneja la peticion
        public async Task<List<VideosVm>> Handle(GetVideoListQuery request, CancellationToken cancellationToken)
        {
            // se utiliza el metodo para buscar los records y el parametro a utilizar
            var videoList = await _videoRepository.GetVideoByUsername(request._UserName);
            // se mapea el resultado de la clase general a la clase especifica, de videos(general) a videos viewmodel(especifico)
            return _mapper.Map<List<VideosVm>>(videoList);
        }
    }
}
