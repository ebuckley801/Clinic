using Library.Clinic.DTO;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.Clinic.ViewModels
{
    public class PatientViewModel : INotifyPropertyChanged
    {
        public PatientDTO? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
        public int Id
        {
            get
            {
                if(Model == null)
                {
                    return -1;
                }

                return Model.Id;
            }

            set
            {
                if(Model != null && Model.Id != value) {
                    Model.Id = value;
                }
            }
        }

        public string Name
        {
            get => Model?.Name ?? string.Empty;
            set
            {
                if(Model != null)
                {
                    Model.Name = value;
                }
            }
        }

        public string Address
        {
            get => Model?.Address ?? string.Empty;
            set
            {
                if(Model != null)
                {
                    Model.Address = value;
                }
            }
        }

        public DateTime? Birthday
        {
            get => Model?.Birthday;
            set
            {
                if(Model != null)
                {
                    Model.Birthday = value;
                }
            }
        }

        public string Gender
        {
            get => Model?.Gender ?? string.Empty;
            set
            {
                if(Model != null)
                {
                    Model.Gender = value;
                }
            }
        }

        public string SSN
        {
            get => Model?.SSN ?? string.Empty;
            set
            {
                if(Model != null)
                {
                    Model.SSN = value;
                }
            }
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PatientViewModel));
        }

        private void DoDelete()
        {
            if (Id > 0)
            {
                PatientServiceProxy.Current.DeletePatient(Id);
                Shell.Current.GoToAsync("//Patients");
            }
        }

        private void DoEdit(PatientViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPatientId = pvm?.Id ?? 0;
            Shell.Current.GoToAsync($"//PatientDetails?patientId={selectedPatientId}");
        }

        public PatientViewModel()
        {

            Model = new PatientDTO();
            SetupCommands();
        }

        public PatientViewModel(PatientDTO? _model)
        {
            Model = _model;
            SetupCommands();
        }

        public async void ExecuteAdd()
        {
            if (Model != null)
            {
                await PatientServiceProxy
                .Current
                .AddOrUpdatePatient(Model);
            }

            await Shell.Current.GoToAsync("//Patients");
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}