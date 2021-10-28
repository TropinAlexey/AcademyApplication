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
        private readonly ISubjectsRepository _subjectsRepository;
        private readonly ITeachersRepository _teachersRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IGroupLectureRepository _groupLectureRepository;
        private string _rate;

        public LectureService(ICurrencyExchangeService currencyExchangeService, 
            ILecturesRepository lecturesRepository, ISubjectsRepository subjectsRepository, ITeachersRepository teachersRepository, IGroupsRepository groupsRepository, IGroupLectureRepository groupLecture)
        {
            _lecturesRepository = lecturesRepository;
            _subjectsRepository = subjectsRepository;
            _teachersRepository = teachersRepository;
            _groupsRepository = groupsRepository;
            _groupLectureRepository = groupLecture;
            _rate = currencyExchangeService.GetCurrencyExchangeRateAsync("USD", "EUR", 1).GetAwaiter().GetResult();
        }

        public async Task<IList<Lecture>> GetManyLecturesAsync()
        {
            var lectures = await _lecturesRepository.GetMany().ToListAsync();
            var teachers = await _teachersRepository.GetMany().ToListAsync();
            var subjects = await _subjectsRepository.GetMany().ToListAsync();

            foreach (var lecture in lectures)
            {
                lecture.Teacher = teachers.FirstOrDefault(t => t.Id == lecture.TeacherId);
                lecture.Subject = subjects.FirstOrDefault(t => t.Id == lecture.SubjectId);
            }
            return lectures;
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

            var groupLecture = new GroupLecture
            {
                Id = 0,
                DayOfWeek = entity.DayOfWeek,
                GroupId = entity.GroupId,
                LectureId = result.Id
            };
            await _groupLectureRepository.AddAsync(groupLecture);
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
            var result = await _lecturesRepository.GetMany().Where(d => d.LectureRoom.Contains(search)).ToListAsync();
            return result;
        }

        public async Task<IList<Subject>> GetSubjectsAsync()
        {
            var result = await _subjectsRepository.GetMany().ToListAsync();
            return result;
        }

        public async Task<IList<Teacher>> GetTeachersAsync()
        {
            var result = await _teachersRepository.GetMany().ToListAsync();
            return result;
        }

        public async Task<IList<Group>> GetGroupsAsync()
        {
            var result = await _groupsRepository.GetMany().ToListAsync();
            return result;
        }
    }
}