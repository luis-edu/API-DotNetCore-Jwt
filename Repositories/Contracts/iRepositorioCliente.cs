using System.Collections.Generic;
using API.Model;

namespace API.Repositories.Contracts
{
    public interface iRepositorioCliente
    {
        List<ModeloCliente> ObterTodos();
        ModeloCliente ObterCliente(int id);
        void CadastrarCliente (ModeloCliente cliente);
        void Atualizar (ModeloCliente cliente);
        void Excluir (int id);
        ModeloCliente Login(string usuario, string senha);
    }
}