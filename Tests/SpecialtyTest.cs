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
      Patient.DeleteAll();
      Doctor.DeleteAll();
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

    [Fact]
    public void Test_ReturnDoctorsBySpecialty()
    {
      //Arrange
      Specialty newSpecialty = new Specialty("Pediatrician", 1);
      List<Doctor> expectedResult = new List<Doctor> {};
      Doctor doc1 = new Doctor("Harris", 1);
      Doctor doc2 = new Doctor("Kepner", 1);
      expectedResult.Add(doc1);
      expectedResult.Add(doc2);
      doc1.Save();
      doc2.Save();

      //Act
      List<Doctor> result = newSpecialty.FindDoctors();

      //Assert
      Assert.Equal(expectedResult, result);
    }
  }
}
