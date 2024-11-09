using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public enum SortChoiceEnum
    {
        None,
        NameAscending,
        NameDescending
    }

    public enum GenderEnum
    {
        Male,
        Female
    }

    public enum DiagnosisEnum
    {
        None,
        Cold,
        Flu,
        Headache,
        StomachAche,
        Diabetes, 
        HighBloodPressure,
        HeartAttack,
        KidneyFailure
    }
}

public enum PrescriptionEnum
{
    None,
    Aspirin,
    Ibuprofen,
    Rub_Some_Dirt_On_It,
    Avoid_Stress,
    Tums,
    Penicillin
}