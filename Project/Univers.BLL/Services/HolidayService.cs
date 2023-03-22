using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Repositories;
using Univers.Models.Models;

namespace Univers.BLL.Services
{
    public class HolidayService
    {
        public HolidayRepository HolidayRepository { get; set; }

        public HolidayService()
        {
            HolidayRepository = new HolidayRepository();
        }
        // Transfer data from Holiday entity to Holiday model
        public static List<Holiday> TransferDataFromEntityToModel()
        {
            List<Holiday> models = new();

            var entities = HolidayRepository.ReadAllData();

            foreach (var entity in entities)
            {
                var newModel = new Holiday();

                newModel.Name = entity.Name;
                newModel.DateOfStart = entity.DateOfStart;
                newModel.DateOfEnd = entity.DateOfEnd;

                models.Add(newModel);
            }

            return models;
        }
    }
}
