using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minha_API.DTOs;
using Minha_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<int> Post(TodoItemDto todoItemDto)
        {
            TodoItemModel todoItemModel = new TodoItemModel();
            todoItemModel.Name = todoItemDto.Name;
            todoItemModel.Ativo = todoItemDto.Ativo;
            todoItemModel.Cadastro = DateTime.Now;

            todoContext.TodoItemModels.Add(todoItemModel);
            todoContext.SaveChanges();

            return Ok(todoItemModel.Id);
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
        public ActionResult <IEnumerable<TodoItemDto>> Get()
        {
            //busca dados no bd e devolve um lista
            //IEnumerable<TodoItemDto> modelsItem = (IEnumerable<TodoItemDto>)todoContext.TodoItemModels.GetType();

            return Ok();
        }

        // Verbo GET (SELECT) - por ID
        [HttpGet("{id}")]
        public ActionResult<TodoItemDto> Get([FromRoute] int id)
        {
            //busca dados no bd pelo ID e devolve 1 objeto
            return Ok();
        }
    }
}
