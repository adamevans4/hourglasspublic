using Hourglass;
using Hourglass.Shared;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Hourglass.Pages
{
    public partial class Index
    {
        [Inject]
        private IUnitofWork? _unitofWork { get; set; }
        private IEnumerable<Template>? TemplateList { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; } // Inject NavigationManager

        private void OpenTemplateSession(int templateID)
        {
            Navigation.NavigateTo($"/TemplateSession/{templateID}");
        }

        protected override void OnInitialized()
        {
            // Gather all of the templates associated with the user
            // Can utilize a predicate function to gather the templates you want
            TemplateList = _unitofWork.Template.List();
        }
    }
}