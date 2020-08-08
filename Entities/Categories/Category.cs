using Entities.Bases;
using Entities.Posts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Categories
{
    public class Category : BaseEntity
    {

        #region Properties

        [Required(ErrorMessage = "*")]
        [MaxLength(200)]
        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }

        #endregion


        #region Relations

        public Category ParentCategory { get; set; }

        public List<Category> ChildCategories { get; set; }
        public List<Post> Posts { get; set; }

        #endregion
    }

}