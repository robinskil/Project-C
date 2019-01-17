"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
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
function CheckValidityInputElement(id) {
    let element = document.getElementById(id);
    return element.checkValidity();
}
exports.CheckValidityInputElement = CheckValidityInputElement;
function SetCustomValidityElement(id, errorMessage) {
    let element = document.getElementById(id);
    element.setCustomValidity(errorMessage);
}
exports.SetCustomValidityElement = SetCustomValidityElement;
//# sourceMappingURL=RegisterBackend.js.map