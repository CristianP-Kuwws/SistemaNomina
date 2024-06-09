using System;
using ExcepcionesPers;
using static ComunidadOBJ.Empleado;

namespace ComunidadOBJ
{
    public abstract class Empleado
    {
        #region CaptarDatosComunes
        private string _primerNombre;
        private string _apellidoPaterno;
        private string _NumSeguroSocial;

        public string PrimerNombre
        {
            get { return _primerNombre; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ExcepcionComunidad("El nombre es requerido.");
                }
                if (value.Length > 20)
                {
                    throw new ExcepcionComunidad("El nombre no puede exceder los 20 caracteres.");
                }
                _primerNombre = value;
            }
        }

        public string ApellidoPaterno
        {
            get { return _apellidoPaterno; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ExcepcionComunidad("El apellido es requerido.");
                }
                if (value.Length > 20)
                {
                    throw new ExcepcionComunidad("El apellido no puede exceder los 20 caracteres.");
                }
                _apellidoPaterno = value;
            }
        }

        public string NumeroSeguroSocial
        {
            get { return _NumSeguroSocial; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ExcepcionComunidad("El número de seguro social es requerido.");
                }
                _NumSeguroSocial = value;
            }
        }
        #endregion

        #region Constructor
        public Empleado(string primerNombre, string apellidoPaterno, string NumSegSocial)
        {
            PrimerNombre = primerNombre ?? throw new ExcepcionComunidad("El nombre no puede ser nulo.");
            ApellidoPaterno = apellidoPaterno ?? throw new ExcepcionComunidad("El apellido no puede ser nulo.");
            NumeroSeguroSocial = NumSegSocial;
        }
        #endregion

        #region Metodo
        public abstract decimal CalcularIngreso();
        #endregion

        #region ClasesHijos
        public class EmpleadoAsalariado : Empleado
        {
            public decimal SalarioSemanal { get; set; }

            public EmpleadoAsalariado(string primerNombre, string apellidoPaterno, string NumSegSocial, decimal salarioSemanal)
                : base(primerNombre, apellidoPaterno, NumSegSocial)
            {
                SalarioSemanal = salarioSemanal;
            }

            public override decimal CalcularIngreso()
            {
                return SalarioSemanal;
            }
        }

        public class EmpleadoPorComision : Empleado
        {
            public decimal VentasBrutas { get; set; }
            public decimal TarifaComision { get; set; }

            public EmpleadoPorComision(string primerNombre, string apellidoPaterno, string NumSegSocial, decimal ventasBrutas, decimal tarifaComision)
               : base(primerNombre, apellidoPaterno, NumSegSocial)
            {
                VentasBrutas = ventasBrutas;
                TarifaComision = tarifaComision;
            }

            public override decimal CalcularIngreso()
            {
                return TarifaComision * VentasBrutas;
            }
        }

        public class EmpleadoPorHoras : Empleado
        {
            public decimal SueldoPorHoras { get; set; }
            public decimal HorasTrabajadas { get; set; }

            public EmpleadoPorHoras(string primerNombre, string apellidoPaterno, string NumSegSocial, decimal sueldoPorHoras, decimal horasTrabajadas)
               : base(primerNombre, apellidoPaterno, NumSegSocial)
            {
                SueldoPorHoras = sueldoPorHoras;
                HorasTrabajadas = horasTrabajadas;
            }

            public override decimal CalcularIngreso()
            {
                if (HorasTrabajadas <= 40)
                {
                    return SueldoPorHoras * HorasTrabajadas;
                }
                else
                {
                    return (40 * SueldoPorHoras) + (HorasTrabajadas - 40) * (SueldoPorHoras * 1.5m);
                }
            }
        }

        public class EmpleadoBaseMasComision : EmpleadoPorComision
        {
            public decimal SalarioBase { get; set; }
            public EmpleadoBaseMasComision(string primerNombre, string apellidoPaterno, string NumSegSocial, decimal ventasBrutas, decimal tarifaComision, decimal salarioBase)
               : base(primerNombre, apellidoPaterno, NumSegSocial, ventasBrutas, tarifaComision)
            {
                SalarioBase = salarioBase;
            }

            public override decimal CalcularIngreso()
            {
                return (TarifaComision * VentasBrutas) + SalarioBase;
            }
        }
        #endregion
    }

    public class Inicio
    {
        public void Ejecutar()
        {
            Empleado empleadoAsalariado = new EmpleadoAsalariado("Juan", "Pérez", "123-45-6789", 800.00m);
            Empleado empleadoPorHoras = new EmpleadoPorHoras("Ana", "Gómez", "987-65-4321", 20.00m, 45);
            Empleado empleadoPorComision = new EmpleadoPorComision("Luis", "Martínez", "555-55-5555", 10000.00m, 0.10m);
            Empleado empleadoBaseMasComision = new EmpleadoBaseMasComision("Maria", "Lopez", "999-99-9999", 5000.00m, 0.05m, 300.00m);

            Console.WriteLine($"{empleadoAsalariado.PrimerNombre} {empleadoAsalariado.ApellidoPaterno}: Ingreso = {empleadoAsalariado.CalcularIngreso():C}");
            Console.WriteLine($"{empleadoPorHoras.PrimerNombre} {empleadoPorHoras.ApellidoPaterno}: Ingreso = {empleadoPorHoras.CalcularIngreso():C}");
            Console.WriteLine($"{empleadoPorComision.PrimerNombre} {empleadoPorComision.ApellidoPaterno}: Ingreso = {empleadoPorComision.CalcularIngreso():C}");
            Console.WriteLine($"{empleadoBaseMasComision.PrimerNombre} {empleadoBaseMasComision.ApellidoPaterno}: Ingreso = {empleadoBaseMasComision.CalcularIngreso():C}");
        }
    }
}

































