using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo_App_API.DataContext;
using ToDo_App_API.DBHelper;
using ToDo_App_API.Model;

namespace ToDo_App_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ToDoDBHelper _db;

        public TasksController(ToDoDBContext taskDBContext)
        {
            _db = new ToDoDBHelper(taskDBContext);
        }

        [HttpGet]
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

        [HttpPost]
        public IActionResult AddTask(TaskToAddModel taskToAddModel)
        {
            try
            {
                _db.AddTask(taskToAddModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured on the server.");
            }
        }

        [HttpPut]
        public IActionResult EditTask(TaskToEditModel taskToEditModel)
        {
            try
            {
                _db.EditTask(taskToEditModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured on the server.");
            }
        }

        [HttpDelete("{id}")]
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
