using Microsoft.AspNetCore.Mvc;
using ToDo_App_API.DataContext;
using ToDo_App_API.DBHelper;
using ToDo_App_API.Model;

namespace ToDo_App_API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ToDoDBHelper _db;

        public TasksController(TaskDBContext taskDBContext)
        {
            _db = new ToDoDBHelper(taskDBContext);
        }

        [HttpGet("GetTasks")]
        public IActionResult GetTasks()
        {
            try
            {
                return Ok(_db.GetTasks());
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured on the server.");
            }
        }

        [HttpPost("AddTask")]
        public IActionResult AddTask([FromBody] TaskModel taskModel)
        {
            try
            {
                _db.AddTask(taskModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured on the server.");
            }
        }

        [HttpPut("EditTask")]
        public IActionResult EditTask([FromBody] TaskModel taskModel)
        {
            try
            {
                _db.EditTask(taskModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured on the server.");
            }
        }

        [HttpDelete("DeleteTask/{id}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                _db.SoftDeleteTask(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured on the server.");
            }
        }

    }
}
