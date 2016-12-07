using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using DrOffice.Objects;

namespace DrOffice
{
  public class DoctorTest : IDisposable
  {
    public DoctorTest()
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
    public void Test_Save()
    {
      //Arrange
      Doctor newDoctor = new Doctor("Harris", 1);
      List<Doctor> expectedResult = new List<Doctor> {};

      //Act
      newDoctor.Save();
      expectedResult.Add(newDoctor);
      List<Doctor> result = Doctor.GetAll();

      //Assert
      Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Test_ReturnPatientsByDoctor()
    {
      //Arrange
      Doctor newDoctor = new Doctor("Horton", 1, 1);
      List<Patient> expectedResult = new List<Patient> {};
      Patient patient1 = new Patient("Fred", "Broken Leg", 1);
      Patient patient2 = new Patient("Joe", "Sore throat", 1);
      patient1.Save();
      patient2.Save();
      expectedResult.Add(patient1);
      expectedResult.Add(patient2);

      //Act
      List<Patient> result = newDoctor.FindPatients();
      
      //Assert
      Assert.Equal(expectedResult, result);
    }
  }
}


//     [Fact]
//     public void Test_FindFindsTaskInDatabase()
//     {
//       //Arrange
//       Task testTask = new Task("Mow the lawn", 1);
//       testTask.Save();
//
//       //Act
//       Task foundTask = Task.Find(testTask.GetId());
//
//       //Assert
//       Assert.Equal(testTask, foundTask);
//     }
//   }
// }
