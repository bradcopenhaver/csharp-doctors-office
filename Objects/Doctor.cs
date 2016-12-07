using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace DrOffice.Objects
{
  public class Doctor
  {
    private string _name;
    private int _id;
    private int _specialty_id;

    public Doctor(string name, int specialty_id, int id = 0)
    {
      _name = name;
      _specialty_id = specialty_id;
      _id = id;
    }

    public int GetSpecialtyId()
    {
      return _specialty_id;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM doctors;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public override bool Equals(System.Object otherDoctor)
    {
      if (!(otherDoctor is Doctor))
      {
        return false;
      }
      else
      {
        Doctor newDoctor = (Doctor) otherDoctor;
        bool idEquality = (this.GetId() == newDoctor.GetId());
        bool nameEquality = (this.GetName() == newDoctor.GetName());
        bool specialtyEquality = (this.GetSpecialtyId() == newDoctor.GetSpecialtyId());
        return (idEquality && nameEquality && specialtyEquality);
      }
    }

    public override int GetHashCode()
    {
     return this.GetName().GetHashCode();
    }

    public static List<Doctor> GetAll()
    {
      List<Doctor> allDoctors = new List<Doctor>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM doctors ORDER BY name;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int doctorId = rdr.GetInt32(0);
        string doctorName = rdr.GetString(1);
        int specialty_id = rdr.GetInt32(2);
        Doctor newDoctor = new Doctor(doctorName, specialty_id, doctorId);
        allDoctors.Add(newDoctor);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allDoctors;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO doctors (name, specialty_id) OUTPUT INSERTED.id VALUES (@DoctorName, @SpecialtyId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@DoctorName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter specialtyIdParameter = new SqlParameter();
      specialtyIdParameter.ParameterName = "@SpecialtyId";
      specialtyIdParameter.Value = this.GetSpecialtyId();
      cmd.Parameters.Add(specialtyIdParameter);

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

    public List<Patient> FindPatients()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patients WHERE doctor_id = @DoctorId ORDER BY name;", conn);
      SqlParameter drIdParameter = new SqlParameter();
      drIdParameter.ParameterName = "@DoctorId";
      drIdParameter.Value = (this)._id.ToString();
      cmd.Parameters.Add(drIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Patient> foundPatients = new List<Patient> {};
      while(rdr.Read())
      {
        int foundPatientId = 0;
        string foundPatientName = null;
        string foundPatientAilment = null;
        int foundPatientDrId = 0;

        foundPatientId = rdr.GetInt32(0);
        foundPatientName = rdr.GetString(1);
        foundPatientAilment = rdr.GetString(2);
        foundPatientDrId = rdr.GetInt32(3);

        Patient foundPatient = new Patient(foundPatientName, foundPatientAilment, foundPatientDrId, foundPatientId);
        foundPatients.Add(foundPatient);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundPatients;
    }
  }
}
