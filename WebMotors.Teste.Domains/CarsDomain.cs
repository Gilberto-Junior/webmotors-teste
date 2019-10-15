using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebMotors.Teste.Domains.Generics;
using WebMotors.Teste.Entities;
using WebMotors.Teste.Entities.Exceptions;
using WebMotors.Teste.Infrastructure;

namespace WebMotors.Teste.Domains
{
    public class CarsDomain : IDomain<Car>
    {
        private Context _context;
        private readonly IConfiguration _configuration;

        public CarsDomain(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Remove um carro específico da base de dados
        /// </summary>
        /// <param name="entityId">Identificador do carro</param>
        /// <exception cref="CarException">Retorna uma mensagem amigável para o usuário. O erro original pode ser acessado via "InnerException"</exception> 
        /// <returns></returns>
        public async Task DeleteAsync(int entityId)
        {
            try
            {
                var car = await GetByIdAsync(entityId);

                using (_context = new Context(_configuration))
                {
                    var entry = _context.Cars.Remove(car);
                    var rowsAffected = await _context.SaveChangesAsync();

                    if (rowsAffected > 0)
                    {
                        car = entry.Entity;
                    }
                }

                if (car == null)
                {
                    throw new CarException("O carro não foi removido na base.");
                }
            }
            catch (CarException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new CarException("Não foi possível remover o carro.", e);
            }
        }

        /// <summary>
        /// Busca todos os carros disponíveis na base de dados
        /// </summary>
        /// <exception cref="CarException">Retorna uma mensagem amigável para o usuário. O erro original pode ser acessado via "InnerException"</exception> 
        /// <returns></returns>
        public async Task<IEnumerable<Car>> GetAsync()
        {
            try
            {
                var cars = default(IEnumerable<Car>);
                using (_context = new Context(_configuration))
                {
                    cars = await _context.Cars.ToListAsync();
                }

                if (cars == null)
                {
                    throw new CarException("Carro não encontrado.");
                }

                return cars;
            }
            catch (CarException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new CarException("Não foi possível buscar os carros.", e);
            }
        }

        /// <summary>
        /// Busca um carro específico
        /// </summary>
        /// <param name="id">Indentificador único</param>
        /// <exception cref="CarException">Retorna uma mensagem amigável para o usuário. O erro original pode ser acessado via "InnerException"</exception> 
        /// <returns></returns>
        public async Task<Car> GetByIdAsync(int id)
        {
            try
            {
                var car = default(Car);
                using (_context = new Context(_configuration))
                {
                    car = await _context.Cars.FirstOrDefaultAsync(c => c.ID.Equals(id));
                }

                if (car == null)
                {
                    throw new CarException("Carro não encontrado.");
                }

                return car;
            }
            catch (CarException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new CarException("Não foi possível buscar o carro.", e);
            }
        }

        /// <summary>
        /// Salva um carro na base de dados
        /// </summary>
        /// <param name="entity">Carro a ser salvo</param>
        /// <exception cref="CarException">Retorna uma mensagem amigável para o usuário. O erro original pode ser acessado via "InnerException"</exception> 
        /// <returns></returns>
        public async Task<Car> SaveAsync(Car entity)
        {
            try
            {
                var car = default(Car);
                using (_context = new Context(_configuration))
                {
                    var entry = await _context.Cars.AddAsync(entity);
                    var rowsAffected = await _context.SaveChangesAsync();

                    if (rowsAffected > 0)
                    {
                        car = entry.Entity;
                    }
                }

                if (car == null)
                {
                    throw new CarException("O carro não foi salvo na base.");
                }

                return car;
            }
            catch (CarException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new CarException("Não foi possível salvar o carro.", e);
            }
        }

        /// <summary>
        /// Atualiza um carro na base de dados
        /// </summary>
        /// <param name="entity">Carro a ser atualizado</param>
        /// <exception cref="CarException">Retorna uma mensagem amigável para o usuário. O erro original pode ser acessado via "InnerException"</exception> 
        /// <returns></returns>
        public async Task<Car> UpdateAsync(Car entity)
        {
            try
            {
                var car = default(Car);
                using (_context = new Context(_configuration))
                {
                    var entry = _context.Cars.Update(entity);
                    var rowsAffected = await _context.SaveChangesAsync();

                    if (rowsAffected > 0)
                    {
                        car = entry.Entity;
                    }
                }

                if (car == null)
                {
                    throw new CarException("O carro não foi atualizado na base.");
                }

                return car;
            }
            catch (CarException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new CarException("Não foi possível atualizar o carro.", e);
            }
        }
    }
}
