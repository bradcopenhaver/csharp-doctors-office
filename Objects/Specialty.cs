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
  }  
}
