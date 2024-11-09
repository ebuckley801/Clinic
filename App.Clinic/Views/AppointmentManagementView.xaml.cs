using App.Clinic.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.Views;

public partial class AppointmentManagementView : ContentPage, INotifyPropertyChanged
{
	public AppointmentManagementView()
	{
		InitializeComponent();
        BindingContext = new AppointmentManagementViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AppointmentDetails?appointmentId=0");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        var selectedAppointmentId = (BindingContext as AppointmentManagementViewModel)?
            .SelectedAppointment?.Id ?? 0;
        Shell.Current.GoToAsync($"//AppointmentDetails?appointmentId={selectedAppointmentId}");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentManagementViewModel)?.Delete();
    }

    private void AppointmentManagementView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as AppointmentManagementViewModel)?.Refresh();
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentManagementViewModel)?.Refresh();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentManagementViewModel)?.Search();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}