﻿using System.Linq.Expressions;

namespace HospitalManagementSystem
{
	public class AppointmentRepository : IRepository<Appointment>
	{
		protected readonly HospitalManagementSystemContext dbContext;

		public AppointmentRepository(HospitalManagementSystemContext context)
		{
			dbContext = context;
		}

		/// <summary>
		/// Adds an Appointment to the database
		/// </summary>
		/// <param name="entity"></param>
		public void Add(Appointment entity)
		{
			dbContext.Set<Appointment>().Add(entity);
		}

		/// <summary>
		/// Adds multiple instances of Appointments to the database
		/// </summary>
		/// <param name="entities"></param>
		public void AddRange(IEnumerable<Appointment> entities)
		{
			dbContext.Set<Appointment>().AddRange(entities);
		}

		/// <summary>
		/// Returns all instances of Appointments that satisfy the predicate
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public IEnumerable<Appointment> Find(Expression<Func<Appointment, bool>> predicate)
		{
			return dbContext.Set<Appointment>().Where(predicate);
		}

		/// <summary>
		/// Returns all instances of Appointments in the database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Appointment> GetAll()
		{
			return dbContext.Set<Appointment>();
		}

		/// <summary>
		/// Returns the first Appointment that matches the provided ID, or null if none do
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Appointment? GetById(int id)
		{
			return dbContext.Set<Appointment>().Find(id);
		}

		/// <summary>
		/// Saves any changes that have been made to the database including adding, removing and updating entries.
		/// </summary>
		public void SaveChanges()
		{
			dbContext.SaveChanges();
		}
	}
}