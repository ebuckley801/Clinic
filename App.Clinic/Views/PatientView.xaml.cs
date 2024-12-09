using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace App.Clinic.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
        BindingContext = new PatientViewModel();
	}
    public string PatientId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Patients");
    }

    private async void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as PatientViewModel)?.ExecuteAdd();
        await Shell.Current.GoToAsync("//Patients");
    }

    private void PatientView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (string.IsNullOrEmpty(PatientId))
        {
            BindingContext = new PatientViewModel();
            return;
        }

        if (PatientId == "0")
        {
            // New patient
            BindingContext = new PatientViewModel();
        }
        else
        {
            // Existing patient
            var model = PatientServiceProxy.Current
                .Patients.FirstOrDefault(p => p.Id == PatientId);
            BindingContext = model != null 
                ? new PatientViewModel(model) 
                : new PatientViewModel();
        }
    }
}
