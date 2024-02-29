namespace CleanArchitecture.Application.Exceptions
{
    public class NotFoundException: ApplicationException
    {
        //base es una palabra clave que se utiliza para llamar al constructor de la clase se esta derivando en este caso seria ApplicationException
        public NotFoundException(string name, object key): base($"Entity \"{name}\" ({key}) no fue encontrado")
        {
            
        }
    }
}
