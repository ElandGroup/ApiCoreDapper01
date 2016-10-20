using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloApiWithCoreDapper.Common.HttpPack
{
    public static class ControllerExtension
    {
        #region simple

        public static OkObjectResult OkEx(this Controller context, object result)
        {
            return context.Ok(new ResultDto
            {
                success = true,
                result = result,
                error = null
            });
        }
        public static ObjectResult NoContentEx(this Controller context)
        {
            return context.StatusCode((int)HttpStatusCode.Created
                , new ResultDto
                {
                    success = true,
                    result = null,
                    error = null
                });
        }
        public static ObjectResult NotFoundEx(this Controller context)
        {
            return context.StatusCode((int)HttpStatusCode.NotFound
                , new ResultDto
                {
                    success = true,
                    result = null,
                    error = null
                });
        }
        public static BadRequestObjectResult BadRequestEx(this Controller context, string message)
        {
            return context.BadRequest(new ResultDto
            {
                success = false,
                result = null,
                error = new ErrorDto
                {
                    code = 1,
                    message = "A required parameter is missing or doesn't have the right format:" + message,
                    details = null,
                    validationErrors = null
                }
            });
        }
        public static ObjectResult ErrorEx(this Controller context, string errorMessage)
        {
            return context.StatusCode((int)HttpStatusCode.InternalServerError
                , new ResultDto
                {
                    success = false,
                    result = null,
                    error = new ErrorDto
                    {
                        code = 2,
                        message = errorMessage,
                        details = null,
                        validationErrors = null
                    }
                });
        }

        #endregion

        #region Stay

        public static BadRequestObjectResult BadRequestEx(this Controller context, ErrorDto errorDto)
        {
            return context.BadRequest(new ResultDto
            {
                success = false,
                result = null,
                error = errorDto
            });
        }
        public static ObjectResult ErrorEx(this Controller context, ErrorDto errorDto)
        {
            return context.StatusCode((int)HttpStatusCode.InternalServerError
                , new ResultDto
                {
                    success = false,
                    result = null,
                    error = errorDto
                });
        }

        #endregion

    }
}
