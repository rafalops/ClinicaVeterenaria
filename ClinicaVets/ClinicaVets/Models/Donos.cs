using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClinicaVets.Models
{
    public class Donos
    {
        //#############################################################
        // criação das classes DONOS, VETERINARIOS, ANIMAIS e CONSULTAS
        //#############################################################

        // DONOS
        //=============================================================

        // vai representar os dados da tabela dos DONOS

        // criar o construtor desta classe
        // e carregar a lista de Animais
        public Donos()
        {
            ListaDeAnimais = new HashSet<Animais>();
        }


        [Key]//indica que o atributo é PK
        [DatabaseGenerated(DatabaseGeneratedOption.None)]//marca que a PK não é auto number
        [Display(Name = "Identificador do Cliente")]
        public int DonoID { get; set; }
        //Atributo é obrigatorio
        [Required(ErrorMessage = "O {0} é de preencimento obrigatório")]
        [Display(Name = "Nome do cliente")]
        //Criar um filtro que valide um nome
        [RegularExpression("[A-Z][a-záéíóúàèìòùãõç]+((( )|(-)|( (e|de|da|do|dos) )|( d'))[A-Z][a-záéíóúàèìòùãõç]+){1,6}",ErrorMessage = "O seu nome não é válido.Dica comece o Nome com maiscúlas")]
        // http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?         link
        // \w+([-+.']\w+)*@\w+ \. \w                             mail
        // d{4} (-\d{3})?( \w+)*                                 codigo postal
        public string Nome { set; get; }

        [Required(ErrorMessage = "O NIF é de preencimento obrigatório")]
        [Display(Name = "NIF do cliente")]
        [RegularExpression("[0-9]{9}",ErrorMessage = "{0} incorreto,insira o seu {0} com algarismos entre 0 e 9, com tamanho de 9 algarismos")]
        public string NIF { get; set; }

        // especificar que um DONO tem muitos ANIMAIS
        public ICollection<Animais> ListaDeAnimais { get; set; }
    }
}