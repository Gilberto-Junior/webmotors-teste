using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebMotors.Teste.Domains.Generics
{
    /// <summary>
    /// Define os métodos padrões para operações de CRUD
    /// </summary>
    /// <typeparam name="T">Entidade a ser mapeada</typeparam>
    public interface IDomain<T> where T : class
    {
        /// <summary>
        /// Busca todas as entidades disponíveis na base de dados
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAsync();
        /// <summary>
        /// Busca uma entidade especifica através de seu identificador único
        /// </summary>
        /// <param name="id">Identificador da entidade</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);
        /// <summary>
        /// Salva uma nova entidade na base de dados
        /// </summary>
        /// <param name="entity">Entidade a ser salva</param>
        /// <returns></returns>
        Task<T> SaveAsync(T entity);
        /// <summary>
        /// Atualiza uma entidade específica
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada</param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);
        /// <summary>
        /// Remove uma entidade específica através de seu identificador único
        /// </summary>
        /// <param name="entityId">Identificador da entidade</param>
        /// <returns></returns>
        Task DeleteAsync(int entityId);
    }
}
