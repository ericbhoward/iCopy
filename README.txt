iCopy
----------------------------
iCopy is a free and open source software that lets you combine your scanner and printer into a powerful, but easy to use photocopier by only pressing a button.
Its simple user interface lets you manage scanner and printer options, like brightness, contrast, number of copies in a couple of seconds.
As it's small and no installation is required, iCopy is also suitable for USB pen drives.

1) Features
2) Requirements
2) Command line parameters
3) Known bugs & solutions
4) Support
5) Please help!

1) Features
---------------------------

# Simple interface
# Scanning mode selection
# Run by pressing scanner button
# Brightness and contrast settings
# Scanner quality setting
# Scan to file function
# Scan Multiple Pages before printing
# Preview function
# Enlargement by percentage function
# Command-line parameters
# No installation needed
# Little hard disk space required
# Compatible with all WIA scanners and all printers

2) System requirements
--------------------------
* Windows XP SP1/Vista
* Microsoft .NET Framework 2.0 or higher
* A WIA (Windows Image Acquisition) compatible scanner

NOTE: unfortunately not all scanners are compatible, if you have troubles with your scanner, please report it to iCopy bug tracker
A list of the known compatible scanners can be found at http://icopy.sourceforge.net/?page_id=174

3) Command line parameters
--------------------------
iCopy can be run as a Windows application, but it has also some command line parameters to copy directly to the printer or to a file

No parameters: standard mode with windows (also if neither /c or /f parameters are passed)

> icopy.exe [/c or /f] [params]
	
	>icopy.exe /c [/r:resolution] [/n:number of copies] [i:intent] [s:enlargement] [/b:brightness] [/cn:contrast] [/p]
	
	>icopy.exe /f:path [/r:resolution] [i:intent] [s:enlargement] [/b:brightness] [/cn:contrast] [/p]



/c					Directly copy from scanner to printer, using settings provided or else saved settings
/f:path				Scan to the provided file path (if omitted, a dialog will appear to ask where to save the file)

/r:resolution		Specify a valid scanning resolution in DPI (eg 100, 500)
/n:n of copies		Specify the number of copies to be printed (default one copy)
/i:intent			The way the page will be acquired: 1: color, 2: grayscale, 4:text
/s:enlargement		The enlargement percentage (eg /s:150) default value: 100
/b:brightness		Value from -100 to 100 for brightness
/cn:contrast		Value from -100 to 100 for constrast
/p					Enables preview mode

> icopy.exe -diag	Analyses the scanner informations and saves an XML report to be attached to iCopy bug tracker in case of problems
> icopy.exe -r		Registers components needed for proper execution. Needs administrator privileges			


3) Known bugs & solutions 
-------------------------
* Wiaaut.dll not registered error.
	iCopy asks if you want to register WIA Automation Layer. If it fails in doing so, you can register it manually:
		* Copy the file wiaaut.dll that you find in iCopy directory to C:\Windows\system32\
		* Open a Prompt command with administrator rights
		* Enter commands:
			cd C:\Windows\system32\
			regsvr32 wiaaut.dll			
		* You should receive a confirmation of wiaaut.dll registration	

	Now iCopy should run as expected.

* Scanner isn't recognized by iCopy
	If you think that your scanner is WIA compatible but it isn't recognized by iCopy, try to run icopy with the -diag parameter and post the log to iCopy bug tracker.
	A list of the known compatible scanners can be found at http://icopy.sourceforge.net/?page_id=174

4) Support
-------------------------
Please read iCopy F.A.Q.:
http://icopy.sourceforge.net/?page_id=10

If you want to report an exception or a bug, to suggest a new feature or a change, or ask for support, please use Sourceforge Bug tracker:

http://sourceforge.net/apps/trac/icopy/newticket

NOTE: 	Please leave a valid email address in order to be contacted for more details or fixes to your problem. Anonymous reports will be discarded.
		Your email address will be safe and not visible to other users.

5) Please Help!
------------------------
iCopy is free software, so it is supported only by your generous donations and by advertisements on the website.
If you like iCopy, please help me to make it a better software! You can:

	* Tell your friends about iCopy, on your blog, on Facebook, Twitter or wherever you like, so that it gets more and more famous.
	* Surf the website, leave comments, and maybe give a look (and a click) at the advertisements.
	* Donate a small amount (even a few $) with Paypal: http://sourceforge.net/donate/index.php?group_id=201245.