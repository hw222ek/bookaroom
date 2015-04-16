using BookARoom.Model.BLL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookARoom.Model.DAL
{
    public class BookingDAL : DALBase
    {
        /// <summary>
        /// Hämtar alla bokningar valt datum och tidsintervall
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startDayTime"></param>
        /// <param name="endDayTime"></param>
        /// <returns></returns>
        public List<Booking> GetBookingsByDay(int room, string date, string startDayTime, string endDayTime)
        {
            using(var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_GetBookingsByDay", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till parametrar att skicka med till SPROC:en.
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@TimeOne", startDayTime);
                    cmd.Parameters.AddWithValue("@TimeTwo", endDayTime);
                    cmd.Parameters.AddWithValue("@RoomId", room);

                    //Skapar List-objekt som lagrar Bookings-objekt som läses in.
                    List<Booking> bookings = new List<Booking>(10);

                    //Öppnar databasanslutning.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        //Tar reda på och lagrar index över kolumnerna i databastabellen.
                        var roomIdIndex = reader.GetOrdinal("RoomId");
                        var customerIdIndex = reader.GetOrdinal("CustomerId");
                        var startTimeIndex = reader.GetOrdinal("StartTime");
                        var endTimeIndex = reader.GetOrdinal("EndTime");
                        var personIndex = reader.GetOrdinal("Person");

                        while (reader.Read())
                        {
                            bookings.Add(new Booking
                            {
                                RoomId = reader.GetInt32(roomIdIndex),
                                CustomerId = reader.GetInt32(customerIdIndex),
                                StartTime = reader.GetDateTime(startTimeIndex),
                                EndTime = reader.GetDateTime(endTimeIndex),
                                Person = reader.GetString(personIndex)
                            });
                        }
                        //Korrigerar kapaciteten vid behov på List-objektet
                        bookings.TrimExcess();

                        return bookings;
                    }
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när bokningsinformation hämtades från databasen.");
                }
            }
        }

        /// <summary>
        /// Hämtar alla lediga tider som bokningsobjekt valt datum och tidsintervall
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startDayTime"></param>
        /// <param name="endDayTime"></param>
        /// <returns></returns>
        public List<Booking> GetAvailableTimes(string date, string startDayTime, string endDayTime)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_GetAvailableTimes", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till parametrar att skicka med till SPROC:en.
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@TimeOne", startDayTime);
                    cmd.Parameters.AddWithValue("@TimeTwo", endDayTime);

                    //Skapar List-objekt som lagrar Bookings-objekt som läses in.
                    List<Booking> bookings = new List<Booking>(10);

                    //Öppnar databasanslutning.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        //Tar reda på och lagrar index över kolumnerna i databastabellen.
                        var roomIdIndex = reader.GetOrdinal("RoomId");
                        var customerIdIndex = reader.GetOrdinal("CustomerId");
                        var startTimeIndex = reader.GetOrdinal("StartTime");
                        var endTimeIndex = reader.GetOrdinal("EndTime");

                        while (reader.Read())
                        {
                            bookings.Add(new Booking
                            {
                                RoomId = reader.GetInt32(roomIdIndex),
                                CustomerId = reader.GetInt32(customerIdIndex),
                                StartTime = reader.GetDateTime(startTimeIndex),
                                EndTime = reader.GetDateTime(endTimeIndex),
                            });
                        }
                        //Korrigerar kapaciteten vid behov på List-objektet
                        bookings.TrimExcess();

                        return bookings;
                    }
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när bokningsinformation hämtades från databasen.");
                }
            }
        }

        /// <summary>
        /// Hämtar vald bokning ur databasen
        /// </summary>
        /// <param name="customerTypeId"></param>
        /// <returns></returns>
        public Booking GetBooking(int roomId, int customerId, string date, string hour)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_GetBooking", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till parametrar att skicka med till SPROC:en.
                    cmd.Parameters.AddWithValue("@RoomId", roomId);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@StartTime", Convert.ToDateTime(String.Format("{0} {1}:00:00", date, hour)));

                    //Öppnar databasanslutning.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        //Tar reda på och lagrar index över kolumnerna i databastabellen.
                        var roomIdIndex = reader.GetOrdinal("RoomId");
                        var customerIdIndex = reader.GetOrdinal("CustomerId");
                        var startTimeIndex = reader.GetOrdinal("StartTime");
                        var endTimeIndex = reader.GetOrdinal("EndTime");
                        var personIndex = reader.GetOrdinal("Person");

                        while (reader.Read())
                        {
                            return new Booking
                            {
                                RoomId = reader.GetInt32(roomIdIndex),
                                CustomerId = reader.GetInt32(customerIdIndex),
                                StartTime = reader.GetDateTime(startTimeIndex),
                                EndTime = reader.GetDateTime(endTimeIndex),
                                Person = reader.GetString(personIndex)
                            };
                        }
                    }
                    //Returnerar null om ingen bokning returneras
                    return null;
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när bokningsinformation hämtades från databasen.");
                }
            } 
        }

        ///// <summary>
        ///// Hämtar bokning ur databasen om den finns - annars default bokning med customerId satt till 0
        ///// </summary>
        ///// <param name="customerTypeId"></param>
        ///// <returns></returns>
        //public Booking CheckTimeForBooking(int roomId, DateTime startTime)
        //{
        //    using (var conn = CreateConnection())
        //    {
        //        try
        //        {
        //            //Initierar SQLCommand-objekt.
        //            var cmd = new SqlCommand("app.usp_GetBooking", conn);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            //Lägger till parametrar att skicka med till SPROC:en.
        //            cmd.Parameters.AddWithValue("@RoomId", roomId);
        //            cmd.Parameters.AddWithValue("@StartTime", startTime);

        //            //Öppnar databasanslutning.
        //            conn.Open();

        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                //Tar reda på och lagrar index över kolumnerna i databastabellen.
        //                var roomIdIndex = reader.GetOrdinal("RoomId");
        //                var customerIdIndex = reader.GetOrdinal("CustomerId");
        //                var startTimeIndex = reader.GetOrdinal("StartTime");
        //                var endTimeIndex = reader.GetOrdinal("EndTime");
        //                var personIndex = reader.GetOrdinal("Person");

        //                while (reader.Read())
        //                {
        //                    return new Booking
        //                    {
        //                        RoomId = reader.GetInt32(roomIdIndex),
        //                        CustomerId = reader.GetInt32(customerIdIndex),
        //                        StartTime = reader.GetDateTime(startTimeIndex),
        //                        EndTime = reader.GetDateTime(endTimeIndex),
        //                        Person = reader.GetString(personIndex)
        //                    };
        //                }
        //            }
        //            //Returnerar null om ingen bokning returneras
        //            return null;
        //        }
        //        catch (Exception)
        //        {
        //            throw new ApplicationException("Ett fel inträffade när bokningsinformation hämtades från databasen.");
        //        }
        //    }
        //}

        /// <summary>
        /// Lägger till ny bokning
        /// </summary>
        /// <param name="customer"></param>
        public void AddBooking(Booking booking)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_AddBooking", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till parametrar att skicka med till SPROC:en.
                    cmd.Parameters.AddWithValue("@RoomId", booking.RoomId);
                    cmd.Parameters.AddWithValue("@CustomerId", booking.CustomerId);
                    cmd.Parameters.AddWithValue("@StartTime", booking.StartTime);
                    cmd.Parameters.AddWithValue("@Person", booking.Person);

                    //Öppnar databasanslutning.
                    conn.Open();

                    //Exekverar lagracde proceduren
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när bokningsinformation skulle sparas till databasen.");
                }
            }
        }

        /// <summary>
        /// Uppdaterar befintlig bokning
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateBooking(Booking booking)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_UpdateBooking", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till parametrar att skicka med till SPROC:en.
                    cmd.Parameters.AddWithValue("@RoomId", booking.RoomId);
                    cmd.Parameters.AddWithValue("@CustomerId", booking.CustomerId);
                    cmd.Parameters.AddWithValue("@StartTime", booking.StartTime);
                    cmd.Parameters.AddWithValue("@OldTime", booking.EndTime);
                    cmd.Parameters.AddWithValue("@Person", booking.Person);

                    //Öppnar databasanslutning.
                    conn.Open();

                    //Exekverar lagracde proceduren
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när bokningsinformationen skulle uppdateras i databasen.");
                }
            }
        }

        /// <summary>
        /// Raderar en befintlig bokning
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteBooking(int roomId, int customerId, string date, string hour)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_DeleteBooking", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till parametrar att skicka med till SPROC:en.
                    cmd.Parameters.AddWithValue("@RoomId", roomId);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@StartTime", Convert.ToDateTime(String.Format("{0} {1}:00:00", date, hour)));

                    //Öppnar databasanslutning.
                    conn.Open();

                    //Exekverar lagracde proceduren
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när bokningsinformation skuller raderas i databasen.");
                }
            }
        }
    }
}