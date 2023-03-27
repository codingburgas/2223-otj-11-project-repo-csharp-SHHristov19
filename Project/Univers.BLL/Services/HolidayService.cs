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
        private readonly HolidayRepository _holidayRepository;

        public HolidayService()
        {
            _holidayRepository = new HolidayRepository();
        }

        /// <summary>
        /// Transfer data from Holiday entity to Holiday model
        /// </summary>
        /// <returns></returns>
        public List<Holiday> TransferDataFromEntityToModel()
        {
            List<Holiday> models = new();

            var entities = _holidayRepository.ReadAllData();

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
