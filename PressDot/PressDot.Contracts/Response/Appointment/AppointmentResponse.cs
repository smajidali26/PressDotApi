using PressDot.Contracts.Response.Saloon;
using PressDot.Contracts.Response.State;
using PressDot.Contracts.Response.Users;
using System;

namespace PressDot.Contracts.Response.Appointment
{
    public class AppointmentResponse : BasePressDotEntityModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public UsersResponse Customer { get; set; }
        public int? DoctorId { get; set; }
        public UsersResponse Doctor { get; set; }
        public int? SaloonId { get; set; }
        public SaloonResponse Saloon { get; set; }
        public string Symptoms { get; set; }
        public int? StateId { get; set; }
        public StateResponse State { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }
    }
}
