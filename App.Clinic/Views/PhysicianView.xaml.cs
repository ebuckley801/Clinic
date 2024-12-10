using App.Clinic.ViewModels;
using Library.Clinic.Services;
using System;
using System.Linq;

namespace App.Clinic.Views
{
    [QueryProperty(nameof(PhysicianId), "physicianId")]
    public partial class PhysicianView : ContentPage
    {
        public PhysicianView()
        {
            InitializeComponent();
            BindingContext = new PhysicianViewModel();
        }

        public string PhysicianId { get; set; } = "0";

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Physicians");
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            (BindingContext as PhysicianViewModel)?.ExecuteAdd();
            await Shell.Current.GoToAsync("//Physicians");
        }

        private void PhysicianView_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            if (PhysicianId != "0")
            {
                var model = PhysicianServiceProxy.Current
                    .Physicians.FirstOrDefault(p => p.Id == PhysicianId);
                BindingContext = model != null 
                    ? new PhysicianViewModel(model) 
                    : new PhysicianViewModel(null);
            }
            else
            {
                BindingContext = new PhysicianViewModel(null);
            }
        }
    }
}
