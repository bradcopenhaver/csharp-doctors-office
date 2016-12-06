using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using DrOffice.Objects;

namespace DrOffice
{
  public class PatientTest : IDisposable
  {
    public PatientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=doctors_office_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Patient.DeleteAll();
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      Patient newPatient = new Patient("Loren", "head cold", 1);
      List<Patient> expectedResult = new List<Patient> {};

      //Act
      newPatient.Save();
      expectedResult.Add(newPatient);
      List<Patient> result = Patient.GetAll();

      //Assert
      Assert.Equal(expectedResult, result);
    }
  }
}
