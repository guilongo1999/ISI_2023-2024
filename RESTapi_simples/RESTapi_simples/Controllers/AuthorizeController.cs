using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Configuration;





namespace RESTapi_simples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {

        public static UserInfo user = new UserInfo();
        private readonly IConfiguration _configuration;

        public AuthorizeController(IConfiguration configuration) 
        {
        
            _configuration = configuration;
        
        }

        [HttpPost("registo")]   // Cria o hash da senha e outros processos relacionados ao registro
                                // Retorna um objeto User registrado em caso de sucesso

        public async Task<ActionResult<UserInfo>> Register(UtilizadorAuth request)
        {

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt); 
                

                    user.Username = request.Username;
                    user.passwordHash = passwordHash;
                    user.passwordSalt = passwordSalt;

                return Ok(user);
                    
                

        }


        /// <summary>
        /// // Get, Retorna informações do usuário registrado se existir, caso contrário, retorna NotFound
        /// </summary>
        /// <returns></returns>

        [HttpGet("registo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public ActionResult<UserInfo> GetRegistration()
        {
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Nenhuma informação de registro encontrada");
            }
        }




        // Verifica se o usuário existe e se a senha está correta
        // Se as verificações passarem, gera e retorna um token JWT

        [HttpPost("Login")]

        public async Task<ActionResult<string>> Login(UtilizadorAuth request)

        {

            if (user.Username != request.Username) 
            {

                return BadRequest("Nao encontrado esse utilizador");
            
            }

            if (!VerifyPasswordHash(request.Password, user.passwordHash, user.passwordSalt)) 
            {
            
            return BadRequest("essa nao e a password correta");
                
            }


            string token = CreateToken(user);
            return Ok(token);
            //return Ok("Token necessario para a autenticacao");
        }

        private string CreateToken(UserInfo user)
        {

            List<Claim> claims = new List<Claim>
            {

                new Claim(ClaimTypes.Name, user.Username)

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }








        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using (var hmac = new HMACSHA512()) //codigo mensagem autenticacao em hash
            {

                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            
            
            }
        
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) 
        {

            using (var hmac = new HMACSHA512(passwordSalt)) //verifica hash se forem iguais e o username
            {

                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return ComputedHash.SequenceEqual(passwordHash);

            }
        
        
        }


        
        

    }
}
