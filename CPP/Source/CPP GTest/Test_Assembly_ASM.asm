EXTERN printf: PROC

.data
    format db "The result is: %d", 0Ah, 0  ; Define format string with newline

.code
    public AddAndPrint
    AddAndPrint proc
        ; Parameters: A in RCX, B in RDX

        ; Calculate A + B
        add rcx, rdx

        ; Prepare to call printf
        sub rsp, 28h     ; Allocate shadow space

        ; Set up printf arguments
        mov r8, rcx      ; Result in r8 (third parameter)
        lea rcx, [format]  ; Address of format string in rdx (second parameter)
        call printf      ; Call printf

        add rsp, 28h     ; Clean up shadow space

        ret
    AddAndPrint endp
end