using Library.Clinic.DTO;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MongoDB.Bson;
namespace App.Clinic.ViewModels
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        public AppointmentDTO? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        public int Id
        {
            get => Model?.Id ?? -1;
            set
            {
                if (Model != null && Model.Id != value)
                {
                    Model.Id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? StartTime
        {
            get => Model?.StartTime;
            set
            {
                if (Model != null && Model.StartTime != value)
                {
                    Model.StartTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? EndTime
        {
            get => Model?.EndTime;
            set
            {
                if (Model != null && Model.EndTime != value)
                {
                    Model.EndTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<PatientDTO> Patients { 
            get
            {
                return new ObservableCollection<PatientDTO>(PatientServiceProxy.Current.Patients);
            }
        }
        public ObservableCollection<PhysicianDTO> Physicians {
            get
            {
                return new ObservableCollection<PhysicianDTO>(PhysicianServiceProxy.Current.Physicians);
            }
        }

        public PatientDTO? SelectedPatient { 
            get{
                return Model?.Patient;
            } set{
                if(Model != null)
                {
                    Model.Patient = value;
                    Model.PatientId = value?.Id ?? String.Empty;
                    NotifyPropertyChanged();
                }
            }
        }
        public PhysicianDTO? SelectedPhysician { get{
            return Model?.Physician;
        } set{
            if(Model != null)
            {
                Model.Physician = value;
                Model.PhysicianId = value?.Id ?? String.Empty;
                NotifyPropertyChanged();
                }
            }
        }

        public string PatientName{
            get{
                if(Model != null && !String.IsNullOrEmpty(Model.PatientId))
                {
                    if(Model.Patient != null)
                    {
                        Model.Patient = PatientServiceProxy.Current.Patients.FirstOrDefault(p => p.Id == Model.PatientId);
                        NotifyPropertyChanged();
                    }
                }
                return Model?.Patient?.Name ?? "";
            }
        }

        public string PhysicianName{
            get{
                if(Model != null && !String.IsNullOrEmpty(Model.PhysicianId))
                {
                    if(Model.Physician != null)
                    {
                        Model.Physician = PhysicianServiceProxy.Current.Physicians.FirstOrDefault(p => p.Id == Model.PhysicianId);
                        NotifyPropertyChanged();
                    }
                }
                return Model?.Physician?.Name ?? "";
            }
        }

        public AppointmentViewModel()
        {
            Model = new AppointmentDTO();
            SetupCommands();
        }

        public AppointmentViewModel(AppointmentDTO? _model)
        {
            Model = _model;
            SetupCommands();
        }

        private void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as AppointmentViewModel));
        }

        private void DoDelete()
        {
            if (Id > 0)
            {
                AppointmentServiceProxy.Current.DeleteAppointment(Id);
                Shell.Current.GoToAsync("//Appointments");
            }
        }

        private void DoEdit(AppointmentViewModel? avm)
        {
            if (avm == null)
            {
                return;
            }
            var selectedAppointmentId = avm?.Id ?? 0;
            Shell.Current.GoToAsync($"//AppointmentDetails?appointmentId={selectedAppointmentId}");
        }

        public async void ExecuteAdd()
        {
            if (Model != null)
            {
                await AppointmentServiceProxy
                    .Current
                    .AddOrUpdateAppointment(Model);
            }

            await Shell.Current.GoToAsync("//Appointments");
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}