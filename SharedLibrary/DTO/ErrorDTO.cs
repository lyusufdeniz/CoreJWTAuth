namespace SharedLibrary.DTO
{
    public class ErrorDTO
    {
        public List<string> Errors { get;  }
        public bool IsShow { get;  }

        public ErrorDTO()
        {
            Errors = new List<string>();
        }
        public ErrorDTO(string error,bool IsShow)
        {
            Errors.Add(error);
            this.IsShow = IsShow;

            
        }
        public ErrorDTO(List<string> error, bool IsShow)
        {
            Errors=error;
            this.IsShow = IsShow;


        }
    }
}
