using Microsoft.AspNetCore.Components;
using Radzen;

namespace LawyerServices.Web.RadzenCustomValidators
{
    public class UniquePhoneValidator : Radzen.Blazor.ValidatorBase
    {
        [Parameter]
        public override string Text { get; set; }

        protected override bool Validate(IRadzenFormComponent component)
        {
            var phone = component.GetValue();

            return Phone == phone;
        }

        [Parameter]
        public string Phone { get; set; }
    }
}
