using AutoMapper;
using Carpark.Business.Repositories.IRepositories;
using Carpark.Data.Entities;
using Carpark.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Carpark.Business.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public CarRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task CreateAsync(CarDTO objDTO)
        {
            var obj = mapper.Map<CarDTO, Car>(objDTO);
            db.Car.Add(obj);
            await db.SaveChangesAsync();
        }


        public async Task<Car> GetAsync(string carName)
        {
            return await db.Car.FirstOrDefaultAsync(u => u.Manufacturer.ToLower() == carName.ToLower());
        }

        public async Task<IEnumerable<CarDTO>> GetAllAsync()
        {
            var obj = await db.Car.ToListAsync();
            return mapper.Map<IEnumerable<Car>, IEnumerable<CarDTO>>(obj);
        }

        public async Task<CarDTO> GetAsync(int id)
        {
            var obj = await db.Car.FirstOrDefaultAsync(u => u.Id == id);
            return mapper.Map<Car, CarDTO>(obj);
        }

        public async Task<int> RemoveAsync(int id)
        {
            var obj = await db.Car.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                db.Car.Remove(obj);
                return await db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task UpdateAsync(CarDTO objDTO)
        {
            var objFromDb = await db.Car.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Manufacturer = objDTO.Manufacturer;
                objFromDb.Model = objDTO.Model;
                objFromDb.TicketBought = objDTO.TicketBought;

                db.Car.Update(objFromDb);
                await db.SaveChangesAsync();
            }
        }
    }
}
