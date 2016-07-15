using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
    public class Stylist
    {
        private int _id;
        private string _name;
        private string _phone;
        private string _email;

        public Stylist(string name, string phone, string email, int id = 0)
        {
            _name = name;
            _phone = phone;
            _email = email;
            _id = id;
        }

        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }

        public string GetPhone()
        {
            return _phone;
        }

        public string GetEmail()
        {
            return _email;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
        }
    }
}
