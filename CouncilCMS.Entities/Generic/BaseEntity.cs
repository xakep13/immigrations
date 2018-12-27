using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bissoft.CouncilCMS.Core;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public abstract class BaseEntity<T> : IEntity<T>
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        public bool Deleted { get; set; }
    }
}
