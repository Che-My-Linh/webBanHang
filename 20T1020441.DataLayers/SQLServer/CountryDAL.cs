using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020441.DomainModels;
using System.Data;
using System.Data.SqlClient;

namespace _20T1020441.DataLayers.SQLServer
{
    
        public class CountryDAL:_BaseDAL, ICountryDAL
        {
        public CountryDAL(string ConnectionString) : base(ConnectionString) {}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param
        public IList<Country> List()
            {
            List<Country> data = new List<Country>();
            using(var connection = OpenConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select CountryName From [dbo].[Countries]";
                cmd.CommandType = CommandType.Text;
                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); 
                while (dbReader.Read())
                {
                    data.Add(new Country()
                    {
                        CountryName = Convert.ToString(dbReader["CountryName"])
                    });
                }

                dbReader.Close();
                connection.Close();
                

            }
            return data;
            }
        }

    }

