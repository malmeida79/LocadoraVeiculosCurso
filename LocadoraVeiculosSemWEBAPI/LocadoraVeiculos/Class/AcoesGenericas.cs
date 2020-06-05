using LocadoraVeiculos.UTILS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

namespace LocadoraVeiculos.Class
{
    /// <summary>
    /// Classe generica para acoes com banco de dados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AcoesGenericas<T>
    {
        SQLBase db = new SQLBase();

        private string _erro = string.Empty;
        private string _procedure = string.Empty;

        public AcoesGenericas()
        {
            string classe = typeof(T).Name;

            switch (classe.ToLower())
            {
                case "entveiculos":
                    _procedure = "SPC_MANIPULADADOSVEICULO";
                    break;
                case "entusuarios":
                    _procedure = "SPC_MANIPULADADOSUSUARIO";
                    break;
                case "entmodeloveiculo":
                    _procedure = "SPC_MANIPULADADOSMODELOVEICULO";
                    break;
                case "entmarcaveiculo":
                    _procedure = "SPC_MANIPULADADOSMARCAVEICULO";
                    break;
                case "entlocacoes":
                    _procedure = "SPC_MANIPULADADOSLOCACAO";
                    break;
                case "entclientes":
                    _procedure = "SPC_MANIPULADADOSCLIENTE";
                    break;
                default:
                    throw new Exception("Classe não definida ou desconhecida para ações com banco de dados.");
            }
        }

        /// <summary>
        /// Caso ocorra algum erro, essa variavel sera preenchida com o mesmo
        /// para retorno
        /// </summary>
        public string GetRetornoErro
        {
            get
            {
                return this._erro;
            }
        }

        /// <summary>
        /// Salvar dados
        /// </summary>
        /// <param name="entity">Objeto a ser tratado</param>
        /// <returns></returns>
        public bool SalvarDados(T entity)
        {
            bool retorno = false;

            try
            {
                db.Conectar();
                db.ExecutaProcedure(_procedure, ConfiguraParansSQL(entity, "U"));
                db.Desconectar();
                retorno = true;
            }
            catch (Exception ex)
            {
                _erro = "Ocorreu erro:" + ex.Message.ToString();
            }

            return retorno;
        }

        /// <summary>
        /// Cadastrar dados do usuario
        /// </summary>
        /// <param name="entity">Objeto a ser tratado</param>
        /// <returns></returns>
        public bool CadastrarDados(T entity)
        {
            bool retorno = false;

            try
            {
                db.Conectar();
                db.ExecutaProcedure(_procedure, ConfiguraParansSQL(entity, "I"));
                db.Desconectar();
                retorno = true;
            }
            catch (Exception ex)
            {
                _erro = "Ocorreu erro:" + ex.Message.ToString();
            }

            return retorno;
        }

        /// <summary>
        /// Listar Dados
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<T> ListarDados(T entity)
        {
            List<T> retorno = null;
            SqlDataReader dr;

            try
            {
                db.Conectar();
                dr = db.GeraReaderProcedure(_procedure, ConfiguraParansSQL(entity, "P"));
                retorno = TransfereDrParaLista(dr);
                db.Desconectar();
            }
            catch (Exception ex)
            {
                _erro = "Ocorreu erro:" + ex.Message.ToString();
            }

            return retorno;
        }

        /// <summary>
        /// Excluir dados
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ExcluirDados(T entity)
        {
            bool retorno = false;

            try
            {
                db.Conectar();
                db.ExecutaProcedure(_procedure, ConfiguraParansSQL(entity, "D"));
                db.Desconectar();
                retorno = true;
            }
            catch (Exception ex)
            {
                _erro = "Ocorreu erro:" + ex.Message.ToString();
            }

            return retorno;
        }

        /// <summary>
        /// Consulta dados para objeto passado
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<T> ConsultarDados(T entity)
        {
            List<T> retorno = null;
            SqlDataReader dr;

            try
            {
                db.Conectar();
                dr = db.GeraReaderProcedure(_procedure, ConfiguraParansSQL(entity, "P"));
                retorno = TransfereDrParaLista(dr);
                db.Desconectar();
            }
            catch (Exception ex)
            {
                _erro = "Ocorreu erro:" + ex.Message.ToString();
                throw new Exception("");
            }

            return retorno;
        }

        /// <summary>
        /// Conta quantas linhas tem na tabela do objeto passado
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int ContarDados(T entity)
        {
            int retorno = 0;
            SqlDataReader dr;

            try
            {
                db.Conectar();
                dr = db.GeraReaderProcedure(_procedure, ConfiguraParansSQL(entity, "C"));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        retorno = Convert.ToInt32(dr["contagem"]);
                    }
                }
                db.Desconectar();
            }
            catch (Exception ex)
            {
                _erro = "Ocorreu erro:" + ex.Message.ToString();
            }

            return retorno;
        }

        /// <summary>
        /// Configuracao de parametros SQL para acoes de banco de dados
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acao"></param>
        /// <returns></returns>
        private List<SqlParameter> ConfiguraParansSQL(T entity, string acao)
        {
            List<SqlParameter> par = new List<SqlParameter>();
            // lista para devolver os dados
            List<T> lista = new List<T>();

            // descobrindo o tipo da lista
            Type tipo = typeof(T);

            // instanciando o objeto
            object obj = Activator.CreateInstance(tipo);

            // descobre as propriedades do objeto.
            PropertyInfo[] arrP = obj.GetType().GetProperties();

            // atribuindo o parametro inicial de acao a lista de parametros
            // do sql
            par.Add(new SqlParameter("@acao", acao));

            // carrega o objeto
            foreach (PropertyInfo p in arrP)
            {
                if (p.GetValue(entity) != null)
                {
                    // quando data é inválida não adiconar
                    if (p.GetValue(entity).ToString() == @"01/01/0001 00:00:00")
                    {
                        continue;
                    }

                    par.Add(new SqlParameter("@" + p.Name, p.GetValue(entity)));
                }
            }

            // destruindo o obj
            obj = null;

            // retornando comandos
            return par;
        }

        /// <summary>
        /// Quando em uma consulta, recebe os dados retornados do SQL e 
        /// tranforma em lista do tipo generico passado oomo parametro
        /// em T
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private List<T> TransfereDrParaLista(SqlDataReader dr)
        {
            // lista para devolver os dados
            List<T> lista = new List<T>();

            // descobrindo o tipo da lista
            Type t = typeof(T);

            // consultando os dados
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    // instanciando o objeto que ira transportar os dados 
                    // para a lista
                    object o = Activator.CreateInstance(t);

                    // descobre as propriedades do objeto.
                    PropertyInfo[] arrP = o.GetType().GetProperties();

                    // carrega o objeto
                    foreach (PropertyInfo p in arrP)
                    {
                        if (dr[p.Name] != System.DBNull.Value)
                        {
                            o.GetType().GetProperty(p.Name).SetValue(o, Convert.ChangeType(dr[p.Name], p.PropertyType, System.Globalization.CultureInfo.CurrentCulture), null);
                        }
                    }

                    // adiciona o objeto a lista
                    lista.Add((T)o);
                }
            }

            // devolve a lista carregada
            return lista;
        }
    }
}