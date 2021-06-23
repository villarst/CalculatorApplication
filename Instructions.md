# CalculatorApplication
This is my Calculator Application.

It basically is a similiar verison of the Windows 10 "Standard" Calculator. I am still adding
functionality to it but it does what most calculators nowadays should do. I will be planning on
adding an exponent portion and possibly a square root part to the calculator but for now it seems
to be able to compute most calculations that would otherwise be considered hard by human standards.

I was also able to map out each key to the corresponding buttons except the "CE" buttons and the "x"
button. The "CE" button imo should not have a keystroke associated with it. The "x" or multiply
button does have the * binded withit if you have a number pad but if you try holdin SHIFT + "*"
the key does not register correctly and instead produces an "8" on the display. I did some testing
to determine why it was not working and it turns out because the 8 button on the keyboard is assigned
to the number 8 already, the "*" will never be registered which is why SHIFT + "*" will not work.

Other than those issues the calculations and buttons should work as intended. 
