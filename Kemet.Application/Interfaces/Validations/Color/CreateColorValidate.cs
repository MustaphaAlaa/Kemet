using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;



public interface IValidate<T>
{
    Task Validate(T entity);
}


public interface IUpdateValidation<in T, TResult>
{
    Task<TResult> Validate(T entity);
}



public interface ICreateColorValidation : IValidate<ColorCreateDTO>
{
}

public interface IUpdateColorValidation : IUpdateValidation<ColorUpdateDTO, ColorReadDTO>
{
}
public interface IDeleteColorValidation : IValidate<ColorDeleteDTO>
{
}

public interface ICreateSizeValidation : IValidate<SizeCreateDTO>
{
}

public interface IUpdateSizeValidation : IUpdateValidation<SizeUpdateDTO, SizeReadDTO>
{
}
public interface IDeleteSizeValidation : IValidate<SizeDeleteDTO>
{
}
