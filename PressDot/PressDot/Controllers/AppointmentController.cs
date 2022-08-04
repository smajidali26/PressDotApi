﻿using Microsoft.AspNetCore.Mvc;
using PressDot.Contracts.Request.Appointment;
using PressDot.Facade.Infrastructure;

namespace PressDot.Controllers
{
    [Route("api/v1/Appointment")]
    public class AppointmentController : AuthenticatedController
    {
        #region private

        private readonly IAppointmentFacade _appointmentFacade;

        #endregion

        #region ctor

        public AppointmentController(IAppointmentFacade appointmentFacade)
        {
            _appointmentFacade = appointmentFacade;
        }

        #endregion

        #region actions


        [HttpGet]
        [Route("GetAppointmentbySaloonId/")]
        public ActionResult GetAppointmentbySaloonId(int saloonId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentbySaloonId(saloonId, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("GetAppointmentbyDoctorId/")]
        public ActionResult GetAppointmentbyDoctorId(int doctorId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentbyDoctorId(doctorId, pageIndex, pageSize);
            return Ok(appointments);
        }
        [HttpGet]
        [Route("GetAppointmentbyCustomerId/")]
        public ActionResult GetAppointmentbyCustomerId(int customerId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentbyCustomerId(customerId, pageIndex, pageSize);
            return Ok(appointments);
        }

        [HttpGet]
        [Route("GetAppointmentbySaloonAdministratorId/")]
        public ActionResult GetAppointmentbySaloonAdministratorId(int saloonAdministratorId,int stateId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentbySaloonAdministratorId(saloonAdministratorId,stateId, pageIndex, pageSize);
            return Ok(appointments);
        }

        [HttpPost]
        [Route("GetSaloonAppointmentsForSaloonAdministrator/")]
        public ActionResult GetSaloonAppointmentsForSaloonAdministrator([FromBody]AppointmentStateRequest states,int userId, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetSaloonAppointmentsForSaloonAdministrator(states,userId, pageIndex, pageSize);
            return Ok(appointments);
        }

        [HttpGet]
        [Route("GetAppointmentsbyAppointmentStates/")]
        public ActionResult GetAppointmentsbyAppointmentStates(AppointmentStateRequest states, int pageIndex = 0, int pageSize = 20)
        {
            var appointments = _appointmentFacade.GetAppointmentsbyAppointmentStates(states, pageIndex, pageSize);
            return Ok(appointments);
        }

        [HttpGet]
        [Route("GetCurrentUserAppointments/")]
        public ActionResult GetCurrentUserAppointments(int customerId, bool isFutureAppointments = true,
            int pageIndex = 0, int pageSize = 20)

        {
            var appointments =
                _appointmentFacade.GetCurrentUserAppointments(customerId, isFutureAppointments, pageIndex, pageSize);
            return Ok(appointments);
        }

        [HttpPut]
        [Route("UpdateAppointment/")]
        public ActionResult UpdateAppointment(AppointmentRequest request)
        {
            var result = _appointmentFacade.UpdateAppointment(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateAppointmentState/")]
        public ActionResult UpdateAppointmentState(int id, int stateId)
        {
            var result = _appointmentFacade.UpdateAppointmentState(id, stateId);
            return Ok(result);
        }
        [HttpPost]
        [Route("CreateAppointment/")]
        public ActionResult CreateAppointment(AppointmentRequest request)
        {
            var result = _appointmentFacade.CreateAppointment(request);
            return Ok(result);
        }
        #endregion
    }

}
