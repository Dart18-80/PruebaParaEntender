using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaParaEntender.Models;
namespace PruebaParaEntender.Helpers
{
    public class SingletonController
    {
        private static SingletonController _instance = null;
        public static SingletonController Instance
        {
            get
            {
                if (_instance == null) _instance = new SingletonController();
                return _instance;
            }
        }
        public List<Medicina> Medicinas = new List<Medicina>();
        public List<Medicina> MedicinasFaltantes = new List<Medicina>();
        public List<Medicina> Carrito = new List<Medicina>();
        public Nodo ABMedicinas = new Nodo();
    }
}