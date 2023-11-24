using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Application.Service;
using Domain.AggregationModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace O2GEN.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;
        public JwtMiddleware(RequestDelegate next, IJwtService jwtService)
        {
            _next = next;
            _jwtService = jwtService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var user = _jwtService.ValidateJwtToken(token);
            if (user != null)
            {
                context.Items["User"] = user;
            }

            await _next(context);
        }
    }
}
