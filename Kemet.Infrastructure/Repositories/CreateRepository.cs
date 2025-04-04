﻿using IRepository.Generic;
using Kemet.Infrastructure;
using Microsoft.EntityFrameworkCore;



namespace Repositorties.Generic;

/// <summary>
/// Repository for creating an entity in the database
/// </summary>
/// <typeparam name="TEntity">Genreic type of the entity</typeparam>
public class CreateRepository<TEntity> : RepositoryDbContext,
    ICreateAsync<TEntity> where TEntity : class
{
    /// <summary>
    /// DbSet representing the entity
    /// </summary>
    protected DbSet<TEntity> _entity;

    /// <summary>
    ///  intialize a new instance of the CreateRepository class
    /// </summary>
    /// <param name="context">DbContext of the entity</param>
    /// public CreateRepository(KemetDbContext context) : base(context)
    public CreateRepository(KemetDbContext context) : base(context)
    {
        _entity = context.Set<TEntity>();

    }


    /// <summary>
    /// Create an entity in the database.
    /// </summary>
    /// <param name="entity">the entity type to be creat.</param>
    /// <returns>the entity after created it.</returns>
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _entity.AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }
}