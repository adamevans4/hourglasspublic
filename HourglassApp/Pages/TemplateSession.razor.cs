using Microsoft.Maui.Controls;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;
using System;
using Microsoft.AspNetCore.Components;

namespace HourglassApp.Pages
{
    public partial class TemplateSession
    {
        [Parameter]
        public int TemplateID { get; set; }
        [Parameter]
        public int? SessionID { get; set; }
        public Template? SessionTemplate { get; set; }
        [Inject]
        private IUnitofWork? _unitofWork { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; } // Inject NavigationManager
        private DateTime StartTime { get; set; } = DateTime.Now;
        private DateTime EndTime { get; set; } = DateTime.Now;
        protected override void OnInitialized()
        {
            if (_unitofWork != null)
            {
                // Get the template
                SessionTemplate = _unitofWork.Template.GetById(TemplateID);

                if (SessionID != null && SessionID >0)
                {
                    var CurrentSession = _unitofWork.Session.GetById(SessionID.Value);
                    StartTime = CurrentSession.SessionStart;
                    EndTime = CurrentSession.SessionEnd;
                }
                
                  
            }

        }
        private DateTime GetRoundedToMinute(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }
        private bool IsValidTimeRange()
        {
            return StartTime < EndTime && StartTime != EndTime; // Allows submission only if end time is after start time
        }
        private void HandleBackRequest()
        {
            //Go back the previous page
            if (SessionID == null || SessionID <= 0)
            {
                NavigationManager.NavigateTo("");
            }
            else
            {
                NavigationManager.NavigateTo("/PastSessions/");
            }

        }
        private void HandleSubmit()
        {
            if (SessionID != null)
            {
                var CurrentSession = _unitofWork.Session.GetById(SessionID.Value);
                CurrentSession.SessionStart = GetRoundedToMinute(StartTime);
                CurrentSession.SessionEnd = GetRoundedToMinute(EndTime);
                CurrentSession.Duration = EndTime.AddMinutes(1) - StartTime;
                _unitofWork.Session.Update(CurrentSession);
                _unitofWork.Commit();
                NavigationManager.NavigateTo("/PastSessions/");
            }
            else if (SessionTemplate != null)
            {

                Session UserSession = new()
                {
                    Template = SessionTemplate,
                    SessionStart = GetRoundedToMinute(StartTime),
                    SessionEnd = GetRoundedToMinute(EndTime).AddMinutes(1),
                    Duration = EndTime - StartTime
                };
                _unitofWork.Session.Add(UserSession);
                _unitofWork.Commit();

                NavigationManager.NavigateTo("");

            }


        }

    }
}

