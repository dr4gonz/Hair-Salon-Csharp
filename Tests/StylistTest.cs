using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
    public class StylistTest : IDisposable
    {
        public StylistTest()
        {
            // DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
            DBConfiguration.ConnectionString = "Data Source=(DESKTOP-7OLC9FT)\\SQLEXPRESS;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
        }
        public void Dispose()
        {
            Stylist.DeleteAll();
        }
