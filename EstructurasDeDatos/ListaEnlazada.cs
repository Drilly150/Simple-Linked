using System.Collections;

namespace ProyectoLES.EstructurasDeDatos
{
    // Nodo genérico para la lista enlazada
    public class Nodo<T>
    {
        public T Dato { get; set; }
        public Nodo<T> Siguiente { get; set; }

        public Nodo(T dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }

    // Lista enlazada simple genérica
    public class ListaEnlazada<T> : IEnumerable<T>
    {
        public Nodo<T> Cabeza { get; private set; }

        public void Agregar(T dato)
        {
            var nuevoNodo = new Nodo<T>(dato);
            if (Cabeza == null)
            {
                Cabeza = nuevoNodo;
            }
            else
            {
                var actual = Cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoNodo;
            }
        }

        public T Buscar(Func<T, bool> predicado)
        {
            var actual = Cabeza;
            while (actual != null)
            {
                if (predicado(actual.Dato))
                {
                    return actual.Dato;
                }
                actual = actual.Siguiente;
            }
            return default(T); // Retorna null para tipos de referencia
        }
        
        public void Eliminar(Func<T, bool> predicado)
        {
            if (Cabeza == null) return;

            if (predicado(Cabeza.Dato))
            {
                Cabeza = Cabeza.Siguiente;
                return;
            }

            var actual = Cabeza;
            while (actual.Siguiente != null)
            {
                if (predicado(actual.Siguiente.Dato))
                {
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    return;
                }
                actual = actual.Siguiente;
            }
        }

        // Implementación de IEnumerable para poder usar foreach
        public IEnumerator<T> GetEnumerator()
        {
            var actual = Cabeza;
            while (actual != null)
            {
                yield return actual.Dato;
                actual = actual.Siguiente;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}