using BookARoom.Model.BLL;
using BookARoom.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookARoom.Model
{
    public class Service
    {
        //Fält
        private BookingDAL _bookingDAL;
        private CustomerDAL _customerDAL;
        private CustomerTypeDAL _customerTypeDAL;

        //Egenskaper
        private BookingDAL BookingDAL
        {
            get { return _bookingDAL ?? (_bookingDAL = new BookingDAL()); }
        }

        private CustomerDAL CustomerDAL
        {
            get { return _customerDAL ?? (_customerDAL = new CustomerDAL()); }
        }

        private CustomerTypeDAL CustomerTypeDAL
        {
            get { return _customerTypeDAL ?? (_customerTypeDAL = new CustomerTypeDAL()); }
        }

        /// <summary>
        /// Hämtar bokningar
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startDayTime"></param>
        /// <param name="endDayTime"></param>
        /// <returns></returns>
        public List<Booking> GetBookingsByDay(int room, string date, string startDayTime, string endDayTime)
        {
            return BookingDAL.GetBookingsByDay(room, date, startDayTime, endDayTime);
        }

        /// <summary>
        /// Hämtar lediga bopkningstider
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startDayTime"></param>
        /// <param name="endDayTime"></param>
        /// <returns></returns>
        public List<Booking> GetAvailableTimes(string date, string startDayTime, string endDayTime)
        {
            return BookingDAL.GetAvailableTimes(date, startDayTime, endDayTime);
        }
        

        /// <summary>
        /// Hämtar en bokning
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="customerId"></param>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public Booking GetBooking(int roomId, int customerId, string date, string hour)
        {
            return BookingDAL.GetBooking(roomId, customerId, date, hour);
        }

        ///// <summary>
        ///// Hämtar en bokning efter vald tid alternativt en default bokning med CustomerId satt till 0
        ///// </summary>
        ///// <param name="roomId"></param>
        ///// <param name="customerId"></param>
        ///// <param name="date"></param>
        ///// <param name="hour"></param>
        ///// <returns></returns>
        //public Booking CheckTimeForBooking(int roomId, DateTime startTime)
        //{
        //    return BookingDAL.CheckTimeForBooking(roomId, startTime);
        //}

        /// <summary>
        /// Sparar bokningsuppgifter (insert)
        /// </summary>
        /// <param name="customer"></param>
        public void SaveBooking(Booking booking)
        {
            //Validera objektet innan DAL anropas
            ICollection<ValidationResult> validationResults;
            if (!booking.Validate(out validationResults))
            {
                var ex = new ValidationException("Objektet klarade inte validering.");
                ex.Data.Add("Validationresults", validationResults);
                throw ex;
            }

            BookingDAL.AddBooking(booking);
        }

        /// <summary>
        /// Uppdaterar bokningsuppgifter
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateBooking(Booking booking)
        {
            //Validera objektet innan DAL anropas
            ICollection<ValidationResult> validationResults;
            if (!booking.Validate(out validationResults))
            {
                var ex = new ValidationException("Objektet klarade inte validering.");
                ex.Data.Add("Validationresults", validationResults);
                throw ex;
            }

            BookingDAL.UpdateBooking(booking);
        }

        /// <summary>
        /// Raderar en bokning
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="customerId"></param>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        public void DeleteBooking(int roomId, int customerId, string date, string hour)
        {
            BookingDAL.DeleteBooking(roomId, customerId, date, hour);
        }

        /// <summary>
        /// Hämtar kund
        /// </summary>
        /// <returns></returns>
        public Customer GetCustomer(int customerId)
        {
            return CustomerDAL.GetCustomer(customerId);
        }

        /// <summary>
        /// Hämtar alla kunder
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetAllCustomers()
        {
            return CustomerDAL.GetAllCustomers();
        }

        /// <summary>
        /// Raderar en kund
        /// </summary>
        /// <param name="customerId"></param>
        public void DeleteCustomer(int customerId)
        {
            CustomerDAL.DeleteCustomer(customerId);
        }

        /// <summary>
        /// Sparar kunduppgifter (insert och update)
        /// </summary>
        /// <param name="customer"></param>
        public void SaveCustomer(Customer customer)
        {
            //Validera objektet innan DAL anropas
            ICollection<ValidationResult> validationResults;
            if (!customer.Validate(out validationResults))
            {
                var ex = new ValidationException("Objektet klarade inte validering.");
                ex.Data.Add("Validationresults", validationResults);
                throw ex;
            }

            if (customer.CustomerId == 0)
            {
                CustomerDAL.AddCustomer(customer);
            }
            else
            {
                CustomerDAL.UpdateCustomer(customer);
            }
        }

        /// <summary>
        /// Hämtar en kundtyp
        /// </summary>
        /// <param name="customerTypeId"></param>
        /// <returns></returns>
        public CustomerTypeClass GetCustomerType(int customerTypeId)
        {
            return CustomerTypeDAL.GetCustomerType(customerTypeId);
        }

        /// <summary>
        /// Hämtar alla kundtyper
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerTypeClass> GetAllCustomerTypes()
        {
            return CustomerTypeDAL.GetAllCustomerTypes();
        }
    }
}