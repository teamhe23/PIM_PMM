using Domain.Models;
using Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Util;

namespace Facade

{

    class Program

    {
        public static async Task Main()
        {
            do
            {
                try
                {
                    var Services = IoCBootstrapper.Bootstrap().BuildServiceProvider();
                    var PrintService = Services.GetService<IPrinterService>();
                    var pimService = Services.GetService<IPIMService>();
                    //get tramas pendientes
                    PrintService.PrintInfoJson();
                    var lstTrama = pimService.GetTramaPendiente(1);
                    PrintService.Print("Cant de Tramas a procesar: " + lstTrama.Count.ToString());
                    String errorMsg = "Trama ok";
                    Char flg = '0';


                    foreach (var trama in lstTrama)
                    {
                        //aqui insertamos los productos y detalles de cada trama
                        PIM_PRODUCTO objProd = ObtenerObjetoProducto(trama.JSON_Data);
                        //aqui llamamos al metodo para insertar la cabecera del producto
                        if (pimService.InsertaProducto(objProd, trama.Id_Modelo, ref errorMsg))
                        {
                            Int32 order;
                            foreach (var atrib in objProd.LstAtributos)
                            {
                                //aqui insertamos por cada atributo en las tablas
                                pimService.InsertaAtribProducto(objProd.Id_PIM, atrib, ref errorMsg);
                            }
                        }
                        else
                        {//actualizar la trama como no procesada
                            errorMsg = "Errores en la trama";
                            flg = '1';
                        }
                        trama.Mensaje = errorMsg;
                        trama.Flg_Error = flg;


                        pimService.ActualizarEstadoTrama(trama);
                        PrintService.Print("Trama: " + trama.Id_Modelo.ToString() + ", detalle: " + errorMsg);
                    }


                    var lstProductos = pimService.GetProductosPendientes();// aqui devolvemos un listado con los id pim para crear los productos en pmm
                    Int64 child;
                    foreach (var item in lstProductos)
                    {
                        child = 0;
                        if (pimService.CreaProductoPMM(item.Id_PIM_PROD, ref errorMsg, ref child))
                        {
                            errorMsg = "Producto " + item.Id_PIM.ToString() + ", " + errorMsg;
                            item.MENSAJE = errorMsg;
                            item.COD_ERROR = "0";
                        }
                        else
                        {
                            item.MENSAJE = errorMsg;
                            item.COD_ERROR = "1";


                        }
                        pimService.ActualizarEstadoPIMProducto(item, child);
                        PrintService.Print(errorMsg);
                    }






                    var enviarPIM = pimService.GetProductosPIM();//aqui devemos devolver los productos para la respuesta a pim
                    foreach (var item in enviarPIM)
                    {
                        //aqui procedemos a crear un json y enviarlo a la interface de oic 
                        PIM_PRODUCTO_OUT prod = new PIM_PRODUCTO_OUT();








                    }


                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
                System.Threading.Thread.Sleep(10000);
            } while (true);


        }
        private static PIM_PRODUCTO ObtenerObjetoProducto(JsonDocument jSON_Data)
        {
            //jSON_Data = JsonDocument.Parse(File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\productTest.json"));


            JsonElement root = jSON_Data.RootElement;


            var jsonProd = root.GetProperty("productos").EnumerateArray();


            var objProd = new PIM_PRODUCTO();




            objProd.Id_PIM = jsonProd.ElementAt(0).GetProperty("IdPim").ToString();
            objProd.Nombre_PROD = jsonProd.ElementAt(0).GetProperty("NombreProducto").ToString();
            objProd.LstAtributos = new List<PIM_PRODUCTO_ATRIBUTO>();

            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "CodLinea",
                Valor = jsonProd.ElementAt(0).GetProperty("CodigoLineaPromart").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "Proveedor",
                Valor = jsonProd.ElementAt(0).GetProperty("CodigoProveedor").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "Marca",
                Valor = jsonProd.ElementAt(0).GetProperty("Marca").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "TipoMarca",
                Valor = jsonProd.ElementAt(0).GetProperty("TipoMarca").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "TipoNegociacion",
                Valor = jsonProd.ElementAt(0).GetProperty("TipoNegociacion").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "ClusterSurtido",
                Valor = jsonProd.ElementAt(0).GetProperty("ClusterSurtido").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "TipoSurtido",
                Valor = jsonProd.ElementAt(0).GetProperty("TipoSurtido").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "UMI",
                Valor = jsonProd.ElementAt(0).GetProperty("UnidadMedidadInventarioUMI").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "UMV",
                Valor = jsonProd.ElementAt(0).GetProperty("UnidadMedidaVenta").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "PuntoPrecio",
                Valor = jsonProd.ElementAt(0).GetProperty("PuntoPrecio").ToString(),
                Id_PIM = objProd.Id_PIM
            });


            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "PrecioUnitario",
                Valor = jsonProd.ElementAt(0).GetProperty("PrecioUnitario").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "Costo",
                Valor = jsonProd.ElementAt(0).GetProperty("Costo").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "MasterPackCantidad",
                Valor = jsonProd.ElementAt(0).GetProperty("CantidadMasterpack").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "SkuProveedor",
                Valor = jsonProd.ElementAt(0).GetProperty("SkuProveedor").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "EAN",
                Valor = jsonProd.ElementAt(0).GetProperty("EAN").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "TipoEan",
                Valor = jsonProd.ElementAt(0).GetProperty("TipoCodigoEAN").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "DUN14",
                Valor = jsonProd.ElementAt(0).GetProperty("DUN14").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            /*
            C_ATR_PROCEDENCIA       CONSTANT VARCHAR2(50) := 'Procedencia';
            */
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "AtLogist.NumeroPiezas",
                Valor = jsonProd.ElementAt(0).GetProperty("NumeroPiezas").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "MasterPackTipo",
                Valor = jsonProd.ElementAt(0).GetProperty("TipoMasterpack").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "Perecible",
                Valor = jsonProd.ElementAt(0).GetProperty("Perecible").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "AlturaUnidadLogistica",
                Valor = jsonProd.ElementAt(0).GetProperty("AlturaUnidadLogistica").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "AnchoUnidadLogistica",
                Valor = jsonProd.ElementAt(0).GetProperty("AnchoUnidadLogistica").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "ProfundidadUnidadLogistica",
                Valor = jsonProd.ElementAt(0).GetProperty("ProfundidadUnidadLogistica").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "VolumendelaUnidadLogistica",
                Valor = jsonProd.ElementAt(0).GetProperty("VolumenUnidadLogistica").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "PesoUnidadLogistica",
                Valor = jsonProd.ElementAt(0).GetProperty("PesoUnidadLogistica").ToString(),
                Id_PIM = objProd.Id_PIM
            });
            objProd.LstAtributos.Add(new PIM_PRODUCTO_ATRIBUTO()
            {
                Id_ATRIBUTO = "PesoProducto",
                Valor = jsonProd.ElementAt(0).GetProperty("PesoProducto").ToString(),
                Id_PIM = objProd.Id_PIM
            });


            return objProd;
        }

    }

}