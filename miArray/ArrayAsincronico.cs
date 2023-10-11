namespace miArray
{
    public class ArrayAsincronico
    {
        private int[] _array;
        private int _numerosAleatorio;
        private Random _random;
        private object _bloqueo = new object();

        public ArrayAsincronico(int tamañoArray, int numeroAleatorio)
        {
            _array = new int[tamañoArray];

            _numerosAleatorio = numeroAleatorio;

            _random = new Random();

            LlenarArrayConNumerosAleatorios();

        }

        private void LlenarArrayConNumerosAleatorios()
        {

            for (int i = 0; i < _array.Length; i++)
            {
                lock (_bloqueo)
                {
                    _array[i] = _random.Next(0, _numerosAleatorio + 1);
                }
            }

        }

        public int BuscarMayorPrimeraMitad()
        {
            var primeraMitad = _array.Length / 2;
            var mayorEnPrimeraMitad = _array[0];


            for (var i = 0; i < primeraMitad; i++)
            {
                lock (_bloqueo)
                {
                    if (_array[i] > mayorEnPrimeraMitad)
                    { mayorEnPrimeraMitad = _array[i]; }

                }

            }
            return mayorEnPrimeraMitad;
        }



        public int BuscarMayorSegundaMitad()
        {

            var primeraMitad = _array.Length / 2;
            var mayorEnSegundaMitad = _array[0];

            for (var i = primeraMitad; i < _array.Length; i++)
            {
                lock (_bloqueo)
                {
                    if (_array[i] > mayorEnSegundaMitad)
                    { mayorEnSegundaMitad = _array[i]; }
                }
            }

            return mayorEnSegundaMitad;

        }

        public async Task<int> BuscarMayorDelArrayCompleto()
        {

            Task<int> tarea1 = Task.Run(() => BuscarMayorPrimeraMitad());
            Task<int> tarea2 = Task.Run(() => BuscarMayorSegundaMitad());

            await Task.WhenAll(tarea1, tarea2);

            int numeroArrayPrimeramitad = tarea1.Result;
            int numeroArraySegundaMitad = tarea2.Result;


            if (numeroArrayPrimeramitad < numeroArraySegundaMitad)
            {
                Console.WriteLine($"El numero mayor se encontro en la segunda mitad y es: {numeroArraySegundaMitad}");
                return numeroArraySegundaMitad;
            }
            else
            {
                Console.WriteLine($"El numero mayor se encontro en la primera mitad y es: {numeroArrayPrimeramitad}");
                return numeroArrayPrimeramitad;
            }
        }
    }
}