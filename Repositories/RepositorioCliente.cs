using System.Collections.Generic;
using System.Linq;
using API.Database;
using API.Model;
using API.Repositories.Contracts;

namespace API.Repositories
{
    public class RepositorioCliente : iRepositorioCliente
    {
        private ConexaoDB _db;
        public RepositorioCliente(ConexaoDB db)
        {
            _db = db;
        }
        public void Atualizar(ModeloCliente cliente)
        {
            throw new System.NotImplementedException();
        }

        public void CadastrarCliente(ModeloCliente cliente)
        {
            _db.Clientes.Add(cliente);
            _db.SaveChanges();
        }

        public void Excluir(int id)
        {
            _db.Clientes.Remove(_db.Clientes.Find(id));
            _db.SaveChanges();
        }

        public ModeloCliente Login(string usuario, string senha)
        {
            if(usuario != null && senha != null)
            {
                return _db.Clientes.First(x => x.Usuario == usuario && x.Senha == senha);
            }return null;
            
        }

        public ModeloCliente ObterCliente(int id)
        {
            return _db.Clientes.Find(id);
        }

        public List<ModeloCliente> ObterTodos()
        {
            return _db.Clientes.ToList();
        }
    }
}