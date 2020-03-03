using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PruebaParaEntender.Helpers;
namespace PruebaParaEntender.Models
{
    public class Medicina
    {
         public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string casaproductora { get; set; }
        public double precio { get; set; }
        public int existencia { get; set; }

        Nodo Raiz = null;
        public bool Save()
        {
            try
            {
                SingletonController.Instance.Medicinas.Add(this);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Save(int existencias)
        {
            try
            {
                if(existencias > 0)
                {
                    SingletonController.Instance.MedicinasFaltantes.Add(this);           
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AñadirArbol(int posición)
        {
            try
            {
                SingletonController.Instance.ABMedicinas.insertar(Raiz,this.nombre, posición,null);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}