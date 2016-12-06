using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace DrOffice.Objects
{
  public class Doctor
  {
    private string _name;
    private int _id;
    private string _specialty_id;

    public Doctor(string name, string specialty_id, int id = 0)
    {
      _name = name;
      _specialty_id = specialty_id;
      _id = id;
    }
  }
}
