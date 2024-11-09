using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace App.Clinic.ViewModels
{
    public enum AppointmentSortChoiceEnum
    {
        StartTimeAscending,
        StartTimeDescending
    }

    public class AppointmentManagementViewModel : INotifyPropertyChanged
    {
        public AppointmentManagementViewModel()
        {
            SortChoices = new List<AppointmentSortChoiceEnum>
            {
                AppointmentSortChoiceEnum.StartTimeAscending,
                AppointmentSortChoiceEnum.StartTimeDescending
            };

            SortChoice = AppointmentSortChoiceEnum.StartTimeAscending;
            Query = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<AppointmentSortChoiceEnum> SortChoices { get; set; }

        private AppointmentSortChoiceEnum sortChoice;
        public AppointmentSortChoiceEnum SortChoice
        { 
            get
            {
                return sortChoice;
            }
            set
            {
                if (sortChoice != value)
                {
                    sortChoice = value;
                    NotifyPropertyChanged(nameof(Appointments));
                }
            }
        }

        private string query = string.Empty;
        public string Query 
        { 
            get => query; 
            set
            {
                if (query != value)
                {
                    query = value;
                    NotifyPropertyChanged(nameof(Appointments));
                }
            }
        }

        public AppointmentViewModel? SelectedAppointment { get; set; }

        public ObservableCollection<AppointmentViewModel> Appointments
        {
            get
            {
                var currentQuery = Query.ToUpper();

                var retVal = new ObservableCollection<AppointmentViewModel>(
                    AppointmentServiceProxy
                    .Current
                    .Appointments
                    .Where(a => a != null)
                    .Where(a => 
                        (a.Patient != null && a.Patient.Name.ToUpper().Contains(currentQuery)) ||
                        (a.Physician != null && a.Physician.Name.ToUpper().Contains(currentQuery)) ||
                        (a.StartTime.HasValue && a.StartTime.Value.ToString().ToUpper().Contains(currentQuery)) ||
                        (a.EndTime.HasValue && a.EndTime.Value.ToString().ToUpper().Contains(currentQuery))
                    )
                    .Select(a => new AppointmentViewModel(a))
                );

                switch (SortChoice)
                {
                    case AppointmentSortChoiceEnum.StartTimeAscending:
                        return new ObservableCollection<AppointmentViewModel>(retVal.OrderBy(a => a.StartTime));
                    case AppointmentSortChoiceEnum.StartTimeDescending:
                        return new ObservableCollection<AppointmentViewModel>(retVal.OrderByDescending(a => a.StartTime));
                    default:
                        return retVal;
                }
            }
        }

        public void Delete()
        {
            if (SelectedAppointment == null)
            {
                return;
            }
            AppointmentServiceProxy.Current.DeleteAppointment(SelectedAppointment.Id);
            Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Appointments));
        }

        public async void Search()
        {
            if (!string.IsNullOrEmpty(Query))
            {
                await AppointmentServiceProxy.Current.Search(Query);
            }
            Refresh();
        }
    }
}