using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNetAPI.Models.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
