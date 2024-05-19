using Omni_Airbus.Utils.Logging;
using System;
using System.Net;
using System.Text;

namespace Omni_Airbus.Model.FIDS
{
	/// <summary>
	/// <c>WebServer</c> exposes a webserver.
	/// </summary>
	internal class FIDSWebServer
	{
		public static readonly string BasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Source", "Repos", "Omni-Airbus", "Omni-Airbus", "View", "WebSite", "fids", "build"); //the base path of the website.
		public static readonly string BaseDebugPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Source", "Repos", "Omni-Airbus", "Omni-Airbus", "View", "WebSite", "fids", "public"); // use this string when debugging the react script
		private static Logger Log = new Logger(0);

		/// <summary>
		/// Initiate the webserver.
		/// </summary>
		public void Host()
		{
			string url = "http://localhost:8080/"; // Change the URL and port as needed
			HttpListener listener = new HttpListener();
			listener.Prefixes.Add(url);
			listener.Start();
			Console.WriteLine($"Listening for requests on {url}");
			Log.Information($"Listening for requests on {url}");

			Run(listener);
		}

		/// <summary>
		/// Run the web server.
		/// </summary>
		/// <param name="listener"></param> 
		private static void Run(HttpListener listener)
		{
			while (true)
			{
				HttpListenerContext context = listener.GetContext();
				HttpListenerResponse response = context.Response;
				HttpListenerRequest request = context.Request;

				string requestUrl = BasePath + request.Url.LocalPath; //the base path with the requested path.
				Log.Information($"request url {requestUrl}");
				TryServeFile(response, requestUrl);
				response.Close();
			}
		}

		/// <summary>
		/// Try to serve the file.
		/// </summary>
		/// <param name="response"></param>
		/// <param name="requestUrl"></param>
		private static void TryServeFile(HttpListenerResponse response, string requestUrl)
		{
			if (Directory.Exists(requestUrl))
			{
				ServeFile(response, BasePath + @"\index.html", "html");
			}
			else if (File.Exists(requestUrl))
			{
				switch (Path.GetExtension(requestUrl).ToLower())
				{
					case ".html":
						ServeFile(response, requestUrl, "html");
						break;
					case ".css":
						ServeFile(response, requestUrl, "css");
						break;
					case ".js":
						ServeFile(response, requestUrl, "js");
						break;
					case ".json":
						ServeFile(response, requestUrl, "json");
						break;
					case ".svg":
						ServeFile(response, requestUrl, "svg");
						break;
					case ".png":
						ServeFile(response, requestUrl, "png");
						break;
					case ".jpg":
						ServeFile(response, requestUrl, "jpg");
						break;
					case ".map":
						ServeFile(response, requestUrl, "map");
						break;
					default:
						ServeFile(response, BasePath + @"\index.html", "html");
						break;
				}
			}
			else
			{
				ServeFile(response, BasePath + @"\index.html", "html");
			}
		}

		/// <summary>
		/// Serve the file to the browser.
		/// </summary>
		/// <param name="response"></param>
		/// <param name="fileName"></param>
		/// <param name="fileType"></param>
		private static void ServeFile(HttpListenerResponse response, string fileName, string fileType)
		{
			SetContentType(response, fileType);
			SetNoCacheHeaders(response);

			// Construct the path to the file
			string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

			try
			{
				if (File.Exists(filePath))
				{
					// Read the file content
					string content = File.ReadAllText(filePath);

					// Convert the string to bytes using UTF-8 encoding
					byte[] buffer = Encoding.UTF8.GetBytes(content);

					// write the content to the response stream 
					response.ContentLength64 = buffer.Length;
					response.OutputStream.Write(buffer, 0, buffer.Length);
				}
				else
				{
					// Handle 404 Not Found for missing files
				}
				response.StatusCode = (int)HttpStatusCode.NotFound;
			}
			catch (Exception ex) { }
		}

		/// <summary>
		/// make sure the site is not getting catched data when it updates.
		/// </summary>
		/// <param name="response"></param>
		private static void SetNoCacheHeaders(HttpListenerResponse response)
		{
			response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
			response.Headers["Pragma"] = "no-cache";
			response.Headers["Expires"] = "0";
		}

		/// <summary>
		/// Set the content type of the response.
		/// </summary>
		/// <param name="response"></param>
		/// <param name="fileType"></param>
		private static void SetContentType(HttpListenerResponse response, string fileType)
		{
			switch (fileType)
			{
				case "html":
					response.ContentType = "text/html; charset=utf-8";
					break;
				case "css":
					response.ContentType = "text/css";
					break;
				case "js":
					response.ContentType = "application/javascript; charset=utf-8";
					break;
				case "json":
					response.ContentType = "application/json; charset=utf-8";
					break;
				case "svg":
					response.ContentType = "image/svg+xml";
					break;
				case "png":
					response.ContentType = "image/png";
					break;
				case "jpg":
					response.ContentType = "image/jpeg";
					break;
				case "map": // Correct content type for source map files
					response.ContentType = "application/json; charset=utf-8";
					break;
				default:
					response.ContentType = "application/octet-stream";
					break;
			}
		}
	}
}
