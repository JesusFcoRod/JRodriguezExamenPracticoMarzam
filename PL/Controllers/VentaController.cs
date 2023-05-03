using DL;
using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        public ActionResult MedicamentoGetAll()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            ML.Result result = BL.Medicamento.GetAll();

            if (result.Correct)
            {
                medicamento.Medicamentos = result.Objects;
                return View(medicamento);
            }
            return View(medicamento);
        }
        //COMPRAS
        public ActionResult Carrito()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CartPost(ML.Medicamento medicamento)
        {
            //OBTENEMOS IMAGEN DEL MEDICAMENTO POR EL METODO GETBYID
            int IdMed = medicamento.IdMedicamento;
            ML.Result resultImg = BL.Medicamento.GetById(IdMed);
            ML.Medicamento med2 = new ML.Medicamento();
            med2 = (ML.Medicamento)resultImg.Object;
            medicamento.Imagen = med2.Imagen;
            //----------------------------------------------------------

            bool existe = false;
            ML.VentaMedicamento ventaMedicamento = new ML.VentaMedicamento();
            ventaMedicamento.Ventas = new List<object>();

            if (HttpContext.Session.GetString("Medicamento") == null)
            {
                medicamento.Cantidad = medicamento.Cantidad = 1;
                medicamento.SubTotal = medicamento.Precio * medicamento.Cantidad;
                medicamento.Imagen = medicamento.Imagen;
                ventaMedicamento.Ventas.Add(medicamento);

                HttpContext.Session.SetString("Medicamento", Newtonsoft.Json.JsonConvert.SerializeObject(ventaMedicamento.Ventas));
                var session = HttpContext.Session.GetString("Medicamento");

            }
            else
            {
                GetCarrito(ventaMedicamento);
                foreach (ML.Medicamento venta in ventaMedicamento.Ventas.ToList())
                {
                    if (medicamento.IdMedicamento == venta.IdMedicamento)
                    {
                        venta.Cantidad = venta.Cantidad + 1;
                        venta.SubTotal = venta.Precio * venta.Cantidad;
                        venta.Imagen = venta.Imagen;
                        existe = true;
                    }
                    else
                    {
                        existe = false;
                    }

                    if (existe == true)
                    {
                        break;
                    }
                }
                if (existe == false)
                {
                    medicamento.Cantidad = medicamento.Cantidad = 1;
                    medicamento.SubTotal = medicamento.Cantidad * medicamento.Precio;
                    medicamento.Imagen = medicamento.Imagen;
                    ventaMedicamento.Ventas.Add(medicamento);
                }
                HttpContext.Session.SetString("Medicamento", Newtonsoft.Json.JsonConvert.SerializeObject(ventaMedicamento.Ventas));
            }
            if (HttpContext.Session.GetString("Producto") == null)
            {
                ViewBag.Message = "Medicamento agregado al carrito";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "Error al intentar agregar el medicamento al carrito";
                return PartialView("Modal");
            }
        }

        [HttpGet]
        public ActionResult ResumenCompra(ML.VentaMedicamento ventaMedicamento)
        {
            decimal TotalPagar = 0;

            if (HttpContext.Session.GetString("Medicamento") == null)
            {
                return View();
            }
            else
            {
                ventaMedicamento.Ventas = new List<object>();
                GetCarrito(ventaMedicamento);

                TotalPagar = GetTotalPagar(ventaMedicamento);
                ventaMedicamento.Total = TotalPagar;
            }
            return View(ventaMedicamento);
        }

        public ML.VentaMedicamento GetCarrito(ML.VentaMedicamento ventaMedicamento)
        {
            var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Medicamento"));

            foreach (var obj in ventaSession)
            {
                ML.Medicamento objMedicamento = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Medicamento>(obj.ToString());
                ventaMedicamento.Ventas.Add(objMedicamento);
            }
            return ventaMedicamento;
        }

        public decimal GetTotalPagar(ML.VentaMedicamento VentaMedicamento)
        {
            decimal TotalPagar = 0;
            decimal[] Totales = new decimal[VentaMedicamento.Ventas.Count()];// Creamos arreglo del tam de la lista de productos

            for (int i = 0; i<Totales.Count(); i++)//recorremos la lista de productos en el carrito
            {
                ML.Medicamento medicamento = new ML.Medicamento();
                medicamento = (ML.Medicamento)VentaMedicamento.Ventas[i];
                decimal SubT = medicamento.SubTotal;
                Totales[i] = SubT;//agregamos los subtotales al arreglos

            }

            TotalPagar = Totales.Sum();//sumamos los subtotales del arreglo
            return TotalPagar;
        }

        [HttpGet]
        public ActionResult PagarCarrito()
        {           
            return PartialView("CompraFinalizada");
        }

        [HttpGet]
        public ActionResult FormularioPago()
        {
            HttpContext.Session.Clear();
            return PartialView("FormularioPago");
        }

        [HttpGet]
        public ActionResult Delete(int IdMedicamento)
        {
            bool existe = false;
            ML.VentaMedicamento ventaMedicamento = new ML.VentaMedicamento();
            ventaMedicamento.Ventas = new List<object>();

            if (HttpContext.Session.GetString("Medicamento") == null)
            {
                return View();
            }
            GetCarrito(ventaMedicamento);
            foreach (ML.Medicamento venta in ventaMedicamento.Ventas.ToList())
            {
                if (IdMedicamento == venta.IdMedicamento)
                {
                    venta.Cantidad = venta.Cantidad - 1;
                    venta.SubTotal = venta.Precio * venta.Cantidad;
                    venta.Imagen = venta.Imagen;
                    existe = true;

                    if (venta.Cantidad == 0)
                    {
                        ventaMedicamento.Ventas.Remove(venta);
                    }
                }
                else
                {
                    existe = false;
                }

                if (existe == true)
                {
                    break;
                }
            }
            HttpContext.Session.SetString("Medicamento", Newtonsoft.Json.JsonConvert.SerializeObject(ventaMedicamento.Ventas));
            ViewBag.Message = "Medicamento retirado de su carrito";
            return PartialView("Modal");
        }

        [HttpGet]

        public ActionResult Add(int IdMedicamento)
        {
            bool existe = false;
            ML.VentaMedicamento ventaMedicamento = new ML.VentaMedicamento();
            ventaMedicamento.Ventas = new List<object>();

            if (HttpContext.Session.GetString("Medicamento") == null)
            {
                return View();
            }
            GetCarrito(ventaMedicamento);
            foreach (ML.Medicamento venta in ventaMedicamento.Ventas.ToList())
            {
                if (IdMedicamento == venta.IdMedicamento)
                {
                    venta.Cantidad = venta.Cantidad + 1;
                    venta.SubTotal = venta.Precio * venta.Cantidad;
                    venta.Imagen = venta.Imagen;
                    existe = true;
                }
                else
                {
                    existe = false;
                }
                if (existe == true)
                {
                    break;
                }
            }
            HttpContext.Session.SetString("Medicamento", Newtonsoft.Json.JsonConvert.SerializeObject(ventaMedicamento.Ventas));
            ViewBag.Message = "Medicamento agregado a su carrito";
            return PartialView("Modal");
        }


    }
}
