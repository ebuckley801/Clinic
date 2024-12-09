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
using MongoDB.Bson;
using Newtonsoft.Json;

namespace App.Clinic.ViewModels
{
    public class PatientViewModel : INotifyPropertyChanged
    {
        public PatientDTO? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        public string Id
        {
            get
            {
                if(Model == null)
                {
                    return string.Empty;
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
            if (!string.IsNullOrEmpty(Id))
            {
                PatientServiceProxy.Current.DeletePatient(ObjectId.Parse(Id));
                Shell.Current.GoToAsync("//Patients");
            }
        }

        private void DoEdit(PatientViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPatientId = pvm?.Id ?? string.Empty;
            Shell.Current.GoToAsync($"//PatientDetails?patientId={selectedPatientId}");
        }

        public PatientViewModel()
        {
            Model = new PatientDTO();
            SetupCommands();
        }

        public PatientViewModel(PatientDTO? _model)
        {
            if(_model == null)
            {
                Model = new PatientDTO();
            }
            else
            {
                var serialized = JsonConvert.SerializeObject(_model);
                Model = JsonConvert.DeserializeObject<PatientDTO>(serialized);
            }
            SetupCommands();
        }

        public async void ExecuteAdd()
        {
            await PatientServiceProxy.Current.AddOrUpdatePatient(Model);
        }

        public async void Cancel()
        {
            await Shell.Current.GoToAsync("//Patients");
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void NavigatedTo()
        {
            
            await PatientServiceProxy.Current.AddOrUpdatePatient(Model);
            NotifyPropertyChanged(nameof(Patient));
        }

        public async void Refresh()
        {
            NotifyPropertyChanged(nameof(Patient));
        }
    }

}