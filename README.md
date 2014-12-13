LinkChecker
===========

Takes a txt file named "URLS.txt" and outputs a list of both good and bad links.
Cavet: Doesn't handle paths. i.e. google.com/doesntwork or C:/Windows/

Example of a URL.txt file

```
bannana
popsicle
google.com
http://twitter.com
www.facebook.com
doesntexist.org
doesntexist.org
bbbbbooonnnnaannnaaa.com
```

Example output

```
Started Checking!
NameResolutionFailure: http://bbbbbooonnnnaannnaaa.com
SendFailure: http://twitter.com
Good: http://google.com
SendFailure: http://www.facebook.com
Good: http://doesntexist.org
Press any key to continue . . .
```

Errors:

You may recieve an error but that doesn't mean the server/site doesn't exist.
Good just means a site responed with a html file, even sites that have a "Domain available" site will be considered as existing.
NameResolutionFailure means that a server/site doesn't exist
SendFailure means that the site requires or is able to use a authinctication header to get a site.
You can find out more @ http://msdn.microsoft.com/en-us/library/system.net.webexceptionstatus%28v=vs.110%29.aspx