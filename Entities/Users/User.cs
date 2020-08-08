using Entities.Bases;
using Entities.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Users
{
    public class User : BaseEntity
    {
        public User()
        {
            IsActive = true;
        }

        #region Properties


        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset LastLoginDate { get; set; }


        #endregion


        #region Relations

        public List<Post> Posts { get; set; }

        #endregion


    }




    public enum GenderType
    {
        [Display(Name = "مرد")]
        Male = 1,
        [Display(Name = "زن")]
        Female = 2
    }
}