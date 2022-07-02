namespace Blog.ViewModels
{
    public class ResultViewModel<T> where T : class
    {
        public bool isSuccess => Errors.Count == 0;
        public List<string> Errors { get; private set; } = new();
        public T Data { get; private set; }

        public ResultViewModel(T data) => Data = data;
        public ResultViewModel(List<string> errors) => Errors = errors;
        public ResultViewModel(string errorMessage) => Errors.Add(errorMessage);
        public void AddError(string errorMessage) => Errors.Add(errorMessage);
    }
}