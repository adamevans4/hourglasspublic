using global::Microsoft.AspNetCore.Components;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace HourglassApp.Pages
{
    public partial class UpsertTemplate
    {
        [Inject]
        private IUnitofWork? _unitofWork { get; set; }
        protected string TemplateName { get; set; } = string.Empty;
        protected string SelectedIcon { get; set; } = string.Empty;
        protected string SelectedColor { get; set; } = string.Empty;
        protected string SelectedGroup { get; set; } = string.Empty;
        [Inject]
        private NavigationManager Navigation { get; set; } // Inject NavigationManager
                                                           // List of available icons and groups
        private List<string> AvailableIcons { get; set; } = new List<string>
        {
            "fa-globe", // Global
            "fa-cog", // Gear
            "fa-chart-line", // Chart Line
            "fa-utensils", // Utensils
            "fa-glass-martini", // Martini Glass
            "fa-dumbbell", // Dumbbell
            "fa-heartbeat", // Heartbeat
            "fa-book", // Book
            "fa-briefcase", // Briefcase
            "fa-graduation-cap", // Graduation Cap
            "fa-gavel", // Gavel
            "fa-laptop", // Laptop
            "fa-bicycle", // Bicycle
            "fa-balance-scale", // Balance Scale
            "fa-shopping-cart", // Shopping Cart
            "fa-plane", // Plane
            "fa-bed", // Bed
            "fa-running", // Running
            "fa-music", // Music
            "fa-paint-brush", // Paint Brush
            "fa-camera", // Camera
            "fa-film", // Film
            "fa-lightbulb", // Lightbulb
            "fa-train", // Train
            "fa-bus" // Bus
        };


        private List<string> AvailableGroups { get; set; } = new List<string>();
        protected override void OnInitialized()
        {
            //Populate available groups and icons

            AvailableGroups = _unitofWork.TemplateGroup
                .List()
                .Select(group => group.Name)
                .ToList();
        }
        private void HandleSubmit()
        {

            if (_unitofWork != null)
            {

                var SelectedTemplateGroup = _unitofWork.TemplateGroup.Get(g => g.Name == SelectedGroup);
                Template UserTemplate = new()
                {
                    Name = TemplateName,
                    TemplateImage = SelectedIcon,
                    TemplateColor = SelectedColor,
                    TemplateGroup = SelectedTemplateGroup
                };
                _unitofWork.Template.Add(UserTemplate);
                _unitofWork.Commit();

                Navigation.NavigateTo("");

            }


        }
        private void SelectIcon(string icon)
        {
            SelectedIcon = icon; // Update the SelectedIcon property on selection
        }
        private void HandleBackRequest()
        {
            //Go back the previous page
            Navigation.NavigateTo("");
        }
    }
}