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

	public LinkChecker()
	{
		/*
		Client.Headers.Add("Content-Type", "text/plain");
		Client.Headers.Add("Accept-Language", "en;q=0.8");
		Client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36");
		Client.Encoding = System.Text.Encoding.Unicode;
		Client.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", CredentialCache.DefaultCredentials);
		*/
	}

	void Method(object inurl)
	{
		var url = (string)inurl;

		try {
			var data = Client.DownloadData(url);
			if (data.Length > 0) {
				Console.WriteLine("Good: " + url);
			} else {
				Console.WriteLine("Bad: " + url);
			}
		} catch (WebException exc) {
			Console.WriteLine(exc.Status + ": " + url);
		} 
		Client.Dispose();
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


