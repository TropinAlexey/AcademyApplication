using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data.Repositories;
using WebApplication.Models;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class LectureService : ILectureService
    {
        private readonly ILecturesRepository _lecturesRepository;
        private string _rate;

        public LectureService(ICurrencyExchangeService currencyExchangeService, 
            ILecturesRepository lecturesRepository)
        {
            _lecturesRepository = lecturesRepository;
            _rate = currencyExchangeService.GetCurrencyExchangeRateAsync("USD", "EUR", 1).GetAwaiter().GetResult();
        }

        public async Task<IList<Lecture>> GetManyLecturesAsync()
        {
            var result = await _lecturesRepository.GetMany().ToListAsync();
            return result;
        }

        public async Task<Lecture> GetLectureIdAsync(int id)
        {
            var result = await _lecturesRepository.GetNotTrackedAsync(d => d.Id == id);
            return result;
        }

        public async Task<Lecture> CreateLectureAsync(Lecture entity)
        {
            entity.Id = 0;
            var result = await _lecturesRepository.AddAsync(entity);
            return result;
        }

        public async Task<Lecture> UpdateLectureAsync(Lecture entity)
        {
            var result = await _lecturesRepository.UpdateAsync(entity);
            return result;
        }

        public async Task<Lecture> DeleteLectureAsync(int id)
        {
            var result = await _lecturesRepository.DeleteAsync(id);
            return result;
        }

        public async Task<ICollection<Lecture>> SearchAsync(string search)
        {
            var result = await _lecturesRepository.GetMany().Include(s => s.Subject)
                .Where(d => d.LectureRoom.Contains(search) || d.Subject.Name.Contains(search)).ToListAsync();
            return result;
        }
    }
}