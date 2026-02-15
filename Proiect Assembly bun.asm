.model small 
.stack 100h
.data

intrebare_ora12 db 'E trecut de ora 12? (d/n)? $', 13,10, '$'
intrebare_ora17 db 13,10, 'E trecut de ora 17? (d/n)? $', 13,10, '$'
dimineata db 13,10, 'Buna dimineata!', 13,10, '$'
ziua db 13,10, 'Buna ziua!', 13,10, '$'
seara db 13,10, 'Buna seara!', 13,10, '$'
invalid db 13,10, 'Raspuns invalid!', 13,10, '$'

.code
start:
    mov ax,@data
    mov ds,ax

    ; Prima intrebare: E trecut de ora 12?
    mov dx, offset intrebare_ora12
    mov ah, 09h
    int 21h

    ; Citim input-ul
    mov ah, 01h
    int 21h
    or al, 20h      ; Convertim la litera mica

    cmp al, 'd'
    je ora17        ; Daca este 'd', sarim la intrebarea urmatoare (ora 17)

    cmp al, 'n'
    je estedimineata   ; Daca este 'n', sarim la dimineata

    ; Daca nu e nici 'd', nici 'n', afisam mesaj invalid
    jmp invalid_input

ora17:
    ; A doua intrebare: E trecut de ora 17?
    mov dx, offset intrebare_ora17
    mov ah, 09h
    int 21h

    ; Citim input-ul
    mov ah, 01h
    int 21h
    or al, 20h      ; Convertim la litera mica

    cmp al, 'd'
    je esteseara       ; Daca este 'd', afisam "Buna seara!"

    cmp al, 'n'
    je esteziua        ; Daca este 'n', afisam "Buna ziua!"

    ; Daca nu e nici 'd', nici 'n', afisam mesaj invalid
    jmp invalid_input

estedimineata: 
    mov dx, offset dimineata
    jmp afisare

esteziua: 
    mov dx, offset ziua
    jmp afisare

esteseara: 
    mov dx, offset seara
    jmp afisare

invalid_input:
    mov dx, offset invalid
    jmp afisare

afisare:
    mov ah, 09h
    int 21h

    ; Terminam programul
    mov ax, 4C00h
    int 21h

end start