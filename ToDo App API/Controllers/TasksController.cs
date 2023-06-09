﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo_App_API.DataContext;
using ToDo_App_API.DBHelper;
using ToDo_App_API.Model;
using ToDo_App_API.Utilities;

namespace ToDo_App_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ToDoDBHelper _db;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ToDoDBContext taskDBContext, ILogger<TasksController> logger)
        {
            _db = new ToDoDBHelper(taskDBContext);
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            _logger.LogInformation("API called to get tasks.");

            try
            {
                var authorId = User.Claims.FirstOrDefault(c => c.Type == "authorId")?.Value;

                if (authorId == null) return Unauthorized();

                return Ok(_db.GetTasks(Int32.Parse(authorId)));
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: {message}", ex.Message);
                return BadRequest("An error occured on the server.");
            }
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            _logger.LogInformation("API called to get categories.");

            try
            {
                var authorId = User.Claims.FirstOrDefault(c => c.Type == "authorId")?.Value;

                if (authorId == null) return Unauthorized();

                return Ok(_db.GetCategories(Int32.Parse(authorId)));
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: {message}", ex.Message);
                return BadRequest("An error occured on the server.");
            }
        }

        [HttpPost]
        public IActionResult AddTask(TaskToAddModel taskToAddModel)
        {
            _logger.LogInformation("API called to add a new task.");

            try
            {
                var authorId = User.Claims.FirstOrDefault(c => c.Type == "authorId")?.Value;

                if (authorId == null) return Unauthorized();

                taskToAddModel.AuthorId = Int32.Parse(authorId);

                _db.AddTask(taskToAddModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: {message}", ex.Message);
                return BadRequest("An error occured on the server.");
            }
        }

        [HttpPut]
        public IActionResult EditTask(TaskToEditModel taskToEditModel)
        {

            _logger.LogInformation("API called to update task of Id:{id}", taskToEditModel.Id);

            try
            {
                var existingTask = _db.GetTaskById(taskToEditModel.Id);

                var authorId = User.Claims.FirstOrDefault(c => c.Type == "authorId")?.Value;

                if (existingTask == null) return NotFound("Update task failed. " + APIErrorMessage.TASK_NOT_FOUND);

                if (authorId == null) return Unauthorized("Update task failed. " + APIErrorMessage.INVALID_CREDENTIALS);

                if (existingTask.AuthorId != Int32.Parse(authorId)) return Unauthorized("Update task failed. " + APIErrorMessage.TASK_ACCESS_VIOLATED);

                _db.EditTask(taskToEditModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: {message}", ex.Message);
                return BadRequest("An error occured on the server.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            _logger.LogInformation("API called to delete task of Id:{id}", id);

            try
            {
                var existingTask = _db.GetTaskById(id);

                var authorId = User.Claims.FirstOrDefault(c => c.Type == "authorId")?.Value;

                if (existingTask == null) return NotFound("Delete task failed. " + APIErrorMessage.TASK_NOT_FOUND);

                if (authorId == null) return Unauthorized("Delete task failed. " + APIErrorMessage.INVALID_CREDENTIALS);

                if (existingTask.AuthorId != Int32.Parse(authorId)) return Unauthorized("Delete task failed. " + APIErrorMessage.TASK_ACCESS_VIOLATED);

                _db.SoftDeleteTask(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured: {message}", ex.Message);
                return BadRequest("Delete task failed. " + APIErrorMessage.BAD_REQUEST);
            }
        }

    }
}
