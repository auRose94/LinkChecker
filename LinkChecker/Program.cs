using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading;
using System.Linq;

class MainClass
{
	public static void Main()
	{
		var args = File.ReadAllLines("URLS.txt");
		var PossibleURLS = new List<string>(args);
		var URLS = new List<string>();

		URLS.AddRange(PossibleURLS.FindAll(str => str.EndsWith(".com", StringComparison.Ordinal)));
		URLS.AddRange(PossibleURLS.FindAll(str => str.EndsWith(".org", StringComparison.Ordinal)));
		URLS.AddRange(PossibleURLS.FindAll(str => str.EndsWith(".net", StringComparison.Ordinal)));

		for (int j = 0; j < URLS.Count; j++) {
			URLS[j] = URLS[j].ToLower();
		}
		URLS = URLS.Distinct().ToList();
		foreach (var url in URLS) {
			var LinkChecker = new LinkChecker();
			var reurl = url;
			if (!url.StartsWith("http://", StringComparison.Ordinal)) {
				reurl = "http://" + url;
			}
			LinkChecker.Exist(reurl);
		}
		Console.WriteLine("Started Checking!");
	}
}

public class LinkChecker
{
	public WebClient Client = new WebClient();

	void Method(object inurl)
	{
		var url = (string)inurl;

		try {
			string htmlCode = Client.DownloadString(url);
			if (!string.IsNullOrWhiteSpace(htmlCode)) {
				Console.WriteLine("Good: " + url);
			} else {
				Console.WriteLine("Bad: " + url);
			}
		} catch (WebException exc) {
			switch (exc.Status) {
				case WebExceptionStatus.ProtocolError:
				case WebExceptionStatus.SendFailure:
					Console.WriteLine("Good: " + url);
					break;
				case WebExceptionStatus.NameResolutionFailure:
				case WebExceptionStatus.ReceiveFailure:
				case WebExceptionStatus.ConnectFailure:
					Console.WriteLine("Bad: " + url);
					break;
				default:
					Console.WriteLine(exc.Status + ": " + url);
					break;
			}
		} 
	}

	public void Exist(string url)
	{
		using (Client) {
			try {
				Thread newThread;
				newThread = new Thread(Method);
				newThread.Start(url);
			} catch (AggregateException exc) {
			}
		}
	}


}


