# ResetMii

This tool, developed using C# (C Sharp), WPF and NET 8.0, allows you to generate unlock codes, a.k.a. 'master keys,' for the process of resetting the parental controls password for the Nintendo Wii and Nintendo 3DS. This tool is free, unlike the [one officially offered by Nintendo](https://parentalcontrols.nintendo.com/?sys=wii), which aims to charge you 50 cents.

## Algorithm

To ensure this tool works, you must have the date and time on your console and computer up to date, as the generation of a working unlock code depends on your computer's date. For validation, your Wii will use an inverse transformation, which, depending on the date set on the console, will return ```True``` if the code entered matches the expected value, or ```False``` if the code entered does not match the expected value.

1. Validation
   - An 8-digit value, e.g., the confirmation code, is taken and a check is made to see if the user has selected a valid time zone, as this will be required for further calculations.
2. Time Delta Calculation
   - Two parameters are taken, ```date``` (already calculated) and ```confirmationNumber``` (entered by the user), and are concatenated with the last 4 digits of the confirmation number, e.g. 5678, and the last 4 digits are extracted from the 8-character string starting from the fifth, to be stored in the variable ```fullNum```.
3. Result
     - The CRC32 checksum of the variable ```fullNum``` is then calculated by calling the ```ComputeCrc32()``` method, which will compute it. The result is stored in the variable ```crc```.
     - A bitwise XOR operation is then performed between the CRC32 checksum value stored in the variable ```crc``` and 0xAAAA. The result of such an operation is stored in the variable ```xorResult```.
     - The modulo (%) of ```sumResult``` is then calculated by 100000 to ensure that the result is a 5-digit number. The result, treated as an integer, is stored in the variable ```unlockCode```.
     - Finally, a message box is displayed with the unlock code, formatted as a 5-digit integer.

For debugging purposes, message boxes will also be displayed with the values ​​involved in calculating the unlock code.

## Use

To obtain the confirmation code, go to Wii Options > Wii Settings > Second Page > Parental Controls. When asked, "Change Parental Controls?", select "Yes," then "I forgot" > "I forgot" (again). You will see a confirmation code, which you will use in the program to generate a unlock code.

![First step](/img/1.jpg)
![Second step](/img/2.jpg)
![Third step](/img/3.jpg)
