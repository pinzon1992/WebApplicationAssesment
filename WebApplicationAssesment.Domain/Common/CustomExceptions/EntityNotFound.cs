namespace WebApplicationAssesment.Domain.Common.CustomExceptions
{
    public class EntityNotFound : Exception
    {
        public EntityNotFound(string message) : base(message) 
        {
            
        }
    }
}
