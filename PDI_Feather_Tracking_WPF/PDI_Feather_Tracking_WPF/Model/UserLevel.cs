﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDI_Feather_Tracking_WPF.DataModel;

namespace PDI_Feather_Tracking_WPF.Models
{
    public class UserLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }

        [Required]
        public string Name { get; set; }

        [DefaultValue(true)]
        public bool Status{ get; set; }
        
        public ModuleAccess? ModuleAccess{ get; set; }
        
        [Required]
        public int CreatedBy{ get; set; }

        [Required]
        public int UpdatedBy{ get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public ICollection<User> Users { get; set; }    
    }
}
