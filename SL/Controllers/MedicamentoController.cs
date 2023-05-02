using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;

namespace SL.Controllers
{
    public class MedicamentoController : Controller
    {
        [HttpGet]
        [Route("api/Medicamento/GetAll")]
        public ActionResult GetAll()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            ML.Result result = BL.Medicamento.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet]
        [Route("api/Medicamento/GetById/{IdMedicamento}")]
        public ActionResult GetById(int IdMedicamento)
        {

            ML.Result result = BL.Medicamento.GetById(IdMedicamento);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Medicamento/Add")]
        public ActionResult Add([FromBody] ML.Medicamento medicamento)
        {
            ML.Result result = BL.Medicamento.Add(medicamento);

            if (result.Correct)
            {
                return Ok(result);
            }
            else { return NotFound(result); }
        }

        [HttpPost]
        [Route("api/Medicamento/Update")]
        public ActionResult Update([FromBody] ML.Medicamento medicamento)
        {
            ML.Result result = BL.Medicamento.Update(medicamento);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpGet]
        [Route("api/Medicamento/Delete/{IdMedicamento}")]
        public ActionResult Delete(int IdMedicamento)
        {
            ML.Result result = BL.Medicamento.Delete(IdMedicamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
