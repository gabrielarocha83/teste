using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.ViewModel
{
    public static class ApiLogDto
    {
        public static LogDto GetLog(ClaimsIdentity user, string descricao, EnumLogLevelDto level, Guid? idTransacao = null)
        {
            var ip = HttpContext.Current.Request.Headers["ip"];
            var pagina = HttpContext.Current.Request.Headers["pagina"];
            var navegador = HttpContext.Current.Request.Headers["navegador"];
            var idioma = HttpContext.Current.Request.Headers["idioma"];

            return new LogDto()
            {
                IP = ip,
                IDTransacao = idTransacao,
                Navegador = navegador,
                Pagina = pagina,
                Idioma = idioma,
                ID = Guid.NewGuid(),

                Descricao = descricao,
                LogLevelID = (int)level,
                Usuario = user.Name,
                DataCriacao = DateTime.Now,
                UsuarioID = new Guid(user.Claims.First(c => c.Type.Equals("Usuario")).Value)
            };
        }
    }
}