using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TBADev.MVC.Entity;

//found at : http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
namespace GlacialArchiving.DataAccess.Base
{
    public partial class GenericRepository<TEntity> where TEntity : class, ITBAEntity
    {
    }
}