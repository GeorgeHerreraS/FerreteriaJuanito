using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.CompilerServices;

namespace FerreteriaJuanito.Exception
{
    

    public class MyException : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        public MyException(IWebHostEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            context.Result = new JsonResult("Fallo en la API" +
                _hostingEnvironment.ApplicationName + "" + _hostingEnvironment.EnvironmentName + " the exception type :" +
                context.Exception.GetType());
        }
    }
}
