using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using swebapi.Data;
using swebapi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace swebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public LoginController(IdentityContext context, UserManager<CustomIdentityUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        // GET: api/<CuentasController>
        [HttpGet]
        public IActionResult Get()
        {
            // Regresa 401 Acceso no autorizado
            return Unauthorized("Acceso no autorizado.");
        }

        // POST api/<CuentasController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LoginViewModel login)
        {
            // Verificamos credenciales con Identity
            var usuario = await _userManager.FindByEmailAsync(login.Correo);

            if (usuario is null || !await _userManager.CheckPasswordAsync(usuario, login.Password))
            {
                // Regresa 401 Acceso no autorizado
                return Unauthorized("Acceso no autorizado.");
            }

            // Generamos un token según los claims
            // Estos valores nos indicarán el usuario autenticado en cada petición usando el token
            // Con su Id o Email podemos buscar datos
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, usuario.Id),
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.GivenName, usuario.Nombre)
            };

            // Obtenemos los roles y los agregamos a los claims
            var roles = await _userManager.GetRolesAsync(usuario);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Creamos el token de acceso de 20 minutos
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            // Le regresa su token de acceso al usuario con validez de 20 minutos
            return Ok(new
            {
                usuario.Id,
                usuario.Email,
                usuario.Nombre,
                rol = string.Join(",", roles),
                AccessToken = jwt
            });
        }
    }
}
