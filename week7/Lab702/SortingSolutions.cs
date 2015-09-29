using System;
using System.Collections.Generic;
 
namespace Lab7
{
  class MainApp
  {
    static void Main()
    {
      // Two contexts following different strategies
      SortedList studentRecords = new SortedList();
 
      studentRecords.Add("Samual");
      studentRecords.Add("Jimmy");
      studentRecords.Add("Sandra");
      studentRecords.Add("Vivek");
      studentRecords.Add("Anna");
 
      studentRecords.SetSortMechanism(new QuickSort());
      studentRecords.Sort();
 
      studentRecords.SetSortMechanism(new ShellSort());
      studentRecords.Sort();
 
      studentRecords.SetSortMechanism(new MergeSort());
      studentRecords.Sort();

      Console.ReadKey();
    }
  }
 
  abstract class SortThings
  {
    public abstract void Sort(List<string> list);
  }
 
  class QuickSort : SortThings
  {
    public override void Sort(List<string> list)
    {
      list.Sort(); // Default is Quicksort
      Console.WriteLine("QuickSorted list ");
    }
  }

  class ShellSort : SortThings
  {
    public override void Sort(List<string> list)
    {
      //list.ShellSort(); not-implemented
      Console.WriteLine("ShellSorted list ");
    }
  }
 
  class MergeSort : SortThings
  {
    public override void Sort(List<string> list)
    {
      //list.MergeSort(); not-implemented
      Console.WriteLine("MergeSorted list ");
    }
  }
 
  class SortedList
  {
    private List<string> _list = new List<string>();
    private SortThings _sortmechanism;
 
    public void SetSortMechanism(SortThings sortmechanism)
    {
	  this._sortmechanism = sortmechanism;
    }
 
    public void Add(string name)
    {
      _list.Add(name);
    }
 
    public void Sort()
    {
      _sortmechanism.Sort(_list);
 
      // Iterate over list and display results
      foreach (string name in _list)
      {
        Console.WriteLine(" " + name);
      }
      Console.WriteLine();
    }
  }
}

