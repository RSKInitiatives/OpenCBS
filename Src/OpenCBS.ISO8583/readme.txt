This archive contains Free.iso8583 library (binary and source code), library documentation and example applications.

Directory Structure:

[Install Dir]
    |
    |- bin
    |- docs
    |- sample
    |    |- Iso8583Client
    |    |- Iso8583Server
    |    |- Iso8583WebClient
    |    |- StressTester
    |
    |- src
    |- tutorial
         |- Client
         |- Models
         |- Servers
         |- WebClient

* bin directory contains the binary DLL file of Free.iso8583
* docs directory contains the documentation of the libary. ISO 8583 Message Processor.pdf contains the explanation of the components
  inside the Free.iso8583 library, including the API (Application Programming Interface). There is also a tutorial file.
* sample directory contains example applications. There are four applications inside this directory. Read 
  [Install Dir]/sample/readme.txt  file for further information.
* src directory contains the source code of Free.iso8583. There are Free.iso8583.csproj and Free.iso8583.sln file you can open in the
  case of you want to compile it yourself. The project has been set to store the binary file in [Install Dir]/bin directory if it is
  compiled in the realease mode. You may want to change it in the project properties.
* tutorial directory contains the example code used by the tutorial. The tutorial itself is in docs directory.



DONATION
--------
If you feel this library is useful, please make a donation via PayPal by visiting this link:

https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4ZKPC3URPZ24U
