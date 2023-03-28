namespace ToDo_App_API.Utilities
{
    public static class APIErrorMessage
    {
        public const string TASK_NOT_FOUND = "The requested task was not found.";

        public const string TASK_ACCESS_VIOLATED = "The requested task can only be accessed by its author.";

        public const string INVALID_CREDENTIALS = "Please ensure your credentials are valid.";

        public const string BAD_REQUEST = "A bad request was made.";
    }
}
