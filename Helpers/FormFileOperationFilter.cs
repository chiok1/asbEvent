   using Microsoft.OpenApi.Models;
   using Swashbuckle.AspNetCore.SwaggerGen;
   using System.Linq;

   public class FormFileOperationFilter : IOperationFilter
   {
       public void Apply(OpenApiOperation operation, OperationFilterContext context)
       {
           var formFileParams = context.MethodInfo.GetParameters()
               .Where(p => p.ParameterType == typeof(IFormFile))
               .ToList();

           if (!formFileParams.Any()) return;

           operation.Parameters.Clear();
           operation.RequestBody = new OpenApiRequestBody
           {
               Content = {
                   ["multipart/form-data"] = new OpenApiMediaType
                   {
                       Schema = new OpenApiSchema
                       {
                           Type = "object",
                           Properties = {
                               ["qrCodeImage"] = new OpenApiSchema
                               {
                                   Type = "string",
                                   Format = "binary"
                               }
                           }
                       }
                   }
               }
           };
       }
   }