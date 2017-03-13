There are four example application projects of the use of Free.iso8583 library, those are:
Iso8583Client, Iso8583WebClient Iso8583Server and StressTester.
sample.sln file is the solution file that includes all these projects you can open in
Visual Studio. All these projects depend on 
[Install Dir]/bin/Free.iso8583.dll  and  [Install Dir]/src/messagemap-config.xml
file. So, don't change the directory structure in order to compile them successfully.
Or you must fix it yourself.

Especially for Iso8583Server, it also depends on
[Install Dir]/sample/Iso8583Server/bin/log4net.dll  and
[Install Dir]/sample/Iso8583Server/bin/log4net.xml  file. 
These files are from another open source project:
	http://logging.apache.org/log4net
This is the logging tool. It's used to show the example how to use the logging tool with
Free.iso8583 library. This project also uses
[Install Dir]/sample/Iso8583Server/bin/logs
directory to store log files.

The idea behind creation of Iso8583Client and Iso8583Server is in order for both applications
to communicate each other. Iso8583Server becomes the server and Iso8583Client becomes the
client. Of course, they communicate using ISO 8583 message protocol.

To see how they work, firstly compile them. Then run the server via console. If you set the
project in the Debug mode and for Any CPU (default option), type in the console like this:

	> [Install Dir]\sample\Iso8583Server\bin\Debug\Iso8583Server.exe
	
After you see the message starting with 'Starts listening port', it indicates the server has
run successfully. Don't close the console.

You may change the IP address and port to which the server listens, by editing the
configuration file:

[Install Dir]\sample\Iso8583Server\bin\Debug\Iso8583Server.exe.config

Find applicationSettings part. Restart the server after editing the configuration.
You may set "sslCertificateFile" in this configuration to use SSL/TLS communication.
To close the server, you may either close the console or press Ctrl+C

Iso8583Client is a desktop (windows form) application. NOTE, you must compile Iso8583Client
project first to see MessageEditor.cs design to avoid seeing the error message because it uses
custom controls. You may run this application by double clicking

[Install Dir]\sample\Iso8583Client\bin\Debug\Iso8583Client.exe

Iso8583WebClient is a web application that mimics Iso8583Client. It uses ASP.NET MVC 5. Try
to compile it first to avoid seeing errors.

StressTester is the message client application. But specifically it is to make stress test to
the server. It will flood the server by many requests. It is a console application. After compile
the project, type in the different console (from the console where the server runs) like this:

	> [Install Dir]\sample\StressTester\bin\Debug\StressTester.exe 500 127.0.0.1 3107

First argument is the number of request that will be made. The second and the third one are
the server address and port. To use SSL/TLS communication, add "ssl" parameter like:

	> [Install Dir]\sample\StressTester\bin\Debug\StressTester.exe 500 127.0.0.1 3107 ssl
	
After you run this program on the console, there will be a message on the last line, like:
	
	Done. Success: 251, Failed: 249, Total Time = 00:00:02.7812500
	
It shows how many success requests and how many failed ones. To make zero failed, you must tune
the number of maximum connection in the server configuration file (Iso8583Server.exe.config).
Find "max_connections". The higher value needs the more memory that may not be supported by your
hardware. So, decide it wisely.