namespace ExcepcionesPers
{
    public class ExcepcionComunidad : Exception
    {
        public ExcepcionComunidad(string message) : base(message)
        {
            // Llamamos algún método para guardar en base de datos o realizar alguna tarea
            // Aquí puedes agregar lógica adicional al momento de lanzar esta excepción
        }

        public class ExcepcionNula : ExcepcionComunidad //terminar
        {
            public ExcepcionNula(string message) : base(message)
            {
                // Lógica adicional en el constructor de la nueva clase derivada
            }
        }
    }

}