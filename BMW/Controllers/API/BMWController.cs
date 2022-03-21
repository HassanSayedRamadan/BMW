using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BMW.Models;
using BMW.Models.API.GET;
using BMW.Models.API.POST;
using Newtonsoft.Json;

namespace BMW.Controllers.API
{
    [RoutePrefix("api/BMW")]
    public class BMWController : ApiController
    {
        string ResponsePresProcessing(string Response)
        {
            Response = Response.Replace("{\"vehicle\":", "");
            Response = Response.Substring(0, Response.Length - 1);

            return Response;
        }
        [HttpPost, Route("POSTGenerateToken")]
        public Response<ResponseToken> POSTGenerateToken()
        {
            try
            {
                var ResponseToeknObj = new ResponseToken();

                var CallerObj = new Caller();

                string RequestJsonBody = "username=" + ConfigurationManager.AppSettings["username"].ToString() + "&password=" + ConfigurationManager.AppSettings["password"].ToString() + "&grant_type=password&scope=vehicle_data remote_services";
                Response<string> Response = CallerObj.PostAuth(ConfigurationManager.AppSettings["MainRequest"].ToString() + "/oauth/token", RequestJsonBody);

                if (Response.State)
                {
                    ResponseToeknObj = JsonConvert.DeserializeObject<ResponseToken>(Response.Result);

                    var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                    config.AppSettings.Settings["access_token"].Value = ResponseToeknObj.access_token;
                    config.AppSettings.Settings["refresh_token"].Value = ResponseToeknObj.refresh_token;
                    config.Save();

                    ConfigurationManager.AppSettings.Set("access_token", ResponseToeknObj.access_token);
                    ConfigurationManager.AppSettings.Set("refresh_token", ResponseToeknObj.refresh_token);

                    return Response<ResponseToken>.Valid(ResponseToeknObj);
                }
                else
                {
                    return Response<ResponseToken>.Failed(Response.Description, Response.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                return Response<ResponseToken>.Failed(e);
            }

        }

        [HttpPost, Route("POSTRefreshToken")]
        public Response<ResponseToken> POSTRefreshToken()
        {
            try
            {
                var ResponseTokenObj = new ResponseToken();

                var CallerObj = new Caller();

                string RequestJsonBody = "refresh_token=" + ConfigurationManager.AppSettings["refresh_token"].ToString() + "&grant_type=refresh_token";
                Response<string> Response = CallerObj.PostAuth(ConfigurationManager.AppSettings["MainRequest"].ToString() + "/oauth/token", RequestJsonBody);

                if (Response.State)
                {
                    ResponseTokenObj = JsonConvert.DeserializeObject<ResponseToken>(Response.Result);

                    var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                    config.AppSettings.Settings["access_token"].Value = ResponseTokenObj.access_token;
                    config.AppSettings.Settings["refresh_token"].Value = ResponseTokenObj.refresh_token;
                    config.Save();

                    ConfigurationManager.AppSettings.Set("access_token", ResponseTokenObj.access_token);
                    ConfigurationManager.AppSettings.Set("refresh_token", ResponseTokenObj.refresh_token);

                    return Response<ResponseToken>.Valid(ResponseTokenObj);
                }
                else
                {
                    return Response<ResponseToken>.Failed(Response.Description, Response.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                return Response<ResponseToken>.Failed(e);
            }

        }

        [HttpGet, Route("GetVehicles")]
        public Response<ResponseVehicles> GetVehicles()
        {
            try
            {
                var ResponseVehiclesObj = new ResponseVehicles();

                var CallerObj = new Caller();

                Response<string> Response = CallerObj.GetRequest(ConfigurationManager.AppSettings["MainRequest"].ToString() + "/v1/user/vehicles");

                if (Response.State)
                {
                    ResponseVehiclesObj = JsonConvert.DeserializeObject<ResponseVehicles>(Response.Result);

                    return Response<ResponseVehicles>.Valid(ResponseVehiclesObj);
                }
                else
                {
                    return Response<ResponseVehicles>.Failed(Response.Description, Response.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                return Response<ResponseVehicles>.Failed(e);
            }

        }

        [HttpGet, Route("GetVehicle/{VIN}")]
        public Response<ResponseVehicle> GetVehicle(string VIN)
        {
            try
            {
                var ResponseVehiclesObj = new ResponseVehicle();

                var CallerObj = new Caller();

                Response<string> Response = CallerObj.GetRequest(ConfigurationManager.AppSettings["MainRequest"].ToString() + "/v1/user/vehicles/" + VIN);

                if (Response.State)
                {
                    Response.Result = ResponsePresProcessing(Response.Result);

                    ResponseVehiclesObj = JsonConvert.DeserializeObject<ResponseVehicle>(Response.Result);

                    return Response<ResponseVehicle>.Valid(ResponseVehiclesObj);
                }
                else
                {
                    return Response<ResponseVehicle>.Failed(Response.Description, Response.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                return Response<ResponseVehicle>.Failed(e);
            }

        }

        [HttpGet, Route("GetVehicleStatus/{VIN}")]
        public Response<ResponseVehicleStatus> GetVehicleStatus(string VIN)
        {
            try
            {
                var ResponseVehiclesObj = new ResponseVehicleStatus();

                var CallerObj = new Caller();

                Response<string> Response = CallerObj.GetRequest(ConfigurationManager.AppSettings["MainRequest"].ToString() + "/v1/user/vehicles/" + VIN + "/status");

                if (Response.State)
                {
                    ResponseVehiclesObj = JsonConvert.DeserializeObject<ResponseVehicleStatus>(Response.Result);

                    return Response<ResponseVehicleStatus>.Valid(ResponseVehiclesObj);
                }
                else
                {
                    return Response<ResponseVehicleStatus>.Failed(Response.Description, Response.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                return Response<ResponseVehicleStatus>.Failed(e);
            }

        }

        [HttpPost, Route("POSTLockDoors/{VIN}")]
        public Response<string> POSTLockDoors(string VIN)
        {
            try
            {
                var ResponseExecutionStatusObj = new ResponsePOSTExecutionStatus();

                var CallerObj = new Caller();
                string ServiceType = "DOOR_LOCK";

                string RequestJsonBody = "serviceType=" + ServiceType;
                Response<string> Response = CallerObj.PostRequest(ConfigurationManager.AppSettings["MainRequest"].ToString() + "/v1/user/vehicles/" + VIN + "/executeService", RequestJsonBody);

                if (Response.State)
                {
                    ResponseExecutionStatusObj = JsonConvert.DeserializeObject<ResponsePOSTExecutionStatus>(Response.Result);

                    var ResponseStatus = GetServiceExecutionStatus(VIN, ServiceType, ResponseExecutionStatusObj.executionStatus.eventId);

                    if (ResponseStatus.State)
                        return Response<string>.Valid("Command Executed Successfully");
                    else return Response<string>.Failed(ResponseStatus.Description, ResponseStatus.ExceptionMessage);
                }
                else
                {
                    return Response<string>.Failed(Response.Description, Response.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                return Response<string>.Failed(e);
            }
        }

        [HttpPost, Route("POSTUnlockDoors/{VIN}")]
        public Response<string> POSTUnlockDoors(string VIN)
        {
            try
            {
                var ResponseExecutionStatusObj = new ResponsePOSTExecutionStatus();

                var CallerObj = new Caller();
                string ServiceType = "DOOR_UNLOCK";

                string RequestJsonBody = "serviceType=" + ServiceType;
                Response<string> Response = CallerObj.PostRequest(ConfigurationManager.AppSettings["MainRequest"].ToString() + "/v1/user/vehicles/" + VIN + "/executeService", RequestJsonBody);

                if (Response.State)
                {
                    ResponseExecutionStatusObj = JsonConvert.DeserializeObject<ResponsePOSTExecutionStatus>(Response.Result);

                    var ResponseStatus = GetServiceExecutionStatus(VIN, ServiceType, ResponseExecutionStatusObj.executionStatus.eventId);

                    if (ResponseStatus.State)
                        return Response<string>.Valid("Command Executed Successfully");
                    else return Response<string>.Failed(ResponseStatus.Description, ResponseStatus.ExceptionMessage);
                }
                else
                {
                    return Response<string>.Failed(Response.Description, Response.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                return Response<string>.Failed(e);
            }
        }

        [HttpPost, Route("POSTVEHICLEFINDER/{VIN}")]
        public Response<string> POSTVEHICLEFINDER(string VIN)
        {
            try
            {
                var ResponseExecutionStatusObj = new ResponsePOSTExecutionStatus();

                var CallerObj = new Caller();
                string ServiceType = "VEHICLE_FINDER";

                string RequestJsonBody = "serviceType=" + ServiceType;
                Response<string> Response = CallerObj.PostRequest(ConfigurationManager.AppSettings["MainRequest"].ToString() + "/v1/user/vehicles/" + VIN + "/executeService", RequestJsonBody);

                if (Response.State)
                {
                    ResponseExecutionStatusObj = JsonConvert.DeserializeObject<ResponsePOSTExecutionStatus>(Response.Result);

                    var ResponseStatus = GetServiceExecutionStatus(VIN, ServiceType, ResponseExecutionStatusObj.executionStatus.eventId);

                    if (ResponseStatus.State)
                        return Response<string>.Valid("Command Executed Successfully");
                    else return Response<string>.Failed(ResponseStatus.Description, ResponseStatus.ExceptionMessage);
                }
                else
                {
                    return Response<string>.Failed(Response.Description, Response.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                return Response<string>.Failed(e);
            }
        }

        public Response<string> GetServiceExecutionStatus(string VIN, string ServiceType, string EventId)
        {
            try
            {
                System.Threading.Thread.Sleep(15000);

                var ResponseVehiclesObj = new ResponseGETExecutionStatus();

                var CallerObj = new Caller();

                Response<string> Response = CallerObj.GetRequest(ConfigurationManager.AppSettings["MainRequest"].ToString() + "/v1/user/vehicles/" + VIN + "/serviceExecutionStatus?serviceType=" + ServiceType);

                if (Response.State)
                {
                    ResponseVehiclesObj = JsonConvert.DeserializeObject<ResponseGETExecutionStatus>(Response.Result);

                    if (ResponseVehiclesObj.executionStatus.eventId == EventId)
                    {
                        if (ResponseVehiclesObj.executionStatus.status == "EXECUTED")
                            return Response<string>.Valid(ResponseVehiclesObj.executionStatus.status);
                        else return Response<string>.Failed(ResponseVehiclesObj.executionStatus.status);
                    }
                    else return Response<string>.Failed("Failed");
                }
                else
                {
                    return Response<string>.Failed(Response.Description, Response.ExceptionMessage);
                }
            }
            catch (Exception e)
            {
                return Response<string>.Failed(e);
            }

        }
    }
}
