using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services.Interfaces
{
    public interface ILectureService
    {
        Task<IList<Lecture>> GetManyLecturesAsync();
        Task<Lecture> GetLectureIdAsync(int id);
        Task<Lecture> CreateLectureAsync(Lecture entity);
        Task<Lecture> UpdateLectureAsync(Lecture entity);
        Task<Lecture> DeleteLectureAsync(int id);
        Task<ICollection<Lecture>> SearchAsync(string search);
    }
}