using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minha_API.Models
{
    [Table("TodoItem")]
    public class TodoItemModel
    {
        [Key] // chave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR"), StringLength(250), Required]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR"), StringLength(50)]
        public string Apelido { get; set; }
        [Required]
        public bool Ativo { get; set; }
        [Required]
        public DateTime Cadastro { get; set; }
    }
}
