using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using PruebaParaEntender.Helpers;
using PruebaParaEntender.Models;
namespace PruebaParaEntender.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult About(HttpPostedFileBase Archivo)
        {
            try
            {
                string CaminoArchivo = string.Empty;
                if (Archivo != null)
                {
                    string Camino = Server.MapPath("~/MedicinasCSV/");
                    if (!Directory.Exists(Camino))
                    {
                        Directory.CreateDirectory(Camino);
                    }
                    CaminoArchivo = Camino + Path.GetFileName(Archivo.FileName);
                    string Extension = Path.GetExtension(Archivo.FileName);
                    Archivo.SaveAs(CaminoArchivo);
                    string DatosCSV = System.IO.File.ReadAllText(CaminoArchivo);
                    foreach (string Fila in DatosCSV.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(Fila))
                        {
                            string[] DatosS = Regex.Split(Fila, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                            var medicinas = new Medicina
                            {
                                id = Convert.ToInt32(DatosS[0]),
                                nombre = DatosS[1],
                                descripcion = DatosS[2],
                                casaproductora = DatosS[3],
                                precio = Convert.ToDouble(DatosS[4].Substring(1, DatosS[4].Length - 1)),
                                existencia = Convert.ToInt32(DatosS[5])
                            };
                            medicinas.Save();
                        }
                    }
                    foreach (var item in SingletonController.Instance.Medicinas)
                    {
                        if (item.existencia > 0)
                        {
                            SingletonController.Instance.ABMedicinas.insertar(SingletonController.Instance.ABMedicinas.Raíz(),item.nombre,item.id,null);
                        }
                        else
                        {
                            item.Save(item.existencia);
                        }
                    }
                }
                return RedirectToAction("About");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult About()
        {
            return View(SingletonController.Instance.Medicinas);
        }
        [HttpPost]
        public ActionResult Contact(string Nombre)
        {
            try
            {

                Nodo Result = SingletonController.Instance.ABMedicinas.buscar(SingletonController.Instance.ABMedicinas.Raíz(), Nombre);

                if (Result == null)
                {
                    ViewBag.MyMessage = "No se encontró el objeto";
                    return View();
                }
                else
                {
                    ViewBag.MyMessage = Result.nombre;
                    return View();
                }
            }
            catch
            {
                ViewBag.MyMessage = "No se encontró el objeto";
                return View();
            }

        }
    }
}