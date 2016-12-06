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
      Doctor.DeleteAll();
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
  }
}
//
//     [Fact]
//     public void Test_SaveAssignsIdToObject()
//     {
//       //Arrange
//       Task testTask = new Task("Mow the lawn", 1);
//       testTask.Save();
//
//       //Act
//       Task savedTask = Task.GetAll()[0];
//
//       int result = savedTask.GetId();
//       int testId = testTask.GetId();
//
//       //Assert
//       Assert.Equal(testId, result);
//     }
//
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
