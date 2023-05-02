using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MedicamentoController : Controller
    {
        //Inyeccion de dependencias-- patron de diseño
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public MedicamentoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            ML.Medicamento medicamento = new ML.Medicamento();

            result.Objects = new List<object>();

            try
            {
                using (var Client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    Client.BaseAddress = new Uri(urlApi);

                    var responseTask = Client.GetAsync("Medicamento/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Medicamento resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Medicamento>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    medicamento.Medicamentos = result.Objects;
                }

            }
            catch (Exception ex)
            {

            }
            return View(medicamento);
        }

        [HttpGet]
        public ActionResult Form(int? IdMedicamento)
        {
            ML.Medicamento medicamento = new ML.Medicamento();

            if (IdMedicamento != null)
            {
                medicamento.IdMedicamento = IdMedicamento.Value;
                ML.Result result = new ML.Result();

                try
                {
                    using (var Client = new HttpClient())
                    {
                        string urlApi = _configuration["urlApi"];
                        Client.BaseAddress = new Uri(urlApi);

                        var responseTask = Client.GetAsync("Medicamento/GetById/" + IdMedicamento);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            string medicamentoCast = readTask.Result.Object.ToString();

                            ML.Medicamento resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Medicamento>(medicamentoCast);
                            result.Object = resultItem;
                            result.Correct = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }
                if (result.Correct)
                {
                    medicamento = (ML.Medicamento)result.Object;
                    return View(medicamento);
                }
                else
                {
                    ViewBag.Message = "Ocurrio algo al consultar la informacion del Medicamento" + result.ErrorMessage;
                    return View("Modal");
                }
            }
            else
            {
                return View(medicamento);
            }
        }

        [HttpPost]

        public ActionResult Form(ML.Medicamento medicamento)
        {
            //PARA IMAGEN
            IFormFile file = Request.Form.Files["ImgMed"];

            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);

                medicamento.Imagen = Convert.ToBase64String(imagen);
            }

            using (var client = new HttpClient())
            {
                if (medicamento.IdMedicamento == 0)
                {

                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Medicamento>("Medicamento/Add", medicamento);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se ha registrado el Medicamento";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha registrado el Medicamento";
                        return PartialView("Modal");
                    }

                }
                else
                {
                    client.BaseAddress = new Uri(_configuration["urlApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Medicamento>("Medicamento/Update/", medicamento);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se ha actualizado el usuario";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha registrado el usuario";
                        return PartialView("Modal");
                    }

                }

            }
        }


        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            return bytes;
        }
    }
}
