namespace ML
{
    public class Result
    {
        //Excepciones o mensajes
        public bool Correct { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Ex { get; set; }

        //Para Objetos
        public object Object { get; set; }
        public List<object> Objects { get; set; }
    }
}