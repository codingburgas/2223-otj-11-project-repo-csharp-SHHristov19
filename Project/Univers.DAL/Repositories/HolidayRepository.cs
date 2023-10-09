﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class HolidayRepository
    {
        /// <summary>
        /// Read the data from the Holidays table
        /// </summary>
        /// <returns></returns>
        public List<Holiday> ReadAllData()
        {
            using Context.Context context = new();

            return context.Holidays.ToList();
        }
    }
}
