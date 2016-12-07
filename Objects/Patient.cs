using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Globalization;

namespace DrOffice.Objects
{
  public class Patient
  {
    private string _name;
    private string _ailment;
    private int _doctor_id;
    private int _id;

    public Patient(string name, string ailment, int doctor_id, int id = 0)
    {
      _name = name;
      _ailment = ailment;
      _doctor_id = doctor_id;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetAilment()
    {
      return _ailment;
    }

    public int GetDoctorId()
    {
      return _doctor_id;
    }

    public int GetId()
    {
      return _id;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM patients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public override bool Equals(System.Object otherPatient)
    {
      if (!(otherPatient is Patient))
      {
        return false;
      }
      else
      {
        Patient newPatient = (Patient) otherPatient;
        bool idEquality = (this.GetId() == newPatient.GetId());
        bool nameEquality = (this.GetName() == newPatient.GetName());
        bool ailmentEquality = (this.GetAilment() == newPatient.GetAilment());
        bool drIdEquality = (this.GetDoctorId() == newPatient.GetDoctorId());
        return (idEquality && nameEquality && ailmentEquality && drIdEquality);
      }
    }

    public override int GetHashCode()
    {
     return this.GetName().GetHashCode();
    }

    public static List<Patient> GetAll()
    {
      List<Patient> allPatients = new List<Patient>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients ORDER BY name;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int patientId = rdr.GetInt32(0);
        string patientName = rdr.GetString(1);
        int doctor_id = rdr.GetInt32(3);
        string patientAilment = rdr.GetString(2);
        Patient newPatient = new Patient(patientName, patientAilment, doctor_id, patientId);
        allPatients.Add(newPatient);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allPatients;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO patients (name, ailment, doctor_id) OUTPUT INSERTED.id VALUES (@PatientName, @PatientAilment, @DoctorId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@PatientName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter ailmentParameter = new SqlParameter();
      ailmentParameter.ParameterName = "@PatientAilment";
      ailmentParameter.Value = this.GetAilment();
      cmd.Parameters.Add(ailmentParameter);

      SqlParameter doctorParameter = new SqlParameter();
      doctorParameter.ParameterName = "@DoctorId";
      doctorParameter.Value = this.GetDoctorId();
      cmd.Parameters.Add(doctorParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
