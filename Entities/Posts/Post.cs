using Entities.Bases;
using Entities.Categories;
using Entities.Users;
using System;

namespace Entities.Posts
{
    public class Post : BaseEntity<Guid>
    {

        #region Properties

        public string Title { get; set; }

        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }


        #endregion

        #region Relationst


        public Category Category { get; set; }

        public User Author { get; set; }


        #endregion

    }



}