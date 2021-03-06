﻿using System;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApplicationGRACE
{
    public class Uploads
    {
	    public static void Main()
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("sftp://172.16.100.56");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            
            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential ("test1","test2");
            
            // Copy the contents of the file to the request stream.
            StreamReader sourceStream = new StreamReader("reports.txt");
            byte [] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
    
            Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
    
            response.Close();
        }
    }
}