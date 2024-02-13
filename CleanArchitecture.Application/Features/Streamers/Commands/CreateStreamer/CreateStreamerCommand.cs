using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommand : IRequest<int> // devuelve un entero del id de la insercion
    {
        public string Nombre { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;


    }
}
