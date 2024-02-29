using AutoMapper;
using CleanArchitecture.Application.Contracts.Infraestructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    internal class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateStreamerCommandHandler> _logger;

        public CreateStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;   
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            /*El cliente enviara un objeto tipo Streamer y yo estoy esperando un objeto tipo request (StreamerCommand)
            por lo que necesito mapear ambas clases para que no haya un choque de tipos*/
            var streamerEntity = _mapper.Map<Streamer>(request);

            //este es el objeto insertado en la base de datos, 
            var newStreamer = await _streamerRepository.AddAsync(streamerEntity);

            //logger para mandar un mensaje por bitacora de que fue insertado el record
            _logger.LogInformation($"Streamer {newStreamer.Id} fue insertado exitosamente");

            // se envia el email
            await SendEmail(newStreamer);

            // se retorna el id
            return newStreamer.Id;

        }
        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "vaxi.drez.social@gmail.com",
                Body = "La compania de streamer se creo correctamente",
                Subject = "Mensaje de alerta"
            };

            try
            {
                await _emailService.SendEmailAsync(email);
            }

            catch (Exception ex)
            {

                _logger.LogError($"Errores enviando el email de {streamer.Id}");
            }


        }
    }
}
