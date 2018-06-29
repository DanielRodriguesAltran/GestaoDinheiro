using Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDinheiro.Interfaces
{
    interface IMovimentoRepository
    {
        void Insert(Movimento m);
        Movimento FindById(int id);
        IEnumerable<Movimento> GetAll();
        void Remove(int id);
        void Commit();
    }
}
