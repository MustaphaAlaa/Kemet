using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;

namespace Entities.Models.Utilities;

public static class Utility
{
    public static void IsNull<T>(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException($" {typeof(T)} is Null");
    }

    public static void IsNullOrEmpty(string str, string msg)
    {
        if (string.IsNullOrEmpty(str))
            throw new ArgumentException($"{msg} cannot by null or empty.");
    }

    public static void IdBoundry(int Id)
    {
        if (Id <= 0)
            throw new InvalidOperationException("Id Is out of boundry");
    }

    public static void DoesExist<T>(T entity, string Model = "Model")
    {
        if (entity is null)
            throw new DoesNotExistException($"{Model} doesn't exist.");
    }

    public static void AlreadyExist<T>(T entity, string Model = "Model")
    {
        if (entity is not null)
            throw new AlreadyExistException($"{Model} is already exist.");
    }
}
