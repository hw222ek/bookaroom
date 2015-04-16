using BookARoom.Model.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookARoom.Model.DAL
{
    /// <summary>
    /// Hämtar en eller alla kunder från databastabellen
    /// </summary>
    public class CustomerDAL : DALBase
    {
        /// <summary>
        /// Hämtar alla kunder ur databasen
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_GetCustomers", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Skapar List-objekt som lagrar Bookings-objekt som läses in.
                    List<Customer> customers = new List<Customer>(200);

                    //Öppnar databasanslutning.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        //Tar reda på och lagrar index över kolumnerna i databastabellen.
                        var customerIdIndex = reader.GetOrdinal("CustomerId");
                        var nameIndex = reader.GetOrdinal("Name");
                        var customerTypeIdIndex = reader.GetOrdinal("CustomerTypeId");

                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                CustomerId = reader.GetInt32(customerIdIndex),
                                Name = reader.GetString(nameIndex),
                                CustomerTypeId = reader.GetInt32(customerTypeIdIndex)
                            });
                        }
                        //Korrigerar kapaciteten vid behov på List-objektet
                        customers.TrimExcess();

                        return customers;
                    }
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när kundinformation hämtades från databasen.");
                }
            }
        }
        
        /// <summary>
        /// Hämtar vald kund ur databasen
        /// </summary>
        /// <param name="customerTypeId"></param>
        /// <returns></returns>
        public Customer GetCustomer(int customerId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_GetCustomers", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till parametrar att skicka med till SPROC:en.
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    //Öppnar databasanslutning.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        //Tar reda på och lagrar index över kolumnerna i databastabellen.
                        var customerIdIndex = reader.GetOrdinal("CustomerId");
                        var nameIndex = reader.GetOrdinal("Name");
                        var customerTypeIdIndex = reader.GetOrdinal("CustomerTypeId");

                        while (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerId = reader.GetInt32(customerIdIndex),
                                Name = reader.GetString(nameIndex),
                                CustomerTypeId = reader.GetInt32(customerTypeIdIndex)
                            };
                        }
                    }
                    //Returnerar null om ingen kundinformation returneras
                    return null;
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när kundinformation hämtades från databasen.");
                }
            }
            
        }

        /// <summary>
        /// Lägger till ny kund
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_AddCustomer", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till parametrar att skicka med till SPROC:en.
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@CustomerTypeId", customer.CustomerTypeId);

                    //ID SOM OUTPUT??? - FIXA SPROC:EN

                    //Öppnar databasanslutning.
                    conn.Open();

                    //Exekverar lagracde proceduren
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när kundinformation skulle sparas till databasen.");
                }
             }
        }

        /// <summary>
        /// Uppdaterar befintlig kund
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateCustomer(Customer customer)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_UpdateCustomer", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till parametrar att skicka med till SPROC:en.
                    cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@CustomerTypeId", customer.CustomerTypeId);

                    //Öppnar databasanslutning.
                    conn.Open();

                    //Exekverar lagracde proceduren
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när kundinformation skulle uppdateras i databasen.");
                }
            }
        }

        /// <summary>
        /// Raderar en befintlig kund
        /// </summary>
        /// <param name="customer"></param>
        public void DeleteCustomer(int customerId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_DeleteCustomer", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till parametrar att skicka med till SPROC:en.
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    //Öppnar databasanslutning.
                    conn.Open();

                    //Exekverar lagracde proceduren
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när kundinformation skulle raderas i databasen.");
                }
            }
        }
    }


}