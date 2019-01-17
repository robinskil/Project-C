"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class KeyValuePair {
    constructor(key, value) {
        this.key = key;
        this.value = value;
    }
}
exports.KeyValuePair = KeyValuePair;
function CreateRequest(url, bodyParameters) {
    return fetch(url, {
        method: "POST",
        headers: {
            'Accept': 'application/json, application/xml, text/plain, text/html, *.*',
        },
        body: CreateFormData(bodyParameters)
    });
}
exports.CreateRequest = CreateRequest;
function CreateFormData(keyValuePairs) {
    let dataForm = new FormData();
    for (let i = 0; i < keyValuePairs.length; i++) {
        dataForm.append(keyValuePairs[i].key, keyValuePairs[i].value);
    }
    return dataForm;
}
//# sourceMappingURL=RequestCreater.js.map