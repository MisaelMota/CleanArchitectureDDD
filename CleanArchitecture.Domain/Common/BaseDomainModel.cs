namespace CleanArchitecture.Domain.Common
{
    public abstract class BaseDomainModel //Es abstract para que no permita instancias, solo usar sus propiedades
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
