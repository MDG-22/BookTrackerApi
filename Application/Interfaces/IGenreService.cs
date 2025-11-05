using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class GenreRepository :  Repository<Genre>, IGenreRepository
    {
       
         public GenreRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
