namespace FinancialControl.Core.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {

        }

        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public void NewInsertion(string user)
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
            CreatedBy = user;
        }

        public void NewUpdate(string user)
        {
            ModifiedDate = DateTime.UtcNow;
            ModifiedBy = user;
        }

    }
}
