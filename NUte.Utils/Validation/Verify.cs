using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NUte.Utils.Validation
{
  public static class Verify
  {
    public static void NotNull(Expression<Func<object>> argument, string message)
    {
      NotNull<InvalidOperationException>(argument, new { message });
    }

    public static void NotNull<TException>(Expression<Func<object>> argument, string message)
        where TException : Exception, new()
    {
      NotNull<TException>(argument, new { message });
    }

    public static void NotNull<TException>(Expression<Func<object>> argument, object parameters)
        where TException : Exception, new()
    {
      Validator.NotNull(argument, (m, p, n) => ThrowException<TException>(m, parameters));
    }

    public static void NotDefault<TType>(Expression<Func<TType>> argument, string message)
    {
      NotDefault<TType, InvalidOperationException>(argument, new { message });
    }

    public static void NotDefault<TType, TException>(Expression<Func<TType>> argument, string message)
        where TException : Exception, new()
    {
      NotDefault<TType, TException>(argument, new { message });
    }

    public static void NotDefault<TType, TException>(Expression<Func<TType>> argument, object parameters)
        where TException : Exception, new()
    {
      Validator.NotDefault(argument, (m, p, n) => ThrowException<TException>(m, parameters));
    }

    public static void NotNullOrEmpty(Expression<Func<string>> argument, string message)
    {
      NotNullOrEmpty<InvalidOperationException>(argument, new { message });
    }

    public static void NotNullOrEmpty<TException>(Expression<Func<string>> argument, string message)
        where TException : Exception, new()
    {
      NotNullOrEmpty<TException>(argument, new { message });
    }

    public static void NotNullOrEmpty<TException>(Expression<Func<string>> argument, object parameters)
        where TException : Exception, new()
    {
      Validator.NotNullOrEmpty(argument, (m, p, n) => ThrowException<TException>(m, parameters));
    }

    public static void NotNullOrWhiteSpace(Expression<Func<string>> argument, string message)
    {
      NotNullOrWhiteSpace<InvalidOperationException>(argument, new { message });
    }

    public static void NotNullOrWhiteSpace<TException>(Expression<Func<string>> argument, string message)
        where TException : Exception, new()
    {
      NotNullOrWhiteSpace<TException>(argument, new { message });
    }

    public static void NotNullOrWhiteSpace<TException>(Expression<Func<string>> argument, object parameters)
        where TException : Exception, new()
    {
      Validator.NotNullOrWhiteSpace(argument, (m, p, n) => ThrowException<TException>(m, parameters));
    }

    public static void NotNullOrEmpty(Expression<Func<IEnumerable>> argument, string message)
    {
      NotNullOrEmpty<InvalidOperationException>(argument, new { message });
    }

    public static void NotNullOrEmpty<TException>(Expression<Func<IEnumerable>> argument, string message)
        where TException : Exception, new()
    {
      NotNullOrEmpty<TException>(argument, new { message });
    }

    public static void NotNullOrEmpty<TException>(Expression<Func<IEnumerable>> argument, object parameters)
        where TException : Exception, new()
    {
      Validator.NotNullOrEmpty(argument, (m, p, n) => ThrowException<TException>(m, parameters));
    }

    public static void NotNullOrNullElements(Expression<Func<IEnumerable>> argument, string message)
    {
      NotNullOrNullElements<InvalidOperationException>(argument, new { message });
    }

    public static void NotNullOrNullElements<TException>(Expression<Func<IEnumerable>> argument, string message)
        where TException : Exception, new()
    {
      NotNullOrNullElements<TException>(argument, new { message });
    }

    public static void NotNullOrNullElements<TException>(Expression<Func<IEnumerable>> argument, object parameters)
        where TException : Exception, new()
    {
      Validator.NotNullOrNullElements(argument, (m, p, n) => ThrowException<TException>(m, parameters));
    }

    public static void NotNullEmptyOrNullElements(Expression<Func<IEnumerable>> argument, string message)
    {
      NotNullEmptyOrNullElements<InvalidOperationException>(argument, new { message });
    }

    public static void NotNullEmptyOrNullElements<TException>(Expression<Func<IEnumerable>> argument, string message)
        where TException : Exception, new()
    {
      NotNullEmptyOrNullElements<TException>(argument, new { message });
    }

    public static void NotNullEmptyOrNullElements<TException>(Expression<Func<IEnumerable>> argument, object parameters)
        where TException : Exception, new()
    {
      Validator.NotNullEmptyOrNullElements(argument, (m, p, n) => ThrowException<TException>(m, parameters));
    }

    public static void NotNullEmptyOrNullWhiteSpaceElements(Expression<Func<IEnumerable<string>>> argument, string message)
    {
      NotNullEmptyOrNullWhiteSpaceElements<InvalidOperationException>(argument, new { message });
    }

    public static void NotNullEmptyOrNullWhiteSpaceElements<TException>(Expression<Func<IEnumerable<string>>> argument, string message)
        where TException : Exception, new()
    {
      NotNullEmptyOrNullWhiteSpaceElements<TException>(argument, new { message });
    }

    public static void NotNullEmptyOrNullWhiteSpaceElements<TException>(Expression<Func<IEnumerable<string>>> argument, object parameters)
        where TException : Exception, new()
    {
      Validator.NotNullEmptyOrNullWhiteSpaceElements(argument, (m, a, n) => ThrowException<TException>(m, parameters));
    }

    public static void IsTrue(Func<bool> condition, string message)
    {
      IsTrue<InvalidOperationException>(condition, new { message });
    }

    public static void IsTrue<TException>(Func<bool> condition, string message)
        where TException : Exception, new()
    {
      IsTrue<TException>(condition, new { message });
    }

    public static void IsTrue<TException>(Func<bool> condition, object parameters)
        where TException : Exception, new()
    {
      if (condition == null)
      {
        return;
      }

      var valid = condition.Invoke();

      if (!valid)
      {
        ThrowException<TException>(null, parameters);
      }
    }

    public static void IsFalse(Func<bool> condition, string message)
    {
      IsFalse<InvalidOperationException>(condition, new { message });
    }

    public static void IsFalse<TException>(Func<bool> condition, string message)
        where TException : Exception, new()
    {
      IsFalse<TException>(condition, new { message });
    }

    public static void IsFalse<TException>(Func<bool> condition, object parameters)
        where TException : Exception, new()
    {
      if (condition == null)
      {
        return;
      }

      var valid = condition.Invoke();

      if (valid)
      {
        ThrowException<TException>(null, parameters);
      }
    }

    private static void ThrowException<TException>(string message, object parameters)
        where TException : Exception, new()
    {
      var exceptionType = typeof(TException);
      var parametersDictionary = parameters.ToDictionary();

      if (!parametersDictionary.ContainsKey("message") && !string.IsNullOrWhiteSpace(message))
      {
        parametersDictionary.Add("message", message);
      }

      foreach (var constructor in exceptionType.GetConstructors())
      {
        var constructorValues = (from parameter in constructor.GetParameters()
                                 from item in parametersDictionary
                                 where parameter.Name.IsEqual(item.Key)
                                 select item.Value).ToArray();

        if (constructorValues.Length == parametersDictionary.Count)
        {
          throw (TException)constructor.Invoke(constructorValues);
        }
      }

      throw new TException();
    }
  }
}