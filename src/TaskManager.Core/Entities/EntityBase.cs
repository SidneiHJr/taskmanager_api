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

        public void NewInsertion()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }

        public void NewUpdate()
        {
            ModifiedDate = DateTime.UtcNow;
        }

    }
}
