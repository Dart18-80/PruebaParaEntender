using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaParaEntender.Models
{
    public class Nodo
    {
        public int ID { get; set; }
        public string nombre { get; set; }
        public Nodo izquierda { get; set; }
        public Nodo derecha { get; set; }
        public Nodo padre { get; set; }

        Nodo arbol;
        public Nodo Raíz()
        {
            return this.arbol;
        }
        Nodo CrearNodo(string nombre, int exis, Nodo padre)
        {
            Nodo nuevo = new Nodo();
            nuevo.nombre = nombre;
            nuevo.ID = exis;
            nuevo.derecha = null;
            nuevo.izquierda = null;
            nuevo.padre = padre;
            return nuevo;
        }
        public void insertar(Nodo prueba, string nombre, int exis, Nodo padre)
        {
            if (prueba == null)
            {
                Nodo nuevo = CrearNodo(nombre, exis, padre);
                arbol = nuevo;
            }
            else
            {
                int comprobar = prueba.nombre.CompareTo(nombre);
                if (comprobar < 0)
                {
                    insertar(prueba.izquierda, nombre, exis, arbol);
                }
                else
                {
                    insertar(prueba.derecha, nombre, exis, arbol);
                }
            }
        }
        public Nodo buscar(Nodo arbol, string nombre)
        {
            int comprobar = arbol.nombre.CompareTo(nombre);

            if (arbol == null)
            {
                return null;
            }
            else if (comprobar == 0)
            {
                return arbol;
            }
            else if (comprobar < 0)
            {
                return buscar(arbol.izquierda, nombre);
            }
            else if (comprobar > 0)
            {
                return buscar(arbol.derecha, nombre);
            }
            else
            {
                return null;
            }

        }

        public void Eliminar(Nodo arbol, string nombre)
        {
            int comprobar = arbol.nombre.CompareTo(nombre);
            if (arbol == null)
            {
                return;
            }
            else if (comprobar < 0)
            {
                Eliminar(arbol.izquierda, nombre);
            }
            else if (comprobar > 0)
            {
                Eliminar(arbol.derecha, nombre);
            }
            else if (comprobar == 0)
            {
                eliminarNodo(arbol);
            }
        }
        Nodo masIzquierdo(Nodo arbol)
        {
            if (arbol == null)
            {
                return null;
            }
            if (arbol.izquierda != null)
            {
                return masIzquierdo(arbol.izquierda);
            }
            else
            {
                return arbol;
            }
        }

        public void remplazar(Nodo arbol, Nodo nuevoRemplazo)
        {
            if (arbol.padre != null)
            {
                if (arbol.nombre == arbol.padre.izquierda.nombre)
                {
                    arbol.padre.izquierda = nuevoRemplazo;
                }
                else if (arbol.nombre == arbol.padre.derecha.nombre)
                {
                    arbol.padre.derecha = nuevoRemplazo;
                }
            }
        }

        public void destruir(Nodo nodo)
        {
            nodo.izquierda = null;
            nodo.derecha = null;
            nodo.padre = null;
            nodo.nombre = null;
            nodo.ID = 0;
        }

        public void eliminarNodo(Nodo elimino)
        {
            if (elimino.izquierda != null && elimino.derecha != null)
            {
                Nodo menor = masIzquierdo(elimino.derecha);
                elimino.nombre = menor.nombre;
                elimino.ID = menor.ID;
                eliminarNodo(menor);
            }
            else if (elimino.izquierda != null)
            {
                remplazar(elimino, elimino.izquierda);
                destruir(elimino);
            }
            else if (elimino.derecha != null)
            {
                remplazar(elimino, elimino.derecha);
                destruir(elimino);
            }
            else
            {
                remplazar(elimino, null);
                destruir(elimino);
            }
        }
    }
}