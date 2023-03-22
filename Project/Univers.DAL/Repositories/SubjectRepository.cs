﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class SubjectRepository
    {
        // Read the data from the Subject table
        public List<Subject> ReadAllData()
        {
            using Context.Context context = new();

            return context.Subjects.ToList();
        }
    }
}
