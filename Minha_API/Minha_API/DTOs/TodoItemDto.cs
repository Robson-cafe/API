using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minha_API.DTOs
{
    public class TodoItemDto
    {
        public string Name { get; set; }

        public bool Ativo { get; set; }
        public string Apelido { get; internal set; }
    }

    public class TodoItemResponseDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Apelido { get; set; }
    }
}
