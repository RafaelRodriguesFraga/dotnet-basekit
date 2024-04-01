namespace DotnetBaseKit.Components.Domain.MongoDb.Entities.Base
{
    public interface IBaseEntity
    {
         public Guid Id {get;}
         public DateTime CreatedAt {get;}
        void Validate();
    }
}