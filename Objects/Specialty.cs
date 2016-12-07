using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace DrOffice.Objects
{
  public class Specialty
  {
    private string _name;
    private int _id;
    public Specialty(string name, int id = 0)
    {
      _name = name;
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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM specialties;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = (this.GetId() == newSpecialty.GetId());
        bool nameEquality = (this.GetName() == newSpecialty.GetName());
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
     return this.GetName().GetHashCode();
    }

    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties = new List<Specialty>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM specialties ORDER BY name;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialtyName = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
        allSpecialties.Add(newSpecialty);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allSpecialties;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO specialties (name) OUTPUT INSERTED.id VALUES (@SpecialtyName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@SpecialtyName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

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

    public static Specialty Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM specialties WHERE id = @SpecialtyId;", conn);
      SqlParameter specialtyIdParameter = new SqlParameter();
      specialtyIdParameter.ParameterName = "@SpecialtyId";
      specialtyIdParameter.Value = id.ToString();
      cmd.Parameters.Add(specialtyIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundSpecialtyId = 0;
      string foundSpecialtyName = null;
      while(rdr.Read())
      {
        foundSpecialtyId = rdr.GetInt32(0);
        foundSpecialtyName = rdr.GetString(1);
      }
      Specialty foundSpecialty = new Specialty(foundSpecialtyName, foundSpecialtyId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundSpecialty;
    }

    public List<Doctor> FindDoctors()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM doctors WHERE specialty_id = @SpecialtyId ORDER BY name;", conn);
      SqlParameter specialtyIdParameter = new SqlParameter();
      specialtyIdParameter.ParameterName = "@SpecialtyId";
      specialtyIdParameter.Value = (this)._id.ToString();
      cmd.Parameters.Add(specialtyIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Doctor> foundDoctors = new List<Doctor> {};
      while(rdr.Read())
      {
        int foundDoctorId = 0;
        string foundDoctorName = null;
        int foundDoctorSpecialty = 0;

        foundDoctorId = rdr.GetInt32(0);
        foundDoctorName = rdr.GetString(1);
        foundDoctorSpecialty = rdr.GetInt32(2);

        Doctor foundDoctor = new Doctor(foundDoctorName, foundDoctorSpecialty, foundDoctorId);
        foundDoctors.Add(foundDoctor);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundDoctors;
    }
  }
}
