using PressDot.Core;
using PressDot.Core.Data;
using PressDot.Core.Domain;
using PressDot.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PressDot.Service.Implementation
{
    public class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        #region private members

        private readonly IStatesService _stateService;
        #endregion

        #region ctor

        public AppointmentService(IRepository<Appointment> repository, IStatesService stateService) : base(repository)
        {
            _stateService = stateService;
        }

        #endregion

        #region methods
        public IPagedList<Appointment> GetAppointmentsbySaloonId(int saloonId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.SaloonId == saloonId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public IPagedList<Appointment> GetAppointmentsbySaloonIdAndState(int saloonId,int stateId, int pageIndex,
            int pageSize)
        {
            var query = Repository.Table.Where(x => x.SaloonId == saloonId && x.StateId == stateId && x.Date >= DateTime.UtcNow.Date);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public List<Appointment> GetAppointmentsBySaloonId(int saloonId)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.SaloonId == saloonId);
            return query.ToList();
        }
        public IPagedList<Appointment> GetAppointmentsbySaloonId(int saloonId, DateTime date, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.SaloonId == saloonId && ap.Date.Date.Equals(date.Date));
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public IPagedList<Appointment> GetAppointmentsbyDoctorId(int doctorId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.DoctorId == doctorId && !ap.IsDeleted);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }
        public List<Appointment> GetAppointmentsbyDoctorId(int doctorId)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.DoctorId == doctorId && !ap.IsDeleted);
            return query.ToList();
        }
        public IPagedList<Appointment> GetAppointmentsbyCustomerId(int customerId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => ap.CustomerId == customerId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }
        public IPagedList<Appointment> GetAppointmentsbyAppointmentState(List<int> stateIds, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => stateIds.Contains(ap.StateId));
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public IPagedList<Appointment> GetSaloonAppointmentsForSaloonAdministrator(List<int> stateIds,int saloonId, int pageIndex, int pageSize)
        {
            var query = Repository.Table;
            query = query.Where(ap => stateIds.Contains(ap.StateId) && ap.SaloonId == saloonId);
            return new PagedList<Appointment>(query, pageIndex, pageSize);
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            var dbAppointmentObj = Get(appointment.Id);
            if (dbAppointmentObj != null)
            {
                dbAppointmentObj.CustomerId = appointment.CustomerId;
                dbAppointmentObj.DoctorId = appointment.DoctorId;
                dbAppointmentObj.SaloonId = appointment.SaloonId;
                dbAppointmentObj.StateId = appointment.StateId;
                dbAppointmentObj.Symptoms = appointment.Symptoms;
                dbAppointmentObj.UpdatedBy = appointment.UpdatedBy;
                dbAppointmentObj.UpdatedDate = DateTime.UtcNow;
                dbAppointmentObj.StartTime = appointment.StartTime;
                dbAppointmentObj.EndTime = appointment.EndTime;
                return Update(dbAppointmentObj);
            }
            return false;
        }
        public Appointment CreateAppointment(Appointment appointment)
        {
            if (appointment != null)
            {
                appointment.CreatedDate = DateTime.UtcNow;
                appointment.UpdatedDate = DateTime.UtcNow;
                appointment.StateId = _stateService.GetStatesByStateNameAndType("Pending", "Appointment").Id;
                Create(appointment);
                return appointment;
            }
            return null;
        }
        public bool UpdateAppointmentState(int Id, int stateId)
        {
            var dbAppointmentObj = Get(Id);
            if (dbAppointmentObj != null)
            {
                dbAppointmentObj.StateId = stateId;
                dbAppointmentObj.UpdatedDate = DateTime.UtcNow;
                return Update(dbAppointmentObj);
            }
            return false;
        }
        public ICollection<Appointment> GetAppointmentsBySaloonAndSates(int saloonId, int[] statesIds)
        {
            return Repository.Table.Where(ap => ap.SaloonId == saloonId && statesIds.Contains(ap.StateId)).ToList();
        }

        public ICollection<Appointment> GetAppointmentsByUserAndSates(int userId, int[] statesIds)
        {
            return Repository.Table.Where(ap => ap.CustomerId == userId && statesIds.Contains(ap.StateId)).ToList();
        }
        public IPagedList<Appointment> GetCurrentUserAppointments(int customerId, bool isFutureAppointments, int pageIndex, int pageSize)
        {
            if (customerId == 0)
                return null;
            IQueryable<Appointment> appointments;
            if (isFutureAppointments)
            {

                appointments = Repository.Table.Where(ap => ap.CustomerId == customerId && ap.Date >= DateTime.UtcNow.Date && !ap.IsDeleted);
                return new PagedList<Appointment>(appointments, pageIndex, pageSize);
            }
            appointments = Repository.Table.Where(ap => ap.CustomerId == customerId && ap.Date <= DateTime.UtcNow.Date && !ap.IsDeleted);
            return new PagedList<Appointment>(appointments, pageIndex, pageSize);
        }

        public bool IsSlotOccupied(int saloonId, int docId, DateTime date, string startTime, string endTime)
        {
            var sTime = TimeSpan.Parse(startTime);
            var eTime = TimeSpan.Parse(endTime);
            var pendingStatusId = _stateService.GetStatesByStateNameAndType("Pending", "Appointment").Id;

            var dbObj = Repository.Table.FirstOrDefault(x => x.StartTime == sTime && x.EndTime == eTime
                                                                                  && x.SaloonId == saloonId &&
                                                                                  x.DoctorId == docId
                                                                                  && x.StateId == pendingStatusId
                                                                                  && x.Date.Day == date.Day);
            return dbObj != null;

        }

        public ICollection<Appointment> GetSaloonsAppointmentsByDate(int[] saloonIds, DateTime dateTime)
        {
            return Repository.Table.Where(x => saloonIds.Contains(x.SaloonId) && x.Date == dateTime).ToList();
        }

        public ICollection<Appointment> GetSaloonsAppointmentsAfterDate(int[] saloonIds, DateTime dateTime)
        {
            return Repository.Table.Where(x => saloonIds.Contains(x.SaloonId) && x.Date >= dateTime).ToList();
        }
        #endregion
    }
}
