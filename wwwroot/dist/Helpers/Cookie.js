"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function getCookie(name) {
    const nameLenPlus = (name.length + 1);
    return document.cookie
        .split(';')
        .map(c => c.trim())
        .filter(cookie => {
        return cookie.substring(0, nameLenPlus) === `${name}=`;
    })
        .map(cookie => {
        return decodeURIComponent(cookie.substring(nameLenPlus));
    })[0] || null;
}
exports.getCookie = getCookie;
//# sourceMappingURL=Cookie.js.map