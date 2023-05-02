using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Persona
    {
        public static ML.Result GetByUserName(ML.Persona persona)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezExamenPracticoMarzamContext contex = new DL.JrodriguezExamenPracticoMarzamContext())
                {
                    var query = contex.Personas.FromSqlRaw($"[PersondaGetByUserName] {persona.UserName}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        persona = new ML.Persona();
                        persona.UserName = query.UserName;
                        persona.Password = query.Password;

                        result.Object = persona;
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
    }
}
