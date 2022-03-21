using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMW.Models
{
    public class Response<Type>
    {
        public Type Result { get; set; }
        public string Description { get; set; }
        public string ExceptionMessage { get; set; }
        public bool State { get; set; }

        /// <summary>
        /// Valids the specified result object.
        /// </summary>
        /// <typeparam name="Type">The type of the ype.</typeparam>
        /// <param name="resultObject">The result object.</param>
        /// <returns></returns>
        public static Response<Type> Valid<Type>(Type resultObject)
        {
            return new Response<Type>
            {
                Result = resultObject,
                Description = Models.Description.Succeeded,
                State = true
            };
        }

        public static Response<Type> Valid<Type>(Type resultObject, string Message)
        {
            return new Response<Type>
            {
                Result = resultObject,
                Description = Message,
                State = true
            };
        }


        /// <summary>
        /// Nots the found.
        /// </summary>
        /// <returns></returns>
        public static Response<Type> NotFound()
        {
            return new Response<Type>
            {
                Result = default(Type),
                Description = Models.Description.NotFound,
                State = false
            };
        }
        /// <summary>
        /// Nots the found.
        /// </summary>
        /// <returns></returns>
        public static Response<Type> NotFound(string message)
        {
            return new Response<Type>
            {
                Result = default(Type),
                Description = message,
                State = true
            };
        }
        /// <summary>
        /// Faileds the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static Response<Type> Failed(Exception exception)
        {
            string exp = exception.Message + (exception.InnerException == null ? "" : " InnerException: " + exception.InnerException.ToString());
            return new Response<Type>
            {
                Result = default(Type),
                Description = "Unable to Process Your Request.",
                ExceptionMessage = exp,
                State = false
            };
        }


        /// <summary>
        /// Faileds the specified failed message.
        /// </summary>
        /// <param name="failedMessage">The failed message.</param>
        /// <returns></returns>
        public static Response<Type> Failed(string failedMessage)
        {
            return new Response<Type>
            {
                Result = default(Type),
                Description = failedMessage,
                ExceptionMessage = "",
                State = false
            };
        }
        public static Response<Type> Failed(string failedMessage, string exceptionMessage)
        {
            return new Response<Type>
            {
                Result = default(Type),
                Description = failedMessage,
                ExceptionMessage = exceptionMessage,
                State = false
            };
        }
    }
}