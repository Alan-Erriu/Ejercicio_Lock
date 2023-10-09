namespace miArray
{
    public class ArrayAsincronico
    {
        private int[] _Array;
        private int _numerosAleatorio;
        private Random _generadorAleatorio;
        private object _bloqueo = new object();

        public ArrayAsincronico(int tamañoArray, int numeroAleatorio)
        {
            _Array = new int[tamañoArray];

            _numerosAleatorio = numeroAleatorio;

            _generadorAleatorio = new Random(1);

            LlenarArrayConNumerosAleatorios();

        }

        private void LlenarArrayConNumerosAleatorios()
        {
            lock (_bloqueo)
            {
                for (int i = 0; i < _Array.Length; i++)
                {
                    _Array[i] = _generadorAleatorio.Next(0, _numerosAleatorio + 1);
                }
            }
        }

        public async Task<int> BuscarMayorPrimeraMitad()
        {
            lock (_bloqueo)
            {
                var primeraMitad = _Array.Length / 2;
                var mayorEnPrimeraMitad = _Array[0];

                for (var i = 0; i < primeraMitad; i++)
                {
                    if (_Array[i] > mayorEnPrimeraMitad)
                    { mayorEnPrimeraMitad = _Array[i]; }


                }
                return mayorEnPrimeraMitad;
            }
        }
        public async Task<int> BuscarMayorSegundaMitad()
        {
            lock (_bloqueo)
            {
                var primeraMitad = _Array.Length / 2;
                var mayorEnSegundaMitad = _Array[0];

                for (var i = primeraMitad; i < _Array.Length; i++)
                {
                    if (_Array[i] > mayorEnSegundaMitad)
                    { mayorEnSegundaMitad = _Array[i]; }

                }



                return mayorEnSegundaMitad;
            }
        }

        public async Task<int> BuscarMayorDelArrayCompleto()
        {

            Task<int> mayorPrimeraMitad = BuscarMayorPrimeraMitad();
            Task<int> mayorSegundaMitad = BuscarMayorSegundaMitad();



            await Task.WhenAll(mayorPrimeraMitad, mayorSegundaMitad);


            int numeroArray1 = await mayorPrimeraMitad;
            int numeroArray2 = await mayorSegundaMitad;

            if (numeroArray1 < numeroArray2)
            {
                Console.WriteLine(numeroArray2);
                return numeroArray2;
            }
            else
            {
                Console.WriteLine(numeroArray1);
                return numeroArray1;
            }
        }
    }
}