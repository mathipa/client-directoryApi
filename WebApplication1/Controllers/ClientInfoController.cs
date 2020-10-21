using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClientInfoController : ApiController
    {
        // Get all the clients
        public HttpResponseMessage Get()
        {
            string query = @"select ClientId,FirstName,LastName,MobileNumber,IdNumber,PhysicalAddress from dbo.ClientInfo";

            DataTable table = new DataTable();
            using(var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ClientInfoDB"].ConnectionString))
                using(var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        // Add Client
        public string Post(ClientInfo client)
        {
            try
            {
                string query = @"insert into dbo.ClientInfo values 
                (
                    '" + client.FirstName+ @"'
                    ,'" + client.LastName + @"'
                    ,'" + client.MobileNumber + @"'
                    ,'" + client.IdNumber + @"'
                    ,'" + client.PhysicalAddress + @"'
                )
                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ClientInfoDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Client Added Successfully!";
            }
            catch
            {
                return "Failed to Add!";
            }
        }

        //Update Client
        public string Put(ClientInfo client)
        {
            try
            {
                string query = @"
                update dbo.ClientInfo set 
                FirstName='" + client.FirstName + @"'
                ,LastName='" + client.LastName + @"'
                ,MobileNumber='" + client.MobileNumber + @"'
                ,IdNumber='" + client.IdNumber + @"'
                ,PhysicalAddress ='" + client.PhysicalAddress + @"'
                where ClientId=" + client.ClientId + @"
                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ClientInfoDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Client Updated Successfully!";
            }
            catch
            {
                return "Failed to Update!";
            }
        }

        //Delete Client
        public string Delete(int id)
        {
            try
            {
                string query = @"delete from dbo.ClientInfo where ClientId=" + id + @" ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ClientInfoDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Client Deleted Successfully!";
            }
            catch
            {
                return "Failed to Delete!";
            }
        }

    }
}
