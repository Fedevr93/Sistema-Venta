using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Response;
using WSVenta.Models.Request;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;


//CRUD
namespace WSVenta.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        //READ
        [HttpGet]
        [EnableCors("MiCors")]
        public IActionResult Get()
        {

            Respuesta oRespuesta = new Respuesta();
            //Regresar la lista de datos
            try
            {
                using (VentasRealContext db = new VentasRealContext())
                {
                    var lst = db.Clientes.OrderByDescending(d=>d.Id).ToList(); //var el tipo de la variable es definido por su contenido (valor)
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                 
                }
            } 
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        //CREATE
        [HttpPost]
        public IActionResult Add(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {

                //Insercion en Entity Framework
                using(VentasRealContext db = new VentasRealContext())
                {
                    //Creamos un objeto de la clase (Creada en entity) correspondiente a la tabla 
                    //en la quequeramos insertar la informacion, en este caso un nuevo cliente:
                    Cliente oCliente = new Cliente();
                    //Se setea el objeto con los valores del atributo recibido 
                    oCliente.Nombre = oModel.Nombre;
          
                    //Agregamos nuestro objeto a la tabla clientes de la base de datos
                    db.Clientes.Add(oCliente);
                    //Sentencia - metodo que GUARDA los cambios realizados en la base de datos
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
                
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        //UPDATE
        [HttpPut]
        public IActionResult Edit(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {

                //UPDATE en Entity Framework
                using (VentasRealContext db = new VentasRealContext())
                {

                    //Se busca el dato a editar a travez del objeto recibido oModel
                    Cliente oCliente = db.Clientes.Find(oModel.Id);

                    oCliente.Nombre = oModel.Nombre;

                    db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }

            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        [HttpDelete("{Id}")]

        public IActionResult Delete(int Id)
        {
            Respuesta oRepuesta = new Respuesta();

            try
            {
                using(VentasRealContext db = new VentasRealContext())
                {
                    Cliente oCliente = db.Clientes.Find(Id);

                    db.Remove(oCliente);

                    db.SaveChanges();
                    oRepuesta.Exito = 1;


                }
            }
            catch (Exception ex)
            {
                oRepuesta.Mensaje = ex.Message;
            }

            return Ok(oRepuesta);
        }
    }


      
}   
