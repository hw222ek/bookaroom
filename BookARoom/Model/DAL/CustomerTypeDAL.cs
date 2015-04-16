using BookARoom.Model.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookARoom.Model.DAL
{
    public class CustomerTypeDAL : DALBase
    {
        /// <summary>
        /// Hämtar alla kundtyper ur databasen
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerTypeClass> GetAllCustomerTypes()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_GetCustomerTypes", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Skapar List-objekt som lagrar Bookings-objekt som läses in.
                    List<CustomerTypeClass> customerTypes = new List<CustomerTypeClass>(4);

                    //Öppnar databasanslutning.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        //Tar reda på och lagrar index över kolumnerna i databastabellen.
                        var customerTypeIdIndex = reader.GetOrdinal("CustomerTypeId");
                        var customerTypeIndex = reader.GetOrdinal("CustomerType");

                        while (reader.Read())
                        {
                            customerTypes.Add(new CustomerTypeClass
                            {
                                CustomerTypeId = reader.GetInt32(customerTypeIdIndex),
                                CustomerType = reader.GetString(customerTypeIndex)
                            });
                        }
                        //Korrigerar kapaciteten vid behov på List-objektet
                        customerTypes.TrimExcess();

                        return customerTypes;
                    }
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när kundtypsinformation hämtades från databasen.");
                }
            }
        }

        /// <summary>
        /// Hämtar en kundtyp
        /// </summary>
        /// <param name="customerTypeId"></param>
        /// <returns></returns>
        public CustomerTypeClass GetCustomerType(int customerTypeId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Initierar SQLCommand-objekt.
                    var cmd = new SqlCommand("app.usp_GetCustomerType", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till parametrar att skicka med till SPROC:en.
                    cmd.Parameters.AddWithValue("@CustomerTypeId", customerTypeId);

                    //Öppnar databasanslutning.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        //Tar reda på och lagrar index över kolumnerna i databastabellen.
                        var customerTypeIdIndex = reader.GetOrdinal("CustomerTypeId");
                        var customerTypeIndex = reader.GetOrdinal("CustomerType");

                        while (reader.Read())
                        {
                            return new CustomerTypeClass
                            {
                                CustomerTypeId = reader.GetInt32(customerTypeIdIndex),
                                CustomerType = reader.GetString(customerTypeIndex)
                            };
                        }
                    }
                    //Returnerar null om ingen kundtyp returneras
                    return null;
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel inträffade när kundtypsinformation hämtades från databasen.");
                }
            } 
        }
    }
}