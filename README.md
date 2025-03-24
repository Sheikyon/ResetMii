# ResetMii

This tool, developed using C# (C Sharp), WPF and NET 8.0, allows you to generate recovery keys for parental controls on the Nintendo Wii and Nintendo 3DS. 

## Algorithmn

1. Validation
   - An 8-digit value, e.g., the confirmation code, is taken and a check is made to see if the user has selected a valid time zone, as this will be required for further calculations.
2. Time Delta Calculation
   - Two parameters are taken, ```date``` (already calculated) and ```confirmationNumber``` (entered by the user), and are concatenated with the last 4 digits of the confirmation number, e.g. 5678, and the last 4 digits are extracted from the 8-character string starting from the fifth, to be stored in the variable ```fullNum```.
3. Result
     - The CRC32 checksum of the variable ```fullNum``` is then calculated by calling the ```ComputeCrc32()``` method, which will compute it. The result is stored in the variable ```crc```.
     - A bitwise XOR operation is then performed between the CRC32 checksum value stored in the variable ```crc``` and 0xAAAA. The result of such an operation is stored in the variable ```xorResult```.
     - The modulo (%) of ```sumResult``` is then calculated by 100000 to ensure that the result is a 5-digit number. The result, treated as an integer, is stored in the variable ```unlockCode```.
     - Finally, a message box is displayed with the unlock code, formatted as a 5-digit integer.

For debugging purposes, message boxes will also be displayed with the values ​​involved in calculating the unlock key.

## Use

To obtain the confirmation code, go to Wii Options > Wii Settings > Second Page > Parental Controls. When asked, "Change Parental Controls?", select "Yes," then "I forgot" > "I forgot" (again).

![Screenshot of Wii Parental Controls with confirmation code](/img/wii_parental_controls.jpg "Wii Parental Controls")

If you're in the right place, you'll see the confirmation code, which is an 8-digit integer. This will be the code you will use in the program.
