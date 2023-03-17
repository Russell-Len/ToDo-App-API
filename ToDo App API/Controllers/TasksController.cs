using Microsoft.AspNetCore.Mvc;
using ToDo_App_API.DataContext;
using ToDo_App_API.DBHelper;
using ToDo_App_API.Model;
using ToDo_App_API.Response;

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
            ResponseType type = ResponseType.Success;

            try
            {
                IEnumerable<TaskModel> data = _db.GetTasks();

                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }

        }

        [HttpPost("AddTask")]
        public IActionResult AddTask([FromBody] TaskModel taskModel)
        {
            ResponseType type = ResponseType.Success;

            try
            {
                _db.AddTask(taskModel);
                return Ok(ResponseHandler.GetAppResponse(type, taskModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut("EditTask")]
        public IActionResult EditTask([FromBody] TaskModel taskModel)
        {
            ResponseType type = ResponseType.Success;

            try
            {
                _db.EditTask(taskModel);
                return Ok(ResponseHandler.GetAppResponse(type, taskModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete("DeleteTask/{id}")]
        public IActionResult DeleteTask(int id)
        {
            ResponseType type = ResponseType.Success;

            try
            {
                _db.SoftDeleteTask(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Deleted Successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

    }
}
