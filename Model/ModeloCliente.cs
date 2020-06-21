namespace API.Model
{
    public class ModeloCliente
    {
        public int Id {get;set;}
        public string Nome {get;set;}
        public string Usuario {get;set;}
        public string Senha {get;set;}
        public string Email {get;set;}
        public bool Ativo {get;set;}
        public bool Confirmado{get;set;}
    }
}