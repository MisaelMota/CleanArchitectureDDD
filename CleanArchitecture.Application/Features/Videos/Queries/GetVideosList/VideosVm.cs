namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class VideosVm
    {
        //se necesita un viewmodel para traer de los datos generales los datos especificos que necesitamos
        public string? Nombre { get; set; }

        public int StreamerId { get; set; }
    }
}
