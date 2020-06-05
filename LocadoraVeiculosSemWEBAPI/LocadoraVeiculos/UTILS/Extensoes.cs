using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;

namespace LocadoraVeiculos.UTILS
{

    public static class Extensoes
    {
        /// <summary>
        /// Em um banco de dados relacional um valor nulo é usado em uma coluna quando o valor é desconhecido ou ausente. 
        /// Um nulo não é uma string vazia (para tipos de dados string ou datetime), nem um valor zero 
        /// (para tipos de dados numéricos). Atraves da forma abaixo podemos extender o metodo e apresentar um
        /// valor default nesses casos
        /// </summary>
        public static T DefaultDbNull<T>(this Object value, object defaultValue)
        {
            if (value == Convert.DBNull)
                return (T)defaultValue;
            return (T)value;
        }

        /// <summary>
        /// Seleciona item em ComboBox na posição indicada
        /// </summary>
        /// <param name="dropDownList">ComboBox a ser tratado</param>
        /// <param name="selectedValue">Posição a ser selecionada</param>
        /// <returns></returns>
        public static Boolean SetSelectedValue(this DropDownList dropDownList, String selectedValue)
        {
            ListItem selectedListItem = dropDownList.Items.FindByValue(selectedValue);

            if (selectedListItem != null)
            {
                selectedListItem.Selected = true;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Preenche combo box
        /// </summary>
        /// <param name="dropDownList">Combobox a ser preenchida</param>
        /// <param name="ListaDados">Lista com dados para inserir no combobox</param>
        /// <param name="DataTextField">Campo para texto</param>
        /// <param name="DataValueField">Campo para value</param>
        /// <param name="incluiSelecione">Sera incluido o item "-- Selecione --"?</param>
        /// <param name="selectedValue">Valor pre-selecionado caso exista</param>
        public static void Preencher<T>(this DropDownList dropDownList, List<T> ListaDados, string DataTextField, string DataValueField, bool incluiSelecione = false, string selectedValue = null, string textoSelecione = null)
        {

            // Descarrega dropdown list
            dropDownList.Items.Clear();
            dropDownList.DataSource = null;
            dropDownList.DataBind();

            // Carrega dropdown list
            dropDownList.DataSource = ListaDados;
            dropDownList.DataTextField = DataTextField;
            dropDownList.DataValueField = DataValueField;
            dropDownList.DataBind();

            // Caso necessario insere instrução selecione
            if (incluiSelecione)
            {
                if (!string.IsNullOrEmpty(textoSelecione))
                {
                    dropDownList.Items.Insert(0, textoSelecione);
                }
                else
                {
                    dropDownList.Items.Insert(0, "-- Selecione --");
                }
            }

            // Caso seja pre-selecionado um valor, inserir o mesmo.
            if (selectedValue != null)
            {
                dropDownList.SetSelectedValue(selectedValue);
            }

        }

        /// <summary>
        /// Preenche GridView
        /// </summary>
        /// <typeparam name="T">Tipo de dados da listagem</typeparam>
        /// <param name="gridView">Cridview a ser preenchido</param>
        /// <param name="ListaDados">Lista com dados para preencher gridview</param>
        public static void Preencher<T>(this GridView gridView, List<T> ListaDados)
        {
            // Descarrega dropdown list
            gridView.DataSource = null;
            gridView.DataBind();

            // Carrega dropdown list
            gridView.DataSource = ListaDados;
            gridView.DataBind();
        }
        /// <summary>
        /// Descarrega Combo box
        /// </summary>
        /// <param name="dropDownList"></param>
        public static void Descarregar(this DropDownList dropDownList)
        {
            // Descarrega dropdown list
            dropDownList.Items.Clear();
            dropDownList.DataSource = null;
            dropDownList.DataBind();
            dropDownList.Items.Insert(0, "-- Selecione --");
        }

        /// <summary>
        /// Descarrega Combo box
        /// </summary>
        /// <param name="dropDownList"></param>
        public static void Descarregar(this GridView gridView)
        {
            // Descarrega gridview
            gridView.DataSource = null;
            gridView.DataBind();
        }


        /// <summary>
        /// Preenche GridView
        /// </summary>
        /// <typeparam name="T">Tipo de dados da listagem</typeparam>
        /// <param name="chkList">Cridview a ser preenchido</param>
        /// <param name="ListaDados">Lista com dados para preencher gridview</param>
        public static void Preencher<T>(this CheckBoxList chkList, List<T> ListaDados, string dataTextField, string dataValueFiled)
        {
            // Descarrega dropdown list
            chkList.DataSource = null;

            chkList.DataBind();

            // Carrega dropdown list
            chkList.DataSource = ListaDados;
            chkList.DataTextField = dataTextField;
            chkList.DataValueField = dataValueFiled;
            chkList.DataBind();
        }

        /// <summary>
        /// Descarrega Combo box
        /// </summary>
        /// <param name="CheckBoxList"></param>
        public static void Descarregar(this CheckBoxList ckList)
        {
            // Descarrega gridview
            ckList.DataSource = null;
            ckList.DataBind();
        }

        /// <summary>
        /// Ordenação de listagem
        /// </summary>
        /// <typeparam name="T">Classe generica a ser devolvida</typeparam>
        /// <param name="list">Lista a ser ordenada</param>
        /// <param name="sortBy">Campo para ordenar</param>
        /// <param name="SortDir">Direção de ordenação ASC ou DSC</param>
        /// <returns>Devolve a listagem ordenada conforme parametros</returns>
        public static List<T> toSort<T>(this List<T> list, string sortBy, string SortDir)
        {
            SortDirection direction;

            // declaração generica para pesquisada lista
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);

            // definindo direção de ordenação
            if (SortDir.ToLower() == "asc")
            {
                direction = SortDirection.Ascending;
            }
            else
            {
                direction = SortDirection.Descending;
            }

            // ordenando a listagem e devolvendo conforme campo selecionado.
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<T>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<T>();
            }
        }

    }
}
