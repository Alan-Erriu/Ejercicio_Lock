namespace miArray
{
    public class Array
    {
        private int[] _Array;
        private int _numerosAleatorio;
        private Random _generadorAleatorio;
        private object _bloqueo = new object();

        public Array(int tamañoArray, int numeroAleatorio)
        {
            _Array = new int[tamañoArray];

            _numerosAleatorio = numeroAleatorio;

            _generadorAleatorio = new Random();

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

        public int BuscarMayorPrimeraMitad()
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
        public int BuscarMayorSegundaMitad()
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

        public int BuscarMayorDelArrayCompleto()
        {

            int mayorPrimeraMitad = BuscarMayorPrimeraMitad();
            int mayorSegundaMitad = BuscarMayorSegundaMitad();


            if (mayorPrimeraMitad < mayorSegundaMitad)
            {
                Console.WriteLine(mayorSegundaMitad);
                return mayorSegundaMitad;
            }
            else
            {
                Console.WriteLine(mayorPrimeraMitad);
                return mayorPrimeraMitad;
            }
        }
    }
}