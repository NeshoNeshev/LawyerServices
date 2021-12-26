namespace LawyerServices.Data.Models.SystemModels
{
    public interface IDateInfo
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
