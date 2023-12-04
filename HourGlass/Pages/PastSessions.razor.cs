using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Hourglass;
using Hourglass.Shared;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace Hourglass.Pages
{
    public partial class PastSessions
    {
        
        [Inject]
        private IUnitofWork? _unitofWork { get; set; }
        
        [Inject]
        private NavigationManager Navigation { get; set; } // Inject NavigationManager
        private IEnumerable<Session>? SessionList { get; set; }

        protected override void OnInitialized()
        {
            // Gather all of the templates associated with the user
            // Can utilize a predicate function to gather the templates you want
            if(_unitofWork != null)
            {
                // Get the current date and time
                DateTime currentDate = DateTime.UtcNow;

                // Calculate the date one week ago from the current date
                DateTime oneWeekAgo = currentDate.AddDays(-7);

                SessionList = _unitofWork.Session.List(s => s.SessionStart >= oneWeekAgo && s.SessionStart <= currentDate, null,"Template");
            }
            
        }
        protected void UpdateSession(int templateId, int sessionId)
        {
            Navigation.NavigateTo($"/TemplateSession/{templateId}?SessionID={sessionId}");
        }
        private void HandleDeleteRequest(int SessionID)
        {
            var sessionToDelete = _unitofWork.Session.GetById(SessionID);
            _unitofWork.Session.Delete(sessionToDelete);
            _unitofWork.Commit();
            // Navigate to the current page to refresh the content
            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);

        }
    }
}