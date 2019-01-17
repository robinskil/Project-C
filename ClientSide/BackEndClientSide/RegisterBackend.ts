import * as React from "react";

// export function ValidatePassword(password: string , passwordRetyped : string) : boolean{
//     let inputElement2: HTMLInputElement = <HTMLInputElement>document.getElementById("inputPasswordRetyped");
//     if(password != passwordRetyped){
//         console.log("passwords don't match");
//         inputElement2.setCustomValidity("Passwords don't match");
//         return false;
//     }
//     else {
//         console.log("passwords match");
//         inputElement2.setCustomValidity("");
//         return true;
//     }
// }
export function CheckValidityInputElement(id:string) : boolean{
    let element : HTMLInputElement = <HTMLInputElement>document.getElementById(id);
    return element.checkValidity();
}
export function SetCustomValidityElement(id:string , errorMessage : string){
    let element : HTMLInputElement = <HTMLInputElement>document.getElementById(id);
    element.setCustomValidity(errorMessage);
}