﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BaseEntity
    {
        public int id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
