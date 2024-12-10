using Library.Clinic.DTO;
using Library.Clinic.Services;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MongoDB.Bson;

namespace App.Clinic.ViewModels
{
    public class PhysicianViewModel : INotifyPropertyChanged
    {
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        public PhysicianDTO Model { get; private set; }

        public PhysicianViewModel()
        {
            Model = new PhysicianDTO();
            SetupCommands();
        }

        public PhysicianViewModel(PhysicianDTO? model)
        {
            if (model == null)
            {
                Model = new PhysicianDTO();
            }
            else
            {
                var serialized = JsonConvert.SerializeObject(model);
                Model = JsonConvert.DeserializeObject<PhysicianDTO>(serialized);
            }
            SetupCommands();
        }

        public string Name
        {
            get => Model.Name ?? string.Empty;
            set
            {
                if (Model.Name != value)
                {
                    Model.Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Id
        {
            get => Model.Id ?? string.Empty;
            set
            {
                if (Model.Id != value)
                {
                    Model.Id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string License
        {
            get => Model.License ?? string.Empty;
            set
            {
                if (Model.License != value)
                {
                    Model.License = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime GraduationDate
        {
            get => Model.GraduationDate ?? DateTime.MinValue;
            set
            {
                if (Model.GraduationDate != value)
                {
                    Model.GraduationDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public async void ExecuteAdd()
        {
            await PhysicianServiceProxy.Current.AddOrUpdatePhysician(Model);
        }

        public async void Cancel()
        {
            await Shell.Current.GoToAsync("//Physicians");
        }

        private void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PhysicianViewModel));
        }

        private void DoDelete()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                PhysicianServiceProxy.Current.DeletePhysician(ObjectId.Parse(Id));
                Shell.Current.GoToAsync("//Physicians");
            }
        }

        private void DoEdit(PhysicianViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPhysicianId = pvm?.Id ?? string.Empty;
            Shell.Current.GoToAsync($"//PhysicianDetails?physicianId={selectedPhysicianId}");
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void NavigatedTo()
        {
            await PhysicianServiceProxy.Current.AddOrUpdatePhysician(Model);
            NotifyPropertyChanged(nameof(Model));
        }

        public async void Refresh()
        {
            NotifyPropertyChanged(nameof(Model));
        }
    }
}
