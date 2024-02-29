using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {

        private readonly IStreamerRepository  _streamerRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DeleteStreamerCommandHandler(IStreamerRepository streamerRepository, ILogger logger, IMapper mapper)
        {
            _streamerRepository = streamerRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var DeletedStreamer = await _streamerRepository.GetIdAsync(request.Id);

            if (DeletedStreamer == null)
            {
                _logger.LogError($"no se encontro el streamerId {request.Id}");
                throw new NotFoundException(nameof(Streamer),request.Id);
            }
            await _streamerRepository.DeleteAsync(DeletedStreamer);
            _logger.LogInformation($"el {request.Id} streamer fue eliminado con exito");
            return Unit.Value;

        }
    }
}
