using BlazorAppOtus.Client.ViewModels;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BlazorAppOtos.Client.Helpers
{

    public static class IJSExtensions
    {   
        public static async Task<object> Confirmation(this IJSRuntime js, ConfirmationModelView model)
        {
            return await js.InvokeAsync<object>("swal.fire", model.Titulo,model.Mensaje,model.TipoMensaje.ToString());
        }
        public static async Task<object> UnauthorizedMessage(this IJSRuntime js)
        {
            return await js.InvokeAsync<object>("swal.fire","Error" , "Usted no esta autorizado para ver el contenido de esta pagina",ConfirmationModelView.TiposMensaje.error);
        }
        public static async Task<object> DBErrorMessage(this IJSRuntime js,string error)
        {
            return await js.InvokeAsync<object>("swal.fire", "Error", error, ConfirmationModelView.TiposMensaje.error.ToString());
        }
        public async static Task<bool> ConfirmDialog(this IJSRuntime js,ConfirmationModelView model) { 
            return await js.InvokeAsync<bool>("CustomConfirmDialog", model.Titulo,model.Mensaje,model.Action,model.TipoMensaje.ToString());
        } 
        public async static Task<object> LogFailed(this IJSRuntime js,ConfirmationModelView model) 
        {
            return await js.InvokeAsync<object>("LoginFailed", model.Titulo, model.Mensaje, model.TipoMensaje.ToString(), model.Action);
        }
        public async static Task<object> Mixin(this IJSRuntime js, ConfirmationModelView model)
        {
            return await js.InvokeAsync<object>("Mixin", model.Mensaje, model.TipoMensaje.ToString());
        }
    }
}
