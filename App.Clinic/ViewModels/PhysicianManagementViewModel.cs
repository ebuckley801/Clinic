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
    public class PhysicianManagementViewModel : INotifyPropertyChanged
    {
        public PhysicianManagementViewModel()
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
                    NotifyPropertyChanged(nameof(Physicians));
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
                    NotifyPropertyChanged(nameof(Physicians));
                }
            }
        }

        public PhysicianViewModel? SelectedPhysician { get; set; }
        public ObservableCollection<PhysicianViewModel> Physicians
        {
            get
            {
                var currentQuery = Query.ToUpper();

                var retVal = new ObservableCollection<PhysicianViewModel>(
                    PhysicianServiceProxy
                    .Current
                    .Physicians
                    .Where(p => p != null)
                    .Where(p => p.Name.ToUpper().Contains(currentQuery))
                    .Select(p => new PhysicianViewModel(p))
                );

                if (SortChoice == SortChoiceEnum.NameAscending)
                {
                    return new ObservableCollection<PhysicianViewModel>(retVal.OrderBy(p => p.Name));
                }
                else
                {
                    return new ObservableCollection<PhysicianViewModel>(retVal.OrderByDescending(p => p.Name));
                }
            }
        }

        public void Delete()
        {
            if (SelectedPhysician == null)
            {
                return;
            }
            PhysicianServiceProxy.Current.DeletePhysician(SelectedPhysician.Id);
            Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Physicians));
        }

        public async void Search()
        {
            if (!string.IsNullOrEmpty(Query))
            {
                await PhysicianServiceProxy.Current.Search(Query);
            }
            Refresh();
        }
    }
}

