using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Linq;

namespace App.Clinic.Views;

[QueryProperty(nameof(AppointmentId), "appointmentId")]
public partial class AppointmentView : ContentPage
{
	public AppointmentView()
	{
		InitializeComponent();
		BindingContext = new AppointmentViewModel();
	}

	public int AppointmentId { get; set; }

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Appointments");
	}

	private void AddClicked(object sender, EventArgs e)
	{
		(BindingContext as AppointmentViewModel)?.ExecuteAdd();
	}

	private void AppointmentView_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		if (AppointmentId > 0)
		{
			var model = AppointmentServiceProxy.Current.Appointments.FirstOrDefault(a => a.Id == AppointmentId);
			if (model != null)
			{
				BindingContext = new AppointmentViewModel(model);
			}
			else
			{
				BindingContext = new AppointmentViewModel();
			}
		}
		else
		{
			BindingContext = new AppointmentViewModel();
		}
	}
}