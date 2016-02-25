using System;
using Newtonsoft.Json;

namespace App2
{
    public class UserAt
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string age { get; set; }

        public string password { get; set; }
        public string password_confirmation { get; set; }
        public string auth_token { get; set; }
        public medicalBookAt book { get; set; }
    }

    public class TokenAt
    {
        public string jwt { get; set; }
    }

    public class Doctor
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string speciality { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string city { get; set; }
    }

    public class Appointment
    {   
        public int id { get; set; }
        public string begins { get; set; }
        public string finish { get; set; }
        public string user_id { get; set; }
        public string docotr_id { get; set; }
        public bool taken { get; set; }
    }
}
