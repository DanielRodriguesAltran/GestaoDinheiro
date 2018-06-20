using GestaoDinheiro.Interfaces;
using GestaoDinheiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoDinheiro.Repository
{
    public class MovimentosRepository : IMovimentoRepository
    {

        ApplicationDbContext _context;

        public MovimentosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            
                _context.SaveChanges();
            
        }

        public Movimento FindById(int id)
        {
            return _context.Movimentos.Find(id);
        }

        public IEnumerable<Movimento> GetAll()
        {
            return _context.Movimentos.AsNoTracking().ToList();
        }
       

        public void Insert(Movimento m)
        {
            _context.Movimentos.Add(m);
        }

        public void Remove(int id)
        {
            Movimento m = _context.Movimentos.Find(id);
            if (m!=null)
            {
                _context.Movimentos.Remove(m);
            }
        }

        public void RemoveAll()
        {
            foreach (var item in _context.Movimentos)
            {
                _context.Movimentos.Remove(item);
            }
            _context.SaveChanges();
        }
    }
}