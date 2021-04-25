using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using ES.Tools.MVVM;
using NUnit.Framework;

namespace ES.Tools.UnitTests.MVVM
{
  /// <summary>
  /// Unit tests testing the <see cref="ViewModelExtensions"/>
  /// </summary>
  public class ViewModelExtensionsTests
  {
    private List<Employee> _testEmployees;

    [OneTimeSetUp]
    public void Init()
    {
      _testEmployees = new List<Employee>
      {
        new Employee("Michael", "Myers", new DateTime(1957, 10, 19)),
        new Employee("Jason", "Voorhees", new DateTime(1946, 6, 13)),
        new Employee("Freddy", "Krueger", new DateTime(1942, 9, 1)),
      };
    }


    [Test]
    public void GetViewTest()
    {
      var view = _testEmployees.GetView();
      Assert.AreEqual(_testEmployees.Count, view.Count);
      Assert.That(view, Is.EquivalentTo(_testEmployees));
    }

    [Test]
    public void ViewSortingTest()
    {
      var view = _testEmployees.GetView();
      _testEmployees.SetSorting(new SortDescription(nameof(Employee.LastName), ListSortDirection.Ascending));
      CheckLastNameOrder(view);

      _testEmployees.SetSorting(new SortDescription(nameof(Employee.FirstName), ListSortDirection.Ascending));
      CheckFirstNameOrder(view);

      _testEmployees.SetSorting(new SortDescription(nameof(Employee.DOB), ListSortDirection.Ascending));
      CheckDOBOrder(view);

      _testEmployees.ClearSorting();
      CheckUnordered(view);
    }

    private static void CheckLastNameOrder(CollectionView view)
    {
      var orderedList = view.OfType<Employee>().ToList();

      for (int i = 0; i < orderedList.Count - 1; i++)
      {
        Assert.That(orderedList[i].LastName, Is.LessThan(orderedList[i + 1].LastName));
      }
    }

    private static void CheckFirstNameOrder(CollectionView view)
    {
      var orderedList = view.OfType<Employee>().ToList();

      for (int i = 0; i < orderedList.Count - 1; i++)
      {
        Assert.That(orderedList[i].FirstName, Is.LessThan(orderedList[i + 1].FirstName));
      }
    }

    private static void CheckDOBOrder(CollectionView view)
    {
      var orderedList = view.OfType<Employee>().ToList();

      for (int i = 0; i < orderedList.Count - 1; i++)
      {
        Assert.That(orderedList[i].DOB, Is.LessThan(orderedList[i + 1].DOB));
      }
    }

    private void CheckUnordered(CollectionView view)
    {
      var unOrderedList = view.OfType<Employee>().ToList();

      for (int i = 0; i < unOrderedList.Count; i++)
      {
        Assert.That(unOrderedList[i], Is.SameAs(_testEmployees[i]));
      }
    }

    private class Employee
    {
      public Employee(string firstName, string lastName, DateTime dob)
      {
        FirstName = firstName;
        LastName = lastName;
        DOB = dob;
      }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public DateTime DOB { get; set; }
    }
  }
}
