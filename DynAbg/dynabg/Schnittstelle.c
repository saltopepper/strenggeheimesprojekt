


static real_T ABTASTZEIT = 60;

VENTILRAUMLISTE[0]=1;
VENTILRAUMLISTE[1]=2;
VENTILRAUMLISTE[2]=3;
AnzRaum = 3 

BedienParam  AnzRaum * 12 * 7 *2 
// BedienParam -> Siehe datei in usb


// y1 SollTemperatur | y2= Hlim   u1 = Tist     u2 = Hub    (1, 2, 3) --> y1[0] = Solltemp Raum 1, 


/* Regelung initialisieren */
SA_Init_sil(y1, y2, u1, u2, ABTASTZEIT, BedienParam, VENTILRAUMLISTE);


// alle ABTASTZEIT
SA_Output_sil(y1, y2, uPtrs1, uPtrs2);


// beim Stop
SA_Terminate_sil();