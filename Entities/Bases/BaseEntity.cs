using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Bases
{

    public interface IEntity
    {

    }

    public abstract class BaseEntity<T> : IEntity
    {
        [Key]
        public T Id { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {

    }
}