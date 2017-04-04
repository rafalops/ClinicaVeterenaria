using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClinicaVets.Models
{
    public class Animais
    {
        public int AnimaisID { get; set; }
        public string  NomeDoAnimal { get; set; }
        public string Especie { get; set; }
        public string Raca { get; set; }
        //...outros atributos ficariam aqui

        //criar uma chave forasteira - FK
        [ForeignKey("Dono")]
        public int DonoFK { get; set; } //Existe para criar a FK na BD
        public Donos Dono { get; set; } //Existe para relacionar os objetos no C#

    }
}