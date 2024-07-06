using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Domain.Shared
{
    public interface IEntity<out TId>
    {
        TId Id { get; }
    }
    public interface IEntityLong
    {
        [Key]
        long Id { get; }
    }
}
