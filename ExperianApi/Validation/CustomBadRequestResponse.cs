using ExperianApi.Models.Response.PhotoAlbum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExperianApi.Validation
{
    public class CustomBadRequestResponse : PhotoAlbumResponse
    {
        public CustomBadRequestResponse(ActionContext context) 
        {
            Message = $"One of Validation Errors Occured: {this.ConstructErrorMessages(context)}";
        }

        //src https://dotnetblog.asphostportal.com/how-to-custom-automatic-http-400-error-response-in-asp-net-core-web-api/
        private string ConstructErrorMessages(ActionContext context)
        {
            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var msg = GetErrorMessage(errors[0]);

                        Message += $"{(key, msg)}";
                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = GetErrorMessage(errors[i]);
                        }

                        Message+= $"{(key, errorMessages)}";
                    }
                }
            }

            return Message;
        }

        string GetErrorMessage(ModelError error)
        {
            return string.IsNullOrEmpty(error.ErrorMessage) ? "The input was not valid" : error.ErrorMessage;
        }
    }
}
