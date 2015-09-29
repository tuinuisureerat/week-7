using System;
using System.Collections.Generic;
 
namespace Lab7
{

  class MainApp
  {
    static void Main()
    {
      // Create user and let her compute
      User user = new User();
 
      // User presses calculator buttons
      user.Compute('+', 100);
      user.Compute('-', 50);
      user.Compute('*', 10);
      user.Compute('/', 2);
 
      user.Undo(4);
 
      user.Redo(3);
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  abstract class Operation
  {
    public abstract void Execute();
    public abstract void UnExecute();
  }
 
  class CalculatorOperation : Operation
  {
    private char _operator;
    private int _operand;
    private Calculator _calculator;
 
    // Constructor
    public CalculatorOperation(Calculator calculator,
      char @operator, int operand)
    {
      this._calculator = calculator;
      this._operator = @operator;
      this._operand = operand;
    }
 
    // Gets operator
    public char Operator
    {
      set { _operator = value; }
    }
 
    // Get operand
    public int Operand
    {
      set { _operand = value; }
    }
 
    // Execute new
    public override void Execute()
    {
      _calculator.Operation(_operator, _operand);
    }
 
    // Unexecute last
    public override void UnExecute()
    {
      _calculator.Operation(Undo(_operator), _operand);
    }
 
    // Returns opposite operator for given operator
    private char Undo(char @operator)
    {
      switch (@operator)
      {
        case '+': return '-';
        case '-': return '+';
        case '*': return '/';
        case '/': return '*';
        default: throw new
         ArgumentException("@operator");
      }
    }
  }
 
  class Calculator
  {
    private int _curr = 0;
 
    public void Operation(char @operator, int operand)
    {
      switch (@operator)
      {
        case '+': _curr += operand; break;
        case '-': _curr -= operand; break;
        case '*': _curr *= operand; break;
        case '/': _curr /= operand; break;
      }
      Console.WriteLine(
        "Current value = {0,3} (following {1} {2})",
        _curr, @operator, operand);
    }
  }
 
  class User
  {
    // Initializers
    private Calculator _calculator = new Calculator();
    private List<Operation> _operations = new List<Operation>();
    private int _current = 0;
 
    public void Redo(int levels)
    {
      Console.WriteLine("\n---- Redo {0} levels ", levels);
      // Perform redo operations
      for (int i = 0; i < levels; i++)
      {
        if (_current < _operations.Count - 1)
        {
          Operation op = _operations[_current++];
          op.Execute();
        }
      }
    }
 
    public void Undo(int levels)
    {
      Console.WriteLine("\n---- Undo {0} levels ", levels);
      // Perform undo operations
      for (int i = 0; i < levels; i++)
      {
        if (_current > 0)
        {
          Operation op = _operations[--_current] as Operation;
          op.UnExecute();
        }
      }
    }
 
    public void Compute(char @operator, int operand)
    {
      // Create operation and execute it
      Operation op = new CalculatorOperation(
        _calculator, @operator, operand);
      op.Execute();
 
      // Add command to undo list
      _operations.Add(op);
      _current++;
    }
  }
}
