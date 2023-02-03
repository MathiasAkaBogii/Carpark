using Carpark.Data.Entities;
using Carpark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Carpark.Business.Repositories.IRepositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<CarDTO>> GetAllAsync();
        Task<CarDTO> GetAsync(int id);
        Task<Car> GetAsync(string carName);
        Task CreateAsync(CarDTO car);
        Task UpdateAsync(CarDTO car);
        Task<int> RemoveAsync(int id);
    }
}
