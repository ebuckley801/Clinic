using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MongoDB.Bson;
using AuthenticationServices;
namespace App.Clinic.ViewModels
{
    public class PatientManagementViewModel : INotifyPropertyChanged
    {
        public PatientManagementViewModel()
        {
            SortChoices = new List<SortChoiceEnum>
            {
               SortChoiceEnum.NameAscending,
               SortChoiceEnum.NameDescending
            };

            SortChoice = SortChoiceEnum.NameAscending;
            Query = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<SortChoiceEnum> SortChoices { get; set; }

        private SortChoiceEnum sortChoice;
        public SortChoiceEnum SortChoice
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
                    NotifyPropertyChanged(nameof(Patients));
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
                    NotifyPropertyChanged(nameof(Patients));
                }
            }
        }

        public PatientViewModel? SelectedPatient { get; set; }
        public ObservableCollection<PatientViewModel> Patients
        {
            get
            {
                var currentQuery = Query.ToUpper();

                var retVal = new ObservableCollection<PatientViewModel>(
                    PatientServiceProxy
                    .Current
                    .Patients
                    .Where(p => p != null)
                    .Where(p => p.Name.ToUpper().Contains(currentQuery))
                    .Select(p => new PatientViewModel(p))
                );

                if (SortChoice == SortChoiceEnum.NameAscending)
                {
                    return new ObservableCollection<PatientViewModel>(retVal.OrderBy(p => p.Name));
                }
                else
                {
                    return new ObservableCollection<PatientViewModel>(retVal.OrderByDescending(p => p.Name));
                }
            }
        }

        public async void Delete()
        {
            if (SelectedPatient == null)
            {
                return;
            }
            PatientServiceProxy.Current.DeletePatient(ObjectId.Parse(SelectedPatient.Id));
        }

        public async Task Refresh()
        {
            await Task.Delay(100);
            NotifyPropertyChanged(nameof(Patients));
        }

        public async void Search()
        {
            if (!string.IsNullOrEmpty(Query))
            {
                await PatientServiceProxy.Current.Search(Query);
            }
        }
    }
}