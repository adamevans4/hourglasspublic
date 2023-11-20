using global::Microsoft.AspNetCore.Components;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;

namespace HourglassNative.Pages
{
    public partial class TemplateSession
    {
        [Parameter]
        public int templateID { get; set; }
        public Template? SessionTemplate { get; set; }
        [Inject]
        private IUnitofWork? _unitofWork { get; set; }
        [Inject]
        private NavigationManager Navigation { get; set; } // Inject NavigationManager
        private DateTime StartTime { get; set; } = DateTime.Now;
        private DateTime EndTime { get; set; } = DateTime.Now;
        protected override void OnInitialized()
        {
            if (_unitofWork != null)
            {
                // Get the template
                SessionTemplate = _unitofWork.Template.GetById(templateID);
            }
        }
        private void HandleSubmit()
        {
            if (SessionTemplate != null)
            {
                
                    Session UserSession = new ()
                    {
                        Template = SessionTemplate,
                        SessionStart = StartTime,
                        SessionEnd = EndTime,
                        Duration = EndTime - StartTime
                    };
                    _unitofWork.Session.Add(UserSession);
                    _unitofWork.Commit();

                    Navigation.NavigateTo("");
               
            }


        }

    }
}