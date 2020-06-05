using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace LocadoraVeiculosWebApi.UTIL
{
    public class SQLBase
    {

        string cnString = LocadoraVeiculosWebApi.Properties.Settings.Default.DBCnstr;
        private string _erro = string.Empty;

        SqlConnection cn;
        SqlCommand cmd;

        /// <summary>
        /// conecta Banco de dados
        /// </summary>
        public void Conectar()
        {
            cn = new SqlConnection(cnString);
            cn.Open();
        }

        /// <summary>
        /// Desconecta banco de dados se conectado
        /// </summary>
        public void Desconectar()
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }

        }

        /// <summary>
        /// Caso ocorra algum erro, essa variavel sera preenchida com o mesmo
        /// para retorno
        /// </summary>
        public string GetErro
        {
            get
            {
                return this._erro;
            }
        }
      
        /// <summary>
        /// Busca dados conforme comando informado
        /// </summary>
        /// <param name="comando">Comando a ser executado</param>
        /// <returns>DataReader com os dados</returns>
        public SqlDataReader GeraReaderProcedure(string comando, List<SqlParameter> paramList = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = comando;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            if (paramList != null)
            {
                foreach (var parametro in paramList)
                {
                    cmd.Parameters.Add(parametro);
                }
            }          

            SqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }

        /// <summary>
        /// Executa um comando sem retorno de informacoes
        /// </summary>
        /// <returns>True caso sucesso.</returns>
        public bool ExecutaProcedure(string procedure, List<SqlParameter> paramList, string msgLog = null, int tempo = 20)
        {
            bool retorno = false;
            Int32 contaLinhasAfetadas = 0;

            try
            {
                Conectar();

                cmd = new SqlCommand(procedure, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = tempo;

                foreach (var parametro in paramList)
                {
                    cmd.Parameters.Add(parametro);
                }

                retorno = true;

                contaLinhasAfetadas = cmd.ExecuteNonQuery();

                Desconectar();

            }
            catch (Exception ex)
            {
                _erro = "Erro:" + ex.Message.ToString();
                retorno = false;
            }
            finally
            {
                cmd.Dispose();
            }

            return retorno;

        }

        /// <summary>
        /// Executa comando sem retorno
        /// </summary>
        /// <param name="comando">Comando ou procedure a ser executados</param>
        /// <param name="tipoComando">Tipo de comando a ser executado</param>
        /// <param name="paramList">Lista de parametros</param>
        /// <param name="msgLog">opcional de msg para log</param>
        /// <param name="tempo">Tempo para execução do comando default 20</param>
        /// <returns></returns>
        public bool ExecutaComandoSemRetorno(string comando, CommandType tipoComando, List<SqlParameter> paramList, string msgLog = null, int tempo = 20)
        {
            bool retorno = false;
            Int32 contaLinhasAfetadas = 0;

            try
            {
                Conectar();

                cmd = new SqlCommand(comando, cn);
                cmd.CommandType = tipoComando;
                cmd.CommandTimeout = tempo;

                foreach (var parametro in paramList)
                {
                    cmd.Parameters.Add(parametro);
                }

                retorno = true;

                contaLinhasAfetadas = cmd.ExecuteNonQuery();

                Desconectar();

            }
            catch (Exception ex)
            {
                _erro = "Erro:" + ex.Message.ToString();
                retorno = false;
            }
            finally
            {
                cmd.Dispose();
            }

            return retorno;

        }

        /// <summary>
        /// Método que retorna o resultado da consulta sql em um dataset.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataSet GeraDataSet(string comando)
        {
            try
            {
                //Instância o sqlcommand com a query sql que será executada e a conexão.
                SqlCommand cmd = new SqlCommand(comando, cn);

                //Instância o sqldataAdapter.
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Instância o dataSet de retorno.
                DataSet dataSet = new DataSet();

                //Atualiza o dataSet
                adapter.Fill(dataSet);
                //Retorna o dataSet com o resultado da query sql.
                return dataSet;
            }
            catch (Exception ex)
            {
                _erro = ex.Message.ToString();
                return null;
            }
        }

        /// <summary>
        /// Encerra Comando
        /// </summary>
        public void EncerraComando()
        {
            cmd.Dispose();
        }
    }
}