using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using DrOffice.Objects;

namespace DrOffice
{
  public class SpecialtyTest : IDisposable
  {
    public SpecialtyTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=doctors_office_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Specialty.DeleteAll();
    }

    [Fact]
    public void Test_CheckSaveAndGetFunctions_True()
    {
      //Arrange
      List<Specialty> testList = new List<Specialty> {};
      Specialty newSpecialty = new Specialty("Ear, Nose, Throat");

      //Act
      testList.Add(newSpecialty);
      newSpecialty.Save();
      List<Specialty> allSpecialties = Specialty.GetAll();

      //Assert
      Assert.Equal(testList, allSpecialties);
    }
  }
}
