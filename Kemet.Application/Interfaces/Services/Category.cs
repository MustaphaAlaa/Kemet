using Entities.Models;
using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.ICategoryServices;
public interface IUpdateCategory : IUpdateServiceAsync<CategoryUpdateDTO, CategoryReadDTO>
{
}

public interface IRetrieveCategory : IRetrieveServiceAsync<Category?, CategoryReadDTO?>
{

}

public interface IRetrieveAllCategory : IRetrieveAllServiceAsync<Category, CategoryReadDTO>
{
}


public interface IDeleteCategory : IDeleteServiceAsync<CategoryDeleteDTO>
{

}


public interface ICreateCategory : IServiceAsync<CategoryCreateDTO, CategoryReadDTO>
{

}



