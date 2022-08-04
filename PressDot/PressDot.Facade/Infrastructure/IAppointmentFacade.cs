using PressDot.Contracts;
using PressDot.Contracts.Response.Appointment;
using PressDot.Contracts.Request.Appointment;
using System;

namespace PressDot.Facade.Infrastructure
{
    public interface IAppointmentFacade
    {
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbySaloonId(int Id, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbySaloonId(int Id, DateTime date, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbyDoctorId(int Id, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbyCustomerId(int Id, int pageIndex, int pageSize);

        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentbySaloonAdministratorId(int saloonAdministatorId,int stateId, int pageIndex, int pageSize);
        PressDotPageListEntityModel<AppointmentResponse> GetAppointmentsbyAppointmentStates(AppointmentStateRequest request, int pageIndex, int pageSize);

        PressDotPageListEntityModel<AppointmentResponse> GetSaloonAppointmentsForSaloonAdministrator(
            AppointmentStateRequest request, int userId, int pageIndex, int pageSize);

        PressDotPageListEntityModel<CurrentUserAppointmentResponse> GetCurrentUserAppointments(int customerId,
            bool isFutureAppointments, int pageIndex, int pageSize);
        AppointmentResponse GetAppointmentById(int id);
        bool UpdateAppointment(AppointmentRequest request);
        bool UpdateAppointmentState(int Id, int stateId);
        bool CreateAppointment(AppointmentRequest request);
    }
}
