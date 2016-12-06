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
  }
}
