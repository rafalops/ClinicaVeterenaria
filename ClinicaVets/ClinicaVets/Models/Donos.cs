using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaVets.Models
{
    public class Donos
    {
        //Construtor da classe, que vai ser utilizado
        //Para inicializar o atributo 'ListaDeAnimais'
        public Donos()
        {
            ListaDeAnimais = new HashSet<Animais>();
        }
        //Variaveis publicas começam por maiscula, privadas minusculas
        public int DonosID { get; set;}
        public string Nome { get; set; }
        public string NIF { get; set; }

        //Relacionar os Donos com os Animais 1-N
        //'ICollection' Cria listas enumeradas
        public virtual ICollection<Animais> ListaDeAnimais { get; set; }

    }
}