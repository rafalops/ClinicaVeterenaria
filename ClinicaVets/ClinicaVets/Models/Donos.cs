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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]//marca o atributo
        [Display(Name = "Identificador do Cliente")]
        public int DonoID { get; set; }
        //Atributo é obrigatorio
        [Required(ErrorMessage = "O {0} é de preencimento obrigatório")]
        [Display(Name = "Nome do cliente")]
        public string Nome { set; get; }

        [Required(ErrorMessage = "O NIF é de preencimento obrigatório")]
        [Display(Name = "NIF do cliente")]
        public string NIF { get; set; }

        // especificar que um DONO tem muitos ANIMAIS
        public ICollection<Animais> ListaDeAnimais { get; set; }
    }
}