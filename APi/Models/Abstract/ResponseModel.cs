namespace APi.Models.Abstract
{
    public class ResponseModel<TEntityId>
    {
        public required TEntityId Id { get; set; }
    }
}
