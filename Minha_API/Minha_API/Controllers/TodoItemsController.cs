using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minha_API.DTOs;
using Minha_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Minha_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContextDb todoContext;
        public TodoItemsController(TodoContextDb todoContextDb)
        {
            todoContext = todoContextDb;
        }

        // Verbo Post (CREATE - INSERT)
        [HttpPost]
        public ActionResult<TodoItemResponseDto> Post(TodoItemDto todoItemDto)
        {
            TodoItemModel todoItemModel = new TodoItemModel();
            todoItemModel.Name = todoItemDto.Name;
            todoItemModel.Ativo = todoItemDto.Ativo;
            todoItemModel.Cadastro = DateTime.Now;

            var indiceNome = todoItemDto.Name.Split(' ');
            todoItemModel.Apelido = $"ap-{indiceNome[0]}";

            todoContext.TodoItemModels.Add(todoItemModel);
            todoContext.SaveChanges();

            return Ok(new TodoItemResponseDto {
                Id = todoItemModel.Id,
                Nome = todoItemDto.Name,
                Apelido = todoItemDto.Apelido
            });
        }

        // Verbo Put (UPDATE - Atualiza)
        [HttpPut("{id}")]
        public ActionResult<bool> Put([FromRoute] int id, TodoItemDto todoItemDto)
        {
            //busca os dados no bd pelo ID
            //TodoItemModel todoItemModel = new TodoItemModel();
            var todoItemModel = todoContext.TodoItemModels.Find(id);

            if (todoItemModel == null) {
                return NotFound("Id não encontrado");
            }
            //todoItemModel.Id = id;
            todoItemModel.Name = todoItemDto.Name;
            todoItemModel.Ativo = todoItemDto.Ativo;

            todoContext.TodoItemModels.Update(todoItemModel);
            todoContext.SaveChanges();

            return Ok(true);
        }

        // Verbo Delete (DELETE)
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            //var existe = todoContext.TodoItemModels.Where(w => w.Id == id).Any();
            var modelsItem = todoContext.TodoItemModels.Find(id);

            if (modelsItem is not null) {
                todoContext.TodoItemModels.Remove(modelsItem);
                todoContext.SaveChanges();
            } 
            else {
                return NotFound("Id não encontrado");
            }

            return Ok();
        }

        // Verbo GET (SELECT)
        [HttpGet]
        public ActionResult<IEnumerable<TodoItemResponseDto>> Get()
        {
            //busca dados no bd e devolve um lista
            var modelTodoItem = todoContext.TodoItemModels.ToList();
            var listaTodoItemDto = new List<TodoItemResponseDto>();

            foreach(var todoItem in modelTodoItem) {
                listaTodoItemDto.Add(new TodoItemResponseDto() {
                    Id = todoItem.Id,
                    Apelido = todoItem.Apelido,
                    Nome = todoItem.Name
                });
            }

            return Ok(listaTodoItemDto);
        }

        // Verbo GET (SELECT) - por ID
        [HttpGet("{id}")]
        public ActionResult<TodoItemResponseDto> Get([FromRoute] int id)
        {
            try {
                var todoItemModel = todoContext.TodoItemModels.Find(id);

                var todoItemResponseDto = new TodoItemResponseDto
                {
                    Id = todoItemModel.Id,
                    Apelido = todoItemModel.Apelido,
                    Nome = todoItemModel.Name
                };

                return Ok(todoItemResponseDto);
            } 
            catch {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(),
                    new {Messagem = "Id não existe mané!"});
            }
            
        }
    }
}
