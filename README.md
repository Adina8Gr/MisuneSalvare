Planificarea unei Misiuni de Salvare - Detalii și Implementare
Context
Un echipaj de salvare trebuie să acopere mai multe puncte de intervenție, fiecare având o prioritate diferită (de exemplu, criticitate ridicată, medie sau scăzută). Obiectivele principale sunt:

Să găsească cel mai rapid traseu între punctele de intervenție.
Să aloce resursele (echipe și echipamente) eficient pentru a respecta constrângerile.
Divizare în Sarcini
Persoana 1: Modelarea traseelor și calcularea costurilor
Modelează problema ca un graf unde:
Nodurile sunt locațiile punctelor de intervenție.
Muchiile sunt traseele posibile, fiecare având un cost (timp sau dificultate).
Găsește cel mai rapid traseu folosind Ant Colony Optimization (ACO).
Persoana 2: Optimizarea distribuției resurselor între echipe
Determină distribuția resurselor (număr de echipe, echipamente) pentru a acoperi cât mai eficient toate punctele.
Folosește Hill Climbing pentru a rafina distribuția.
Plan de Implementare
Vom implementa în două module principale:

Traseele și costurile: ACO pentru găsirea traseului optim.
Distribuția resurselor: Hill Climbing pentru optimizarea alocării resurselor.
