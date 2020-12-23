namespace OnlineBanking.Domain
{
    public abstract class EntityBase
    {
        public bool IsNew { get; private set; }
        public bool HasChanges { get; set; }
        public bool IsValid => Validate();
        public EntityState EntityState { get; set; }

        public abstract bool Validate();
       
    }

    public enum EntityState
    {
        Active,
        Deleted
    }
}