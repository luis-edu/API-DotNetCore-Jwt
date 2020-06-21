using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Model;
using API.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private iRepositorioCliente _db;
        public LoginController(IConfiguration config,iRepositorioCliente db)
        {
            _config = config;
            _db = db;
        }
        [HttpPost("Cadastro")]
        public IActionResult Cadastro(ModeloCliente cliente)
        {
            if (cliente != null)
            {
                _db.CadastrarCliente(cliente);
                return Ok(new { mensagem = "Cadastrado com sucesso!" });
            }
            return BadRequest(new { mensage = "Erro" });
        }
        [HttpGet]
        public IActionResult Login(string user, string pass)
        {
            ModeloCliente login = new ModeloCliente();
            login.Usuario = user;
            login.Senha = pass;
            IActionResult response = Unauthorized();

            ModeloCliente usuario = AutenticarUsuario(login);

            if(usuario != null)
            {
                var tokenStr = GerarJWT(usuario);
                response = Ok(new { token = tokenStr });
            }

            return response;
        }

        private string GerarJWT(ModeloCliente usuario)
        {
            var secutiryKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(secutiryKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim (JwtRegisteredClaimNames.Sub, usuario.Usuario),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                    issuer:_config["jwt:Issuer"],
                    audience:_config["Jwt:Issuer"],
                    claims,
                    expires:DateTime.Now.AddMinutes(120),
                    signingCredentials:credentials
                );

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        private ModeloCliente AutenticarUsuario(ModeloCliente login)
        {
            ModeloCliente usuario = null;
            if(_db.Login(login.Usuario, login.Senha) != null)
            {
                usuario = _db.Login(login.Usuario, login.Senha);
            }
            return usuario;
        }

        [Authorize]
        [HttpPost("Post")]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            return "Welcome to:" + userName;
        }
        [Authorize]
        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Value", "Value2", "Value3" };
        }
    }
}
