using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleMVC.Models
{
    public class Livro
    {
        [Key]
        public int IDLivro { get; set; }
        [Required(ErrorMessage = "Digite o Título do livro.")]
        [StringLength(100, ErrorMessage = "O tamanho máximo deve ser 100 caracteres.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Digite o nome do Autor.")]
        public string Autor { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public bool Disponivel { get; set; }

    }
}