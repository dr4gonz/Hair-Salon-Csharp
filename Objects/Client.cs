using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
    public class Client
    {
        private int _id;
        private string _name;
        private string _phone;
        private string _email;
        private int _stylist_id;

        public Client(string name, string phone, string email, int stylist_id, int id = 0)
        {
            _name = name;
            _phone = phone;
            _email = email;
            _stylist_id = stylist_id;
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

        public int GetStylistId()
        {
            return _stylist_id;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
            cmd.ExecuteNonQuery();
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client>{};

            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
            rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int newClientId = rdr.GetInt32(0);
                string newClientName = rdr.GetString(1);
                string newClientPhone = rdr.GetString(2);
                string newClientEmail = rdr.GetString(3);
                int newClientStylistId = rdr.GetInt32(4);

                Client newClient = new Client(newClientName, newClientPhone, newClientEmail, newClientStylistId, newClientId);
                allClients.Add(newClient);
            }

            if(rdr != null) rdr.Close();
            if(conn != null) conn.Close();

            return allClients;
        }

        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = (_id == newClient.GetId());
                bool nameEquality = (_name == newClient.GetName());
                bool phoneEquality = (_phone == newClient.GetPhone());
                bool emailEquality = (_email == newClient.GetEmail());
                bool stylistIdEquality = (_stylist_id == newClient.GetStylistId());
                return (idEquality && nameEquality && phoneEquality && emailEquality && stylistIdEquality);
            }
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, phone, email, stylist_id) OUTPUT INSERTED.id VALUES(@ClientName, @ClientPhone, @ClientEmail, @ClientStylistId);", conn);

            SqlParameter clientNameParameter = new SqlParameter();
            clientNameParameter.ParameterName = "@ClientName";
            clientNameParameter.Value = this.GetName();
            cmd.Parameters.Add(clientNameParameter);

            SqlParameter clientPhoneParameter = new SqlParameter();
            clientPhoneParameter.ParameterName = "@ClientPhone";
            clientPhoneParameter.Value = this.GetPhone();
            cmd.Parameters.Add(clientPhoneParameter);

            SqlParameter clientEmailParameter = new SqlParameter();
            clientEmailParameter.ParameterName = "@ClientEmail";
            clientEmailParameter.Value = this.GetEmail();
            cmd.Parameters.Add(clientEmailParameter);

            SqlParameter clientStylistIdParameter = new SqlParameter();
            clientStylistIdParameter.ParameterName = "@ClientStylistId";
            clientStylistIdParameter.Value = this.GetStylistId();
            cmd.Parameters.Add(clientStylistIdParameter);

            rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            if(rdr != null) rdr.Close();
            if(conn != null) conn.Close();
        }

        public static Client Find(int id)
        {
            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);
            SqlParameter clientIdParameter = new SqlParameter();
            clientIdParameter.ParameterName = "@ClientId";
            clientIdParameter.Value = id.ToString();
            cmd.Parameters.Add(clientIdParameter);

            rdr = cmd.ExecuteReader();

            int foundClientId = 0;
            string foundClientName = null;
            string foundClientPhone = null;
            string foundClientEmail = null;
            int foundClientStylistId = 0;

            while(rdr.Read())
            {
                foundClientId = rdr.GetInt32(0);
                foundClientName = rdr.GetString(1);
                foundClientPhone = rdr.GetString(2);
                foundClientEmail = rdr.GetString(3);
                foundClientStylistId = rdr.GetInt32(4);
            }
            Client newClient = new Client(foundClientName, foundClientPhone, foundClientEmail, foundClientStylistId, foundClientId);

            if(rdr != null) rdr.Close();
            if(conn != null) conn.Close();

            return newClient;
        }

        public void Update(string newName, string newPhone, string newEmail, int newStylistId)
        {
            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @NewName, phone = @NewPhone, email = @NewEmail, stylist_id = @NewStylistId OUTPUT INSERTED.name, INSERTED.phone, INSERTED.email, INSERTED.stylist_id WHERE id = @ClientId;", conn);

            SqlParameter newNameParameter = new SqlParameter();
            newNameParameter.ParameterName = "@NewName";
            newNameParameter.Value = newName;
            cmd.Parameters.Add(newNameParameter);

            SqlParameter newPhoneParameter = new SqlParameter();
            newPhoneParameter.ParameterName = "@NewPhone";
            newPhoneParameter.Value = newPhone;
            cmd.Parameters.Add(newPhoneParameter);

            SqlParameter newEmailParameter = new SqlParameter();
            newEmailParameter.ParameterName = "@NewEmail";
            newEmailParameter.Value = newEmail;
            cmd.Parameters.Add(newEmailParameter);

            SqlParameter newStylistIdParamter = new SqlParameter();
            newStylistIdParamter.ParameterName = "@NewStylistId";
            newStylistIdParamter.Value = newStylistId;
            cmd.Parameters.Add(newStylistIdParamter);

            SqlParameter clientIdParameter = new SqlParameter();
            clientIdParameter.ParameterName = "@ClientId";
            clientIdParameter.Value = this.GetId();
            cmd.Parameters.Add(clientIdParameter);

            rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._name = rdr.GetString(0);
                this._phone = rdr.GetString(1);
                this._email = rdr.GetString(2);
                this._stylist_id = rdr.GetInt32(3);
            }

            if(rdr != null) rdr.Close();
            if(conn != null) conn.Close();
        }

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);

            SqlParameter clientIdParameter = new SqlParameter();
            clientIdParameter.ParameterName = "@ClientId";
            clientIdParameter.Value = this.GetId();
            cmd.Parameters.Add(clientIdParameter);

            cmd.ExecuteNonQuery();

            if(conn != null) conn.Close();
        }
    }
}
