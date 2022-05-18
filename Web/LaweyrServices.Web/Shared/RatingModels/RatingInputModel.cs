using System.ComponentModel.DataAnnotations;

namespace LaweyrServices.Web.Shared.RatingModels
{
    public class RatingInputModel
    {

        public string? UserId { get; set; }

 
        public string? CompanyId { get; set; }

     
        public byte TrustworthyGreade { get; set; }
 
        public byte ServiceGreade { get; set; }

       
        public string? Commentary { get; set; }

        public string WteId { get; set; }
    }
}
