using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface Validate<T>
{
    Task Validate(T entity);
}

public interface UpdateValidation<T, TResult>
{
    Task<TResult> Validate(T entity);
}

public interface ICreateColorValidation : Validate<ColorCreateDTO>
{
}

public interface IUpdateColorValidation : UpdateValidation<ColorUpdateDTO, ColorReadDTO>
{
}
public interface IDeleteColorValidation : Validate<ColorDeleteDTO>
{
}

public interface ICreateSizeValidation : Validate<SizeCreateDTO>
{
}

public interface IUpdateSizeValidation : UpdateValidation<SizeUpdateDTO, SizeReadDTO>
{
}
public interface IDeleteSizeValidation : Validate<SizeDeleteDTO>
{
}
