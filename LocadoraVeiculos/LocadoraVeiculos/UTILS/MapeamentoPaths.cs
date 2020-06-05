using System;

namespace LocadoraVeiculos.UTILS
{
    public static class MapeamentoPaths
    {
        /// <summary>
        /// Retorna o path do arquivo em questao.
        /// </summary>
        /// <returns></returns>
        public static string GetFilePath()
        {
            return System.Web.HttpContext.Current.Request.FilePath;
        }

        /// <summary>
        /// Mapeia um caminho virtual para um caminho físico no servidor.
        /// </summary>
        /// <returns>Resultado: C:\inetpub\wwwroot\exemplo\</returns>
        public static string GetCaminhoFisico()
        {
            return System.Web.Hosting.HostingEnvironment.MapPath("/");
        }

        /// <summary>
        /// Obtém o diretório base, o resolvedor de assembly usa para investigar os assemblies.
        /// </summary>
        /// <returns>Resultado: C:\inetpub\wwwroot\exemplo\</returns>
        public static string GetDiretorioBase()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// Obtém o caminho de unidade física de diretório de aplicativo hospedado no domínio do aplicativo atual.
        /// </summary>
        /// <returns>Resultado: C:\inetpub\wwwroot\exemplo\</returns>
        public static string GetPathDominio()
        {
            return System.Web.HttpRuntime.AppDomainAppPath;
        }

        /// <summary>
        /// Devolve a pagina atual
        /// </summary>
        /// <returns>exemplo.html</returns>
        public static string GetPaginaAtual()
        {
            return System.Web.HttpContext.Current.Request.Path.Substring(System.Web.HttpContext.Current.Request.Path.LastIndexOf("/") + 1);
        }

        /// <summary>
        /// DEvolve a url da pagina atual
        /// </summary>
        /// <returns>Http://www.exemplo.com.br/pagina.html</returns>
        public static string GetUrlPaginaAtual()
        {
            return System.Web.HttpContext.Current.Request.UrlReferrer.OriginalString;
        }
    }
}
