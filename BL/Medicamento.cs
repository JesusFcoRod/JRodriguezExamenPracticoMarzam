using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Medicamento
    {
        public static ML.Result Add(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezExamenPracticoMarzamContext contex = new DL.JrodriguezExamenPracticoMarzamContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[MedicamentoAdd] '{medicamento.Nombre}',{medicamento.Precio},'{medicamento.Imagen}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else { result.Correct = false; }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result Update(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezExamenPracticoMarzamContext contex = new DL.JrodriguezExamenPracticoMarzamContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[MedicamentoUpdate] {medicamento.IdMedicamento}, '{medicamento.Nombre}',{medicamento.Precio},'{medicamento.Imagen}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result Delete(int IdMedicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezExamenPracticoMarzamContext contex = new DL.JrodriguezExamenPracticoMarzamContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"[MedicamentoDelete] {IdMedicamento}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezExamenPracticoMarzamContext context = new DL.JrodriguezExamenPracticoMarzamContext())
                {
                    var query = context.Medicamentos.FromSqlRaw("[MedicamentoGetAll]").ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Medicamento medicamento = new ML.Medicamento();
                            medicamento.IdMedicamento = item.IdMedicamento;
                            medicamento.Nombre = item.Nombre;
                            medicamento.Precio = item.Precio.Value;
                            medicamento.Imagen = item.Imagen;

                            result.Objects.Add(medicamento);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result GetById(int IdMedicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezExamenPracticoMarzamContext contex = new DL.JrodriguezExamenPracticoMarzamContext())
                {
                    var query = contex.Medicamentos.FromSqlRaw($"[MedicamentosGetById] {IdMedicamento}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.Medicamento medicamento = new ML.Medicamento();
                        medicamento.IdMedicamento = query.IdMedicamento;
                        medicamento.Nombre = query.Nombre;
                        medicamento.Precio = query.Precio.Value;
                        medicamento.Imagen = query.Imagen;

                        result.Object = medicamento;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

    }
}